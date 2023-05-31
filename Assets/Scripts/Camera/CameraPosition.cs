using UnityEngine;
using CustomEventBus;
using CustomEventBus.Signals;


public class CameraPosition: MonoBehaviour
{
    public Transform cameraPosition;
    private Transform focusPosition;
    private void Update()
    {
        if (focusPosition != null)
            transform.position = focusPosition.position + new Vector3(5,10,-20);
    }
    public void OnAttachCamera(AttachCameraSignal signal){
        focusPosition = signal.focusPosition.transform;
    }
}
