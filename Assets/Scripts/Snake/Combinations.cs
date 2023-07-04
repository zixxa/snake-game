using UnityEngine;
using System.Collections.Generic;
using CustomEventBus;
using CustomEventBus.Signals;
public class Combinations : MonoBehaviour {
    private EventBus _eventBus;
    [SerializeField] private List<CombinationObject> combinations;
    public void Init()
    {
        _eventBus = ServiceLocator.Current.Get<EventBus>();
        _eventBus.Subscribe<GetScriptableObjectCombinationsSignal>(OnGetCombinations);
    }
    void OnGetCombinations(GetScriptableObjectCombinationsSignal signal){
        _eventBus.Invoke(new FillCombinationsSignal(combinations));
    }
}
