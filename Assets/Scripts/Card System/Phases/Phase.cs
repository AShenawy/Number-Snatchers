using UnityEngine;
using System.Collections;

public class Phase
{
    public Phases name;
    protected Stages stage;
    protected BattleManager battleManager;
    protected Phase nextPhase;


    public Phase(BattleManager _battleManager)
    {
        battleManager = _battleManager;
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

public enum Phases { BattleStart, GenerateTarget, CardDeal, CardPick, Challenge, GuessCheck, EndTurn, BattleEnd }

public enum Stages { Enter, Update, Exit }