using System.Collections.Generic;
namespace CustomEventBus.Signals{
public class FillCombinationsSignal{
    public readonly List<CombinationData> combinationsObjects;
    public FillCombinationsSignal(List<CombinationData> CombinationsObjects){
        combinationsObjects = CombinationsObjects;
    }
}
}