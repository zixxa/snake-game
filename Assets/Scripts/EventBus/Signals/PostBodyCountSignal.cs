namespace CustomEventBus.Signals{
public class PostBodyCountSignal{
    public readonly int bodyCount;
    public PostBodyCountSignal(int BodyCount){
        bodyCount = BodyCount;
    }
}
}