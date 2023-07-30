using UnityEngine;
using CustomEventBus.Signals;
using Unity.VisualScripting;
using EventBus = CustomEventBus.EventBus;

namespace Levels
{
    public class Level : MonoBehaviour, IService
    {
        [SerializeField] private WinConditionType winConditionType;
        [SerializeField] private LevelData levelData;
        private WinCondition _winCondition;
        private EventBus _eventBus;
        private void Awake() {
            _eventBus = ServiceLocator.Current.Get<EventBus>();
            _eventBus.Subscribe<GameClearSignal>(OnDelete);
            _eventBus.Subscribe<WinConditionCompleteSignal>(WinLevel);
            _eventBus.Invoke(new GetLevelData(levelData));

            
            switch (winConditionType )
            {
                case WinConditionType.TimeCondition:
                    _winCondition = new TimeWinCondition();
                    break;
                case WinConditionType.KilledEnemiesCondition:
                    _winCondition = new KillEnemiesWinCondition();
                    break;
            }
            _winCondition.Init();
        }
        public enum WinConditionType
        {
            TimeCondition,
            KilledEnemiesCondition
        }
        private void WinLevel(WinConditionCompleteSignal signal)
        {
            _eventBus.Invoke(new LevelPassedSignal());
        }
        private void OnDelete(GameClearSignal signal)
        {
            Destroy(gameObject);
        }
        private void OnDestroy() {
            _eventBus.Unsubscribe<GameClearSignal>(OnDelete);
        }
    }
}