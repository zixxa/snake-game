/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Snake
{
    public SnakeHead head;
    //public SnakeHead headPrefab;
    public SnakeBody[] body;
    //public SnakeBody bodyPrefab;
    public int numOfSegments = 10;
}


public class SnakeBody : Segment
{
    public Rigidbody rigidbody;
    [SerializeField] public Transform element;

    public int groundDrag;
    float verticalInput;
    float horizontalInput;
    public float segmentHeight;
    bool grounded;
    public LayerMask whatIsGround;
    void Awake(){
        rigidbody = GetComponent<Rigidbody>();
    }
    void Update(){
        grounded = Physics.Raycast(transform.position, Vector3.down, segmentHeight, whatIsGround);
        if (grounded)
            rigidbody.drag = groundDrag;
        else
            rigidbody.drag = 0;
    Debug.Log(grounded);
    }
}


public class SnakeHead: Segment
{
    Rigidbody rigidbody;
    public Transform element;
    public int moveSpeed;
    public int rotateMoveSpeed;
    public int groundDrag;
    float verticalInput;
    float horizontalInput;
    public float segmentHeight;
    bool grounded;
    public LayerMask whatIsGround;
    Vector3 moveDirection;
    Vector3 angleVelocity;
    void Awake(){
        rigidbody = GetComponent<Rigidbody>();
    }

    void Update(){
        grounded = Physics.Raycast(transform.position,Vector3.down, segmentHeight * 0.5f * 0.2f, whatIsGround);
        ControlInput();
        if (grounded)
            rigidbody.drag = groundDrag;
        else
            rigidbody.drag = 0;
        
        Debug.Log(grounded);
    }
    private void FixedUpdate()
    {
        MovePlayer();
    }
    private void ControlInput(){
        verticalInput = Input.GetAxisRaw("Vertical");
        horizontalInput = Input.GetAxisRaw("Horizontal");
    }
    
    private void MovePlayer(){
        moveDirection = element.forward * verticalInput;
        float speed = Input.GetAxisRaw("Vertical") * Time.deltaTime;

        angleVelocity = new Vector3(0, horizontalInput * Time.deltaTime * rotateMoveSpeed, 0);
        Quaternion deltaRotation = Quaternion.Euler(angleVelocity * Time.deltaTime);
        rigidbody.MoveRotation(rigidbody.rotation * deltaRotation);
        rigidbody.AddForce(moveDirection.normalized * moveSpeed, ForceMode.Force);
    }
}
*/