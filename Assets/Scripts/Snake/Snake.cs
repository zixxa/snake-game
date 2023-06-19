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
    public BodyLinker bodyLinker; 
    private Segment bodyPrefab;
    public List<Segment> body{get; set;} = new List<Segment>();
    Segment GetLastSegment() => body.Count()>0 ? body.Last() : head;
    private Segment GetBodyPrefab(){
        Segment tailSegment = GetLastSegment();
        Vector3 tail = tailSegment.transform.position - (tailSegment.transform.forward * tailSegment.transform.localScale.z * 1.2f);
        return Instantiate(bodyPrefab, tail, tailSegment.transform.rotation);
    }
    void Start() {
        _eventBus = ServiceLocator.Current.Get<EventBus>();
        _eventBus.Invoke(new GetScriptableObjectPointsSignal());        
    }
    void AddHingeJoint(Segment spawnedSegment){
        var hingeJoint = GetLastSegment().gameObject.AddComponent<HingeJoint>();
        hingeJoint.connectedBody = spawnedSegment.GetComponent<Rigidbody>();
    }
    void AddSpringJoint(Segment spawnedSegment){
        var springJoint = GetLastSegment().gameObject.AddComponent<SpringJoint>();
        springJoint.connectedBody = spawnedSegment.GetComponent<Rigidbody>();
        springJoint.enableCollision = true;
        springJoint.spring = 5000;
        springJoint.damper = 100;
        springJoint.autoConfigureConnectedAnchor = false;
        springJoint.anchor = new Vector3(0,0,-1);
    }
    public void AddBodySegment(int mass, float drag){
        var spawnedSegment = GetBodyPrefab();
        AddSpringJoint(spawnedSegment);
        spawnedSegment.GetComponent<Rigidbody>().mass = mass;
        spawnedSegment.GetComponent<Rigidbody>().drag = drag;
        spawnedSegment.transform.parent.SetParent(gameObject.transform);
        spawnedSegment.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        body.Add(spawnedSegment);
    }
    public void OnTouchPoint(TouchPointSignal signal){
        bodyPrefab = bodyLinker.GetBodyByPoint(signal.point);
        _eventBus.Invoke(new GetDataFromModelSignal());
    }
    public void OnPostRigidbodyDataToView(PostBodyRigidbodyDataSignal signal){
        AddBodySegment(signal.bodyMass, signal.bodyDrag);
    }
}

public class BodyLinker
{
    private EventBus _eventBus;
    private List<PointObject> pointObjects;
    public Dictionary<string,Segment> linksByBodyAndPoints = new Dictionary<string, Segment>();  
    public Segment GetBodyByPoint(Point point) => linksByBodyAndPoints[point.code];
    public void OnFillPoints(FillPointsSignal signal)
    {
        pointObjects = signal.pointsObjects;
        linksByBodyAndPoints = Enumerable.Range(0,pointObjects.Count()).ToDictionary(x => pointObjects[x].code , x => pointObjects[x].createdBody.prefab);
    }
}