namespace CustomEventBus.Signals{

public class ReleaseEnemySignal{
    public readonly Enemy enemy;
    public ReleaseEnemySignal(Enemy Enemy)
    {
        enemy = Enemy;
    }
}
}