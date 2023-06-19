using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CombinationObject", menuName = "CombinationObject", order = 0)]
public class CombinationObject: ScriptableObject
{
    public string name;
    public List<BodyObject> combination;
}