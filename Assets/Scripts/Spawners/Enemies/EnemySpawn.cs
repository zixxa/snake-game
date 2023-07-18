using UnityEngine;
using ObjectPool;
using CustomEventBus;
using CustomEventBus.Signals;

public class EnemySpawn : MonoBehaviour, IService, IEnemySpawn{
    private EventBus _eventBus;
    private EnemySpawner _enemySpawner;
    public Transform transform => gameObject.transform;
    void Awake()
    {
        _eventBus = ServiceLocator.Current.Get<EventBus>();
        _enemySpawner = ServiceLocator.Current.Get<EnemySpawner>();
        _eventBus.Subscribe<RegisterEnemySpawnSignal>(OnInit);
        _eventBus.Subscribe<GameClearSignal>(Delete);
    }
    void OnInit(RegisterEnemySpawnSignal signal)
    {
        _enemySpawner.Register(this);
    }
    void Delete(GameClearSignal signal)
    {
        _eventBus.Unsubscribe<RegisterEnemySpawnSignal>(OnInit);
    }
}