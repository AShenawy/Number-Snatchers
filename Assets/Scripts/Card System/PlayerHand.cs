using UnityEngine;

public class PlayerHand : Hand
{
    public delegate void OnHumanCardPlay(Card card);
    public event OnHumanCardPlay humanCardPlayed;

    public override void PlayCard(Card card)
    {
        base.PlayCard(card);
        humanCardPlayed?.Invoke(card);
    }

    public void BlockCardInteractions(bool value)
    {
        GetComponent<CanvasGroup>().blocksRaycasts = !value;
    }
}
