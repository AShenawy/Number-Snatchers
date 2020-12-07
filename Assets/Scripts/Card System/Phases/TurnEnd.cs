using UnityEngine;


// this is the last phase in each loop and determines whether a new loop begins or the battle ends
public class TurnEnd : Phase
{
    bool isChallengeWon;
    float timeEnter;    // time to countdown from
    float timeExit = 2f;    // time to exit the phase
    PlayTable table;
    Pot pot;
    InfoCard infoCard;


    public TurnEnd(BattleManager _bm, Stats _plStats, EnemyBattleData _npcData, PlayerHand _plrHnd, NPCHand _npcHnd, bool _isWon)
        : base(_bm, _plStats, _npcData, _plrHnd, _npcHnd)
    {
        name = Phases.EndTurn;
        isChallengeWon = _isWon;
        table = battleManager.table;
        pot = battleManager.pot;
        infoCard = System.Array.Find(battleManager.infoCardsPrefabs, c => c.cardType == InfoType.TurnEnd);
    }

    public override void Enter()
    {
        Debug.Log("Entering Turn End phase.");
        timeEnter = Time.timeSinceLevelLoad;
        EvaluateTurn();
        
        base.Enter();
    }

    public override void Update()
    {
        //if (Time.timeSinceLevelLoad - timeEnter >= timeExit)
        //    stage = Stages.Exit;
    }

    public override void Exit()
    {
        Debug.Log("Exiting Turn End phase. Phase time is " + (Time.timeSinceLevelLoad - timeEnter) + " seconds.");
        ClearTurnInfo();
        base.Exit();
    }

    void ClearTurnInfo()
    {
        if (battleManager.playerTurn == CurrentPlayer.Human)
            playerHand.ClearTurnInfo();
        else
            npcHand.ClearTurnInfo();
    }

    void EvaluateTurn()
    {
        // if a side lost the battle then end it
        //if ((currentHpNPC <= 0 || currentHpPlayer <= 0) && Time.timeSinceLevelLoad - timeEnter >= timeExit)
        // This is infocard case 1
        if (currentHpNPC <= 0 || currentHpPlayer <= 0)
        {
            Debug.Log("A side has reached 0 HP. Game is over");
            nextPhase = new EndBattle(battleManager, playerStats, npcData, playerHand, npcHand);
            battleManager.nPCDisplay.SetReaction(npcData.endQuotes[Random.Range(0, npcData.endQuotes.Length)], npcData.enemyAngry);
            DisplayInfoCard(1);
        }
        // if challenge is won, even if target isn't reached yet then start a new round
        // this is infocard case 2
        else if (isChallengeWon)
        {
            Debug.Log("Challenge is won by opponent. Moving cards to graveyard and starting a new round.");
            battleManager.SwitchTurn();
            // empty table and pot before new round
            table.ClearTable(CardCollections.Graveyard);
            pot.EmptyPot();
            nextPhase = new NewRound(battleManager, playerStats, npcData, playerHand, npcHand);
            DisplayInfoCard(2);
        }
        // if the target is crossed then start a new round and move cards to pot
        // this is infocard case 3
        else if (battleManager.currentNumber > battleManager.targetNumber)
        {
            Debug.Log("Target number is crossed. Moving cards to Pot and starting a new round.");
            battleManager.SwitchTurn();
            table.ClearTable(CardCollections.Pot);
            nextPhase = new NewRound(battleManager, playerStats, npcData, playerHand, npcHand);
            DisplayInfoCard(3);
        }
        // if the target is reached then start a new round and move cards to graveyard
        // this is infocard case 4
        else if (battleManager.currentNumber == battleManager.targetNumber)
        {
            Debug.Log("Target number is reached. Moving cards to Graveyard and starting a new round.");
            battleManager.SwitchTurn();
            // empty table and pot before new round
            table.ClearTable(CardCollections.Graveyard);
            pot.EmptyPot();
            nextPhase = new NewRound(battleManager, playerStats, npcData, playerHand, npcHand);
            DisplayInfoCard(4);
        }
        // if neither of the above then continue the same round and switch turns
        // this is infocard case 5
        else
        {
            Debug.Log(battleManager.playerTurn + " player's turn has ended. Switching sides.");
            battleManager.SwitchTurn();
            nextPhase = new CardDeal(battleManager, playerStats, npcData, playerHand, npcHand);
            DisplayInfoCard(5);
        }
    }

    void DisplayInfoCard(int cardType)
    {
        InfoCard card = Object.Instantiate(infoCard, battleManager.transform);

        switch (cardType)
        {
            case 1:     // game over
                card.titleText.text = "end of battle!".ToUpper();
                card.descriptionText.text = "";
                break;

            case 2:     // challenger won round
                card.titleText.text = "end of round!".ToUpper();

                if (battleManager.playerTurn == CurrentPlayer.NPC)
                    card.descriptionText.text = $"{npcData.enemyName} won by challenge".ToUpper();
                else
                    card.descriptionText.text = "you won by challenge".ToUpper();
                break;
            case 3:     // target crossed
                card.titleText.text = "end of round!".ToUpper();
                card.descriptionText.text = "current number skipped the target".ToUpper();
                break;

            case 4:     // target reached
                card.titleText.text = "end of round!".ToUpper();
                card.descriptionText.text = "target number reached".ToUpper();
                break;

            case 5:     // switch turn
                card.titleText.text = "end of turn!".ToUpper();
                if (battleManager.playerTurn == CurrentPlayer.NPC)
                    card.descriptionText.text = $"{npcData.enemyName} goes next".ToUpper();
                else
                    card.descriptionText.text = "player goes next".ToUpper();
                break;
        }

        card.onCardDestroyed += MoveToNextPhase;
    }

    void MoveToNextPhase()
    {
        stage = Stages.Exit;
    }
}
