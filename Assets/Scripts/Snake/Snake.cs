using System;
using System.Linq;
using System.Collections.Generic;
using CustomEventBus;
using CustomEventBus.Signals;
using UnityEngine;

public class Snake : MonoBehaviour, IService
{
    private SnakeModel snakeModel;
    private EventBus _eventBus;
    public Segment head;
    public BodyLinker bodyLinker; 
    private Segment bodyPrefab;
    public List<Segment> body = new List<Segment>();
    private List<CombinationObject> combinations;
    Segment GetLastSegment() => body.Count()>0 ? body.Last() : head;
    private Segment GetBodyPrefab(){
        Segment tailSegment = GetLastSegment();
        Vector3 tail = tailSegment.transform.position - (tailSegment.transform.forward * tailSegment.transform.localScale.z * 1.2f);
        return Instantiate(bodyPrefab, tail, tailSegment.transform.rotation);
    }

    void Start() {
        snakeModel = new SnakeModel();
        bodyLinker = new BodyLinker();

        _eventBus = ServiceLocator.Current.Get<EventBus>();

        _eventBus.Subscribe<FillPointsSignal>(bodyLinker.OnFillPoints);

        _eventBus.Subscribe<FillCombinationsSignal>(OnFillCombinations);
        _eventBus.Subscribe<CheckCombinationSignal>(OnCheckCombination);
        _eventBus.Subscribe<TouchPointSignal>(OnTouchPoint);
        _eventBus.Subscribe<TouchPointSignal>(snakeModel.OnIncreaseLength);
        _eventBus.Subscribe<GetDataFromModelSignal>(snakeModel.OnGetDataFromModel);
        _eventBus.Subscribe<PostBodyRigidbodyDataSignal>(OnPostRigidbodyDataToView);

        _eventBus.Invoke(new GetScriptableObjectPointsSignal());        
        _eventBus.Invoke(new GetScriptableObjectCombinationsSignal());        
        _eventBus.Invoke(new AttachCameraSignal(head.transform));

        head.GetComponent<Rigidbody>().mass = snakeModel.head.mass;
        head.GetComponent<Rigidbody>().freezeRotation = true;
    }

    void AddHingeJoint(Segment spawnedSegment){
        var hingeJoint = GetLastSegment().gameObject.AddComponent<HingeJoint>();
        hingeJoint.connectedBody = spawnedSegment.GetComponent<Rigidbody>();
    }
    void AddSpringJoint(Segment spawnedSegment){
        var springJoint = GetLastSegment().gameObject.AddComponent<SpringJoint>();
        springJoint.connectedBody = spawnedSegment.GetComponent<Rigidbody>();
        springJoint.enableCollision = true;
        springJoint.spring = 3000;
        springJoint.damper = 10;
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

        if (body.Count() >= ConstantValues.MIN_COMBINATION_COUNT)
            _eventBus.Invoke(new CheckCombinationSignal());
    }
    public void OnTouchPoint(TouchPointSignal signal){
        bodyPrefab = bodyLinker.GetBodyByPoint(signal.point);
        _eventBus.Invoke(new GetDataFromModelSignal());
    }
    public void OnPostRigidbodyDataToView(PostBodyRigidbodyDataSignal signal){
        AddBodySegment(signal.bodyMass, signal.bodyDrag);
    }
    public void OnCheckCombination(CheckCombinationSignal signal)
    {
        foreach (CombinationObject combination in combinations)
        {
        int coincidence=0;
            for (int i=0; i<combination.elements.Count(); i++)
            {

                if (body[body.Count() - i - 1].code == combination.elements[i].code)
                {
                    coincidence++;                    
                }
            }
            if (coincidence == combination.elements.Count())
            {
                CombinationToResult(combination);
                break;
            }
        }
    }
    private void CombinationToResult(CombinationObject combination)
    {
        for (int i=0; i<combination.elements.Count(); i++)
        {
            Debug.Log(body.Last());
            Destroy(body.Last().gameObject);
            body.RemoveAt(body.Count() - 1);
        }
        bodyPrefab = combination.resultBody.prefab;
        _eventBus.Invoke(new GetDataFromModelSignal());
    }

    public void OnFillCombinations(FillCombinationsSignal signal){
        combinations = signal.combinationsObjects;
    }

    void Dispose()
    {
        _eventBus.Unsubscribe<FillCombinationsSignal>(OnFillCombinations);
        _eventBus.Unsubscribe<CheckCombinationSignal>(OnCheckCombination);
        _eventBus.Unsubscribe<TouchPointSignal>(OnTouchPoint);
        _eventBus.Unsubscribe<TouchPointSignal>(snakeModel.OnIncreaseLength);
        _eventBus.Unsubscribe<GetDataFromModelSignal>(snakeModel.OnGetDataFromModel);
        _eventBus.Unsubscribe<PostBodyRigidbodyDataSignal>(OnPostRigidbodyDataToView);

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