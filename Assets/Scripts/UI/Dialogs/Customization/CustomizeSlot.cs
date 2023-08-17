using UnityEngine;
using UnityEngine.UI;
using CustomEventBus;

public class CustomizeSlot: MonoBehaviour
{
    [SerializeField] private Image _headImage;
    private string _name;
    [SerializeField] private Button _select; 
    private EventBus _eventBus;
    private int slotId;
    
    private void Start()
    {
        _select.onClick.AddListener(SelectSnakeHead);
        _eventBus = ServiceLocator.Current.Get<EventBus>();
    }
    
    public void Init(Head data, int id)
    {
        _name = data.Name;
        _headImage.sprite = data.Image;
        slotId = id;
    }
    
    private void SelectSnakeHead()
    {
        PlayerPrefs.SetInt(ConstantValues.SELECTED_SNAKE_HEAD, slotId); 
    }
}