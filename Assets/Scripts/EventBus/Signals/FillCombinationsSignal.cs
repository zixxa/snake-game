using System.Collections.Generic;
namespace CustomEventBus.Signals{
public class FillCombinationsSignal{
    public readonly List<CombinationObject> combinationsObjects;
    public FillCombinationsSignal(List<CombinationObject> CombinationsObjects){
        combinationsObjects = CombinationsObjects;
    }
}
}