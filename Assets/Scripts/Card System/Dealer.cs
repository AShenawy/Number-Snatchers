using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Dealer : MonoBehaviour
{
    [Header("Source Cards")]
    public Card[] additionCards;
    public Card[] subtractionCards;
    public Card[] wildCards;

    [Header("Deck Cards")]
    public List<Card> deckCards = new List<Card>();
    public battleMode battleMode;

    public Card[] addCardsRange = new Card[45];


    // Start is called before the first frame update
    void Start()
    {
        switch (battleMode)
        {
            case (battleMode.Beginner):
                // for beginner battle there will be 5 copies of each 1-9 card
                
                
                for (int i = 0; i < additionCards.Length; i++)
                    FillArray(addCardsRange, additionCards[i], i*5, 5);
                

                break;

            case (battleMode.Intermediate):
                break;

            case (battleMode.Expert):
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FillArray<T>(T[] cardsArray, T value, int startIndex, int count)
    {
        for (int i = startIndex; i < startIndex+count; i++)
        {
            cardsArray[i] = value;
        }
    }
}

public enum battleMode {Beginner, Intermediate, Expert}
