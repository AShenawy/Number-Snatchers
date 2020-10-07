using UnityEngine;

[System.Serializable]
public class Card
{
    public CardType cardType;
    public Sprite cardArt;


}

public enum CardType { Add, Subtract, Wild}
