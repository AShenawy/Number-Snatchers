using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[DisallowMultipleComponent]
public class BattleManager : MonoBehaviour
{
    public EnemyDifficulty difficulty;
    public Phases battlePhase;
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

        //yield return new WaitForSeconds(1f);
        //dealer.DealCards(nPCHand);
        ////battlePhase = BattlePhases.CardPick;
    }

    void DisplayGuessInput(GameObject card)
    {
        guessHandler.gameObject.SetActive(true);
    }
    /*
    void NewRound()
    {
        // generate new target value for the round
        GenerateTargetNumber();
        currentRound++;
        roundNumberDisplay.text = currentRound.ToString();
    }

    void GenerateTargetNumber()
    {
        targetNumber = Random.Range(5, 25);
        targetNumberDisplay.text = targetNumber.ToString();
    }
    */
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

//public enum BattleModes { Beginner, Intermediate, Expert }

//public enum BattlePhases { Deal, CardPick, Challenge, Attack, TurnSwitch }
public enum CurrentPlayer { Human, NPC }