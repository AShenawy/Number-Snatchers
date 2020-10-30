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
        humanCardPlayed?.Invoke(card);
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
