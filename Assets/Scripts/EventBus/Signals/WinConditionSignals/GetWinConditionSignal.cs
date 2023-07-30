namespace CustomEventBus.Signals
{
    public class GetWinConditionSignal
    {
        public WinCondition condition;
        public GetWinConditionSignal(WinCondition Condition)
        {
            condition = Condition;
        }
    }
}
