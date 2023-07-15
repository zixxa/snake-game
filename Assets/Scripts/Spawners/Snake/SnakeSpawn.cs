using UnityEngine;
using CustomEventBus;
using CustomEventBus.Signals;

public class SnakeSpawn : MonoBehaviour, IService, ISnakeSpawn{
    private EventBus _eventBus;
    private SnakeSpawner _snakeSpawner;
    public Transform transform => gameObject.transform;
    void Awake()
    {
        _eventBus = ServiceLocator.Current.Get<EventBus>();
        _snakeSpawner = ServiceLocator.Current.Get<SnakeSpawner>();
        _eventBus.Subscribe<RegisterSnakeSpawn>(OnInit);
        _eventBus.Subscribe<GameClearSignal>(Delete);
    }
    void OnInit(RegisterSnakeSpawn signal)
    {
        _snakeSpawner.Register(this);
    }
    void Delete(GameClearSignal signal)
    {
        _eventBus.Unsubscribe<RegisterSnakeSpawn>(OnInit);
    }
}