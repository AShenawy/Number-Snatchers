using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CheckGuess : Phase
{
    public CheckGuess(BattleManager _bm, Stats _plStats, EnemyBattleData _npcData, Image _plrHpDisplay, Image _npcHpDisplay)
       : base(_bm, _plStats, _npcData, _plrHpDisplay, _npcHpDisplay)
    {
        name = Phases.GuessCheck;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Update()
    {
    }
}
