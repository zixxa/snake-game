using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BodyObject", menuName = "BodyObject", order = 0)]
public class BodyObject: ScriptableObject
{
    public string name;
    public Segment prefab;

}