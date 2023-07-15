
using CustomEventBus.Signals;

public class EnemySpawn : MonoBehaviour, IService, IPointSpawn{
    private EventBus _eventBus;
    private EnemySpawner _pointSpawner;
    public Transform transform => gameObject.transform;
    void Awake()
    {
        _eventBus = ServiceLocator.Current.Get<EventBus>();
        _enemySpawner = ServiceLocator.Current.Get<EnemySpawner>();
        _eventBus.Subscribe<RegisterPointSpawns>(OnInit);
        _eventBus.Subscribe<GameClearSignal>(Delete);
    }
    void OnInit(RegisterPointSpawns signal)
    {
        _enemySpawner.Register(this);
    }
    void Delete(GameClearSignal signal)
    {
        _eventBus.Unsubscribe<RegisterPointSpawns>(OnInit);
    }
}