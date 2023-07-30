using UnityEngine;
using CustomEventBus;
using CustomEventBus.Signals;
using UI;
public class GameController: IService, IPauseHandler, IDisposable{
    private bool gameOver;
    private EventBus _eventBus;
    private PauseController _pauseController;
    public void Init()
    {
        _eventBus = ServiceLocator.Current.Get<EventBus>();
        _eventBus.Subscribe<SetLevelSignal>(StartGame);
        _eventBus.Subscribe<GameOverSignal>(GameOver);
        _eventBus.Subscribe<GameWinSignal>(GameWin);
        _pauseController = ServiceLocator.Current.Get<PauseController>();
        _pauseController.Register(this);
    }
    public void StartGame(SetLevelSignal signal)
    {
        DialogManager.ShowDialog<LoadingDialog>();
        gameOver = false;
        _pauseController.SetPaused(false);
        _eventBus.Invoke(new GameStartSignal());
    }
    private void GameOver(GameOverSignal signal){
        if (gameOver == false)
        {
            gameOver = true;
            DialogManager.ShowDialog<LoseDialog>();
            _eventBus.Invoke(new GameClearSignal());
        }
    }
    void IPauseHandler.SetPaused(bool isPaused)
    {
        Time.timeScale = isPaused ? 0:1;
    }
    private void GameWin(GameWinSignal signal)
    {
        _pauseController.SetPaused(true);
        DialogManager.ShowDialog<WinDialog>();
    }
    public void Dispose()
    {
        _eventBus.Unsubscribe<GameOverSignal>(GameOver);
        _eventBus.Unsubscribe<GameWinSignal>(GameWin);
        _eventBus.Unsubscribe<SetLevelSignal>(StartGame);
    }
}
