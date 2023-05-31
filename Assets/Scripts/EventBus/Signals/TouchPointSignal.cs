namespace CustomEventBus.Signals{
public class TouchPointSignal{
private static TouchPointSignal instance;
private TouchPointSignal(){}
public static TouchPointSignal getInstance()
{
    if (instance == null)
        instance = new TouchPointSignal();
    return instance;
}
}
}