using UnityEngine;


// this is the last phase in each loop and determines whether a new loop begins or the battle ends
public class TurnEnd : Phase
{
    public TurnEnd(BattleManager _bm, Stats _plStats, EnemyBattleData _npcData, PlayerHand _plrHnd, NPCHand _npcHnd)
        : base(_bm, _plStats, _npcData, _plrHnd, _npcHnd)
    {
        name = Phases.EndTurn;
    }

    public override void Enter()
    {
        Debug.Log("Entering Turn End phase.");
        base.Enter();
    }

    public override void Update()
    {
        // if a side lost the battle then end it
        if (currentHpNPC <= 0 || currentHpPlayer <= 0)
        {
            Debug.Log("A side has reached 0 HP. Game is over");
            nextPhase = new EndBattle(battleManager, playerStats, npcData, playerHand, npcHand);
            stage = Stages.Exit;
        }
        // if the target is reached or crossed then start a new round
        else if (battleManager.currentNumber >= battleManager.targetNumber)
        {
            Debug.Log("Target number is reached. Starting a new round.");
            battleManager.SwitchTurn();
            nextPhase = new NewRound(battleManager, playerStats, npcData, playerHand, npcHand);
            stage = Stages.Exit;
        }
        // if neither of the above then continue the same round and switch turns
        else
        {
            Debug.Log(battleManager.playerTurn + " player's turn has ended. Switching sides.");
            battleManager.SwitchTurn();
            nextPhase = new CardDeal(battleManager, playerStats, npcData, playerHand, npcHand);;
            stage = Stages.Exit;
        }    
    }

    public override void Exit()
    {
        Debug.Log("Exiting Turn End phase");
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
