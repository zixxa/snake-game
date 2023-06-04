using System.Collections.Generic;
using UnityEngine;
public class GameField : MonoBehaviour{
    public int length;
    public int width;
    void Awake(){
        transform.localScale = new Vector3(width, 1, length);
    }
}
