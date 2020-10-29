using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy Data", menuName = "Enemy/Battle Data")]
public class EnemyBattleData : ScriptableObject
{
    public string enemyName;
    
    [Range(0, 200)] public int health;
    public Sprite enemyCard;

    [Tooltip("How the enemy performs in battles.")]
    public EnemyBattleStyles battleStyle;
    
    [Tooltip("Beginner: Deck consists of only 45 Addition type cards." +
            "\nIntermediate: Full deck of 152 cards, including Addition & Wild cards." +
            "\nExpert: Currently same as Intermediate.")]
    public EnemyDifficulty difficulty;
}

public enum EnemyBattleStyles { Normal }
public enum EnemyDifficulty { Beginner, Intermediate, Expert }