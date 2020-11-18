using UnityEngine;


// this is the last phase in each loop and determines whether a new loop begins or the battle ends
public class TurnEnd : Phase
{
    bool isChallengeWon;
    float timeEnter;    // time to countdown from
    float timeExit = 2f;    // time to exit the phase

    public TurnEnd(BattleManager _bm, Stats _plStats, EnemyBattleData _npcData, PlayerHand _plrHnd, NPCHand _npcHnd, bool _isWon)
        : base(_bm, _plStats, _npcData, _plrHnd, _npcHnd)
    {
        name = Phases.EndTurn;
        isChallengeWon = _isWon;
    }

    public override void Enter()
    {
        Debug.Log("Entering Turn End phase.");
        timeEnter = Time.timeSinceLevelLoad;
        base.Enter();
    }

    public override void Update()
    {
        // if a side lost the battle then end it
        if ((currentHpNPC <= 0 || currentHpPlayer <= 0) &&
            Time.timeSinceLevelLoad - timeEnter >= timeExit)
        {
            Debug.Log("A side has reached 0 HP. Game is over");
            DelayedExit(new EndBattle(battleManager, playerStats, npcData, playerHand, npcHand));
        }
        // if the target is reached or crossed then start a new round
        else if (battleManager.currentNumber >= battleManager.targetNumber && Time.timeSinceLevelLoad - timeEnter >= timeExit)
        {
            Debug.Log("Target number is reached. Starting a new round.");
            DelayedExit(new NewRound(battleManager, playerStats, npcData, playerHand, npcHand));

            //TODO if current number went over then it should go to the pot
        }
        // if challenge is won, even if target isn't reached yet then start a new round
        else if (isChallengeWon && Time.timeSinceLevelLoad - timeEnter >= timeExit)
        {
            Debug.Log("Challenge is won by opponent. Starting a new round.");
            DelayedExit(new NewRound(battleManager, playerStats, npcData, playerHand, npcHand));
        }
        // if neither of the above then continue the same round and switch turns
        else if (Time.timeSinceLevelLoad - timeEnter >= timeExit)
        {
            Debug.Log(battleManager.playerTurn + " player's turn has ended. Switching sides.");
            DelayedExit(new CardDeal(battleManager, playerStats, npcData, playerHand, npcHand));
        }
    }

    public override void Exit()
    {
        Debug.Log("Exiting Turn End phase. Phase time is " + (Time.timeSinceLevelLoad - timeEnter) + " seconds.");
        ClearTurnInfo();
        base.Exit();
    }

    void ClearTurnInfo()
    {
        if (battleManager.playerTurn == CurrentPlayer.Human)
            playerHand.ClearTurnInfo();
        else
            npcHand.ClearTurnInfo();
    }

    void DelayedExit(Phase phase)
    {
        battleManager.SwitchTurn();
        nextPhase = phase;
        stage = Stages.Exit;
    }
}
