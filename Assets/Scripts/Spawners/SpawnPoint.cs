using System.Xml.Serialization;
using System;
using UnityEngine;
using CustomEventBus;
using CustomEventBus.Signals;
using Random = System.Random;

public class SpawnPoint: MonoBehaviour
{
    public SnakeModel snakeModel;
    private EventBus _eventBus;
    [SerializeField] private Point _point;
    private int _gameFieldScaleX{get;set;}
    private int _gameFieldScaleZ{get;set;}
    [SerializeField] private Point _pointPrefab;
    [SerializeField] public Point GetPointPrefab() => Instantiate(_pointPrefab, GetPointPosition() ,Quaternion.identity);
    private void Start(){
        snakeModel = new SnakeModel();
        _eventBus = ServiceLocator.Current.Get<EventBus>();
        _eventBus.Subscribe<GetGameFieldScaleSignal>(OnGetGameFieldScale);
        _eventBus.Subscribe<TouchPointSignal>(SpawnNewPoint);
        _point = GetPointPrefab();
    }
    void OnGetGameFieldScale(GetGameFieldScaleSignal signal){
        _gameFieldScaleX = signal.width;
        _gameFieldScaleZ = signal.length;
    }
    private Vector3 GetPointPosition(){
        Random rand = new Random();
        int xPosition = rand.Next(-_gameFieldScaleX*2,_gameFieldScaleX*2);
        int zPosition = rand.Next(-_gameFieldScaleZ*2,_gameFieldScaleZ*2);
        return new Vector3(xPosition, _pointPrefab.transform.localScale.y/2, zPosition);
    }
    void SpawnNewPoint(TouchPointSignal signal){
        _point = GetPointPrefab();
    }
}