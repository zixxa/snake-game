using UnityEngine;
using CustomEventBus;
using CustomEventBus.Signals;
using UI;
public class GameController: IService, IPauseHandler, IDisposable{
    private EventBus _eventBus;
    private PauseController _pauseController;
    public void Init()
    {
        _eventBus = ServiceLocator.Current.Get<EventBus>();
        _eventBus.Subscribe<GameOverSignal>(GameOver);
        _eventBus.Subscribe<SetLevelSignal>(StartGame);
        _pauseController = ServiceLocator.Current.Get<PauseController>();
        _pauseController.Register(this);
    }
    public void StartGame(SetLevelSignal signal)
    {
        _eventBus.Invoke(new GameStartSignal());
    }
    private void GameOver(GameOverSignal signal){
        _eventBus.Invoke(new GameClearSignal());
        DialogManager.ShowDialog<LoseDialog>();
    }
    void IPauseHandler.SetPaused(bool isPaused)
    {
        Time.timeScale = isPaused ? 0:1;
    }
    public void Dispose()
    {
        _eventBus.Unsubscribe<GameOverSignal>(GameOver);
        _eventBus.Unsubscribe<SetLevelSignal>(StartGame);
    }
    
}
