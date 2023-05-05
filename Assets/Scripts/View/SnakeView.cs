using UnityEngine;
public class SnakeView : MonoBehaviour
{
    private SegmentView head;
    [SerializeField] private SegmentView _headPrefab;
    [SerializeField] private SegmentView _bodyPrefab;
    public SegmentView GetHeadPrefab() => Instantiate(_headPrefab, new Vector3(0,0,0),Quaternion.identity);
    public SegmentView GetBodyPrefab() => Instantiate(_bodyPrefab, new Vector3(0,0,0),Quaternion.identity);

    void Start(){
       head = GetHeadPrefab();
    }
}