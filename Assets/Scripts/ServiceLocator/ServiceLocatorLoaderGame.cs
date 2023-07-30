using CustomEventBus;
using UnityEngine;
using UI;
using Levels;
using System.Collections.Generic;
using IDisposable = CustomEventBus.IDisposable;

public class ServiceLocatorLoaderGame: MonoBehaviour
{
    [SerializeField] private PointSpawner _pointSpawner;
    [SerializeField] private SnakeSpawner _snakeSpawner;
    [SerializeField] private EnemySpawner _enemySpawner;
    [SerializeField] private CombinationsProvider _combinationProvider;
    [SerializeField] private SnakeDataProvider _snakeDataProvider;
    [SerializeField] private LevelHolder _levelHolder;
    [SerializeField] private GUIHolder _guiHolder;
    [SerializeField] private HUD _hud;
    private GameController _gameController;
    private PauseController _pauseController;
    private LevelDataManager _levelDataManager;
    private LevelController _levelController;
    private EventBus _eventBus;
    private List<IDisposable> _disposables = new List<IDisposable>();
    private void Awake()
    {
        ServiceLocator.Initialize();
        _eventBus = new EventBus();
        _pauseController = new PauseController();
        _gameController = new GameController();
        _levelController = new LevelController();
        _levelDataManager = new LevelDataManager();
        Register();
        Init();
        AddDisposables();
    }
    private void Register()
    {
        ServiceLocator.Current.Register(_eventBus);
        ServiceLocator.Current.Register<GUIHolder>(_guiHolder);
        ServiceLocator.Current.Register(_levelDataManager);
        ServiceLocator.Current.Register(_levelHolder);
        ServiceLocator.Current.Register(_pauseController);
        ServiceLocator.Current.Register<PointSpawner>(_pointSpawner);
        ServiceLocator.Current.Register<SnakeSpawner>(_snakeSpawner);
        ServiceLocator.Current.Register<EnemySpawner>(_enemySpawner);
    }
    private void Init()
    {
        _hud.Init();
        _combinationProvider.Init();
        _snakeDataProvider.Init();
        _snakeSpawner.Init();
        _pointSpawner.Init();
        _enemySpawner.Init();
        _gameController.Init();
        _levelDataManager.Init();
        _levelController.Init();
    }
    private void AddDisposables()
    {
        _disposables.Add(_gameController);
        _disposables.Add(_levelController);
    }

    private void OnDestroy()
    {
        foreach (var disposable in _disposables)
        {
            disposable.Dispose();
        }
    }
}