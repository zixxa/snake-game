using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PointObject", menuName = "PointObject", order = 0)]
public class PointObject: ScriptableObject
{
    public string name;
    public int count;
    public Point prefab;
    public ColorObject color;
}