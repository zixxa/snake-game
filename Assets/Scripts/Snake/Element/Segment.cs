using UnityEngine;
using CustomEventBus;
using CustomEventBus.Signals;
public class Segment : MonoBehaviour, IService
{
    private EventBus _eventBus;
    public string code;
    public Transform transform;
    void Awake(){
        _eventBus = ServiceLocator.Current.Get<EventBus>();
        gameObject.AddComponent<Rigidbody>();
        GetComponent<Rigidbody>().interpolation = RigidbodyInterpolation.Interpolate;     
        GetComponent<Rigidbody>().collisionDetectionMode =  CollisionDetectionMode.Continuous;
    }
    void OnTriggerEnter(Collider trigger)
    {
        Point point = trigger.transform.parent.gameObject.GetComponent<Point>();
        _eventBus.Invoke(new ReleasePointSignal(point));
        _eventBus.Invoke(new TouchPointSignal(point));
    }
    public void Delete(GameClearSignal signal)
    {
        Destroy(gameObject);
    }
}