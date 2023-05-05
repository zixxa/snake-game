using UnityEngine;

public class PlayerView : MonoBehaviour
{
    [SerializeField] private Transform element;
    Rigidbody rigidbody;
    public int moveSpeed;
    public int rotateMoveSpeed;
    public int groundDrag;
    float verticalInput;
    float horizontalInput;
    public float segmentHeight;
    Vector3 moveDirection;
    Vector3 angleVelocity;
    bool grounded;
    public LayerMask whatIsGround;
    void Awake(){
        rigidbody = GetComponent<Rigidbody>();
        rigidbody.freezeRotation = true;
        //rigidbody.constraints = RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
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


    void Update(){
        grounded = Physics.Raycast(transform.position,Vector3.down, segmentHeight);
        ControlInput();
        if (grounded)
            rigidbody.drag = groundDrag;
        else
            rigidbody.drag = 0;
        Debug.Log(transform.position);
        Debug.Log(grounded);
    }
}