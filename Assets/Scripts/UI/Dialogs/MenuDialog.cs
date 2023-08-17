using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UI;
public class MenuDialog : Dialog{
    [SerializeField] private Button _playButton;
    [SerializeField] private Button _customizationButton;
    protected void Awake()
    {
        _playButton.onClick.AddListener(OnPlayButtonClick);
        _customizationButton.onClick.AddListener(CustomizationButtonClick);
    }
    private void OnPlayButtonClick() => SceneManager.LoadScene(ConstantValues.MAIN_SCENE_NAME);
    private void CustomizationButtonClick() => DialogManager.ShowDialog<CustomizationDialog>();
}