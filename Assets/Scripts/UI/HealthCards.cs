using UnityEngine;
using UnityEngine.UI;

public class HealthCards : MonoBehaviour
{
    public HealthCard[] cards;


    public void RefillCards()
    {
        foreach (HealthCard card in cards)
            card.SetFull();
    }

    void EmptyCards()
    {
        foreach (HealthCard card in cards)
            card.SetEmpty();
    }

    public void UpdateCards(float percentageHP)
    {
        void MakeFull(int cardNum)
        {
            cards[cardNum].SetFull();
        }

        void MakeHalf(int cardNum)
        {
            cards[cardNum].SetHalf();
        }

        void MakeEmpty(int cardNum)
        {
            cards[cardNum].SetEmpty();
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
