using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CardPlay : Phase
{
    public CardPlay(BattleManager _bm, Stats _plStats, EnemyBattleData _npcData, Image _plrHpDisplay, Image _npcHpDisplay)
           : base(_bm, _plStats, _npcData, _plrHpDisplay, _npcHpDisplay)
    {
        name = Phases.CardPick;
    }
}
