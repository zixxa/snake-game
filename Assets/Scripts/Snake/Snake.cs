using System.Linq;
using System.Collections.Generic;
using CustomEventBus;
using CustomEventBus.Signals;
using UnityEngine;

public class Snake : MonoBehaviour, IService, IPauseHandler
{
    private EventBus _eventBus;
    private SnakeDataProvider _snakeDataProvider;
    public Head head;
    private Body bodyPrefab;
    public BodyLinker bodyLinker; 
    public List<Body> bodies;
    private List<CombinationData> combinations;
    private bool IsPaused;
    Segment GetLastSegment() => bodies.Count()>0 ? bodies.Last() : head;
    
    void Start() {
        bodyLinker = new BodyLinker();
        bodies = new List<Body>();
        _eventBus = ServiceLocator.Current.Get<EventBus>();
        
        _eventBus.Subscribe<FillPointsSignal>(bodyLinker.OnFillPoints);
        _eventBus.Subscribe<FillBodiesSignal>(bodyLinker.OnFillBodies);
        _eventBus.Subscribe<FillCombinationsSignal>(OnFillCombinations);
        _eventBus.Subscribe<CheckCombinationSignal>(OnCheckCombination);
        _eventBus.Subscribe<TouchPointSignal>(OnTouchPoint);
        _eventBus.Subscribe<DeleteBodySignal>(OnDeleteBody);
        _eventBus.Subscribe<GetSnakeDataProviderSignal>(OnPostSnakeDataProvider);
        _eventBus.Subscribe<GetSnakeDataProviderSignal>(OnPostSnakeDataProvider);
        _eventBus.Subscribe<GameClearSignal>(OnDelete);

        _eventBus.Invoke(new GetScriptableObjectPointsSignal());        
        _eventBus.Invoke(new GetScriptableObjectBodiesSignal());        
        _eventBus.Invoke(new GetScriptableObjectCombinationsSignal());        
        _eventBus.Invoke(new AttachCameraSignal(head.transform));
        _eventBus.Invoke(new PostSnakeDataProviderSignal());
        _eventBus.Invoke(new GetHeadForEnemiesSignal(head));
        bodyLinker.OnInit();
        SetHeadSettings();
    }
    void SetHeadSettings()
    {
        head.GetComponent<Rigidbody>().mass = _snakeDataProvider.head.mass;
        head.GetComponent<Rigidbody>().freezeRotation = true;
    }

    private void OnDeleteBody(DeleteBodySignal signal)
    {
        for (int i = signal.body.id+1; i < bodies.Count(); i++)
        {
            //bodies[i].transform = bodies[i - 1].transform;
            if (signal.body == bodies[i])
                Debug.Log("AAA");
        }
    }
    void AddSpringJoint(Segment spawnedSegment)
    {
        var springJoint = GetLastSegment().gameObject.AddComponent<SpringJoint>();
        springJoint.connectedBody = spawnedSegment.GetComponent<Rigidbody>();
        springJoint.enableCollision = true;
        springJoint.spring = 3000;
        springJoint.damper = 10;
        springJoint.autoConfigureConnectedAnchor = false;
        springJoint.anchor = new Vector3(0,0,-1);
    }
    public void AddBodySegment(int mass, float drag)
    {
        var spawnedSegment = GetBodyPrefab();
        AddSpringJoint(spawnedSegment);
        spawnedSegment.GetComponent<Rigidbody>().mass = mass;
        spawnedSegment.GetComponent<Rigidbody>().drag = drag;
        spawnedSegment.transform.SetParent(gameObject.transform);
        spawnedSegment.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        bodies.Add(spawnedSegment);

        if (bodies.Count() >= ConstantValues.MIN_COMBINATION_COUNT)
            _eventBus.Invoke(new CheckCombinationSignal());
    }
    private Body GetBodyPrefab()
    {
        var tailSegment = GetLastSegment();
        Vector3 tail = tailSegment.transform.position - (tailSegment.transform.forward * tailSegment.transform.localScale.z * 1.2f);
        return Instantiate(bodyPrefab, tail, tailSegment.transform.rotation);
    }
    public void OnTouchPoint(TouchPointSignal signal){
        bodyPrefab = bodyLinker.GetBodyByPoint(signal.point);
        AddBodySegment(_snakeDataProvider.body.mass, _snakeDataProvider.body.drag);
    }
    public void OnPostSnakeDataProvider(GetSnakeDataProviderSignal signal){
        _snakeDataProvider = signal.snakeDataProvider;
    }
    public void OnCheckCombination(CheckCombinationSignal signal)
    {
        foreach (CombinationData combination in combinations)
        {
            int coincidence = 0;
            for (int i=0; i<combination.elements.Count(); i++)
            {
                if (bodies[bodies.Count() - i - 1].color == combination.elements[i].color)
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
    private void CombinationToResult(CombinationData combination)
    {
        for (int i=0; i<combination.elements.Count(); i++)
        {
            Destroy(bodies.Last().gameObject);
            bodies.RemoveAt(bodies.Count() - 1);
        }
        bodyPrefab = combination.resultBody.prefab;
        AddBodySegment(_snakeDataProvider.body.mass, _snakeDataProvider.body.drag);
    }

    public void OnFillCombinations(FillCombinationsSignal signal){
        combinations = signal.combinationsObjects;
    }
    public void OnDelete(GameClearSignal signal)
    {
        Destroy(gameObject);
    }
    private void OnDestroy() {
        _eventBus.Unsubscribe<FillPointsSignal>(bodyLinker.OnFillPoints);
        _eventBus.Unsubscribe<FillBodiesSignal>(bodyLinker.OnFillBodies);
        _eventBus.Unsubscribe<FillCombinationsSignal>(OnFillCombinations);
        _eventBus.Unsubscribe<CheckCombinationSignal>(OnCheckCombination);
        _eventBus.Unsubscribe<TouchPointSignal>(OnTouchPoint);
        _eventBus.Unsubscribe<GetSnakeDataProviderSignal>(OnPostSnakeDataProvider);
        _eventBus.Unsubscribe<GetSnakeDataProviderSignal>(OnPostSnakeDataProvider);
        _eventBus.Unsubscribe<GameClearSignal>(OnDelete);
    }
    void IPauseHandler.SetPaused(bool isPaused) => IsPaused = isPaused;
    void Update()
    {
        if (IsPaused)
            return;
    }
}