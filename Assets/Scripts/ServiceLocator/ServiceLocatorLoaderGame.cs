using CustomEventBus;
using UnityEngine;
using UI;

public class ServiceLocatorLoaderGame: MonoBehaviour
{
    [SerializeField] private PointSpawner _pointSpawner;
    [SerializeField] private SnakeSpawner _snakeSpawner;
    [SerializeField] private Combinations _combinations;
    [SerializeField] private GUIHolder _guiHolder;
    private GameController _gameController;
    private EventBus _eventBus;
    private void Awake()
    {
        ServiceLocator.Initialize();
        _eventBus = new EventBus();
        _gameController = new GameController();
        Register();
        Init();
    }
    private void Register()
    {
        ServiceLocator.Current.Register(_eventBus);
        ServiceLocator.Current.Register<GUIHolder>(_guiHolder);
    }
    private void Init()
    {
        _pointSpawner.Init();
        _snakeSpawner.Init();
        _gameController.Init();
        _combinations.Init();
    }
}