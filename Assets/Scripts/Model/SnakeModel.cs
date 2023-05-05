using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Segment {
    public const int MAX_HEALTH = 100;
    public Transform segment;
    public int MaxHp {get;set;}
    public int Hp {get;set;}
    public int height{get;private set;} = 1;


    public Segment(){
        MaxHp = MAX_HEALTH;
        Hp = MaxHp;
    }    
}

public class Head: Segment{
    public int mass;
    public Head() : base(){
        mass = 10;
    }
    public bool controlFlag {get; private set;} = true;
}

public class Body: Segment{
    public int mass;
    public Body() : base() {
        mass = 15;
    }
    public bool controlFlag {get; private set;} = false;
} 


public class SnakeModel
{
    public Head head;
    public Body[] body;
    public int bodyCount{get;set;} = 0; 
}

//public class Player(){
//    public Rigidbody rigidbody;
//    public int groundDrag;
//    private float verticalInput;
//    private float horizontalInput;
//    private bool grounded;
//    private LayerMask whatIsGround;
//
//    [SerializeField] public Transform element;
//    [SerializeField] float segmentHeight;
//}