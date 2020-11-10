using UnityEngine;
using UnityEngine.UI;
using System.Collections;

// this phase is only reachable when one side is defeated to end the game.
public class EndBattle : Phase
{
    bool isReplaying;

    public EndBattle(BattleManager _bm, Stats _plStats, EnemyBattleData _npcData, PlayerHand _plrHnd, NPCHand _npcHnd)
        : base(_bm, _plStats, _npcData, _plrHnd, _npcHnd)
    {
        name = Phases.BattleEnd;
    }

    public override void Enter()
    {
        Debug.Log("Entering Battle End phase.");
        DisplayEndScreen(DetermineWinner());
        base.Enter();
    }

    public override void Update()
    {
        if (isReplaying)
        {
            nextPhase = new Start(battleManager, playerStats, npcData, playerHand, npcHand);
            stage = Stages.Exit;
        }
    }

    public override void Exit()
    {
        Debug.Log("Exiting Battle End Phase.");

        //TODO unsub from all events

        base.Exit();
    }

    int DetermineWinner()
    {
        // check if both sides lost or only one side lost
        if (currentHpPlayer <= 0 && currentHpNPC <= 0)
            return 0;
        else if (currentHpNPC <= 0 && currentHpPlayer > 0)  // if NPC lost all health then human won
            return 1;
        else if (currentHpPlayer <= 0 && currentHpNPC > 0)  // opposite of above case
            return 2;
        else
            return -1;       // error case
    }

    void DisplayEndScreen(int win)
    {
        if (win == 0)
        {
            Debug.Log("<color=magenta>Both sides lost. It's a draw!</color>");
            //TODO Display draw screen
        }
        else if (win == 1)
        {
            Debug.Log("<color=magenta>Human player won the battle!");
            //TODO Display human win screen
        }
        else if (win == 2)
        {
            Debug.Log("<color=magenta>Computer player won the battle!");
            //TODO Display NPC win screen
        }
        else
        {
            // this is an error case and should never occur
            Debug.Log("<color=red>Game Error: No winner could be determined!");
        }
    }

    void OnReplay()
    {
        isReplaying = true;
    }
}
