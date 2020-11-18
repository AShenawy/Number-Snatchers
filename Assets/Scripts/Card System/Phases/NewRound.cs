﻿using UnityEngine;
using UnityEngine.UI;

// this phase occurs when a new round starts. It generates a new target number to reach
public class NewRound : Phase
{
    float counter;
    float exitTimer = 2f;   // how long to wait before exiting this phase

    public NewRound(BattleManager _bm, Stats _plStats, EnemyBattleData _npcData, PlayerHand _plrHnd, NPCHand _npcHnd)
            : base (_bm, _plStats, _npcData, _plrHnd, _npcHnd)
    {
        name = Phases.NewRound;
    }

    public override void Enter()
    {
        Debug.Log("Entering New Round phase.");

        UpdateRoundCounter();
        GenerateTargetNumber();
        ResetCurrentNumber();
        counter = Time.timeSinceLevelLoad;
        base.Enter();
    }

    public override void Update()
    {
        // nothing happens during this phase besides generating new values. Move on to next phase
        if (Time.timeSinceLevelLoad - counter >= exitTimer)
        {
            nextPhase = new CardDeal(battleManager, playerStats, npcData, playerHand, npcHand);
            stage = Stages.Exit;
        }
    }

    public override void Exit()
    {
        Debug.Log("Exiting New Round phase after " + (Time.timeSinceLevelLoad - counter) + " seconds.");
        base.Exit();
    }

    void UpdateRoundCounter()
    {
        battleManager.currentRound++;
        battleManager.roundNumberDisplay.text =  battleManager.currentRound.ToString("D2");
    }

    void GenerateTargetNumber()
    {
        int newTarget = Random.Range(5, 25);

        // inform battle manager and npc with the new target
        battleManager.targetNumber = newTarget;
        battleManager.targetNumberDisplay.text = newTarget.ToString();
        npcHand.UpdateTargetNumber(newTarget);
    }

    void ResetCurrentNumber()
    {
        battleManager.currentNumber = 0;
        battleManager.currentNumberDisplay.text = "00";
    }
}
