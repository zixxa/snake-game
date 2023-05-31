using System;
using UnityEngine;
using CustomEventBus;
using CustomEventBus.Signals;
public class Player: MonoBehaviour, IService
{
    private EventBus _eventBus;
    public Segment segment{get;private set;}
    private Vector3 _moveDirection{get;set;}
    private Vector3 _angleVelocity{get;set;}
    private int rotateMoveSpeed{get;set;}
    private int moveSpeed{get;set;}
    private float groundDragOnMove {get;set;}
    private float groundDragOnStop{get;set;}
    private int bodyCount{get;set;}
    void Start(){
        segment = GetComponent<Segment>();
        _eventBus = ServiceLocator.Current.Get<EventBus>();
        _eventBus.Subscribe<MovementSignal>(OnMove);
        _eventBus.Subscribe<PostBodyCountSignal>(OnGetBodyCount);
        _eventBus.Invoke(new MovementSignal());
    }
    public void OnGetBodyCount(PostBodyCountSignal signal){
        bodyCount = signal.bodyCount;
    }
    public void OnMove(MovementSignal signal){
        moveSpeed = signal.movement.moveSpeed;
        rotateMoveSpeed = signal.movement.rotateMoveSpeed;
        groundDragOnMove = signal.movement.dragOnMove;
        groundDragOnStop = signal.movement.dragOnStop;
    }
    void FixedUpdate(){
        var verticalInput = Input.GetAxisRaw("Vertical");
        var horizontalInput = Input.GetAxisRaw("Horizontal");

        segment.rigidbody.drag = (verticalInput == 0) ? groundDragOnStop: groundDragOnMove;

        _moveDirection = segment.transform.forward * verticalInput;
        float speed = Input.GetAxisRaw("Vertical") * Time.deltaTime;

        _angleVelocity = new Vector3(0, horizontalInput * Time.deltaTime * rotateMoveSpeed, 0);

        Quaternion deltaRotation = Quaternion.Euler(_angleVelocity * Time.deltaTime);
        segment.rigidbody.MoveRotation(GetComponent<Rigidbody>().rotation * deltaRotation);

        Debug.Log(moveSpeed + (bodyCount+1)*100);
        segment.rigidbody.AddForce(_moveDirection.normalized * (moveSpeed + (bodyCount+1)*100), ForceMode.Force);
    }
}