using System;
using UnityEngine;
using CustomEventBus;
using CustomEventBus.Signals;
using System.Collections.Generic;
using System.Linq;

public class SnakeSpawner: MonoBehaviour, IService, IPauseHandler
{
    private EventBus _eventBus;
    private Snake _snake;
    private GameObject snakeObj;
    private Head _headPrefab;
    [SerializeField] private List<BodyPrefabData> _bodyScriptableObjects;
    private ISnakeSpawn _snakeSpawn;
    private int _snakeHeadId;
    private bool isPaused;
    [SerializeField] private HeadsListData _headsListData;
    
    public void Init()
    {
        _eventBus = ServiceLocator.Current.Get<EventBus>();
        _eventBus.Subscribe<GameStartSignal>(OnGameStart);
        _eventBus.Subscribe<GetScriptableObjectBodiesSignal>(GetScriptableObjectsBodies);
        _snakeHeadId = PlayerPrefs.GetInt(ConstantValues.SELECTED_SNAKE_HEAD, 0);
    }
    
    private Head GetHeadPrefab() => Instantiate(_headsListData.heads[_snakeHeadId], new Vector3(1,0,0), Quaternion.identity);
    
    private void OnGameStart(GameStartSignal signal)
    {
        GameObject snakeObj = new GameObject("Snake");
        _snake = snakeObj.AddComponent<Snake>();
        _snake.head = GetHeadPrefab();
        _snake.head.transform.SetParent(snakeObj.transform);
        _eventBus.Invoke(new AttachCameraSignal(_snake.head.transform));
        _eventBus.Invoke(new RegisterSnakeSpawnSignal());
    }
    
    public void RegisterSpawn(ISnakeSpawn spawn)
    {
        _snakeSpawn = spawn;
        SetTransform();
    }
    void SetTransform() => transform.position = _snakeSpawn.transform.position;
    private void OnDestroy() => _eventBus.Unsubscribe<GameStartSignal>(OnGameStart);
    private void GetScriptableObjectsBodies(GetScriptableObjectBodiesSignal signal) => _eventBus.Invoke(new FillBodiesSignal(_bodyScriptableObjects));
    void IPauseHandler.SetPaused(bool IsPaused) => isPaused = IsPaused;

    private void Update()
    {
        if (isPaused)
            return;
    }
}