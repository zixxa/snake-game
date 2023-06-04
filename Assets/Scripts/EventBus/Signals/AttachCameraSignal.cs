using UnityEngine;
namespace CustomEventBus.Signals{
public class AttachCameraSignal{
    public readonly Transform focusPosition;
    public AttachCameraSignal(Transform FocusPosition){
        focusPosition = FocusPosition;
    }
}
}