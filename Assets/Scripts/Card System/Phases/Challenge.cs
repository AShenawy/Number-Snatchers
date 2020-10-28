using UnityEngine;
using UnityEngine.UI;

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
        playerHand = battleManager.playerHand;
        npcHand = battleManager.nPCHand;

        base.Enter();
    }

    public override void Update()
    {
        // check who gets to challenge
        if (battleManager.playerTurn == CurrentPlayer.Human)
        {
            //TODO show player option to challenge NPC
            Debug.Log("Player chooses to challenge NPC");
        }
        else
            npcHand.ChallengePlayer();

        nextPhase = new CheckGuess(battleManager, playerStats, npcData, playerHpDisplay, npcHpDisplay);
        stage = Stages.Exit;
    }
}
