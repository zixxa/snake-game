using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UI;
using CustomEventBus;
using CustomEventBus.Signals;
public class HUD: MonoBehaviour, IService
{
    private EventBus _eventBus;
    [SerializeField] private Text _time;
    [SerializeField] private SurvivingTimer timer;
    private int _scoreCount = 0;

    public void Init()
    {
        _eventBus = ServiceLocator.Current.Get<EventBus>();
        _eventBus.Subscribe<GetTimeSignal>(OnGetTime);
        timer.Init();
    }
    private void OnGetTime(GetTimeSignal signal) => _time.text = signal.time;
}
