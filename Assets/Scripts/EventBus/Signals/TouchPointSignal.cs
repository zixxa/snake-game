namespace CustomEventBus.Signals{
public class TouchPointSignal{
    public readonly Point point;
    public TouchPointSignal(Point Point){
        point = Point;
    }
}
}