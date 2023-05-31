using System;
using UnityEngine;

public class SpawnSnake: MonoBehaviour
{
    private Snake _snake;
    [SerializeField] private Segment _headPrefab;
    [SerializeField] private Segment _bodyPrefab;
    [SerializeField] private Segment GetHeadPrefab() => Instantiate(_headPrefab, new Vector3(0,0,0), Quaternion.identity);
    void Start(){
        GameObject snakeObj = new GameObject("Snake");
        snakeObj.AddComponent<SnakeController>();
        var camera = snakeObj.AddComponent<CameraPosition>();
        _snake = snakeObj.AddComponent<Snake>();
        _snake.head = GetHeadPrefab();
        _snake.bodyPrefab = _bodyPrefab;
        _snake.head.transform.SetParent(snakeObj.transform);
    }
    void Update(){
        GetComponent<Camera>().cameraPosition = transform.position;
    }
}