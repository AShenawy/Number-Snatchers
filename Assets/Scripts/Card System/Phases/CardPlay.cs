using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CardPlay : Phase
{
    GuessHandler guessHandler;
    bool isCardPlayed;

    public CardPlay(BattleManager _bm, Stats _plStats, EnemyBattleData _npcData, Image _plrHpDisplay, Image _npcHpDisplay)
           : base(_bm, _plStats, _npcData, _plrHpDisplay, _npcHpDisplay)
    {
        name = Phases.CardPick;

        guessHandler = battleManager.guessHandler;
        guessHandler.onInputSubmitted += OnCardPlayed;
    }

    public override void Enter()
    {
        Debug.Log("Entering Card Play phase. It's " + battleManager.playerTurn + "'s turn to play.");

        if (battleManager.playerTurn == CurrentPlayer.Human)
            battleManager.playerHand.BlockCardInteractions(false);

        base.Enter();
    }

    public override void Update()
    {
        if (isCardPlayed)
        {
            nextPhase = new Challenge(battleManager, playerStats, npcData, playerHpDisplay, npcHpDisplay);
            stage = Stages.Exit;
        }
    }

    public override void Exit()
    {
        battleManager.playerHand.BlockCardInteractions(true);
        isCardPlayed = false;
        guessHandler.onInputSubmitted -= OnCardPlayed;
        Debug.Log("Exiting Card Play phase after " + battleManager.playerTurn + " played their card.");
        base.Exit();
    }

    void OnCardPlayed()
    {
        isCardPlayed = true;
    }
}
