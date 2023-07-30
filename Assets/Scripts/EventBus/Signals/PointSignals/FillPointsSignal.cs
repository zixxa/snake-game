using System.Collections.Generic;
namespace CustomEventBus.Signals{
public class FillPointsSignal{
    public List<PointPrefabData> pointObjects;
    public FillPointsSignal(List<PointPrefabData> PointObjects){
        pointObjects = PointObjects;
    }
}
}