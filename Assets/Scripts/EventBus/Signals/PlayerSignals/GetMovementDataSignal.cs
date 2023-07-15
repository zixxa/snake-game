namespace CustomEventBus.Signals{
public class GetMovementDataSignal{
    public readonly MovementData movement;
    public GetMovementDataSignal(MovementData Movement)
    {
        movement = Movement;
    }
}
}