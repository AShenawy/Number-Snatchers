using UnityEngine;
using System.Collections.Generic;
using Utilities;

public class PlayTable : MonoBehaviour
{
    public Dealer deck;
    public Pot pot;
    public List<Card> cardsOnTable = new List<Card>();

    public void PlaceOnTable(Card card)
    {
        cardsOnTable.Add(card);
        RectTransform rt = card.GetComponent<RectTransform>();
        rt.SetParent(gameObject.transform, false);
        UIFuncs.ResetRect(rt);
    }

    public void ClearTable(CardCollections destination)
    {
        switch (destination)
        {
            case CardCollections.Pot:
                // move cards to pot, clear table list, and remove card GOs
                foreach (Card card in cardsOnTable)
                    pot.PlaceInPot(card);
                
                cardsOnTable.Clear();
                foreach (Transform child in transform)
                    Destroy(child.gameObject);
                break;

            case CardCollections.Graveyard:
                // move cards to graveyard, clear table list, and remove card GOs
                foreach (Card card in cardsOnTable)
                    deck.PlaceInGraveyard(card);

                cardsOnTable.Clear();
                foreach (Transform child in transform)
                    Destroy(child.gameObject);
                break;

            default:
                Debug.LogError("Clearing table to wrong destination. Please set destination to either Pot or Graveyard.");
                break;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
            cardsOnTable.Clear();

        if (Input.GetKeyDown(KeyCode.D))
        {
            foreach (Transform child in transform)
                Destroy(child.gameObject);
        }
    }
}

public enum CardCollections { Deck, Pot, Graveyard }
