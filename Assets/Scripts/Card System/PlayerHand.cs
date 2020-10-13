using UnityEngine;

public class PlayerHand : Hand
{
    public delegate void OnHumanCardPlay(GameObject card);
    public event OnHumanCardPlay humanCardPlayed;

    public override void PlayCard(GameObject card)
    {
        base.PlayCard(card);
        humanCardPlayed?.Invoke(card);
    }
}
