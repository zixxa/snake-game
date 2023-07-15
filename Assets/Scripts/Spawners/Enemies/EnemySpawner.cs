using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using CustomEventBus;
using CustomEventBus.Signals;
using ObjectPool;

public class EnemySpawner : MonoBehaviour {
    private EventBus _eventBus;
    public void Init()
    {
        _eventBus = ServiceLocator.Current.Get<EventBus>();
        _eventBus.Subscribe<ReleaseEnemySignal>(OnReleaseEnemy);
        _eventBus.Subscribe<GetScriptableObjectEnemiesSignal>(GetScriptableObjectsEnemies);
        _eventBus.Subscribe<GameStartSignal>(OnGameStart);
    }
    void OnStartGame()
    {
        _enemySpawns = new List<IPointSpawn>();
        _eventBus.Invoke(new RegisterEnemySpawns());
        pool = new Pool<Point>(_points.Select(x=>x.prefab).ToList(), 3);
        _freePositions = new Queue<Vector3>();

        foreach (var pointSpawn in _pointSpawns){
            if (pointSpawn!=null)
            {
                var point = pool.Get();      
                point.transform.position = pointSpawn.transform.position;
            }
        }
    }
    public void Register(IPointSpawn spawn)
    {
        _enemySpawns.Add(spawn);
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
        Enemy enemy = pool.Get();
    }
}