using CustomEventBus;
using UnityEngine;
using UI;

public class ServiceLocatorLoaderMenu: MonoBehaviour
{
    [SerializeField] private GUIHolder _guiHolder;
    private EventBus _eventBus;
    private void Awake()
    {
        ServiceLocator.Initialize();
        ServiceLocator.Current.Register<GUIHolder>(_guiHolder);
        ServiceLocator.Current.Register(_eventBus);
    }
}