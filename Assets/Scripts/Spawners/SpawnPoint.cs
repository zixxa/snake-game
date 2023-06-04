using System.Collections.Generic;
using UnityEngine;
using CustomEventBus;
using CustomEventBus.Signals;
using Random = System.Random;

public class SpawnPoint: MonoBehaviour
{
    [SerializeField] private Transform beginSpawnPosition;
    private int spawnPointNum;
    private Vector3[] spawnZones;
    public SnakeModel snakeModel;
    private Random _rand;
    private EventBus _eventBus;
    private Point _point;
    [SerializeField] private Point _pointPrefab;
    [SerializeField] private Point GetPointPrefab() => Instantiate(_pointPrefab, GetPointPosition(),Quaternion.identity);
    void Awake(){
        List<Vector3> children = new List<Vector3>();
        foreach (Transform child in transform)
            children.Add(child.localPosition);     
        spawnZones = children.ToArray();
    }
    void Start(){
        _eventBus = ServiceLocator.Current.Get<EventBus>();
        _rand = new Random();
        snakeModel = new SnakeModel();
        _point = GetPointPrefab();
    }
    private Vector3 GetPointPosition(){
        int newSpawnPointNum = _rand.Next(0,spawnZones.Length-1);
        while (newSpawnPointNum  == spawnPointNum)
            newSpawnPointNum = _rand.Next(0,spawnZones.Length-1);
        spawnPointNum = newSpawnPointNum;
        return spawnZones[newSpawnPointNum];
    }
    public void SpawnNewPoint(TouchPointSignal signal){
        if (_point==null)
            _point = GetPointPrefab();
    }
}
    