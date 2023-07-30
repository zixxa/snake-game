using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UI;
using CustomEventBus;
using CustomEventBus.Signals;

public class LoseDialog : Dialog
{
    [SerializeField] private Button _tryAgainButton;
    [SerializeField] private Button _goToMenuButton;
    private PauseController pauseController;
    private EventBus _eventBus;

    private void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        pauseController = ServiceLocator.Current.Get<PauseController>();
        pauseController.SetPaused(true);
        _tryAgainButton.onClick.AddListener(TryAgain);
        _goToMenuButton.onClick.AddListener(GoToMenu);
        _eventBus = ServiceLocator.Current.Get<EventBus>();
    }

    private void TryAgain()
    {
        _eventBus.Invoke(new RestartLevelSignal());
        Hide();
    }

    private void GoToMenu() => SceneManager.LoadScene(ConstantValues.MENU_SCENE_NAME);
}