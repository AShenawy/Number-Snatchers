﻿using UnityEngine;
using UnityEngine.UI;

// this phase is the first to occur every battle. It should only be called once in the beginning to set up the battle stats
public class Start : Phase
{
    Dealer cardDealer;
    //Image npcCard;
    //Text npcNameDisplay;
    NPCDisplay nPCDisplay;
    Text roundNumberDisplay;
    Text targetNumberDisplay;
    Text currentNumberDisplay;
    InfoCard infoCard;

    float counter;
    float exitTimer = 2f;   // how long to wait before exiting this phase

    public Start(BattleManager _battleManager, Stats _playerStats, EnemyBattleData _npcData, PlayerHand _plrHnd, NPCHand _npcHnd) 
        : base(_battleManager, _playerStats, _npcData, _plrHnd, _npcHnd)
    {
        name = Phases.BattleStart;
        
        cardDealer = battleManager.dealer;
        //npcCard = battleManager.nPCCardDisplay;
        //npcNameDisplay = battleManager.nPCNameDisplay;
        nPCDisplay = battleManager.nPCDisplay;

        roundNumberDisplay = battleManager.roundNumberDisplay;
        targetNumberDisplay = battleManager.targetNumberDisplay;
        currentNumberDisplay = battleManager.currentNumberDisplay;

        infoCard = System.Array.Find(battleManager.infoCardsPrefabs, c => c.cardType == InfoType.NewBattle);
    }

    public override void Enter()
    {
        Debug.Log("Entering Start phase");

        //*** set up player stats & display ***
        currentHpPlayer = startingHpPlayer;
        battleManager.playerCurrentHP = currentHpPlayer;
        battleManager.playerHPDisplay.fillAmount = currentHpPlayer / startingHpPlayer;
        battleManager.playerHealthCards.RefillCards();
        // prevent player from playing cards until CardPick phase
        playerHand.BlockCardInteractions(true);

        //*** set up opponent stats & display ***
        npcHand.data = npcData;
        //npcCard.sprite = npcData.enemyDefault;
        nPCDisplay.SetDefaultSprite(npcData.enemyDefault);
        //npcNameDisplay.text = npcData.enemyName.ToUpper();
        nPCDisplay.SetName(npcData.enemyName.ToUpper());
        currentHpNPC = startingHpNPC;
        battleManager.npcCurrentHP = currentHpNPC;
        battleManager.nPCHPDisplay.fillAmount = currentHpNPC / startingHpNPC;
        battleManager.npcHealthCards.RefillCards();
        nPCDisplay.SetReaction(npcData.startQuotes[Random.Range(0, npcData.startQuotes.Length)], npcData.enemyAngry);

        //*** set up deck ***
        cardDealer.FillDeck(npcData.difficulty);

        // deal both sides on game start
        DealBothPlayers();

        //*** initialise counters & parameters ***
        //battleManager.playerTurn = CurrentPlayer.Human;       //********** Uncomment when done with testing *************
        roundNumberDisplay.text = "";
        targetNumberDisplay.text = "";
        currentNumberDisplay.text = "";

        InfoCard card = Object.Instantiate(infoCard, battleManager.transform);
        card.onCardDestroyed += MoveToNextPhase;
        counter = Time.timeSinceLevelLoad;

        base.Enter();
    }

    public override void Update()
    {
        // nothing happens during this phase besides initialisation. Move on to next phase after time passes
        //if (Time.timeSinceLevelLoad - counter >= exitTimer)
        //{
        //    nextPhase = new NewRound(battleManager, playerStats, npcData, base.playerHand, npcHand);
        //    stage = Stages.Exit;
        //}
    }

    public override void Exit()
    {
        Debug.Log("Exiting Start phase after " + (Time.timeSinceLevelLoad - counter) + " seconds.");
        base.Exit();
    }

    void DealBothPlayers()
    {
        battleManager.StartCoroutine(battleManager.DealCards(playerHand));
        battleManager.StartCoroutine(battleManager.DealCards(npcHand));
    }

    void MoveToNextPhase()
    {
        nextPhase = new NewRound(battleManager, playerStats, npcData, base.playerHand, npcHand);
        stage = Stages.Exit;
    }
}
