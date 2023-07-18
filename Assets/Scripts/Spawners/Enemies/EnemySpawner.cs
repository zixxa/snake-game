using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using CustomEventBus;
using CustomEventBus.Signals;
using ObjectPool;

public class EnemySpawner : MonoBehaviour, IService {
    private EventBus _eventBus;
    private int enemiesCount = 0;
    private List<IEnemySpawn> _enemySpawns;
    private Pool<Enemy> pool;
    [SerializeField] private int maxNumOfEnemies;
    [SerializeField] private int _spawnTime;
    [SerializeField] private List<EnemyObject> _enemies;
    public void Init()
    {
        _eventBus = ServiceLocator.Current.Get<EventBus>();
        _eventBus.Subscribe<ReleaseEnemySignal>(OnReleaseEnemy);
        _eventBus.Subscribe<GameStartSignal>(OnGameStart);
    }
    void OnGameStart(GameStartSignal signal)
    {
        _enemySpawns = new List<IEnemySpawn>();
        _eventBus.Invoke(new RegisterEnemySpawnSignal());
        pool = new Pool<Enemy>(_enemies.ToDictionary(x=>x.prefab, x=>x.count));
        StartCoroutine(GetEnemy());
    }
    void Update()
    {
        while (enemiesCount < maxNumOfEnemies)
        {
            StartCoroutine(GetEnemy());
        }
    }
    public void Register(IEnemySpawn spawn)
    {
        _enemySpawns.Add(spawn);

    }
    private void OnReleaseEnemy(ReleaseEnemySignal signal)
    {
        pool.Release(signal.enemy);
        enemiesCount--;
    }
    IEnumerator GetEnemy()
    {
        yield return new WaitForSeconds(_spawnTime);
        Enemy enemy = pool.Get();
        enemiesCount++;
    }
}