namespace CustomEventBus.Signals{
public class PostBodyRigidbodyDataSignal{
    public readonly int bodyMass;
    public readonly float bodyDrag;
    public PostBodyRigidbodyDataSignal(int mass, float drag){
        bodyMass = mass;
        bodyDrag = drag;
    }
}
}