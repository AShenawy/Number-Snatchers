using UnityEngine;
using UnityEngine.UI;
using System.Collections;


// in this phase the game checks the guesses and challenges to ensure it's going forward correctly
public class CheckGuess : Phase
{

    public CheckGuess(BattleManager _bm, Stats _plStats, EnemyBattleData _npcData, Image _plrHpDisplay, Image _npcHpDisplay)
       : base(_bm, _plStats, _npcData, _plrHpDisplay, _npcHpDisplay)
    {
        name = Phases.GuessCheck;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Update()
    {
        //TODO check the guess, challenge, and correct value

        nextPhase = new TurnEnd(battleManager, playerStats, npcData, playerHpDisplay, npcHpDisplay);
        stage = Stages.Exit;
    }

    public override void Exit()
    {
        Debug.Log("Exiting Guess Check phase.");
        base.Exit();
    }

    public void TakeDamagePlayer(int value)
    {
        currentHpPlayer -= value;
        if (currentHpPlayer < 0)
            currentHpPlayer = 0;
        battleManager.playerCurrentHP = currentHpPlayer;
        playerHpDisplay.fillAmount = currentHpPlayer / startingHpPlayer;
    }

    public void TakeDamageEnemy(int value)
    {
        currentHpNPC -= value;
        if (currentHpNPC < 0)
            currentHpNPC = 0;
        battleManager.npcCurrentHP = currentHpNPC;
        npcHpDisplay.fillAmount = currentHpNPC / startingHpNPC;
    }
}
