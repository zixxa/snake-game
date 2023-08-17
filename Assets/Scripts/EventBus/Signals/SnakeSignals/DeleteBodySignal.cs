namespace CustomEventBus.Signals{

public class DeleteBodySignal{
    public readonly Body body;
    public DeleteBodySignal(Body Body)
    {
        body = Body;
    }
}
}
