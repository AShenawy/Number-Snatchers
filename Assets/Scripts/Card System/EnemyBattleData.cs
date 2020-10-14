using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy Data", menuName = "Enemy/Battle Data")]
public class EnemyBattleData : ScriptableObject
{
    public string enemyName;
    [Range(0, 200)] public int health;
    public Sprite enemyCard;
    public EnemyBattleStyles battleStyle;
    public EnemyDifficulty difficulty;
}

public enum EnemyBattleStyles { Normal}
public enum EnemyDifficulty { Beginner, Intermediate, Expert }