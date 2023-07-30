using CustomEventBus;
using CustomEventBus.Signals;
using UnityEngine;
public class KilledEnemiesCounter: IService
{
    private int _countForWin;
    private int _count = 0;
    private EventBus _eventBus;
    public int Count => _count;
    public int CountForWin => _countForWin;
    public KilledEnemiesCounter(int countForWin)
    {
        _countForWin = countForWin;
        _eventBus = ServiceLocator.Current.Get<EventBus>();
        _eventBus.Subscribe<DeadEnemySignal>(IncreaseCounter);
    }
    private void IncreaseCounter(DeadEnemySignal signal) 
    {
        _count++;
        if (_count > _countForWin)
            _eventBus.Invoke(new CompleteWinConditionSignal());
    }
    public void OnDestroy()
    {
        _eventBus.Unsubscribe<DeadEnemySignal>(IncreaseCounter);
    }
}