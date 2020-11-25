using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Utilities;

public class Dealer : MonoBehaviour
{
    [Header("Source Cards")]
    public Card[] additionCardsPrefabs;
    public Card[] subtractionCardsPrefabs;
    public Card[] wildCardsPrefabs;

    [Header("Deck Cards")]
    public List<Card> deckCards = new List<Card>();
    public List<Card> graveyard = new List<Card>();

    [Header("Additional Parameters")]
    [SerializeField, Range(0, 7)] int cardsPerDeal = 5;
    [SerializeField] Image[] cardIndicators;


    public void FillDeck(EnemyDifficulty difficulty)
    {
        ShowCardIndicators();

        switch (difficulty)
        {
            case EnemyDifficulty.Beginner:
                // set up the cards to be played
                // for beginner battle there will be 5 copies of each 1-9 card
                Card[] addCardsBgn = new Card[45];
                for (int i = 0; i < additionCardsPrefabs.Length; i++)
                    ArrayFuncs.FillArray(addCardsBgn, additionCardsPrefabs[i], (i * 5), 5);

                // move the cards to the deck
                deckCards.InsertRange(0, addCardsBgn);
                break;

            case EnemyDifficulty.Intermediate:
                // set up the cards to be played
                // for intermediate battle there will be 16 copies of each 1-9 card
                Card[] addCardsInt = new Card[144];
                for (int i = 0; i < additionCardsPrefabs.Length; i++)
                    ArrayFuncs.FillArray(addCardsInt, additionCardsPrefabs[i], (i * 16), 16);

                // intermediate battle will include 8 wild cards    *** wild cards are assumed to be single card type ***
                Card[] wildCardInt = new Card[8];
                for (int j = 0; j < wildCardsPrefabs.Length; j++)
                    ArrayFuncs.FillArray(wildCardInt, wildCardsPrefabs[j], 0, 8);

                // move the cards to the deck
                deckCards.InsertRange(0, addCardsInt);
                deckCards.InsertRange(addCardsInt.Length, wildCardInt);
                break;

            case EnemyDifficulty.Expert:
                // currently expert is same as intermediate mode
                goto case EnemyDifficulty.Intermediate;
        }
    }

    public void DealCards(Hand hand)
    {
        if (deckCards.Count <= 0)
            RefreshDeck();

        int cardsToDeal = cardsPerDeal - hand.cardsInHand.Count;
        print(hand.name + " started play with " + hand.cardsInHand.Count + " cards. Will be dealt " + cardsToDeal + " cards.");

        for (int i = 0; i < cardsToDeal; i++)
        {
            // pick a random card from the deck
            int random = Random.Range(0, deckCards.Count);
            Card card = Instantiate(deckCards[random]);

            if (hand.GetComponent<NPCHand>())
                card.ShowFace(false);

            //move the card to a hand
            hand.TakeCard(card);

            // move the card from deck
            deckCards.RemoveAt(random);
        }

        if (deckCards.Count < 1)
            HideCardIndicator(3);
        else if (deckCards.Count < 2)
            HideCardIndicator(2);
        else if (deckCards.Count < 3)
            HideCardIndicator(1);
    }

    public void PlaceInGraveyard(Card card)
    {
        switch (card.cardType)
        {
            case CardType.Add:
                graveyard.Add(additionCardsPrefabs[card.value - 1]);
                break;

            case CardType.Subtract:
                graveyard.Add(subtractionCardsPrefabs[(card.value * -1) - 1]);
                break;

            case CardType.Wild:
                graveyard.Add(wildCardsPrefabs[0]);
                break;
        }
    }

    public void RefreshDeck()
    {
        print("Deck empty. Moving cards from graveyard back to deck.");
        foreach (Card card in graveyard)
            deckCards.Add(card);

        graveyard.Clear();
        ShowCardIndicators();
    }

    void ShowCardIndicators()
    {
        foreach (var image in cardIndicators)
            image.enabled = true;
    }

    void HideCardIndicator(int number)
    {
        for (int i = cardIndicators.Length - 1; i >= cardIndicators.Length - number; i--)
            cardIndicators[i].enabled = false;
    }
}
