using UnityEngine;
using CustomEventBus;
using CustomEventBus.Signals;
public class Segment : MonoBehaviour, IService
{
    private EventBus _eventBus;
    public string code;
    Rigidbody rigidbody;
    public Transform transform;
    void Awake(){
        rigidbody = gameObject.AddComponent<Rigidbody>();
        rigidbody.interpolation = RigidbodyInterpolation.Interpolate;     
        rigidbody.collisionDetectionMode =  CollisionDetectionMode.Continuous;
        _eventBus = ServiceLocator.Current.Get<EventBus>();
    }
    void OnTriggerEnter(Collider trigger){
        Point point = trigger.transform.parent.gameObject.GetComponent<Point>();
        _eventBus.Invoke(new ReleasePointSignal(point));
        _eventBus.Invoke(new TouchPointSignal(point));
        _eventBus.Invoke(new GetPointSignal(trigger.transform.parent.gameObject.transform));
    }
}
