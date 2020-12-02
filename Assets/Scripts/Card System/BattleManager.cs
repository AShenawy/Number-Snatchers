using System.Collections;
using System;
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
    public HealthCards npcHealthCards;
    public HealthCards playerHealthCards;
    public Text currentPhaseText;

    [Header("Controllers")]
    public Dealer dealer;
    public Pot pot;
    public PlayTable table;

    [Header("Flow Cards")]      // Prefabs
    public GuessHandler guessHandlerCardPrefab;
    public ChallengeHandler humanChallengerCardPrefab;
    public GameObject turnSwitchCardPrefab;
    public GameEndHandler gameWonCardPrefab;
    public GameEndHandler gameLostCardPrefab;
    public GameEndHandler gameDrawCardPrefab;

    [Header("Flash Cards")] // Prefabs to display info on screen
    public InfoCard[] infoCardsPrefabs;

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

    // Play data
    Phase currentPhase;
    

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

    public void SwitchTurn()
    {
        if (playerTurn == CurrentPlayer.Human)
            playerTurn = CurrentPlayer.NPC;
        else
            playerTurn = CurrentPlayer.Human;
    }
}

public enum CurrentPlayer { Human, NPC }