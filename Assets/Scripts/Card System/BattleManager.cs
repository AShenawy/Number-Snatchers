using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleManager : MonoBehaviour
{
    public BattleModes battleMode;
    public BattlePhases battlePhase;
    public Text targetNumberDisplay;
    public Text currentNumberDisplay;
    public Dealer dealer;
    public PlayerHand playerHand;
    public NPCHand nPCHand;

    // Start is called before the first frame update
    void Start()
    {
        battlePhase = BattlePhases.Deal;
        dealer.FillDeck(battleMode);

        StartCoroutine(DealCards());
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
}

public enum BattleModes { Beginner, Intermediate, Expert }
public enum BattlePhases { Deal, CardPick, Challenge, Attack, TurnSwitch }
public enum CurrentPlayer { Human, NPC }