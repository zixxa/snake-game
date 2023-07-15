using UnityEngine;
using CustomEventBus;
using CustomEventBus.Signals;
using System.Collections.Generic;
public class SnakeSpawner: MonoBehaviour, IService
{
    private EventBus _eventBus;
    private Snake _snake;
    private GameObject snakeObj;
    [SerializeField] private Head _headPrefab;
    [SerializeField] private Head GetHeadPrefab() => Instantiate(_headPrefab, new Vector3(1,0,0), Quaternion.identity);
    [SerializeField] private List<BodyObject> _bodyScriptableObjects;
    private ISnakeSpawn _snakeSpawn;
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
        _eventBus.Invoke(new RegisterSnakeSpawn());
    }
    public void Register(ISnakeSpawn spawn)
    {
        _snakeSpawn = spawn;
        SetTransform();
    }
    void SetTransform()
    {
        transform.position = _snakeSpawn.transform.position;
    }
    private void OnDispose()
    {
        _eventBus.Unsubscribe<GameStartSignal>(OnGameStart);
    }
}