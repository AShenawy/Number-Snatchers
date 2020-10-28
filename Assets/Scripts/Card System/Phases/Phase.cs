using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Phase
{
    public Phases name;
    protected Stages stage;
    protected BattleManager battleManager;
    protected EnemyBattleData npcData;
    protected Stats playerStats;
    protected PlayerHand playerHand;
    protected NPCHand npcHand;
    
    protected Phase nextPhase;
    protected int startingHpPlayer;
    protected int startingHpNPC;
    protected int currentHpPlayer;
    protected int currentHpNPC;

    public Phase(BattleManager _battleManager, Stats _playerStats, EnemyBattleData _npcData, PlayerHand _playerHand, NPCHand _npcHand)
    {
        battleManager = _battleManager;
        npcData = _npcData;
        playerStats = _playerStats;
        playerHand = _playerHand;
        npcHand = _npcHand;
        startingHpPlayer = playerStats.hP;
        currentHpPlayer = battleManager.playerCurrentHP;
        startingHpNPC = npcData.health;
        currentHpNPC = battleManager.npcCurrentHP;
        stage = Stages.Enter;
    }

    public virtual void Enter() { stage = Stages.Update; }
    public virtual void Update() { stage = Stages.Update; }
    public virtual void Exit() { stage = Stages.Exit; }

    public Phase Process()
    {
        if (stage == Stages.Enter)
            Enter();

        if (stage == Stages.Update)
            Update();

        if (stage == Stages.Exit)
        {
            Exit();
            return nextPhase;   // if exiting phase, return the new phase
        }

        return this;    // if not exiting phase, return itself
    }
}

/*
 * 3 loops occur in the below phases:
 * Inner Loop occurs every time the turn switches between player and NPC. It starts at CardDeal and ends at EndTurn.
 * Middle Loop occurs when a target is reached or went over. It starts at NewRound and ends at EndTurn.
 * Outer Loop occurs when a new battle begins. It starts at BattleStart and ends at EndTurn.
 * inner loop always repeats while game is running, middle loop occurs when a new target is needed. outer loop only occurs once a battle.
 */
public enum Phases { BattleStart, NewRound, CardDeal, CardPick, Challenge, GuessCheck, EndTurn, BattleEnd }

public enum Stages { Enter, Update, Exit }