using UnityEngine;
using CustomEventBus;
using CustomEventBus.Signals;
using UI;
public class GameController: IService{
    private EventBus _eventBus;
    public void Init()
    {
        _eventBus = ServiceLocator.Current.Get<EventBus>();
        _eventBus.Subscribe<GameOverSignal>(GameOver);
        _eventBus.Invoke(new GameStartSignal());
    }
    //public void StartGame(SetLevelSignal signal)
    //{
    //}
    private void GameOver(GameOverSignal signal){
        StopGame();
        DialogManager.ShowDialog<LoseDialog>();
    }
    public void StopGame()
    {
        _eventBus.Invoke(new GameStopSignal());
    }
    
}