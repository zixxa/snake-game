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
        _eventBus.Subscribe<RegisterPointSpawns>(OnInit);
        _eventBus.Subscribe<GameClearSignal>(Delete);
    }
    void OnInit(RegisterPointSpawns signal)
    {
        _pointSpawner.Register(this);
    }
    void Delete(GameClearSignal signal)
    {
        _eventBus.Unsubscribe<RegisterPointSpawns>(OnInit);
    }
}