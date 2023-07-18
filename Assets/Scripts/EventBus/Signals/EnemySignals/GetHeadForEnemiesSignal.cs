namespace CustomEventBus.Signals{
public class GetHeadForEnemiesSignal
{
    public readonly Head head;
    public GetHeadForEnemiesSignal(Head Head)
    {
        head = Head;
    }
}
}