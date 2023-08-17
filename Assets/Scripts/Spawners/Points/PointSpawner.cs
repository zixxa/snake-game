using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using CustomEventBus;
using CustomEventBus.Signals;
using ObjectPool;
public class PointSpawner: MonoBehaviour, IService, IPauseHandler
{
    private readonly int spawnPointNum;
    private Pool<Point> pool;
    private List<IPointSpawn> _pointSpawns;
    private EventBus _eventBus;
    [SerializeField] private int _spawnTime;
    [SerializeField] private List<PointPrefabData> _points;
    private Queue<Vector3> _freePositions;
    private bool isPaused;

    public void Init()
    {
        _eventBus = ServiceLocator.Current.Get<EventBus>();
        _eventBus.Subscribe<ReleasePointSignal>(OnReleasePoint);
        _eventBus.Subscribe<GetScriptableObjectPointsSignal>(GetScriptableObjectsPoints);
        _eventBus.Subscribe<GameStartSignal>(OnGameStart);
    }

    private void OnGameStart(GameStartSignal signal)
    {
        _pointSpawns = new List<IPointSpawn>();
        _eventBus.Invoke(new RegisterPointSpawnSignal());
        pool = new Pool<Point>(_points.ToDictionary(x=> x.prefab, x=>x.count));
        _freePositions = new Queue<Vector3>();

        foreach (var pointSpawn in _pointSpawns){
            if (pointSpawn!=null)
            {
                var point = pool.Get();      
                point.transform.position = pointSpawn.transform.position;
            }
        }
    }
    public void Register(IPointSpawn spawn) => _pointSpawns.Add(spawn);

    private void GetScriptableObjectsPoints(GetScriptableObjectPointsSignal signal) => _eventBus.Invoke(new FillPointsSignal(_points));

    private void OnReleasePoint(ReleasePointSignal signal)
    {
        _freePositions.Enqueue(signal.point.transform.position);
        pool.Release(signal.point);
        StartCoroutine(GetPoint());
    }

    private IEnumerator GetPoint()
    {
        yield return new WaitForSeconds(_spawnTime);
        if (_freePositions.Count()>0)
        {
            Point point = pool.Get();
            point.transform.position = _freePositions.Dequeue();
        }
    }

    private void OnDelete(GameClearSignal signal) {
        StopAllCoroutines();
        _eventBus.Unsubscribe<ReleasePointSignal>(OnReleasePoint);
        _eventBus.Unsubscribe<GetScriptableObjectPointsSignal>(GetScriptableObjectsPoints);
    }

    private void OnDestroy() {
        _eventBus.Unsubscribe<GameStartSignal>(OnGameStart);
        _eventBus.Unsubscribe<GameClearSignal>(OnDelete);
    }

    void IPauseHandler.SetPaused(bool IsPaused) => isPaused = IsPaused;

    private void Update()
    {
        if (isPaused)
            return;
    }
}