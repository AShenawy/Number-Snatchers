﻿using UnityEngine;
using UnityEngine.UI;

// this phase occurs when a new round starts. It generates a new target number to reach
public class NewRound : Phase
{
    float counter;
    float exitTimer = 1f;   // how long to wait before exiting this phase

    public NewRound(BattleManager _bm, Stats _plStats, EnemyBattleData _npcData, Image _plrHpDisplay, Image _npcHpDisplay)
            : base (_bm, _plStats, _npcData, _plrHpDisplay, _npcHpDisplay)
    {
        name = Phases.NewRound;
    }

    public override void Enter()
    {
        UpdateRoundCounter();
        GenerateTargetNumber();

        battleManager.currentNumberDisplay.text = "00";
        counter = Time.timeSinceLevelLoad;
        base.Enter();
    }

    public override void Update()
    {
        // nothing happens during this phase besides generating new values. Move on to next phase
        if (Time.timeSinceLevelLoad - counter >= exitTimer)
            DelayedExit();
    }

    void UpdateRoundCounter()
    {
        battleManager.currentRound++;
        battleManager.roundNumberDisplay.text =  battleManager.currentRound.ToString("D2");
    }

    void GenerateTargetNumber()
    {
        int newTarget = Random.Range(5, 25);
        battleManager.targetNumber = newTarget;
        battleManager.targetNumberDisplay.text = newTarget.ToString();
    }

    void DelayedExit()
    {
        Debug.Log("Exiting New Round phase after " + (Time.timeSinceLevelLoad - counter) + " seconds.");
        nextPhase = new Deal(battleManager, playerStats, npcData, playerHpDisplay, npcHpDisplay);
        stage = Stages.Exit;
    }
}