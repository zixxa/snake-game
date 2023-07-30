using UnityEngine;

[CreateAssetMenu(fileName = "EnemyObject", menuName = "EnemyObject", order = 0)]
public class EnemyData: ScriptableObject
{
    public string name;
    public int count;
    public Enemy prefab;
    public ColorData color;
}