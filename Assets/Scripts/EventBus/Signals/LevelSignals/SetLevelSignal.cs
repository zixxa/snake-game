namespace CustomEventBus.Signals{
using Levels;
public class SetLevelSignal{
    public Level level;

    public SetLevelSignal(Level _level)
    {
        level = _level;
    }
}
}