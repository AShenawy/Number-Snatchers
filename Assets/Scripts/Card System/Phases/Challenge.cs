using UnityEngine;

// this phase is where the opponent of the current player gets to challenge their guess, if they choose to do so
public class Challenge : Phase
{
    ChallengeHandler humanChallengerHandler;

    public Challenge(BattleManager _bm, Stats _plStats, EnemyBattleData _npcData, PlayerHand _plrHnd, NPCHand _npcHnd)
        : base(_bm, _plStats, _npcData, _plrHnd, _npcHnd)
    {
        name = Phases.Challenge;
    }

    public override void Enter()
    {
        Debug.Log("Entering Challenge phase. The " + battleManager.playerTurn + " player will be challenged.");

        if (battleManager.playerTurn == CurrentPlayer.NPC)
        {
            humanChallengerHandler = GameObject.Instantiate(battleManager.humanChallengerCardPrefab, battleManager.transform);
            PopulateChallengeDetails();
        }

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
            Debug.Log("Human chooses to challenge NPC player");

        //nextPhase = new CheckGuess(battleManager, playerStats, npcData, playerHand, npcHand);
        //stage = Stages.Exit;
    }

    public override void Exit()
    {
        Debug.Log("Exiting Challenge phase.");
        base.Exit();
    }

    void PopulateChallengeDetails()
    {
        switch (npcHand.lastPlayedCard.cardType)
        {
            case CardType.Add:
                humanChallengerHandler.challengeDetails.text = $"{npcData.enemyName} played an <color=red>Add {npcHand.lastPlayedCard.value}</color> card and guessed the sum equals to <color=red>{npcHand.guess}</color>." +
                    $"\nWould you like to challenge {npcData.enemyName}'s guess?";
                break;

            case CardType.Subtract:
                humanChallengerHandler.challengeDetails.text = $"{npcData.enemyName} played a <color=blue>Subtract {npcHand.lastPlayedCard.value}</color> card and guessed the sum equals to <color=blue>{npcHand.guess}</color>." +
                    $"\nWould you like to challenge {npcData.enemyName}'s guess?";
                break;

            case CardType.Wild:
                humanChallengerHandler.challengeDetails.text = $"{npcData.enemyName} played a <color=green>Wild {npcHand.lastPlayedCard.value}</color> card and guessed the sum equals to <color=green>{npcHand.guess}</color>." +
                    $"\nWould you like to challenge {npcData.enemyName}'s guess?";
                break;
        }
    }
}