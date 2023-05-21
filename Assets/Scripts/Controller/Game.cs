using System.Transactions;
using UnityEngine;
public class Game : MonoBehaviour
{
    //Transform transform;
    private GameField gameField;
    void Awake() {
        gameField = new GameField();
        transform.localScale = new Vector3(gameField.width, 1, gameField.length);
    }
    void Update(){
        
    }
    void SpawnPoint(){
        
    }
}
