using UnityEngine;
using System.Collections.Generic;
using Utilities;

public class PlayTable : MonoBehaviour
{
    public List<GameObject> cardsOnTable = new List<GameObject>();

    public void PlaceOnTable(GameObject card)
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
