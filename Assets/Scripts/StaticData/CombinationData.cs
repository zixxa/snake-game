using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CombinationObject", menuName = "CombinationObject", order = 0)]
public class CombinationData: ScriptableObject
{
    public string name;
    public List<BodyPrefabData> elements;
    public BodyPrefabData resultBody;
}