using UnityEngine;
using System;

// This is a representation of the comnputer opponent's behaviour
public class NPCHand : Hand
{
    public event Action onCardPlayed;
    public EnemyBattleData data;
    bool playerGuessCorrect;


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
            print("Player guesssed correctly. NPC will pass the challenge.");
        else
            print("Player guess is incorrect. NPC will challenge.");
    }

    public void PlayTurn()
    {
        switch (data.battleStyle)
        {
            case EnemyBattleStyles.Normal:
                break;

        }
    }
}
