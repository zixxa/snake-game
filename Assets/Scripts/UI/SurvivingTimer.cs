using UnityEngine;
using System.Collections;
using CustomEventBus;
using CustomEventBus.Signals;

public class SurvivingTimer: MonoBehaviour, IService
{
    private int _countdown;
    private string _time;
    private EventBus _eventBus;
    private bool isStopTimer;
    
    public void Init()
    {
        _eventBus = ServiceLocator.Current.Get<EventBus>();
        _eventBus.Subscribe<GetLevelData>(SetTimer);
        _eventBus.Subscribe<StartTimerSignal>(Run);
    }

    private void Run(StartTimerSignal signal)
    {
        StopAllCoroutines();
        StartCoroutine(Timer(_countdown));
    }

    private void SetTimer(GetLevelData signal)
    {
        _countdown = signal.LevelData.countdown;
    }
    
    private IEnumerator Timer (int timeInSec)
    {
        for (int i = timeInSec; i>=0; i--){
            yield return new WaitForSeconds(1);
            _time = string.Format("{0:00}:{1:00}", i/60, i%60); 
            _eventBus.Invoke(new GetTimeSignal(_time));
        }
        _eventBus.Invoke(new LevelPassedSignal());
    }
}