using CustomEventBus;
using CustomEventBus.Signals;
using UnityEngine;

public class SnakeEventSubscriber: MonoBehaviour, IService{
    private EventBus _eventBus;
    private Snake snake;
    private SnakeModel snakeModel;
    public BodyLinker bodyLinker;
    void Start(){
        snakeModel = new SnakeModel();
        snake = GetComponent<Snake>();

        _eventBus = ServiceLocator.Current.Get<EventBus>();

        bodyLinker = new BodyLinker();
        _eventBus.Subscribe<FillPointsSignal>(bodyLinker.OnFillPoints);
        snake.bodyLinker = bodyLinker;
        _eventBus.Subscribe<TouchPointSignal>(snake.OnTouchPoint);
        _eventBus.Subscribe<TouchPointSignal>(snakeModel.OnIncreaseLength);
        _eventBus.Subscribe<GetDataFromModelSignal>(snakeModel.OnGetDataFromModel);
        _eventBus.Subscribe<PostBodyRigidbodyDataSignal>(snake.OnPostRigidbodyDataToView);
        _eventBus.Invoke(new AttachCameraSignal(snake.head.transform));

        snake.head.GetComponent<Rigidbody>().mass = snakeModel.head.mass;
        snake.head.GetComponent<Rigidbody>().freezeRotation = true;
    }
    void OnDestroy()
    {
        _eventBus.Unsubscribe<TouchPointSignal>(snake.OnTouchPoint);
        _eventBus.Unsubscribe<TouchPointSignal>(snakeModel.OnIncreaseLength);
        _eventBus.Unsubscribe<GetDataFromModelSignal>(snakeModel.OnGetDataFromModel);
        _eventBus.Unsubscribe<PostBodyRigidbodyDataSignal>(snake.OnPostRigidbodyDataToView);
    }

}