using System;
using UnityEngine;
using CustomEventBus;
using CustomEventBus.Signals;
public class SpawnSnake: MonoBehaviour, IService
{
    private EventBus _eventBus;
    private Snake _snake;
    private CameraPosition camera;
    [SerializeField] private Segment _headPrefab;
    [SerializeField] private Segment _bodyPrefab;
    [SerializeField] private Segment GetHeadPrefab() => Instantiate(_headPrefab, new Vector3(0,0,0), Quaternion.identity);
    void Start(){
        GameObject snakeObj = new GameObject("Snake");
        snakeObj.AddComponent<SnakeController>();
        _snake = snakeObj.AddComponent<Snake>();
        _snake.head = GetHeadPrefab();
        _eventBus = ServiceLocator.Current.Get<EventBus>();
        _eventBus.Invoke(new AttachCameraSignal(_snake.head.transform));
        _snake.bodyPrefab = _bodyPrefab;
        _snake.head.transform.SetParent(snakeObj.transform);
    }
}