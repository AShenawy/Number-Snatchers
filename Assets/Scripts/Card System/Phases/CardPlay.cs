using UnityEngine;
using UnityEngine.UI;
using System.Collections;

// this phase occurs after cards are dealt and it's one side's turn to play a card
public class CardPlay : Phase
{
    GuessHandler guessHandler;
    bool isCardPlayed;
    Card playedHumanCard;
    int humanGuess;

    public CardPlay(BattleManager _bm, Stats _plStats, EnemyBattleData _npcData, PlayerHand _plrHnd, NPCHand _npcHnd)
           : base(_bm, _plStats, _npcData, _plrHnd, _npcHnd)
    {
        name = Phases.CardPick;

        playerHand.humanCardPlayed += OnHumanCardPlayed;
        npcHand.onCardPlayed += OnCardPlayed;
    }

    public override void Enter()
    {
        Debug.Log("Entering Card Play phase. It's " + battleManager.playerTurn + " player's turn to play.");

        // if it's human player's trun allow interaction with cards. else let the npc play
        if (battleManager.playerTurn == CurrentPlayer.Human)
            playerHand.BlockCardInteractions(false);
        else
        {
            // inform npc the latest current number and it's their turn to play
            npcHand.UpdateCurrentNumber(battleManager.currentNumber);
            npcHand.PlayTurn();
        }

        base.Enter();
    }

    public override void Update()
    {
        if (isCardPlayed)
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

        Debug.Log("Exiting Card Play phase after " + battleManager.playerTurn + " player played their card.");
        base.Exit();
    }

    void OnHumanCardPlayed(Card playedCard)
    {
        playedHumanCard = playedCard;
        DisplayGuessHandler();
    }

    void DisplayGuessHandler()
    {
        guessHandler = GameObject.Instantiate(battleManager.guessHandlerCardPrefab, battleManager.transform);
        guessHandler.onInputSubmitted += StoreHumanGuess;
    }

    void StoreHumanGuess(int guess)
    {
        humanGuess = guess;
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
}
