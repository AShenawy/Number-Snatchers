using UnityEngine;
using UnityEngine.EventSystems;

public class Card : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Card Data")]
    public CardType cardType;
    [Range(-9, 9)]public int value;

    [Header("Interaction Parameters")]
    [Range(0, 100)] public float peekUpDistance;

    RectTransform rt;

    private void Start()
    {
        rt = GetComponent<RectTransform>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        PlayCard();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        rt.anchoredPosition += new Vector2(0, peekUpDistance);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        rt.anchoredPosition -= new Vector2(0, peekUpDistance);
    }

    public void PlayCard()
    {
        PlayerHand player = GetComponentInParent<PlayerHand>();
        if (player)
            player.PlayCard(gameObject);
    }
}

public enum CardType { Add, Subtract, Wild}
