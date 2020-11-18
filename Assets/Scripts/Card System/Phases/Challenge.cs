using UnityEngine;

// this phase is where the opponent of the current player gets to challenge their guess, if they choose to do so
public class Challenge : Phase
{
    ChallengeHandler humanChallengerHandler;
    bool isCurrentPlayerChallenged;
    bool isChallengeComplete;
    float timeEnter;    // time to countdown from
    float timeExit = 2f;    // time to exit the phase


    public Challenge(BattleManager _bm, Stats _plStats, EnemyBattleData _npcData, PlayerHand _plrHnd, NPCHand _npcHnd)
        : base(_bm, _plStats, _npcData, _plrHnd, _npcHnd)
    {
        name = Phases.Challenge;

        npcHand.onChallengeAccepted += ChallengeHuman;
        npcHand.onChallengePassed += PassChallengingHuman;
    }

    public override void Enter()
    {
        Debug.Log("Entering Challenge phase. The " + battleManager.playerTurn + " player can be challenged.");

        // if it's NPC turn then human gets to challenge and vice versa
        if (battleManager.playerTurn == CurrentPlayer.NPC)
        {
            humanChallengerHandler = Object.Instantiate(battleManager.humanChallengerCardPrefab, battleManager.transform);
            PopulateChallengeDetails();
            humanChallengerHandler.onChallengeSubmitted += StoreNPCChallenge;
            humanChallengerHandler.onChallengePassed += PassChallengingNPC;
        }
        else
            npcHand.ChallengePlayer();

        timeEnter = Time.timeSinceLevelLoad;
        base.Enter();
    }

    public override void Update()
    {
        if (isChallengeComplete && (Time.timeSinceLevelLoad - timeEnter >= timeExit))
        {
            nextPhase = new CheckGuess(battleManager, playerStats, npcData, playerHand, npcHand, isCurrentPlayerChallenged);
            stage = Stages.Exit;
        }
    }

    public override void Exit()
    {
        Debug.Log("Exiting Challenge phase. Phase time is " + (Time.timeSinceLevelLoad - timeEnter) + " seconds.");

        // unsub events
        if (humanChallengerHandler)
        {
            humanChallengerHandler.onChallengeSubmitted -= StoreNPCChallenge;
            humanChallengerHandler.onChallengePassed -= PassChallengingNPC;
        }
        npcHand.onChallengeAccepted -= ChallengeHuman;
        npcHand.onChallengePassed -= PassChallengingHuman;

        base.Exit();
    }

    void PopulateChallengeDetails()
    {
        switch (npcHand.lastPlayedCard.cardType)
        {
            case CardType.Add:
                humanChallengerHandler.challengeDetails.text = $"{npcData.enemyName} played an <color=red>Add {npcHand.lastPlayedCard.value}</color> card and guessed the sum equals to <color=red>{npcHand.guess}</color>." +
                    $"\nChallenge {npcData.enemyName}'s guess?";
                break;

            case CardType.Subtract:
                humanChallengerHandler.challengeDetails.text = $"{npcData.enemyName} played a <color=blue>Subtract {npcHand.lastPlayedCard.value}</color> card and guessed the sum equals to <color=blue>{npcHand.guess}</color>." +
                    $"\nChallenge {npcData.enemyName}'s guess?";
                break;

            case CardType.Wild:
                humanChallengerHandler.challengeDetails.text = $"{npcData.enemyName} played a <color=green>Wild {npcHand.lastPlayedCard.value}</color> card and guessed the sum equals to <color=green>{npcHand.guess}</color>." +
                    $"\nChallenge {npcData.enemyName}'s guess?";
                break;
        }
    }

    // if human player decides to challenge NPC player
    void StoreNPCChallenge(int challenge)
    {
        playerHand.guess = challenge;
        isCurrentPlayerChallenged = true;
        isChallengeComplete = true;
    }

    // if human player passes on challenging NPC player
    void PassChallengingNPC()
    {
        isCurrentPlayerChallenged = false;
        isChallengeComplete = true;
    }

    // if NPC player decides to challenge human player
    void ChallengeHuman()
    {
        isCurrentPlayerChallenged = true;
        isChallengeComplete = true;
    }

    // if NPC player passes on challenging human player
    void PassChallengingHuman()
    {
        isCurrentPlayerChallenged = false;
        isChallengeComplete = true;
    }
}