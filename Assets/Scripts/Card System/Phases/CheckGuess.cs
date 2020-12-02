using UnityEngine;
using UnityEngine.UI;
using System.Collections;


// in this phase the game checks the guesses and challenges to ensure it's going forward correctly
public class CheckGuess : Phase
{
    enum ChallengeCases { ChallengeCorrect, BothWrong, NoChallenge, RightGuess }
    bool isCurrentPlayerChallenged;
    bool isGuessCorrect;
    bool isChallengeCorrect;
    bool isChallengeWon;
    int trueSum;
    bool isCheckComplete;
    float timeEnter;    // time to countdown from
    float timeExit = 2f;    // time to exit the phase
    Pot pot;


    public CheckGuess(BattleManager _bm, Stats _plStats, EnemyBattleData _npcData, PlayerHand _plrHnd, NPCHand _npcHnd, bool _isChallenged)
       : base(_bm, _plStats, _npcData, _plrHnd, _npcHnd)
    {
        name = Phases.GuessCheck;
        isCurrentPlayerChallenged = _isChallenged;
        pot = battleManager.pot;
    }

    public override void Enter()
    {
        Debug.Log("Entering Guess Check phase.");

        EvaluateTurn();

        timeEnter = Time.timeSinceLevelLoad;
        base.Enter();
    }

    public override void Update()
    {
        if (isCheckComplete && (Time.timeSinceLevelLoad - timeEnter >= timeExit))
        {
            nextPhase = new TurnEnd(battleManager, playerStats, npcData, playerHand, npcHand, isChallengeWon);
            stage = Stages.Exit;
        }
    }

    public override void Exit()
    {
        Debug.Log("Exiting Guess Check phase. Phase time is " + (Time.timeSinceLevelLoad - timeEnter) + " seconds.");

        // reset values
        isCheckComplete = false;
        isGuessCorrect = false;
        isChallengeCorrect = false;
        isCurrentPlayerChallenged = false;
        isChallengeWon = false;
        trueSum = 0;

        base.Exit();
    }

    void EvaluateTurn()
    {
        CheckGuessedSum();

        // if current player made a wrong guess and was challenged then check challenger's guess
        if (!isGuessCorrect && isCurrentPlayerChallenged)
        {
            CheckChallengerGuess();

            if (isChallengeCorrect)
            {
                // if challenger guess is right then player will take damage and round is finished
                Debug.Log("<color=yellow>" + battleManager.playerTurn + "'s guess is wrong and receives " + CalculateDamage()
                          + " damage. Opponents' guess is correct. Opponent wins the pot</color>");
                UpdateCurrentNumber(trueSum);
                DisplayInfoCard(ChallengeCases.ChallengeCorrect);
                ApplyDamage(battleManager.playerTurn, CalculateDamage());
                isChallengeWon = true;      // inform next phase that challenge is won and can skip to a new round
                isCheckComplete = true;
                return;
            }
            else
            {
                // if both sides are wrong then both receive damage and round continues
                Debug.Log("<color=yellow>Both sides made wrong guess and receive " + CalculateDamage() + " damage.</color>");
                UpdateCurrentNumber(trueSum);
                DisplayInfoCard(ChallengeCases.BothWrong);
                ApplyDamageBoth(CalculateDamage());
                isCheckComplete = true;
                return;
            }
        }
        // if current player made a wrong guess and wasn't challenged then both receive damage
        else if (!isGuessCorrect && !isCurrentPlayerChallenged)
        {
            Debug.Log("<color=yellow>" + battleManager.playerTurn + "'s guess is wrong and opponent didn't challenge. Both receive "
                      + CalculateDamage() + " damage.</color>");
            UpdateCurrentNumber(trueSum);
            DisplayInfoCard(ChallengeCases.NoChallenge);
            ApplyDamageBoth(CalculateDamage());
            isCheckComplete = true;
            return;
        }

        // if above is passed then player made a correct guess
        Debug.Log("<color=yellow>Proceeding with player's correct guess.</color>");
        UpdateCurrentNumber(trueSum);
        DisplayInfoCard(ChallengeCases.RightGuess);

        // if reached the target then opposite side receives damage and round is ended
        if (battleManager.currentNumber == battleManager.targetNumber)
        {
            Debug.Log("<color=yellow>Target reached. Opponent receives full " + CalculateDamage() + " damage.</color>");
            ApplyDamageOpponent(CalculateDamage());
        }

        isCheckComplete = true;

        int CalculateDamage()
        {
            if (pot.hasCards)
                return trueSum + pot.cardsTotalValue;
            else
                return trueSum;
        }
    }

    void DisplayInfoCard(ChallengeCases challenge)
    {
        //TODO Display appropriate info card based on guesses and challenges.
        Debug.Log("<color=red>" + challenge.ToString() + "</color>");
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

    void UpdateCurrentNumber(int value)
    {
        battleManager.currentNumber = value;
        battleManager.currentNumberDisplay.text = value.ToString();
    }

    void ApplyDamageBoth(int amount)
    {
        ApplyDamage(CurrentPlayer.Human, amount);
        ApplyDamage(CurrentPlayer.NPC, amount);
    }

    void ApplyDamageOpponent(int amount)
    {
        if (battleManager.playerTurn == CurrentPlayer.Human)
            ApplyDamage(CurrentPlayer.NPC, amount);
        else
            ApplyDamage(CurrentPlayer.Human, amount);
    }

    void ApplyDamage(CurrentPlayer side, int amount)
    {
        switch (side)
        {
            case CurrentPlayer.Human:
                currentHpPlayer -= amount;
                if (currentHpPlayer < 0)
                    currentHpPlayer = 0;
                battleManager.playerCurrentHP = currentHpPlayer;
                battleManager.playerHPDisplay.fillAmount = (float)currentHpPlayer / startingHpPlayer;
                battleManager.playerHealthCards.UpdateCards((float)currentHpPlayer / startingHpPlayer);
                break;

            case CurrentPlayer.NPC:
                currentHpNPC -= amount;
                if (currentHpNPC < 0)
                    currentHpNPC = 0;
                battleManager.npcCurrentHP = currentHpNPC;
                battleManager.nPCHPDisplay.fillAmount = (float)currentHpNPC / startingHpNPC;
                battleManager.npcHealthCards.UpdateCards((float)currentHpNPC / startingHpNPC);
                break;
        }
    }
}
