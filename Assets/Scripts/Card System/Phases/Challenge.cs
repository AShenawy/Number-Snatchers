using UnityEngine;
using UnityEngine.UI;

// this phase is where the opponent of the current player gets to challenge their guess, if they choose to do so
public class Challenge : Phase
{
    PlayerHand playerHand;
    NPCHand npcHand;

    public Challenge(BattleManager _bm, Stats _plStats, EnemyBattleData _npcData, Image _plrHpDisplay, Image _npcHpDisplay)
        : base(_bm, _plStats, _npcData, _plrHpDisplay, _npcHpDisplay)
    {
        name = Phases.Challenge;
    }

    public override void Enter()
    {
        Debug.Log("Entering Challenge phase. The " + battleManager.playerTurn + " player will be challenged.");

        playerHand = battleManager.playerHand;
        npcHand = battleManager.nPCHand;

        base.Enter();
    }

    public override void Update()
    {
        // Let the opposite side challenge the current playing side
        if (battleManager.playerTurn == CurrentPlayer.Human)
        {
            //TODO show player option to challenge NPC
            npcHand.ChallengePlayer();
        }
        else
            Debug.Log("NPC chooses to challenge Human player");

        nextPhase = new CheckGuess(battleManager, playerStats, npcData, playerHpDisplay, npcHpDisplay);
        stage = Stages.Exit;
    }

    public override void Exit()
    {
        Debug.Log("Exiting Challenge phase.");
        base.Exit();
    }
}
