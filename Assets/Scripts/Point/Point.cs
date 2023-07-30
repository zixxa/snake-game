using UnityEngine;
using CustomEventBus;
using CustomEventBus.Signals;

public class Point: MonoBehaviour, IService
{
    private EventBus _eventBus;
    public ColorData color;
    private void Awake() {
        _eventBus = ServiceLocator.Current.Get<EventBus>();
        _eventBus.Subscribe<GameClearSignal>(OnDelete);
    }
    private void OnDelete(GameClearSignal signal)
    {
        Destroy(gameObject);
    }
    private void OnDestroy() {
        _eventBus.Unsubscribe<GameClearSignal>(OnDelete);
    }

}