using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Pot : MonoBehaviour
{
    public List<Card> potCards = new List<Card>();
    public int cardsTotalValue;
    [SerializeField] Dealer dealer;


    public void PlaceInPot(Card card)
    {
        switch (card.cardType)
        {
            case CardType.Add:
                potCards.Add(dealer.additionCardsPrefabs[card.value - 1]);
                cardsTotalValue += card.value;
                break;

            case CardType.Subtract:
                potCards.Add(dealer.subtractionCardsPrefabs[(card.value * -1) - 1]);
                cardsTotalValue += card.value * -1;
                break;

            case CardType.Wild:
                potCards.Add(dealer.wildCardsPrefabs[0]);
                // use absolute value to account for +ve and -ve value cards
                cardsTotalValue += Mathf.Abs(card.value);
                break;
        }
    }

    void RefreshCardsValue()
    {
        cardsTotalValue = 0;

        foreach (Card card in potCards)
            cardsTotalValue += card.value;
    }
}
