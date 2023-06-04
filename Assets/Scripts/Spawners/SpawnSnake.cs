using UnityEngine;
using CustomEventBus;
using CustomEventBus.Signals;
public class SpawnSnake: MonoBehaviour, IService
{
    private EventBus _eventBus;
    private Snake _snake;
    [SerializeField] private Segment _headPrefab;
    [SerializeField] private Segment _bodyPrefab;
    [SerializeField] private Segment GetHeadPrefab() => Instantiate(_headPrefab, new Vector3(1,0,0), Quaternion.identity);
    void Start(){
        GameObject snakeObj = new GameObject("Snake");
        snakeObj.AddComponent<SnakeController>();
        _eventBus = ServiceLocator.Current.Get<EventBus>();
        _snake = snakeObj.AddComponent<Snake>();
        _snake.head = GetHeadPrefab();
        _snake.bodyPrefab = _bodyPrefab;
        _snake.head.transform.SetParent(snakeObj.transform);
        _eventBus.Invoke(new AttachCameraSignal(_snake.head.transform));
    }
}