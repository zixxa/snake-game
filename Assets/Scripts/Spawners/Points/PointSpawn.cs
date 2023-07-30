using UnityEngine;
using CustomEventBus;
using CustomEventBus.Signals;

public class PointSpawn : MonoBehaviour, IService, IPointSpawn{
    private EventBus _eventBus;
    private PointSpawner _pointSpawner;
    public Transform transform => gameObject.transform;

    void Awake()
    {
        _eventBus = ServiceLocator.Current.Get<EventBus>();
        _pointSpawner = ServiceLocator.Current.Get<PointSpawner>();
        _eventBus.Subscribe<RegisterPointSpawnSignal>(OnInit);
        _eventBus.Subscribe<GameClearSignal>(OnUnsubscribe);
    }

    void OnInit(RegisterPointSpawnSignal signal) => _pointSpawner.Register(this);

    void OnUnsubscribe(GameClearSignal signal) => _eventBus.Unsubscribe<RegisterPointSpawnSignal>(OnInit);
}