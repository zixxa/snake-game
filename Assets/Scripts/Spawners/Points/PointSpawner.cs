using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using CustomEventBus;
using CustomEventBus.Signals;
using ObjectPool;
public class PointSpawner: MonoBehaviour, IService
{
    private readonly int spawnPointNum;
    private Pool<Point> pool;
    private List<IPointSpawn> _pointSpawns;
    private EventBus _eventBus;
    [SerializeField] private int _spawnTime;
    [SerializeField] private List<PointObject> _points;
    private Queue<Vector3> _freePositions;
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
                var point = pool.RandomGet();      
                point.transform.position = pointSpawn.transform.position;
            }
        }
    }
    public void Register(IPointSpawn spawn)
    {
        _pointSpawns.Add(spawn);
    }
    private void GetScriptableObjectsPoints(GetScriptableObjectPointsSignal signal)
    {
        _eventBus.Invoke(new FillPointsSignal(_points));
    }
    private void OnReleasePoint(ReleasePointSignal signal)
    {
        _freePositions.Enqueue(signal.point.transform.position);
        pool.Release(signal.point);
        StartCoroutine(GetPoint());
    }
    IEnumerator GetPoint()
    {
        yield return new WaitForSeconds(_spawnTime);
        Point point = pool.Get();
        if (_freePositions!=null)
        {
            point.transform.position = _freePositions.Dequeue();
        }
    }
    private void OnDestroy() {
        _eventBus.Unsubscribe<ReleasePointSignal>(OnReleasePoint);
        _eventBus.Unsubscribe<GetScriptableObjectPointsSignal>(GetScriptableObjectsPoints);
        _eventBus.Unsubscribe<GameStartSignal>(OnGameStart);
    }
}