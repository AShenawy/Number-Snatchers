using UnityEngine;
using UnityEngine.UI;
using System.Collections;

// this phase is the first to occur every battle. It should only be called once in the beginning to set up the battle stats
public class Start : Phase
{
    Image npcCard;
    Text npcNameDisplay;
    Text roundNumberDisplay;
    Text targetNumberDisplay;
    Text currentNumberDisplay;

    float counter;
    float exitTimer = 2f;   // how long to wait before exiting this phase

    public Start(BattleManager _battleManager, Stats _playerStats, EnemyBattleData _npcData,Image _playerHpDisplay, Image _npcHpDisplay) 
        : base(_battleManager, _playerStats, _npcData, _playerHpDisplay, _npcHpDisplay)
    {
        name = Phases.BattleStart;
        npcCard = battleManager.nPCCardDisplay;
        npcNameDisplay = battleManager.nPCNameDisplay;

        roundNumberDisplay = battleManager.roundNumberDisplay;
        targetNumberDisplay = battleManager.targetNumberDisplay;
        currentNumberDisplay = battleManager.currentNumberDisplay;
    }

    public override void Enter()
    {
        // set up player stats & display
        startingHpPlayer = playerStats.hP;
        currentHpPlayer = startingHpPlayer;
        playerHpDisplay.fillAmount = currentHpPlayer / startingHpPlayer;

        // set up opponent stats & display
        npcCard.sprite = npcData.enemyCard;
        npcNameDisplay.text = npcData.enemyName;
        startingHpNPC = npcData.health;
        currentHpNPC = startingHpNPC;
        npcHpDisplay.fillAmount = currentHpNPC / startingHpNPC;

        // initialise counters
        roundNumberDisplay.text = "";
        targetNumberDisplay.text = "";
        currentNumberDisplay.text = "";
        counter = Time.timeSinceLevelLoad;

        base.Enter();
    }

    public override void Update()
    {
        // nothing happens during this phase besides initialisation. Move on to next phase after time passes
        if (Time.timeSinceLevelLoad - counter >= exitTimer)
            DelayedExit();
    }

    public override void Exit()
    {
        base.Exit();
    }

    void DelayedExit()
    {
        Debug.Log("Exiting Start phase after " + (Time.timeSinceLevelLoad - counter) + " seconds.");
        nextPhase = new NewRound(battleManager, playerStats, npcData, playerHpDisplay, npcHpDisplay);
        stage = Stages.Exit;
    }
}
