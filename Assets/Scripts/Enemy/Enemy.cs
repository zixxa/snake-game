using UnityEngine;
using UnityEngine.AI;
using CustomEventBus;
using CustomEventBus.Signals;

public class Enemy : MonoBehaviour, IService{

    [SerializeField] private ColorData color;
    private Head target;
    private NavMeshAgent agent;
    private EventBus _eventBus;
    private void Awake()
    {
        _eventBus = ServiceLocator.Current.Get<EventBus>();
        agent = gameObject.AddComponent<NavMeshAgent>();
        agent.speed = 40;
        _eventBus.Subscribe<GetHeadForEnemiesSignal>(OnGetHeadForEnemies);
        _eventBus.Subscribe<GameClearSignal>(OnDelete);
    }
    private void Update() 
    {
        agent.SetDestination(target.transform.position);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Body>())
        {
            if (collision.gameObject.GetComponent<Body>().color == color)
                _eventBus.Invoke(new ReleaseEnemySignal(gameObject.GetComponent<Enemy>()));
        }
        else if (collision.gameObject.GetComponent<Head>())
        {
            _eventBus.Invoke(new GameOverSignal());
        }
    }
    private void OnGetHeadForEnemies(GetHeadForEnemiesSignal signal)
    {
        target = signal.head;
    }
    private void OnDelete(GameClearSignal signal)
    {
        Destroy(gameObject);
    }
    private void OnDestroy() {
        _eventBus.Unsubscribe<GetHeadForEnemiesSignal>(OnGetHeadForEnemies);
        _eventBus.Unsubscribe<GameClearSignal>(OnDelete);
    }
}