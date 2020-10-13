using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[DisallowMultipleComponent]
public class BattleManager : MonoBehaviour
{
    public BattleModes battleMode;
    public BattlePhases battlePhase;

    [Header("Info Displays")]
    public Text targetNumberDisplay;
    public Text currentNumberDisplay;
    public Image nPCCardDisplay;

    [Header("Controllers")]
    public Dealer dealer;
    public GuessHandler guessHandler;

    [Header("Player Parameters")]
    public PlayerHand playerHand;
    [Space]
    public NPCHand nPCHand;

    private int targetNumber;
    private int currentNumber;
    private int round = 0;

    private void OnEnable()
    {
        playerHand.humanCardPlayed += DisplayGuessInput;
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
        battlePhase = BattlePhases.Deal;
        dealer.FillDeck(battleMode);

        // start a new round
        NewRound();
        StartCoroutine(DealCards());

        // hide the guess input screen at start
        guessHandler.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // ************** Testing controls to be deleted 
        if (Input.GetKeyDown(KeyCode.P))
            dealer.DealCards(playerHand);

        if (Input.GetKeyDown(KeyCode.N))
            dealer.DealCards(nPCHand);

        // ***********************************************
    }

    IEnumerator DealCards()
    {
        // currently the cards are dealt after 1 seconds of battle start. this time is arbitrary and should be checked
        yield return new WaitForSeconds(1f);
        dealer.DealCards(playerHand);

        yield return new WaitForSeconds(1f);
        dealer.DealCards(nPCHand);
        battlePhase = BattlePhases.CardPick;
    }

    void DisplayGuessInput(GameObject card)
    {
        guessHandler.gameObject.SetActive(true);
    }

    void NewRound()
    {
        GenerateTargetNumber();
        round++;
    }

    void GenerateTargetNumber()
    {
        targetNumber = Random.Range(5, 25);
        targetNumberDisplay.text = targetNumber.ToString();
    }

    void CompareInputAgainstExact(int playerInput)
    {
        //TODO compare player guess input against what the value should exactly be.
    }
    
    void UpdateCurrentNumber(int value)
    {
        currentNumber += value;
        currentNumberDisplay.text = currentNumber.ToString();
    }
}

public enum BattleModes { Beginner, Intermediate, Expert }
public enum BattlePhases { Deal, CardPick, Challenge, Attack, TurnSwitch }
public enum CurrentPlayer { Human, NPC }