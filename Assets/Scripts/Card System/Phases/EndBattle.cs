using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EndBattle : Phase
{
    public EndBattle(BattleManager _bm, Stats _plStats, EnemyBattleData _npcData, Image _plrHpDisplay, Image _npcHpDisplay)
        : base(_bm, _plStats, _npcData, _plrHpDisplay, _npcHpDisplay)
    {
        name = Phases.BattleEnd;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Update()
    {
        
    }

    public override void Exit()
    {
        base.Exit();
    }
}
