using System;
using UnityEngine;
public class Segment : MonoBehaviour
{
    public Transform transform;
    public static event Action OnGetPoint;
    void OnTriggerEnter(Collider trigger){
        Destroy(trigger.gameObject);
        OnGetPoint?.Invoke();
    }
}
