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
    protected Image playerHpDisplay;
    protected Image npcHpDisplay;
    
    protected Phase nextPhase;
    protected int startingHpPlayer;
    protected int startingHpNPC;
    protected int currentHpPlayer;
    protected int currentHpNPC;

    public Phase(BattleManager _battleManager, Stats _playerStats, EnemyBattleData _npcData, Image _playerHpDisplay, Image _npcHpDisplay)
    {
        battleManager = _battleManager;
        npcData = _npcData;
        playerStats = _playerStats;
        playerHpDisplay = _playerHpDisplay;
        npcHpDisplay = _npcHpDisplay;
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

public enum Phases { BattleStart, NewRound, CardDeal, CardPick, Challenge, GuessCheck, EndTurn, BattleEnd }

public enum Stages { Enter, Update, Exit }