using UnityEngine;

[CreateAssetMenu(fileName = "MovementData", menuName = "MovementData", order = 0)]
public class MovementData : ScriptableObject {
    [SerializeField] private int _moveSpeed;
    [SerializeField] private int _rotateMoveSpeed;
    [SerializeField] private float _dragOnMove;
    [SerializeField] private float _dragOnStop;
    [SerializeField] private float _height;
    [SerializeField] private float _boostSpeedByLength;
    public int MoveSpeed => _moveSpeed ;
    public int RotateMoveSpeed => _rotateMoveSpeed;
    public float DragOnMove => _dragOnMove;
    public float DragOnStop =>_dragOnStop;
    public float Height => _height;
    public float BoostSpeedByLength => _boostSpeedByLength;
}