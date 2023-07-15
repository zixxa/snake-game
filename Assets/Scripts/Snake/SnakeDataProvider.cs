using System;
using System.Collections.Generic;
using UnityEngine;
using CustomEventBus;
using CustomEventBus.Signals;
using UnityEngine;

public class SnakeDataProvider: MonoBehaviour, IService
{
    private EventBus _eventBus; 
    public HeadData head;
    public BodyData body;
    public int bodyCount{get;private set;} = 0;
    public void Init()
    {
        _eventBus = ServiceLocator.Current.Get<EventBus>();
        _eventBus.Subscribe<PostSnakeDataProviderSignal>(OnGetSnakeDataProvider);
        _eventBus.Subscribe<TouchPointSignal>(OnIncreaseLength);
    }
    public void OnIncreaseLength(TouchPointSignal signal)
    {
        bodyCount++;
        _eventBus.Invoke(new PostBodyCountSignal(bodyCount));
    }
    public void OnGetSnakeDataProvider(PostSnakeDataProviderSignal signal)
    {
        _eventBus.Invoke(new GetSnakeDataProviderSignal(this));
    }
}