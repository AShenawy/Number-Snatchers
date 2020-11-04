using UnityEngine;
using UnityEngine.UI;
using System.Collections;


// in this phase the game checks the guesses and challenges to ensure it's going forward correctly
public class CheckGuess : Phase
{
    bool isCurrentPlayerChallenged;
    bool isGuessCorrect;
    bool isChallengeCorrect;
    int trueSum;

    public CheckGuess(BattleManager _bm, Stats _plStats, EnemyBattleData _npcData, PlayerHand _plrHnd, NPCHand _npcHnd, bool _isChallenged)
       : base(_bm, _plStats, _npcData, _plrHnd, _npcHnd)
    {
        name = Phases.GuessCheck;
        isCurrentPlayerChallenged = _isChallenged;
    }

    public override void Enter()
    {
        Debug.Log("Entering Guess Check phase.");

        // check that current player guess is correct
        CheckGuessedSum();
        CheckChallengerGuess();

        base.Enter();
    }

    public override void Update()
    {
        //TODO check the guess, challenge, and correct value

        nextPhase = new TurnEnd(battleManager, playerStats, npcData, playerHand, npcHand);
        stage = Stages.Exit;
    }

    public override void Exit()
    {
        Debug.Log("Exiting Guess Check phase.");
        base.Exit();
    }

    void CheckGuessedSum()
    {
        if (battleManager.playerTurn == CurrentPlayer.Human)
        {
            // calculate the correct sum of values based on latest current number and the last played card
            trueSum = battleManager.currentNumber + playerHand.lastPlayedCard.value;

            // check sum against guess
            if (trueSum == playerHand.guess)
            {
                Debug.Log("<color=green>Human guessed correctly.</color>");
                isGuessCorrect = true;
                return;
            }
        }
        else   // same for NPC
        {
            trueSum = battleManager.currentNumber + npcHand.lastPlayedCard.value;
            if (trueSum == npcHand.guess)
            {
                Debug.Log("<color=green>NPC guessed correctly.</color>");
                isGuessCorrect = true;
                return;
            }
        }

        // if neither guesses were correct and the method didn't return already then guess is wrong
        Debug.Log("<color=red>" + battleManager.playerTurn + " did not guess correctly.</color>");
        isGuessCorrect = false;
    }

    void CheckChallengerGuess()
    {
        if (!isCurrentPlayerChallenged)
        {
            Debug.Log("<color=yellow>" + battleManager.playerTurn + "'s guess wasn't challenged by opponent.</color>");
            return;
        }

        // check the correctness of the challenger (opposite player)
        if (battleManager.playerTurn == CurrentPlayer.Human)
        {
            if (trueSum == npcHand.guess)
            {
                Debug.Log("<color=green>NPC guessed correctly.</color>");
                isChallengeCorrect = true;
                return;
            }
        }
        // if it's NPC turn then check the player guess for the challenge
        else
        {
            if (trueSum == playerHand.guess)
            {
                Debug.Log("<color=green>Human guessed correctly.</color>");
                isChallengeCorrect = true;
                return;
            }
        }

        // if neither guesses were correct and the method didn't return already then guess is wrong
        Debug.Log("<color=red>The opponent did not guess correctly.</color>");
        isChallengeCorrect = false;
    }

    public void TakeDamagePlayer(int value)
    {
        currentHpPlayer -= value;
        if (currentHpPlayer < 0)
            currentHpPlayer = 0;
        battleManager.playerCurrentHP = currentHpPlayer;
        battleManager.playerHPDisplay.fillAmount = currentHpPlayer / startingHpPlayer;
    }

    public void TakeDamageEnemy(int value)
    {
        currentHpNPC -= value;
        if (currentHpNPC < 0)
            currentHpNPC = 0;
        battleManager.npcCurrentHP = currentHpNPC;
        battleManager.nPCHPDisplay.fillAmount = currentHpNPC / startingHpNPC;
    }
}
