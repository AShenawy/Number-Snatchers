using UnityEngine;
using UnityEngine.UI;
using System.Collections;


// in this phase the game checks the guesses and challenges to ensure it's going forward correctly
public class CheckGuess : Phase
{
    bool isCurrentPlayerChallenged;
    bool isGuessCorrect;
    bool isChallengeCorrect;

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
        // calculate the correct sum of values based on latest current number and the last played card
        int trueSum;

        if (battleManager.playerTurn == CurrentPlayer.Human)
        {
            trueSum = battleManager.currentNumber + playerHand.lastPlayedCard.value;

            // check sum against guess
            if (trueSum == playerHand.guess)
            {
                isGuessCorrect = true;
                Debug.Log("<color=green>" + battleManager.playerTurn + " guessed correctly.</color>");
                return;
            }
        }
        // same for NPC
        else
        {
            trueSum = battleManager.currentNumber + npcHand.lastPlayedCard.value;
            
            if (trueSum == npcHand.guess)
            {
                isGuessCorrect = true;
                Debug.Log("<color=green>" + battleManager.playerTurn + " guessed correctly.</color>");
                return;
            }
        }

        isGuessCorrect = false;
        Debug.Log("<color=red>" + battleManager.playerTurn + " did not guess correctly.</color>");
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
