using CustomEventBus;
using CustomEventBus.Signals;
using UnityEngine;

public class CameraController : MonoBehaviour, IService{
    private EventBus _eventBus;
    private CameraPosition cameraPosition;
    void Start(){
        cameraPosition = GetComponent<CameraPosition>();
        _eventBus = ServiceLocator.Current.Get<EventBus>();
        _eventBus.Subscribe<AttachCameraSignal>(cameraPosition.OnAttachCamera);
    }
}