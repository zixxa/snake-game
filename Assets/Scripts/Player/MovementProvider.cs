using UnityEngine;
using CustomEventBus;
using CustomEventBus.Signals;

public class MovementProvider : MonoBehaviour, IService 
{
    private EventBus _eventBus;
    [SerializeField] MovementData _movementData;
    void Start()
    {
        _eventBus = ServiceLocator.Current.Get<EventBus>();
        _eventBus.Subscribe<PostMovementDataSignal>(OnGetMovementDataSignal);    
    }
    void OnGetMovementDataSignal(PostMovementDataSignal signal)
    {
        _eventBus.Invoke(new GetMovementDataSignal(_movementData));    
    }
}