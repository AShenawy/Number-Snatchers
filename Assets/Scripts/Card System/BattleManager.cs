using System.Collections;
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

    [Header("Flow Cards")]
    public GameObject turnSwitchCard;

    [Header("Player Parameters")]
    public PlayerHand playerHand;
    public Stats playerStats;

    [Header("NPC Parameters")]
    public NPCHand nPCHand;
    public EnemyBattleData nPCData;

    [HideInInspector] public int targetNumber;
    [HideInInspector] public int currentNumber;
    [HideInInspector] public int currentRound = 0;
    [HideInInspector] public int playerCurrentHP;
    [HideInInspector] public int npcCurrentHP;
    [HideInInspector] public Card playedHumanCard;
    [HideInInspector] public Card playedNPCCard;

    // Play data
    Phase currentPhase;
    

    private void OnEnable()
    {
        playerHand.humanCardPlayed += DisplayGuessInput;
        playerHand.humanCardPlayed += UpdatePlayInfo;
    }

    private void OnDisable()
    {
        playerHand.humanCardPlayed -= DisplayGuessInput;
    }

    // Start is called before the first frame update
    void Start()
    {
        currentPhase = new Start(this, playerStats, nPCData, playerHand, nPCHand);
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
    }

    void DisplayGuessInput(Card card)
    {
        guessHandler.gameObject.SetActive(true);
    }

    void UpdatePlayInfo(Card card)
    {
        if (playerTurn == CurrentPlayer.Human)
            playedHumanCard = card;
        else
            playedNPCCard = card;
    }

    void UpdateCurrentNumber()
    {
        currentNumber += playedHumanCard.value;
        currentNumberDisplay.text = currentNumber.ToString();
    }

    public void SwitchTurn()
    {
        if (playerTurn == CurrentPlayer.Human)
            playerTurn = CurrentPlayer.NPC;
        else
            playerTurn = CurrentPlayer.Human;
    }
}

public enum CurrentPlayer { Human, NPC }