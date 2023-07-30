using UnityEngine;

[CreateAssetMenu(fileName = "BodyObject", menuName = "BodyObject", order = 0)]
public class BodyPrefabData: ScriptableObject
{
    public string name;
    public Body prefab;
    public ColorData color;
}