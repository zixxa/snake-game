using CustomEventBus;
using CustomEventBus.Signals;
using UnityEngine;
using Levels;

public class LevelController : MonoBehaviour, IService
{
    private int _currentLevelId;
    private LevelManager _levelManager;
    private EventBus _eventBus;
    private Level _level;

    public void Init()
    {
        _eventBus = ServiceLocator.Current.Get<EventBus>();
        _eventBus.Subscribe<LevelPassedSignal>(LevelPassed);
        _eventBus.Subscribe<NextLevelSignal>(NextLevel);
        _eventBus.Subscribe<RestartLevelSignal>(RestartLevel);

        _levelManager = ServiceLocator.Current.Get<LevelManager>();

        PlayerPrefs.SetInt(ConstantValues.CURRENT_LEVEL_NAME, 0);
        _currentLevelId = PlayerPrefs.GetInt(ConstantValues.CURRENT_LEVEL_NAME, 0);
        OnInit();
    }

    private void OnInit()
    {
        LoadLevel(_currentLevelId);
    }
    private void LoadLevel(int levelId){
        var _level = _levelManager.ShowLevel<Level>(levelId);
        if (_level == null)
        {
            Debug.LogErrorFormat("Can't find level with id {0}", _currentLevelId);
            return;
        }
        _eventBus.Invoke(new SetLevelSignal(_level));
    }

    private void NextLevel(NextLevelSignal signal)
    {
        _currentLevelId++;
        LoadLevel(_currentLevelId);
    }

    private void ClearLevel()
    {
        _eventBus.Invoke(new GameClearSignal());
    }

    private void RestartLevel(RestartLevelSignal signal)
    {
        ClearLevel();
        _eventBus.Invoke(new SetLevelSignal(_level));
    }
    
    private void SelectLevel(int levelId)
    {
        _level = _levelManager.GetLevel<Level>(levelId);
        _eventBus.Invoke(new SetLevelSignal(_level));
    }

    private void LevelPassed(LevelPassedSignal signal)
    {
        ClearLevel();
        PlayerPrefs.SetInt(ConstantValues.CURRENT_LEVEL_NAME, (_currentLevelId + 1));
        _eventBus.Invoke(new NextLevelSignal());
    }

    private void OnDestroy()
    {
        _eventBus.Unsubscribe<LevelPassedSignal>(LevelPassed);
        _eventBus.Unsubscribe<NextLevelSignal>(NextLevel);
        //_eventBus.Unsubscribe<LevelRestartSignal>();
    }
}