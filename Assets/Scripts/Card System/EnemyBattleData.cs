using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy Data", menuName = "Enemy/Battle Data")]
public class EnemyBattleData : ScriptableObject
{
    public string enemyName;

    [Range(0, 200)] public int health;
    public Sprite enemyDefault, enemyIdle, enemyAngry;

    [Tooltip("How the enemy performs in battles.")]
    public EnemyBattleStyles battleStyle;

    [Tooltip("Beginner: Deck consists of only 45 Addition type cards." +
            "\nIntermediate: Full deck of 152 cards, including Addition & Wild cards." +
            "\nExpert: Currently same as Intermediate.")]
    public EnemyDifficulty difficulty;

    [Header("In-game Quotes")]
    [Header("Battle Start, New Round, Battle End", order = 1)]
    [TextArea(3, 5)] public string[] startQuotes;
    [TextArea(3, 5)] public string[] endQuotes;
    [TextArea(3, 5)] public string[] battleWinQuotes;
    [TextArea(3, 5)] public string[] battleLoseQuotes;

    [Header("Card Play")]
    [TextArea(3, 5)] public string[] cardPickQuotes;

    [Header("Challenging Player")]
    [TextArea(3, 5)] public string[] challengeQuotes;
    [TextArea(3, 5)] public string[] passQuotes;

    [Header("Challenged by Player")]
    [TextArea(3, 5)] public string[] beingChallengedQuotes;
    [TextArea(3, 5)] public string[] notChallengedQuotes;

    [Header("Challenge Outcomes")]
    [TextArea(3, 5)] public string[] winChallengeQuotes;
    [TextArea(3, 5)] public string[] loseChallengeQuotes;
}

public enum EnemyBattleStyles { Normal }
public enum EnemyDifficulty { Beginner, Intermediate, Expert }