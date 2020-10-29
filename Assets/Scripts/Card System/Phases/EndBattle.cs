using UnityEngine;
using UnityEngine.UI;
using System.Collections;

// this phase is only reachable when one side is defeated to end the game.
public class EndBattle : Phase
{
    public EndBattle(BattleManager _bm, Stats _plStats, EnemyBattleData _npcData, PlayerHand _plrHnd, NPCHand _npcHnd)
        : base(_bm, _plStats, _npcData, _plrHnd, _npcHnd)
    {
        name = Phases.BattleEnd;
    }

    public override void Enter()
    {
        Debug.Log("Entering Battle End phase.");
        base.Enter();
    }

    public override void Update()
    {
        
    }

    public override void Exit()
    {
        Debug.Log("Exiting Battle End Phase.");
        base.Exit();
    }
}
