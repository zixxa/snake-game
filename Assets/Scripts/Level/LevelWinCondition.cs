using UnityEngine;
using CustomEventBus;
using CustomEventBus.Signals;

public abstract class WinCondition
{
    private protected EventBus eventBus;
    public abstract void Init();
}
public class TimeWinCondition: WinCondition
{
    public override void Init()
    {
        eventBus = ServiceLocator.Current.Get<EventBus>();
        eventBus.Subscribe<GameStartSignal>(OnStartTimer);
        eventBus.Subscribe<GameClearSignal>(OnDelete, 1);
    }
    private void OnStartTimer(GameStartSignal signal)
    {
        eventBus.Invoke(new StartTimerSignal());
    }
    public void OnDelete(GameClearSignal signal)
    {
        eventBus.Unsubscribe<GameStartSignal>(OnStartTimer);
    }
}
public class KillEnemiesWinCondition: WinCondition{
    public override void Init()
    {
        eventBus = ServiceLocator.Current.Get<EventBus>();
        eventBus.Invoke(new StartKilledEnemiesCounterSignal());
    }
}