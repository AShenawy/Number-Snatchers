using System.Collections.Generic;
using UnityEngine;

public abstract class Hand : MonoBehaviour
{
    public List<Card> cardsInHand = new List<Card>();
    public PlayTable table;

    public void TakeCard(Card card)
    {
        cardsInHand.Add(card);
        card.GetComponent<RectTransform>().SetParent(gameObject.transform, false);
    }

    public virtual void PlayCard(Card card)
    {
        cardsInHand.Remove(card);
        card.ShowFace(true);
        table.PlaceOnTable(card);
    }
}
