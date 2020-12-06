using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Card : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Card Data")]
    public CardType cardType;
    [Range(-9, 9)]public int value;

    [Header("Interaction Parameters")]
    [Range(0, 100)] public float peekUpDistance;
    public Sprite face;
    public Sprite sleeve;
    public AudioClip hoverSFX, playCardSFX;

    [SerializeField] Image cardGraphic;
    RectTransform artTransform;

    private void Start()
    {
        artTransform = GetComponentInChildren<Image>().GetComponent<RectTransform>();
        
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        PlayCard();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        artTransform.anchoredPosition += new Vector2(0, peekUpDistance);

        if (hoverSFX)
            SoundManager.instance.PlaySFX(hoverSFX);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        artTransform.anchoredPosition -= new Vector2(0, peekUpDistance);
    }

    public void PlayCard()
    {
        PlayerHand player = GetComponentInParent<PlayerHand>();
        if (player)
            player.PlayCard(this);
    }

    public void SetWildValue(int val)
    {
        value = Mathf.Clamp(val, -9, 9);
    }

    public void ShowFace(bool value)
    {
        if (!value)
            cardGraphic.sprite = sleeve;
        else
            cardGraphic.sprite = face;
    }
}

public enum CardType { Add, Subtract, Wild}
