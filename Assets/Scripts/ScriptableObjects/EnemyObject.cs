using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyObject", menuName = "EnemyObject", order = 0)]
public class EnemyObject: ScriptableObject
{
    public string name;
    public int count;
    public Enemy prefab;
    public ColorObject color;
}