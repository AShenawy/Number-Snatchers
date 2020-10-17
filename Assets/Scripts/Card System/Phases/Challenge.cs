using UnityEngine;
using UnityEngine.UI;

public class Challenge : Phase
{
    public Challenge(BattleManager _bm, Stats _plStats, EnemyBattleData _npcData, Image _plrHpDisplay, Image _npcHpDisplay)
       : base(_bm, _plStats, _npcData, _plrHpDisplay, _npcHpDisplay)
    {
        name = Phases.Challenge;
    }
}
