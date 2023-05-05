using System.ComponentModel.Design;
using UnityEngine;

public class SnakeControl
{
    private SnakeView View{get;set;}
    private SnakeModel Model{get;set;}
    public SnakeControl(SnakeModel model, SnakeView view){
        Model = model;
        View = view;
    }
    void Update(){
        View.GetComponent<Rigidbody>().drab = CheckForDrag();
    }
    private int CheckForDrag() => Physics.Raycast(Model.height,Vector3.down, segmentHeight) ? groundDrag : 0;
    
}