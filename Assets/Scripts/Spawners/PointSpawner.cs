using UnityEngine;
using CustomEventBus;
using CustomEventBus.Signals;
using Random = System.Random;
using ObjectPool;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
public class PointSpawner: MonoBehaviour 
{
    private readonly int spawnPointNum;
    private Pool<Point> pool;
    private Random _rand;
    private EventBus _eventBus;
    [SerializeField] private int _spawnTime;
    [SerializeField] private List<PointObject> _pointsScriptableObjects;
    private Queue<Vector3> _freePositions = new Queue<Vector3>();
    public void Init()
    {
        _eventBus = ServiceLocator.Current.Get<EventBus>();
        _eventBus.Subscribe<ReleasePointSignal>(OnReleasePoint);
        _eventBus.Subscribe<GetScriptableObjectPointsSignal>(GetScriptableObjectsPoints);
        _eventBus.Subscribe<GameStartSignal>(OnGameStart);
    }
    private void OnGameStart(GameStartSignal signal)
    {
        _rand = new Random();
        pool = new Pool<Point>(_pointsScriptableObjects.Select(x=>x.prefab).ToList(), 3);

        for (int i=0;i<transform.childCount;i++){
            var point = pool.Get();      
            point.transform.position = transform.GetChild(i).transform.position;
        }
    }
    private void GetScriptableObjectsPoints(GetScriptableObjectPointsSignal signal)
    {
        _eventBus.Invoke(new FillPointsSignal(_pointsScriptableObjects));
    }
    private void OnReleasePoint(ReleasePointSignal signal)
    {
        _freePositions.Enqueue(signal.point.transform.position);
        pool.Release(signal.point);
        StartCoroutine(GetPoint());
    }
    IEnumerator GetPoint(){
        yield return new WaitForSeconds(_spawnTime);
        Point point = pool.Get();
        point.transform.position = _freePositions.Dequeue();
    }
}