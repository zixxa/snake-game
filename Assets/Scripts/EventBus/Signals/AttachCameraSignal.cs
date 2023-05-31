using UnityEngine;
namespace CustomEventBus.Signals{
public class AttachCameraSignal{
    public Transform focusPosition;
    public AttachCameraSignal(Transform FocusPosition){
        focusPosition = FocusPosition;
    }
}
}