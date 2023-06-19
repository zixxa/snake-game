
namespace CustomEventBus.Signals{

public class ReleasePointSignal{
    public readonly Point point;
    public ReleasePointSignal(Point Point)
    {
        point = Point;
    }
}
}