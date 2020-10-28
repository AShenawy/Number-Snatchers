using UnityEngine;
using System.Collections.Generic;
using Utilities;

public class PlayTable : MonoBehaviour
{
    public List<Card> cardsOnTable = new List<Card>();

    public void PlaceOnTable(Card card)
    {
        cardsOnTable.Add(card);
        RectTransform rt = card.GetComponent<RectTransform>();
        rt.SetParent(gameObject.transform, false);
        UIFuncs.ResetRect(rt);
    }

    public void EmptyTable()
    {
        //TODO empty cards to pot or graveyard
    }
}
