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
    private int spawnPointNum;
    private Pool<Point> pool;
    private Random _rand;
    private EventBus _eventBus;
    private List<Point> _points = new List<Point>();

    [SerializeField] private List<PointObject> _pointsScriptableObjects;
    void Start()
    {
        _eventBus = ServiceLocator.Current.Get<EventBus>();
        _eventBus.Subscribe<ReleasePointSignal>(OnReleasePoint);
        _eventBus.Subscribe<GetPointSignal>(OnGetPoint);
        _eventBus.Subscribe<GetScriptableObjectPointsSignal>(GetScriptableObjectsPoints);

        _rand = new Random();

        pool = new Pool<Point>(_pointsScriptableObjects.Select(x=>x.prefab).ToList(), 3);

        for (int i=0;i<transform.childCount;i++){
            var point = pool.Get();      
            point.transform.position = transform.GetChild(i).transform.position;
        }
    }
    void GetScriptableObjectsPoints(GetScriptableObjectPointsSignal signal)
    {
        _eventBus.Invoke(new FillPointsSignal(_pointsScriptableObjects));
    }
    void OnGetPoint(GetPointSignal signal)
    {
        StartCoroutine(WaitForGettingPoint(signal.transform));
    }
    IEnumerator WaitForGettingPoint(Transform transform){
        yield return new WaitForSeconds(4);
        Point point = pool.Get();
        point.transform.position = transform.position;
    }
    void OnReleasePoint(ReleasePointSignal signal)
    {
        pool.Release(signal.point);
    }
}
    