namespace CustomEventBus.Signals
{
    public class GetTimeSignal
    {
        public readonly string time;
        public GetTimeSignal(string Time)
        {
            time = Time;
        }
    }
}