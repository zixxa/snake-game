using UnityEngine;

[CreateAssetMenu(fileName = "PointObject", menuName = "PointObject", order = 0)]
public class PointPrefabData: ScriptableObject
{
    public string name;
    public int count;
    public Point prefab;
    public ColorData color;
}