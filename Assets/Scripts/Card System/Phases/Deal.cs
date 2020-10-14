using UnityEngine;
using UnityEngine.UI;

public class Deal : Phase
{
    Dealer cardDealer;
    PlayerHand plrHand;
    NPCHand npcHand;
    CurrentPlayer player;

    //float counter;
    //float exitTimer = 1f;

    public Deal(BattleManager _bm, Stats _plStats, EnemyBattleData _npcData, Image _plrHpDisplay, Image _npcHpDisplay)
           : base(_bm, _plStats, _npcData, _plrHpDisplay, _npcHpDisplay)
    {
        name = Phases.CardDeal;

        cardDealer = battleManager.dealer;
        plrHand = battleManager.playerHand;
        npcHand = battleManager.nPCHand;
        player = battleManager.playerTurn;
    }

    public override void Enter()
    {
        // deal cards based on who's turn it is
        switch (player)
        {
            case CurrentPlayer.Human:
                battleManager.StartCoroutine(battleManager.DealCards(plrHand));
                break;
        
            case CurrentPlayer.NPC:
                battleManager.StartCoroutine(battleManager.DealCards(npcHand));
                break;
        }
        
        base.Enter();
    }

    public override void Update()
    {
        // check when to move to next phase when side received their cards
        switch (player)
        {
            case CurrentPlayer.Human:
                if (plrHand.cardsInHand.Count > 4)
                {
                    nextPhase = new CardPlay(battleManager, playerStats, npcData, playerHpDisplay, npcHpDisplay);
                    Debug.Log("Exiting Deal phase.");
                    stage = Stages.Exit;
                }
                break;

            case CurrentPlayer.NPC:
                if (npcHand.cardsInHand.Count > 4)
                {
                    nextPhase = new CardPlay(battleManager, playerStats, npcData, playerHpDisplay, npcHpDisplay);
                    Debug.Log("Exiting Deal phase.");
                    stage = Stages.Exit;
                }
                break;
        }
    }
}
