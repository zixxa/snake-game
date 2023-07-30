using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    float xRotation;
    float yRotation;
    [SerializeField] private float sensX;
    [SerializeField] private float sensY;
    public void Start(){

        //transform.rotation = Quaternion.Euler(0, -90, 0);
    }
    //public void Update(){
        //float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
        //float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;

        //xRotation += mouseY;
        //yRotation -= mouseX;
    //}
}
