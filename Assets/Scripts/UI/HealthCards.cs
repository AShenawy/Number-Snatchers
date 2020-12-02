using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthCards : MonoBehaviour
{
    public Image[] cards;
    public Sprite healthFull, healthHalf, healthEmpty;


    public void RefillCards()
    {
        foreach (Image card in cards)
            card.sprite = healthFull;
    }

    void EmptyCards()
    {
        foreach (Image card in cards)
            card.sprite = healthEmpty;
    }

    public void UpdateCards(float percentageHP)
    {
        void MakeFull(int cardNum)
        {
            cards[cardNum].sprite = healthFull;
        }

        void MakeHalf(int cardNum)
        {
            cards[cardNum].sprite = healthHalf;
        }

        void MakeEmpty(int cardNum)
        {
            cards[cardNum].sprite = healthEmpty;
        }


        if (percentageHP <= 0f)
        {
            EmptyCards();
        }
        else if (percentageHP <= 0.1f)
        {
            MakeEmpty(4);
            MakeEmpty(3);
            MakeEmpty(2);
            MakeEmpty(1);
            MakeHalf(0);
        }
        else if (percentageHP <= 0.2f)
        {
            MakeEmpty(4);
            MakeEmpty(3);
            MakeEmpty(2);
            MakeEmpty(1);
        }
        else if (percentageHP <= 0.3f)
        {
            MakeEmpty(4);
            MakeEmpty(3);
            MakeEmpty(2);
            MakeHalf(1);
        }
        else if (percentageHP <= 0.4f)
        {
            MakeEmpty(4);
            MakeEmpty(3);
            MakeEmpty(2);
        }
        else if (percentageHP <= 0.5f)
        {
            MakeEmpty(4);
            MakeEmpty(3);
            MakeHalf(2);
        }
        else if (percentageHP <= 0.6f)
        {
            MakeEmpty(4);
            MakeEmpty(3);
        }
        else if (percentageHP <= 0.7f)
        {
            MakeEmpty(4);
            MakeHalf(3);
        }
        else if (percentageHP <= 0.8f)
        {
            MakeEmpty(4);
        }
        else if (percentageHP <= 0.9f)
        {
            MakeHalf(4);
        }
        else
        {
            MakeFull(4);
            MakeFull(3);
            MakeFull(2);
            MakeFull(1);
            MakeFull(0);
        }
    }
}
