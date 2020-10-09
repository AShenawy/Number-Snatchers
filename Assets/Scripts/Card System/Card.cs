using UnityEngine;

public class Card : MonoBehaviour
{
    public CardType cardType;
    [Range(-9, 9)]public int value;


}

public enum CardType { Add, Subtract, Wild}
