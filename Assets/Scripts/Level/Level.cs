using UnityEngine;
using CustomEventBus;
using CustomEventBus.Signals;

namespace Levels
{
    public class Level : MonoBehaviour
    {
        private EventBus _eventBus;
        private void Start() {
            _eventBus = ServiceLocator.Current.Get<EventBus>();
            _eventBus.Subscribe<GameClearSignal>(DestroyLevel);
        }
        private void DestroyLevel(GameClearSignal signal)
        {
            Destroy(gameObject);
        }
    }
}