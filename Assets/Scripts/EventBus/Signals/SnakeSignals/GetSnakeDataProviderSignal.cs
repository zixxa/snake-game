namespace CustomEventBus.Signals{
public class GetSnakeDataProviderSignal{

public readonly SnakeDataProvider snakeDataProvider;
public GetSnakeDataProviderSignal(SnakeDataProvider SnakeDataProvider){
    snakeDataProvider = SnakeDataProvider;
}
}
}