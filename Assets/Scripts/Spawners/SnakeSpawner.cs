using UnityEngine;
using CustomEventBus;
using CustomEventBus.Signals;
public class SnakeSpawner: MonoBehaviour, IService
{
    private EventBus _eventBus;
    private Snake _snake;
    [SerializeField] private Segment _headPrefab;
    [SerializeField] private Segment GetHeadPrefab() => Instantiate(_headPrefab, new Vector3(1,0,0), Quaternion.identity);
    void Start(){
        GameObject snakeObj = new GameObject("Snake");
        snakeObj.AddComponent<SnakeEventSubscriber>();
        _eventBus = ServiceLocator.Current.Get<EventBus>();
        _snake = snakeObj.AddComponent<Snake>();
        _snake.head = GetHeadPrefab();
        _snake.head.transform.SetParent(snakeObj.transform);
        _eventBus.Invoke(new AttachCameraSignal(_snake.head.transform));
    }
}