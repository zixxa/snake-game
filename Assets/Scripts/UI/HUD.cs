using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using CustomEventBus;
using CustomEventBus.Signals;
public class HUD: MonoBehaviour, IService
{
    private EventBus _eventBus;
    [SerializeField] private Text _score;
    [SerializeField] private Text _time;
    [SerializeField] private int timeInSec;
    void Start()
    {
        _eventBus = ServiceLocator.Current.Get<EventBus>();
        _eventBus.Subscribe<PostBodyCountSignal>(OnRedrawScore);
        StartCoroutine(Timer(timeInSec));
    }
    private void OnRedrawScore(PostBodyCountSignal signal)
    {
        _score.text = $"Score {signal.bodyCount}";
    }
    IEnumerator Timer(int timeInSec)
    {
        for (int i = timeInSec; i>=0; i--)
        {
            string seconds = i<10 ? "0"+Convert.ToString(i) : Convert.ToString(i%60);
            string minutes = i>60 ? Convert.ToString(i/60) :"00";

            _time.text = $"Time {minutes}:{seconds}";
            yield return new WaitForSeconds(1);
        }
        _eventBus.Invoke(new GameOverSignal());
    }
}
