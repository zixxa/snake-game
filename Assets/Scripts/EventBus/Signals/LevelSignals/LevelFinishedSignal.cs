using Levels;
namespace CustomEventBus.Signals{
public class LevelFinishedSignal{
    public Level level;

    public LevelFinishedSignal(Level _level)
    {
        level = _level;
    }
}
}