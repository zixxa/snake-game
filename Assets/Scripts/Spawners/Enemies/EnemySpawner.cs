using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using CustomEventBus;
using CustomEventBus.Signals;
using ObjectPool;
using Random = System.Random;

public class EnemySpawner : MonoBehaviour, IService, IPauseHandler {
    private IEnumerator getEnemy;
    private EventBus _eventBus;
    private Random rand;
    private List<IEnemySpawn> _enemySpawns;
    private Pool<Enemy> pool;
    [SerializeField] private int maxNumOfEnemies;
    [SerializeField] private int _spawnTime;
    private List<EnemyData> _enemies;
    private bool isPaused;

    public void Init()
    {
        getEnemy = GetEnemy();
        _eventBus = ServiceLocator.Current.Get<EventBus>();
        _eventBus.Subscribe<ReleaseEnemySignal>(OnReleaseEnemy);
        _eventBus.Subscribe<GameStartSignal>(OnGameStart);
        _eventBus.Subscribe<GameClearSignal>(OnDelete);
        _eventBus.Subscribe<GetLevelData>(GetPool);
    }

    public void OnGameStart(GameStartSignal signal)
    {
        rand = new Random();
        _enemySpawns = new List<IEnemySpawn>();
        _eventBus.Invoke(new RegisterEnemySpawnSignal());
        _eventBus.Invoke(new RegisterEnemySpawnSignal());
        StartCoroutine(getEnemy);
    }

    public void GetPool(GetLevelData signal)
    {
        _enemies = signal.LevelData.enemies;
        pool = new Pool<Enemy>(_enemies.ToDictionary(x=>x.prefab, x=>x.count));
    }

    public void Register(IEnemySpawn spawn) => _enemySpawns.Add(spawn);

    private void OnReleaseEnemy(ReleaseEnemySignal signal)
    {
        _eventBus.Invoke(new DeadEnemySignal());
        pool.Release(signal.enemy);
    }

    private IEnumerator GetEnemy()
    {
        for(int i=0; i<_enemies.Select(x=>x.count).Sum(); i++)
        {
            yield return new WaitForSeconds(_spawnTime);
            Enemy enemy = pool.Get();
            var position = _enemySpawns.ElementAt(rand.Next(0,_enemySpawns.Count()-1)).transform.position;
            enemy.transform.position = position;       
        }
    }
    void OnDelete(GameClearSignal signal)
    {
        StopCoroutine(getEnemy);
        _eventBus.Unsubscribe<ReleaseEnemySignal>(OnReleaseEnemy);
    }
    
    void IPauseHandler.SetPaused(bool IsPaused) => isPaused = IsPaused;

    private void Update()
    {
        if (isPaused)
            return;
    }
}