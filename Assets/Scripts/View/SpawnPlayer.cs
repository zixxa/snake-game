using System;
using UnityEngine;

public class SpawnPlayer : MonoBehaviour
{
    private Segment head;
    [SerializeField] private Segment _headPrefab;
    [SerializeField] private Segment _bodyPrefab;
    public Segment GetHeadPrefab() => Instantiate(_headPrefab, new Vector3(0,0,0),Quaternion.identity);
    public Segment GetBodyPrefab() => Instantiate(_bodyPrefab, new Vector3(0,0,0),Quaternion.identity);

    void Start(){
       head = GetHeadPrefab();
    }
}