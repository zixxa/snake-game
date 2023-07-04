using CustomEventBus;
using CustomEventBus.Signals;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UI;

public class LoseDialog : Dialog
{
    [SerializeField] private Button _tryAgainButton;
    [SerializeField] private Button _goToMenuButton;

    private EventBus _eventBus;

    private void Start()
    {
        _tryAgainButton.onClick.AddListener(TryAgain);
        _goToMenuButton.onClick.AddListener(GoToMenu);

        _eventBus = ServiceLocator.Current.Get<EventBus>();
    }

    private void TryAgain()
    {
        _eventBus.Invoke(new GameStartSignal());
        Hide();
    }

    private void GoToMenu()
    {
        SceneManager.LoadScene(ConstantValues.MENU_SCENE_NAME);
    }
}