using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UI;
public class MenuDialog : Dialog{
    [SerializeField] private Button _playButton;
    protected void Awake()
    {
        _playButton.onClick.AddListener(OnPlayButtonClick);
    }
    private void OnPlayButtonClick() => SceneManager.LoadScene(ConstantValues.MAIN_SCENE_NAME);
}