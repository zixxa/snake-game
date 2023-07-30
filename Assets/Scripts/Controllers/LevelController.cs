using CustomEventBus;
using CustomEventBus.Signals;
using UnityEngine;
using Levels;

public class LevelController : IService, IDisposable
{
    private int _currentLevelId;
    private LevelDataManager _LevelDataManger;
    private EventBus _eventBus;
    private Level _level;
    private int _levelId;

    public void Init()
    {
        _eventBus = ServiceLocator.Current.Get<EventBus>();
        _eventBus.Subscribe<LevelPassedSignal>(LevelPassed);
        _eventBus.Subscribe<NextLevelSignal>(NextLevel);
        _eventBus.Subscribe<RestartLevelSignal>(RestartLevel);

        _LevelDataManger = ServiceLocator.Current.Get<LevelDataManager>();

        PlayerPrefs.SetInt(ConstantValues.CURRENT_LEVEL_NAME, 0);
        _currentLevelId = PlayerPrefs.GetInt(ConstantValues.CURRENT_LEVEL_NAME, 0);
        OnInit();
    }

    private void OnInit()
    {
        LoadLevel(_currentLevelId);
    }
    private void LoadLevel(int levelId){
        var _level = _LevelDataManger.ShowLevel<Level>(levelId);
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
        if (_currentLevelId < _LevelDataManger.LevelsCount)
            LoadLevel(_currentLevelId);
        else
            _eventBus.Invoke(new GameWinSignal());
    }

    private void ClearLevel()
    {
        _eventBus.Invoke(new GameClearSignal());
    }

    private void RestartLevel(RestartLevelSignal signal)
    {
        ClearLevel();
        LoadLevel(_currentLevelId);
    }
    private void LevelPassed(LevelPassedSignal signal)
    {
        ClearLevel();
        PlayerPrefs.SetInt(ConstantValues.CURRENT_LEVEL_NAME, (_currentLevelId + 1));
        _eventBus.Invoke(new NextLevelSignal());
    }
    public void Dispose()
    {
        _eventBus.Unsubscribe<LevelPassedSignal>(LevelPassed);
        _eventBus.Unsubscribe<NextLevelSignal>(NextLevel);
        _eventBus.Unsubscribe<RestartLevelSignal>(RestartLevel);
    }
}