using UnityEngine;
using UnityEngine.UI;

// This phase will occur every time the game switches turns between player and NPC. It is the 1st phase in the inner loop
public class CardDeal : Phase
{
    CurrentPlayer player;

    bool dealEnded;
    float timeEnter;
    float timeExit = 2f;

    public CardDeal(BattleManager _bm, Stats _plStats, EnemyBattleData _npcData, PlayerHand _plrHnd, NPCHand _npcHnd)
           : base(_bm, _plStats, _npcData, _plrHnd, _npcHnd)
    {
        name = Phases.CardDeal;
        
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
                battleManager.StartCoroutine(battleManager.DealCards(playerHand));
                break;
        
            case CurrentPlayer.NPC:
                battleManager.StartCoroutine(battleManager.DealCards(npcHand));
                break;
        }

        timeEnter = Time.timeSinceLevelLoad;
        base.Enter();
    }

    public override void Update()
    {
        // move to next phase when players have been dealt cards
        if (dealEnded && (Time.timeSinceLevelLoad - timeEnter >= timeExit))
            DelayedExit();
    }   

    public override void Exit()
    {
        // reset the deal ended bool and unsub to event
        dealEnded = false;
        battleManager.onDealEnded -= OnCardDealEnded;
        Debug.Log("Exiting Deal phase after all cards are dealt to " + player +" player. Phase time is " + (Time.timeSinceLevelLoad - timeEnter) + " seconds.");
        base.Exit();
    }

    void OnCardDealEnded()
    {
        dealEnded = true;
    }

    void DelayedExit()
    {
        nextPhase = new CardPlay(battleManager, playerStats, npcData, playerHand, npcHand);
        stage = Stages.Exit;
    }
}
