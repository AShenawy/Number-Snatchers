﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;


// this is the last phase in each loop and determines whether a new loop begins or the battle ends
public class TurnEnd : Phase
{
    public TurnEnd(BattleManager _bm, Stats _plStats, EnemyBattleData _npcData, Image _plrHpDisplay, Image _npcHpDisplay)
        : base(_bm, _plStats, _npcData, _plrHpDisplay, _npcHpDisplay)
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
        // if a side lost the battle then end it, else switch turns.
        if (currentHpNPC <= 0 || currentHpPlayer <= 0)
        {
            Debug.Log("A side has reached 0 HP. Game is over");
            nextPhase = new EndBattle(battleManager, playerStats, npcData, playerHpDisplay, npcHpDisplay);
            stage = Stages.Exit;
        }
        else if (battleManager.currentNumber >= battleManager.targetNumber)
        {
            Debug.Log("Target number is reached. Starting a new round.");
            battleManager.SwitchTurn();
            nextPhase = new NewRound(battleManager, playerStats, npcData, playerHpDisplay, npcHpDisplay);
            stage = Stages.Exit;
        }
        else
        {
            Debug.Log(battleManager.playerTurn + "'s turn has ended. Switching sides.");
            battleManager.SwitchTurn();
            nextPhase = new CardDeal(battleManager, playerStats, npcData, playerHpDisplay, npcHpDisplay);
            stage = Stages.Exit;
        }    
    }

    public override void Exit()
    {
        Debug.Log("Exiting Turn End phase");
        base.Exit();
    }
}
