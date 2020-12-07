using UnityEngine;

public class PlayerHand : Hand
{
    public delegate void OnHumanCardPlay(Card card);
    public event OnHumanCardPlay humanCardPlayed;
    public int guess;
    public Card lastPlayedCard { get; private set; }

    public override void PlayCard(Card card)
    {
        base.PlayCard(card);
        lastPlayedCard = card;

        // set the table as the new parent of card then animate card
        RectTransform rt = card.GetComponent<RectTransform>();
        RestPivot();
        card.transform.SetParent(table.transform);
        LeanTween.move(rt, table.GetComponent<RectTransform>().anchoredPosition, 0.5f).setOnComplete(OnTweenComplete);

        void RestPivot()
        {
            rt.anchorMin = new Vector2(0.5f, 0.5f);
            rt.anchorMax = new Vector2(0.5f, 0.5f);
        }

        void OnTweenComplete()
        {
            if (card.playCardSFX)
                SoundManager.instance.PlaySFX(card.playCardSFX);

            table.PlaceOnTable(card);
            humanCardPlayed?.Invoke(card);
        }
    }

    public void BlockCardInteractions(bool value)
    {
        GetComponent<CanvasGroup>().blocksRaycasts = !value;
    }

    public void ClearTurnInfo()
    {
        lastPlayedCard = null;
        guess = 0;
    }
}
