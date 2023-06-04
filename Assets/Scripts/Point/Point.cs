using UnityEngine;
using CustomEventBus;
using CustomEventBus.Signals;
public class Point: MonoBehaviour, IService
{
    private EventBus _eventBus;
    public Transform transform;
    void Awake(){
        _eventBus = ServiceLocator.Current.Get<EventBus>();
    }
    void OnDestroy(){
        Debug.Log("1 Destroy");
    }
}