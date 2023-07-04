using UnityEngine;
using CustomEventBus;
using CustomEventBus.Signals;
using System.Collections.Generic;
public class SnakeSpawner: MonoBehaviour, IService
{
    private EventBus _eventBus;
    private Snake _snake;
    [SerializeField] private Segment _headPrefab;
    [SerializeField] private Segment GetHeadPrefab() => Instantiate(_headPrefab, new Vector3(1,0,0), Quaternion.identity);
    [SerializeField] private List<BodyObject> _bodyScriptableObjects;
    public void Init()
    {
        _eventBus = ServiceLocator.Current.Get<EventBus>();
        _eventBus.Subscribe<GameStartSignal>(OnGameStart);
    }
    private void OnGameStart(GameStartSignal signal){
        GameObject snakeObj = new GameObject("Snake");
        _snake = snakeObj.AddComponent<Snake>();
        _snake.head = GetHeadPrefab();
        _snake.head.transform.SetParent(snakeObj.transform);
        _eventBus.Invoke(new AttachCameraSignal(_snake.head.transform));
    }
}