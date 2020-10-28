using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CardPlay : Phase
{
    GuessHandler guessHandler;
    bool isCardPlayed;

    public CardPlay(BattleManager _bm, Stats _plStats, EnemyBattleData _npcData, PlayerHand _plrHnd, NPCHand _npcHnd)
           : base(_bm, _plStats, _npcData, _plrHnd, _npcHnd)
    {
        name = Phases.CardPick;

        guessHandler = battleManager.guessHandler;
        guessHandler.onInputSubmitted += OnCardPlayed;
    }

    public override void Enter()
    {
        Debug.Log("Entering Card Play phase. It's " + battleManager.playerTurn + " player's turn to play.");

        if (battleManager.playerTurn == CurrentPlayer.Human)
            playerHand.BlockCardInteractions(false);

        base.Enter();
    }

    public override void Update()
    {
        if (isCardPlayed)
        {
            nextPhase = new Challenge(battleManager, playerStats, npcData, playerHand, npcHand);
            stage = Stages.Exit;
        }
    }

    public override void Exit()
    {
        playerHand.BlockCardInteractions(true);
        isCardPlayed = false;
        guessHandler.onInputSubmitted -= OnCardPlayed;
        Debug.Log("Exiting Card Play phase after " + battleManager.playerTurn + " player played their card.");
        base.Exit();
    }

    void OnCardPlayed()
    {
        CompareInputAgainstExact();
        isCardPlayed = true;
    }

    void CompareInputAgainstExact()
    {
        // calculate expected value
        int valExpected = battleManager.currentNumber + battleManager.playedHumanCard.value;
        int valInput = guessHandler.submittedGuess;

        // update NPC player
        npcHand.EvaluatePlayerMove(valExpected, valInput);
    }
}
