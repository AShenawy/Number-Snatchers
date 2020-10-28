﻿using UnityEngine;
using UnityEngine.UI;

// This phase will occur every time the game switches turns between player and NPC. It is the 1st phase in the inner loop
public class CardDeal : Phase
{
    PlayerHand plrHand;
    NPCHand npcHand;
    CurrentPlayer player;

    bool dealEnded;
    //float counter;
    //float exitTimer = 1f;

    public CardDeal(BattleManager _bm, Stats _plStats, EnemyBattleData _npcData, Image _plrHpDisplay, Image _npcHpDisplay)
           : base(_bm, _plStats, _npcData, _plrHpDisplay, _npcHpDisplay)
    {
        name = Phases.CardDeal;

        plrHand = battleManager.playerHand;
        npcHand = battleManager.nPCHand;
        player = battleManager.playerTurn;

        battleManager.onDealEnded += OnCardDealEnded;
    }

    public override void Enter()
    {
        Debug.Log("Entering Card Deal phase. It's " + player + " player's turn to be dealt.");

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
        // move to next phase when players have been dealt cards
        if (dealEnded)
        {
            nextPhase = new CardPlay(battleManager, playerStats, npcData, playerHpDisplay, npcHpDisplay);
            stage = Stages.Exit;
        }
    }

    public override void Exit()
    {
        // reset the deal ended bool and unsub to event
        dealEnded = false;
        battleManager.onDealEnded -= OnCardDealEnded;
        Debug.Log("Exiting Deal phase after all cards are dealt to " + player +" player.");
        base.Exit();
    }

    void OnCardDealEnded()
    {
        dealEnded = true;
    }
}
