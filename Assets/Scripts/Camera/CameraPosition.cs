using UnityEngine;
using CustomEventBus;
using CustomEventBus.Signals;


public class CameraPosition: MonoBehaviour
{
    private Transform _focusPosition;
    private void Update(){
        if (_focusPosition != null)
            transform.position = _focusPosition.position + new Vector3(0,30,-30);
    }
    public void OnAttachCamera(AttachCameraSignal signal){
        _focusPosition = signal.focusPosition;
    }
}
