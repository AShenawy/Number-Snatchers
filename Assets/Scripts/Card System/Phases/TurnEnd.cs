using UnityEngine;


// this is the last phase in each loop and determines whether a new loop begins or the battle ends
public class TurnEnd : Phase
{
    bool isChallengeWon;
    float timeEnter;    // time to countdown from
    float timeExit = 2f;    // time to exit the phase
    PlayTable table;
    Pot pot;

    public TurnEnd(BattleManager _bm, Stats _plStats, EnemyBattleData _npcData, PlayerHand _plrHnd, NPCHand _npcHnd, bool _isWon)
        : base(_bm, _plStats, _npcData, _plrHnd, _npcHnd)
    {
        name = Phases.EndTurn;
        isChallengeWon = _isWon;
        table = GameObject.FindWithTag("Table").GetComponent<PlayTable>(); ;
        pot = GameObject.FindWithTag("Pot").GetComponent<Pot>();
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
            nextPhase = new EndBattle(battleManager, playerStats, npcData, playerHand, npcHand);
            stage = Stages.Exit; 
        }
        // if the target is crossed then start a new round and move cards to pot
        else if (battleManager.currentNumber > battleManager.targetNumber && Time.timeSinceLevelLoad - timeEnter >= timeExit)
        {
            Debug.Log("Target number is crossed. Moving cards to Pot and starting a new round.");
            battleManager.SwitchTurn();
            table.ClearTable(CardCollections.Pot);
            nextPhase = new NewRound(battleManager, playerStats, npcData, playerHand, npcHand);
            stage = Stages.Exit;
        }
        // if the target is reached then start a new round and move cards to graveyard
        else if (battleManager.currentNumber == battleManager.targetNumber && Time.timeSinceLevelLoad - timeEnter >= timeExit)
        {
            Debug.Log("Target number is reached. Moving cards to Graveyard and starting a new round.");
            battleManager.SwitchTurn();
            // empty table and pot before new round
            table.ClearTable(CardCollections.Graveyard);
            pot.EmptyPot();
            nextPhase = new NewRound(battleManager, playerStats, npcData, playerHand, npcHand);
            stage = Stages.Exit;
        }
        // if challenge is won, even if target isn't reached yet then start a new round
        else if (isChallengeWon && Time.timeSinceLevelLoad - timeEnter >= timeExit)
        {
            Debug.Log("Challenge is won by opponent. Moving cards to graveyard and starting a new round.");
            battleManager.SwitchTurn();
            // empty table and pot before new round
            table.ClearTable(CardCollections.Graveyard);
            pot.EmptyPot();
            nextPhase = new NewRound(battleManager, playerStats, npcData, playerHand, npcHand);
            stage = Stages.Exit;
            
        }
        // if neither of the above then continue the same round and switch turns
        else if (Time.timeSinceLevelLoad - timeEnter >= timeExit)
        {
            Debug.Log(battleManager.playerTurn + " player's turn has ended. Switching sides.");
            battleManager.SwitchTurn();
            nextPhase = new CardDeal(battleManager, playerStats, npcData, playerHand, npcHand);
            stage = Stages.Exit;
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
}
