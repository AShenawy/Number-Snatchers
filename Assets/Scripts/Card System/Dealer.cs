using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Utilities;

public class Dealer : MonoBehaviour
{
    [Header("Source Cards")]
    public GameObject[] additionCardsPrefabs;
    public GameObject[] subtractionCardsPrefabs;
    public GameObject[] wildCardsPrefabs;

    [Header("Deck Cards")]
    public List<GameObject> deckCards = new List<GameObject>();
    public List<GameObject> graveyard = new List<GameObject>();

    [Header("Additional Parameters")]
    [SerializeField, Range(0, 7)] int cardsDealt = 5;
    [SerializeField] Image threeCardsIndicator;
    [SerializeField] Image twoCardsIndicator;
    [SerializeField] Image oneCardIndicator;

    public void FillDeck(BattleModes battMod)
    {
        switch (battMod)
        {
            case BattleModes.Beginner:
                // set up the cards to be played
                // for beginner battle there will be 5 copies of each 1-9 card
                GameObject[] addCardsBgn = new GameObject[45];
                for (int i = 0; i < additionCardsPrefabs.Length; i++)
                    ArrayFuncs.FillArray(addCardsBgn, additionCardsPrefabs[i], (i * 5), 5);

                // move the cards to the deck
                deckCards.InsertRange(0, addCardsBgn);
                break;

            case BattleModes.Intermediate:
                // set up the cards to be played
                // for intermediate battle there will be 16 copies of each 1-9 card
                GameObject[] addCardsInt = new GameObject[144];
                for (int i = 0; i < additionCardsPrefabs.Length; i++)
                    ArrayFuncs.FillArray(addCardsInt, additionCardsPrefabs[i], (i * 16), 16);

                // intermediate battle will include 8 wild cards    *** wild cards are assumed to be single card type ***
                GameObject[] wildCardInt = new GameObject[8];
                for (int j = 0; j < wildCardsPrefabs.Length; j++)
                    ArrayFuncs.FillArray(wildCardInt, wildCardsPrefabs[j], 0, 8);
                
                // move the cards to the deck
                deckCards.InsertRange(0, addCardsInt);
                deckCards.InsertRange(addCardsInt.Length, wildCardInt);
                break;

            case BattleModes.Expert:
                // currently expert is same as intermediate mode
                goto case BattleModes.Intermediate;
        }
    }

    public void DealCards(Hand hand)
    {
        for (int i = 0; i < cardsDealt; i++)
        {
            // pick a random card from the deck
            int random = Random.Range(0, deckCards.Count);
            GameObject card = Instantiate(deckCards[random]);

            //move the card to a hand
            hand.TakeCard(card);

            // move the card from deck
            deckCards.RemoveAt(random);
        }
    }
}
