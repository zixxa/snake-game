using System;
using UnityEngine;
using CustomEventBus;
using CustomEventBus.Signals;
public class Player: MonoBehaviour, IService
{
    private MovementData movement;
    private EventBus _eventBus;
    private int bodyCount;
    
    private bool IsPaused => ServiceLocator.Current.Get<PauseController>().IsPaused;
    public Segment player {get;private set;}
    void Start()
    {
        _eventBus = ServiceLocator.Current.Get<EventBus>();
        _eventBus.Subscribe<PostBodyCountSignal>(OnGetBodyCount);
        _eventBus.Subscribe<GetMovementDataSignal>(OnMove);
        player = GetComponent<Segment>();
        _eventBus.Invoke(new PostMovementDataSignal());
    }

    public void OnGetBodyCount(PostBodyCountSignal signal){
        bodyCount = signal.bodyCount;
    }
    
    void OnMove(GetMovementDataSignal signal){
        movement = signal.movement;       
    }

    void FixedUpdate(){
        var verticalInput = Input.GetAxisRaw("Vertical");
        var horizontalInput = Input.GetAxisRaw("Horizontal");
        
        if (IsPaused)
            return;

        player.GetComponent<Rigidbody>().drag = (verticalInput == 0) ? movement.DragOnStop: movement.DragOnMove;

        float speed = Input.GetAxisRaw("Vertical") * Time.deltaTime;

        Vector3 moveDirection = player.transform.forward * verticalInput;
        Vector3 _angleVelocity = new Vector3(0, horizontalInput * Time.deltaTime * movement.RotateMoveSpeed, 0);

        Quaternion deltaRotation = Quaternion.Euler(_angleVelocity * Time.deltaTime);
        player.GetComponent<Rigidbody>().MoveRotation(GetComponent<Rigidbody>().rotation * deltaRotation);
        player.GetComponent<Rigidbody>().AddForce(moveDirection.normalized * (movement.MoveSpeed + (bodyCount+1)*movement.BoostSpeedByLength), ForceMode.Force);
    }
}
