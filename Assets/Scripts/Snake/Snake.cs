using System;
using System.Linq;
using System.Collections.Generic;
using CustomEventBus;
using CustomEventBus.Signals;
using UnityEngine;
public class Snake : MonoBehaviour, IService
{
    private EventBus _eventBus;
    public Segment head;
    public Segment bodyPrefab;
    public List<Segment> body{get; set;} = new List<Segment>();
    Segment GetLastSegment() => body.Count()>0 ? body.Last() : head;
    //ПОМЕНЯТЬ
    [SerializeField] private Segment GetBodyPrefab(){
        Segment tailSegment = GetLastSegment();
        Vector3 tail = tailSegment.transform.position - (tailSegment.transform.forward * tailSegment.transform.localScale.z * 1.2f);
        return Instantiate(bodyPrefab, tail, tailSegment.transform.rotation);
    }
    void Start(){
        _eventBus = ServiceLocator.Current.Get<EventBus>();
    }
    public void AddBodySegment(int mass, float drag){
        var spawnedSegment = GetBodyPrefab();
        AddSpringJoint(spawnedSegment);
        spawnedSegment.rigidbody.mass = mass;
        spawnedSegment.rigidbody.drag = drag;
        spawnedSegment.transform.parent.SetParent(gameObject.transform);
        spawnedSegment.rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        body.Add(spawnedSegment);
    }
    void AddHingeJoint(Segment spawnedSegment){
        var hingeJoint = GetLastSegment().gameObject.AddComponent<HingeJoint>();
        hingeJoint.connectedBody = spawnedSegment.rigidbody;
    }
    void AddSpringJoint(Segment spawnedSegment){
        var springJoint = GetLastSegment().gameObject.AddComponent<SpringJoint>();
        springJoint.connectedBody = spawnedSegment.rigidbody;
        springJoint.enableCollision = true;
        springJoint.spring = 2000;
        springJoint.damper = 20;
        springJoint.autoConfigureConnectedAnchor = false;
        springJoint.anchor = new Vector3(0,0,-1);
    }
    public void OnTouchPoint(TouchPointSignal signal){
        _eventBus.Invoke(new GetDataFromModelSignal());
    }
    public void OnPostRigidbodyDataToView(PostBodyRigidbodyDataSignal signal){
        AddBodySegment(signal.bodyMass, signal.bodyDrag);
    }
}