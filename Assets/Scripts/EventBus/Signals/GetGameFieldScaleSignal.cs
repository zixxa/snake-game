namespace CustomEventBus.Signals{
public class GetGameFieldScaleSignal{
    public readonly int length;
    public readonly int width;
    public GetGameFieldScaleSignal(int Length, int Width){
        length = Length;
        width = Width;
    }
}
}