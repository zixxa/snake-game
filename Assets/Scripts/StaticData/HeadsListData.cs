using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "HeadsListData", menuName = "HeadsListData", order = 0)]
public class HeadsListData: ScriptableObject
{
    public List<Head> heads;
}