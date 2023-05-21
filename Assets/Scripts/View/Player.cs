using System;
using UnityEngine;
public class Player: MonoBehaviour
{
    [SerializeField] private Segment segment;
    public Transform transform{get;private set;}
    public event Action OnStartMoved;
    private Vector3 moveDirection;
    private Vector3 angleVelocity;
    bool grounded;
    float verticalInput;
    float horizontalInput;
    Rigidbody rigidbody;
    void Awake(){
        segment = GetComponent<Segment>();
        transform = segment.transform;
        rigidbody = GetComponent<Rigidbody>();
        rigidbody.freezeRotation = true;
    }

    private void ControlInput(){
        verticalInput = Input.GetAxisRaw("Vertical");
        horizontalInput = Input.GetAxisRaw("Horizontal");
    }

    public void Move(int moveSpeed, int rotateMoveSpeed){
        ControlInput();
        moveDirection = segment.transform.forward * verticalInput;
        float speed = Input.GetAxisRaw("Vertical") * Time.deltaTime;

        angleVelocity = new Vector3(0, horizontalInput * Time.deltaTime * rotateMoveSpeed, 0);
        Quaternion deltaRotation = Quaternion.Euler(angleVelocity * Time.deltaTime);
        rigidbody.MoveRotation(rigidbody.rotation * deltaRotation);
        rigidbody.AddForce(moveDirection.normalized * moveSpeed, ForceMode.Force);
    }
    public void AddDrag(float groundDragOnMove, float groundDragOnStop){
        rigidbody.drag = (verticalInput == 0) ? groundDragOnStop: groundDragOnMove;
        //Debug.Log($" Drag: {horizontalInput} {verticalInput} {rigidbody.drag}");
    }

    void FixedUpdate(){
        OnStartMoved?.Invoke();
    }
}