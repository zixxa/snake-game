using UnityEngine;
using CustomEventBus;
using CustomEventBus.Signals;

public class Point: MonoBehaviour, IService
{
    private EventBus _eventBus;
    public string code;
    private void Awake() {
        _eventBus = ServiceLocator.Current.Get<EventBus>();
        _eventBus.Subscribe<GameClearSignal>(Delete);
    }
    void Delete(GameClearSignal signal)
    {
        Destroy(gameObject);
    }
}