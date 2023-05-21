using System;
using System.Collections.Generic;
using UnityEngine;

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
    public int mass;
    public Head() : base(){
        mass = 10;
    }
}

public class Body: SegmentModel{
    public int mass;
    public Body() : base() {
        mass = 15;
    }
} 

public class MovementProvider{

    public int moveSpeed{get;private set;} = 2000;
    public int rotateMoveSpeed{get;private set;} = 4000;
    public float dragOnMove{get;private set;} = 0.2f;
    public float dragOnStop{get;private set;} = 1.5f;
    public float height{get;private set;} = 10f;
}

public class SnakeModel
{
    public Action <MovementProvider> OnMoved;
    public Head head;
    public Body[] body;
    public int bodyCount{get;private set;} = 0; 

//    public Vector3 height = new Vector3(0,0,1);
    public void AddSegment(){
        Debug.Log("Increased");
        //OnIncreaseLength();
    }
    public void GetSpeed(){
        OnMoved?.Invoke(new MovementProvider());
    }
}