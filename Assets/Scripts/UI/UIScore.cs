using System;
using UnityEngine;
using UnityEngine.UI;
using CustomEventBus;
using CustomEventBus.Signals;
public class UIScore : MonoBehaviour, IService
{
    EventBus _eventBus;
    [SerializeField] private Text _score;
    void Start(){
        _eventBus = ServiceLocator.Current.Get<EventBus>();
        _eventBus.Subscribe<PostBodyCountSignal>(OnUpdateScore);
        _score.text = $"Score 0";
    }

    private void OnUpdateScore(PostBodyCountSignal signal){
        _score.text = $"Score {signal.bodyCount}";
    }
}
