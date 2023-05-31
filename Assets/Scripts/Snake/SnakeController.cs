using CustomEventBus;
using CustomEventBus.Signals;
using UnityEngine;

public class SnakeController : MonoBehaviour, IService{
    private EventBus _eventBus;
    private Snake snake;
    private SnakeModel snakeModel;
    void Start(){
        snakeModel = new SnakeModel();
        snake = GetComponent<Snake>();

        _eventBus = ServiceLocator.Current.Get<EventBus>();
        _eventBus.Subscribe<TouchPointSignal>(snake.OnTouchPoint);
        _eventBus.Subscribe<TouchPointSignal>(snakeModel.OnIncreaseLength);
        _eventBus.Subscribe<GetDataFromModelSignal>(snakeModel.OnGetDataFromModel);
        _eventBus.Subscribe<PostBodyRigidbodyDataSignal>(snake.OnPostRigidbodyDataToView);

        snake.head.rigidbody.mass = snakeModel.head.mass;
        snake.head.rigidbody.freezeRotation = true;
    }
}