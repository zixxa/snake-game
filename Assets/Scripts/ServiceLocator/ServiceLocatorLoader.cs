using CustomEventBus;
using UnityEngine;

public class ServiceLocatorLoader: MonoBehaviour
{
    private void Awake()
    {
        ServiceLocator.Initialize();
        var eventBus = new EventBus();
        ServiceLocator.Current.Register(eventBus);
    }
}