﻿using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[DisallowMultipleComponent]
public class BattleManager : MonoBehaviour
{
    public event Action onDealEnded;

    //****** difficulty and battlePhase should be removed after testing, as they are set internatlly
    public EnemyDifficulty difficulty;
    public Phases battlePhase;
    //****** playerTurn to be set to hide in inspector after testing, as it is set internally
    public CurrentPlayer playerTurn;

    [Header("Info Displays")]
    public Text roundNumberDisplay;
    public Text targetNumberDisplay;
    public Text currentNumberDisplay;
    public Image nPCCardDisplay;
    public Text nPCNameDisplay;
    public Image nPCHPDisplay;
    public Image playerHPDisplay;
    public Text currentPhaseText;

    [Header("Controllers")]
    public Dealer dealer;
    public GuessHandler guessHandler;

    [Header("Player Parameters")]
    public PlayerHand playerHand;
    public Stats playerStats;

    [Header("NPC Parameters")]
    public NPCHand nPCHand;
    public EnemyBattleData nPCData;

    [HideInInspector] public int targetNumber;
    [HideInInspector] public int currentNumber;
    [HideInInspector] public int currentRound = 0;
    private Phase currentPhase;

    // Play data
    private Card playedHumanCard;
    private Card playedNPCCard;
    

    private void OnEnable()
    {
        playerHand.humanCardPlayed += DisplayGuessInput;
        playerHand.humanCardPlayed += UpdatePlayedCard;
        guessHandler.onInputSubmitted += CompareInputAgainstExact;
    }

    private void OnDisable()
    {
        playerHand.humanCardPlayed -= DisplayGuessInput;
        guessHandler.onInputSubmitted -= CompareInputAgainstExact;
    }

    // Start is called before the first frame update
    void Start()
    {
        //dealer.FillDeck(battleMode);

        // start a new round
        //NewRound();
        //StartCoroutine(DealCards());

        // hide the guess input screen at start
        //guessHandler.gameObject.SetActive(false);
        currentPhase = new Start(this, playerStats, nPCData, playerHPDisplay, nPCHPDisplay);
        difficulty = nPCData.difficulty;
    }

    // Update is called once per frame
    void Update()
    {
        currentPhase = currentPhase.Process();
        battlePhase = currentPhase.name;
        currentPhaseText.text = currentPhase.name.ToString();
    }

    public IEnumerator DealCards(Hand hand)
    {
        // currently the cards are dealt after 1 seconds of battle start. this time is arbitrary and should be checked
        yield return new WaitForSeconds(1f);
        dealer.DealCards(hand);
        onDealEnded?.Invoke();

        //yield return new WaitForSeconds(1f);
        //dealer.DealCards(nPCHand);
        ////battlePhase = BattlePhases.CardPick;
    }

    void DisplayGuessInput(Card card)
    {
        guessHandler.gameObject.SetActive(true);
    }

    void UpdatePlayedCard(Card card)
    {
        if (playerTurn == CurrentPlayer.Human)
            playedHumanCard = card;
        else
            playedNPCCard = card;
    }

    void CompareInputAgainstExact()
    {
        // calculate expected value
        int valExpected = currentNumber + playedHumanCard.value;
        int valInput = guessHandler.submittedGuess;
        
        // update NPC player
        nPCHand.EvaluatePlayerMove(valExpected, valInput);
    }
    
    void UpdateCurrentNumber()
    {
        currentNumber += playedHumanCard.value;
        currentNumberDisplay.text = currentNumber.ToString();
    }
}

public enum CurrentPlayer { Human, NPC }