using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Utilities;

public class Dealer : MonoBehaviour
{
    [Header("Source Cards")]
    public Card[] additionCards;
    public Card[] subtractionCards;
    public Card[] wildCards;

    [Header("Deck Cards")]
    public List<Card> deckCards = new List<Card>();

    public void FillDeck(BattleMode battMod)
    {
        switch (battMod)
        {
            case BattleMode.Beginner:
                // set up the cards to be played
                // for beginner battle there will be 5 copies of each 1-9 card
                Card[] addCardsBgn = new Card[45];
                for (int i = 0; i < additionCards.Length; i++)
                    ArrayFuncs.FillArray(addCardsBgn, additionCards[i], (i * 5), 5);

                // move the cards to the deck
                deckCards.InsertRange(0, addCardsBgn);
                break;

            case BattleMode.Intermediate:
                // set up the cards to be played
                // for intermediate battle there will be 16 copies of each 1-9 card
                Card[] addCardsInt = new Card[144];
                for (int i = 0; i < additionCards.Length; i++)
                    ArrayFuncs.FillArray(addCardsInt, additionCards[i], (i * 16), 16);

                // intermediate battle will include 8 wild cards    *** wild cards are assumed to be single card type ***
                Card[] wildCardInt = new Card[8];
                for (int j = 0; j < wildCards.Length; j++)
                    ArrayFuncs.FillArray(wildCardInt, wildCards[j], 0, 8);
                
                // move the cards to the deck
                deckCards.InsertRange(0, addCardsInt);
                deckCards.InsertRange(addCardsInt.Length, wildCardInt);
                break;

            case BattleMode.Expert:
                // currently expert is same as intermediate mode
                goto case BattleMode.Intermediate;
        }
    }
}
