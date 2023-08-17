using UnityEngine;
using UnityEngine.UI;
using UI;
using CustomEventBus;
using System.Linq;
public class CustomizationDialog : Dialog{
    private EventBus _eventBus;
    
    [SerializeField] private Button _exitButton;
    [SerializeField] private GridLayoutGroup _elementsGrid; 
    [SerializeField] private CustomizeSlot _slotPrefab;
    [SerializeField] private HeadsListData _headsListData;
    private void Start()
    {
        _exitButton.onClick.AddListener(Exit);
        _eventBus = ServiceLocator.Current.Get<EventBus>();
        for (int i = 0; i<_headsListData.heads.Count(); i++)
        {
            var slot = GameObject.Instantiate(_slotPrefab, _elementsGrid.transform);
            slot.Init(_headsListData.heads[i], i);
        }
        PlayerPrefs.GetInt(ConstantValues.SELECTED_SNAKE_HEAD, 0);
    }

    private void Exit() => Hide();
}