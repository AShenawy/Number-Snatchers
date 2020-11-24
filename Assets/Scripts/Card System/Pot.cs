using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class Pot : MonoBehaviour
{
    public List<Card> potCards = new List<Card>();
    public int cardsTotalValue;
    public bool hasCards;
    [SerializeField] Dealer dealer;
    [SerializeField] Image[] cardIndicators;


    void Start()
    {
        EmptyPot();
    }

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

        hasCards = true;

        if (potCards.Count >= 3)
            ShowCardIndicators(3);
        else if (potCards.Count >= 2)
            ShowCardIndicators(2);
        else
            ShowCardIndicators(1);
    }

    public void EmptyPot()
    {
        // move cards to graveyard and empty the pot
        HideCardIndicators();
        foreach (Card card in potCards)
            dealer.PlaceInGraveyard(card);

        cardsTotalValue = 0;
        potCards.Clear();
        hasCards = false;
    }

    void ShowCardIndicators(int number)
    {
        for (int i = 0; i < number; i++)
            cardIndicators[i].enabled = true;
    }

    void HideCardIndicators()
    {
        foreach (var image in cardIndicators)
            image.enabled = false;
    }
}
