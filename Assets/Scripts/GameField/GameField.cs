using UnityEngine;
using CustomEventBus;
using CustomEventBus.Signals;
public class GameField : MonoBehaviour, IService
{
    [SerializeField] private int _length;
    [SerializeField] private int _width;
    private EventBus _eventBus;
    void Start() {

        _eventBus = ServiceLocator.Current.Get<EventBus>();
        transform.localScale = new Vector3(_width, 1, _length);
        _eventBus.Invoke(new GetGameFieldScaleSignal(_width, _length));
    }
}
