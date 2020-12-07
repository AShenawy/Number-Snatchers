using UnityEngine;

// this phase occurs after cards are dealt and it's one side's turn to play a card
public class CardPlay : Phase
{
    GuessHandler guessHandler;
    bool isCardPlayed;
    Card playedHumanCard;
    int humanGuess;
    float timeEnter;    // time to countdown from
    float timeExit = 2f; // time to exit the phase
    InfoCard infoCard;
    bool canProceed;


    public CardPlay(BattleManager _bm, Stats _plStats, EnemyBattleData _npcData, PlayerHand _plrHnd, NPCHand _npcHnd)
           : base(_bm, _plStats, _npcData, _plrHnd, _npcHnd)
    {
        name = Phases.CardPick;

        playerHand.humanCardPlayed += OnHumanCardPlayed;
        npcHand.onCardPlayed += OnCardPlayed;
        infoCard = System.Array.Find(battleManager.infoCardsPrefabs, c => c.cardType == InfoType.CardPick);
    }

    public override void Enter()
    {
        Debug.Log("Entering Card Play phase. It's " + battleManager.playerTurn + " player's turn to play.");
        InfoCard card = Object.Instantiate(infoCard, battleManager.transform);
        card.onCardDestroyed += AllowToProceed;

        // if it's human player's trun allow interaction with cards. else let the npc play
        if (battleManager.playerTurn == CurrentPlayer.Human)
        {
            playerHand.BlockCardInteractions(false);
            card.descriptionText.text = "Player picks a card".ToUpper();
        }
        else
        {
            // inform npc the latest current number and it's their turn to play
            card.descriptionText.text = (npcData.enemyName + " picks a card").ToUpper();
            npcHand.UpdateCurrentNumber(battleManager.currentNumber);
            battleManager.nPCDisplay.SetReaction(npcData.cardPickQuotes[Random.Range(0, npcData.cardPickQuotes.Length)], npcData.enemyAngry);
            npcHand.PlayTurn();
        }

        //timeEnter = Time.timeSinceLevelLoad;
        base.Enter();
    }

    public override void Update()
    {
        //if (isCardPlayed && (Time.timeSinceLevelLoad - timeEnter >= timeExit))
        if (isCardPlayed && canProceed)
        {
            nextPhase = new Challenge(battleManager, playerStats, npcData, playerHand, npcHand);
            stage = Stages.Exit;
        }
    }

    public override void Exit()
    {
        playerHand.BlockCardInteractions(true);
        isCardPlayed = false;
        
        // unsub from events
        playerHand.humanCardPlayed -= OnHumanCardPlayed;
        if (guessHandler)
            guessHandler.onInputSubmitted -= StoreHumanGuess;
        npcHand.onCardPlayed -= OnCardPlayed;

        Debug.Log("Exiting Card Play phase after " + battleManager.playerTurn + " player played their card. Phase time is " + (Time.timeSinceLevelLoad - timeEnter) + " seconds.");
        base.Exit();
    }

    void OnHumanCardPlayed(Card playedCard)
    {
        playedHumanCard = playedCard;
        DisplayGuessHandler();
    }

    void DisplayGuessHandler()
    {
        guessHandler = Object.Instantiate(battleManager.guessHandlerCardPrefab, battleManager.transform);
        guessHandler.onInputSubmitted += StoreHumanGuess;
    }

    void StoreHumanGuess(int guess)
    {
        // make a local store for npc evaluation
        humanGuess = guess;
        // save the guess in the player hand for later use
        playerHand.guess = guess;
        OnCardPlayed(playedHumanCard);
    }

    void OnCardPlayed(Card playedCard)
    {
        if (battleManager.playerTurn == CurrentPlayer.Human)
        {
            CompareExpectedAgainstInput();
            Debug.Log($"<color=green>Human played a {playedCard.cardType} {playedCard.value} and guessed {humanGuess}.</color>");
        }
        else
        {
            Debug.Log($"<color=blue>NPC played a {playedCard.cardType} {playedCard.value} and guessed {npcHand.guess}.</color>");
        }
        
        isCardPlayed = true;
    }

    void CompareExpectedAgainstInput()
    {
        // calculate expected value
        int valExpected = battleManager.currentNumber + playedHumanCard.value;
        int valInput = humanGuess;

        // update NPC player
        npcHand.EvaluatePlayerMove(valExpected, valInput);
    }

    void AllowToProceed()
    {
        canProceed = true;
    }
}
