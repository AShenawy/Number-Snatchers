using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Hand : MonoBehaviour
{
    public List<GameObject> cardsInHand = new List<GameObject>();
    public PlayTable table;

    public void TakeCard(GameObject card)
    {
        cardsInHand.Add(card);
        card.GetComponent<RectTransform>().SetParent(gameObject.transform, false);
    }

    public virtual void PlayCard(GameObject card)
    {
        table.PlaceOnTable(card);
    }
}
