using UnityEngine;
using CustomEventBus;
using CustomEventBus.Signals;
public interface ISnakeSpawn
{
    public Transform transform {get;}
}

public class SnakeSpawn : MonoBehaviour, IService ,ISnakeSpawn{
    private EventBus _eventBus;
    private SnakeSpawner _snakeSpawner;
    public Transform transform => gameObject.transform;
    void Awake()
    {
        _eventBus = ServiceLocator.Current.Get<EventBus>();
        _snakeSpawner = ServiceLocator.Current.Get<SnakeSpawner>();
        _eventBus.Subscribe<RegisterSnakeSpawnSignal>(OnInit);
        _eventBus.Subscribe<GameClearSignal>(OnUnsubscribe);
    }
    void OnInit(RegisterSnakeSpawnSignal signal)
    {
        _snakeSpawner.RegisterSpawn(this);
    }
    void OnUnsubscribe(GameClearSignal signal)
    {
        _eventBus.Unsubscribe<RegisterSnakeSpawnSignal>(OnInit);
    }
}