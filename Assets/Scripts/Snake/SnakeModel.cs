using System;
using System.Collections.Generic;
using UnityEngine;
using CustomEventBus;
using CustomEventBus.Signals;

public abstract class SegmentModel {
    public const int MAX_HEALTH = 100;
    public int Hp {get;set;}
    public int MaxHp {get;set;}

    public SegmentModel(){
        MaxHp = MAX_HEALTH;
        Hp = MaxHp;
    }    
}

public class Head: SegmentModel{
    public int mass{get;private set;} = 100;
    public Head() : base(){
    }
}

public class Body: SegmentModel{
    public int mass{get;set;} = 10;
    public float drag{get;private set;} = 0.3f;
    public Body() : base() {
    }
} 

public class MovementProvider{
    public int moveSpeed{get;private set;} = 2000;
    public int rotateMoveSpeed{get;private set;} = 5000;
    public float dragOnMove{get;private set;} = 0.3f;
    public float dragOnStop{get;private set;} = 1.3f;
    public float height{get;private set;} = 10f;
}
public class SnakeModel: IService
{
    private EventBus _eventBus; 
    public Head head;
    public Body body;
    public int bodyCount{get;private set;} = 0;
    public SnakeModel(){
        head = new Head();
        body = new Body();
    }
    public void OnIncreaseLength(TouchPointSignal signal){
        bodyCount++;
        Debug.Log($"Increased! {bodyCount} segments in snake!");
        _eventBus.Invoke(new PostBodyCountSignal(bodyCount));
    }
    public void OnGetDataFromModel(GetDataFromModelSignal signal){
        _eventBus = ServiceLocator.Current.Get<EventBus>();
        _eventBus.Invoke(new PostBodyRigidbodyDataSignal(body.mass, body.drag));
    }
}