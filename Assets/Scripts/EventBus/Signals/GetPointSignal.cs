using UnityEngine;
namespace CustomEventBus.Signals{

public class GetPointSignal{
    public readonly Transform transform;
    public GetPointSignal(Transform Transform)
    {
        transform = Transform;
    }
}
}