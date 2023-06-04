using UnityEngine;
using CustomEventBus;
using CustomEventBus.Signals;
public class Segment : MonoBehaviour, IService
{
    private EventBus _eventBus;
    public Rigidbody rigidbody;
    public Transform transform;
    void Awake(){
        rigidbody = gameObject.AddComponent<Rigidbody>();
        rigidbody.interpolation = RigidbodyInterpolation.Interpolate;     
        rigidbody.collisionDetectionMode =  CollisionDetectionMode.Continuous;
        _eventBus = ServiceLocator.Current.Get<EventBus>();
    }
    void OnTriggerEnter(Collider trigger){
        Destroy(trigger.transform.parent.gameObject);
        _eventBus.Invoke(new TouchPointSignal());
    }
}
