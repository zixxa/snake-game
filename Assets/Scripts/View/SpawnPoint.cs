using System;
using UnityEngine;
using Random = System.Random;

public class SpawnPoint: MonoBehaviour
{
    private Point point;
    private GameField gameField;
    [SerializeField] private Point _pointPrefab;
    public Point GetPointPrefab() => Instantiate(_pointPrefab, new Vector3(0,0,0),Quaternion.identity);


    void Start(){
        gameField = new GameField();
        SpawnNewPoint();
    }

    private Vector3 GetPointPosition(){
        Random rand = new Random();
        int xPosition = rand.Next(-gameField.length/2,gameField.length/2);
        int zPosition = rand.Next(-gameField.width/2,gameField.width/2);
        return new Vector3(xPosition, point.transform.localScale.y/2, zPosition);
    }
    public void SpawnNewPoint(){
        Debug.Log("Spawn");
        point = GetPointPrefab();
        point.transform.position = GetPointPosition();
    }
}