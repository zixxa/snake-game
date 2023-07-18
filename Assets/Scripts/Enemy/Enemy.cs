using UnityEngine;
using UnityEngine.AI;
using CustomEventBus;
using CustomEventBus.Signals;

public class Enemy : MonoBehaviour, IService{

    private Head target;
    private NavMeshAgent agent;
    private EventBus _eventBus;
    void Awake()
    {
        _eventBus = ServiceLocator.Current.Get<EventBus>();    
        agent = gameObject.AddComponent<NavMeshAgent>();
        _eventBus.Subscribe<GetHeadForEnemiesSignal>(OnGetHeadForEnemies);
    }
    void Update()
    {
        agent.SetDestination(target.transform.position);
    }
    private void OnCollisionEnter(Collision collision)
    {
        _eventBus.Invoke(new ReleaseEnemySignal(gameObject.GetComponent<Enemy>()));
    }
    private void OnGetHeadForEnemies(GetHeadForEnemiesSignal signal)
    {
        target = signal.head;
    }
}