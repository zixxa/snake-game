using CustomEventBus;
using UnityEngine;
using UI;

public class ServiceLocatorLoaderMenu: MonoBehaviour
{
    [SerializeField] private GUIHolder _guiHolder;
    private void Awake()
    {
        ServiceLocator.Initialize();
        ServiceLocator.Current.Register<GUIHolder>(_guiHolder);
    }
}