﻿using System;
using UnityEngine;


// This is a representation of the comnputer opponent's behaviour
public class NPCHand : Hand
{
    public event Action<Card> onCardPlayed;
    public EnemyBattleData data;
    public int guess;
    bool playerGuessCorrect;
    int targetNumber;
    int currentNumber;

    public void UpdateTargetNumber(int num)
    {
        targetNumber = num;
    }

    public void UpdateCurrentNumber(int num)
    {
        currentNumber = num;
    }

    public void EvaluatePlayerMove(int expected, int playerGuess)
    {
        if (expected == playerGuess)
            playerGuessCorrect = true;
        else
            playerGuessCorrect = false;
    }

    public void ChallengePlayer()
    {
        //TODO challenging functionality
        if (playerGuessCorrect)
            print("Player guessed correctly. NPC will pass the challenge.");
        else
            print("Player guess is incorrect. NPC will challenge.");
    }

    public void PlayTurn()
    {
        switch (data.battleStyle)
        {
            case EnemyBattleStyles.Normal:
                PlayCard(FindBestCard());
                break;
        }
    }

    public override void PlayCard(Card card)
    {
        base.PlayCard(card);
        guess = GuessValue(card);
        onCardPlayed?.Invoke(card);
    }

    int GuessValue(Card card)
    {
        // currently NPC always makes a correct guess
        
        // set a 10% chance where the NPC might guess wrong
        if (UnityEngine.Random.Range(0, 10) < 1)
        {
            // NPC will make an error in the summation between -2 and 2
            // However there's 20% chance the error is 0 meaning no mistake
            int error = UnityEngine.Random.Range(-2, 3);
            
            return currentNumber + card.value + error;
        }
        
        return currentNumber + card.value;
    }

    Card FindBestCard()
    {
        // create a comparison var for each card in hand with a starting arbitrary value
        int closestToTarget = 100;
        Card bestCard = null;

        foreach (Card potCard in cardsInHand)
        {
            if (potCard.cardType == CardType.Add)
            {
                int addValue = currentNumber + potCard.value;
                int diff = targetNumber - addValue;
                if (diff < closestToTarget && diff >= 0)    // best card is one that's as close as possible to target but doesn't go over
                {
                    closestToTarget = diff;
                    bestCard = potCard;
                }
            }
            else if (potCard.cardType == CardType.Subtract)
            {
                print(gameObject.name + " is finding best sub card.");
            }
            // then it's wild card
            else
            {
                int possibleValue = Mathf.Clamp(Mathf.Abs(targetNumber - currentNumber), 1, 9);
                potCard.SetWildValue(possibleValue);

                // only play the wild card if there isn't a card already with the same value
                if (HasCardAsWildValue(possibleValue) == false)
                {
                    closestToTarget = targetNumber - (currentNumber + possibleValue);
                    bestCard = potCard;
                }
            }

            // if no card was able to fulfil the selection conditions, pick a random card
            if (bestCard == null)
            {
                bestCard = cardsInHand[UnityEngine.Random.Range(0, cardsInHand.Count)];
                print("NPC couldn't find card satisfying conditions. Picking a random card");
            }
        }

        return bestCard;
    }

    bool HasCardAsWildValue(int wildVal)
    {
        foreach (Card card in cardsInHand)
        {
            // check only non-wild cards in hand
            if (card.cardType == CardType.Add || card.cardType == CardType.Subtract)
            {
                if (card.value == wildVal)
                    return true;
            }
        }

        return false;
    }
}
