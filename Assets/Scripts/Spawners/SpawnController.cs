using UnityEngine;
using CustomEventBus;
using CustomEventBus.Signals;
public class SpawnController : MonoBehaviour, IService{
    private EventBus _eventBus;
    [SerializeField] private SpawnPoint spawnPoint;
    [SerializeField] private SpawnSnake spawnSnake;

    void Start(){
        _eventBus = ServiceLocator.Current.Get<EventBus>();
        _eventBus.Subscribe<TouchPointSignal>(spawnPoint.SpawnNewPoint);
    }
} 