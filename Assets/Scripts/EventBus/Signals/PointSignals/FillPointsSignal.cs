using System.Collections.Generic;
namespace CustomEventBus.Signals{
public class FillPointsSignal{
    public List<PointObject> pointObjects;
    public FillPointsSignal(List<PointObject> PointObjects){
        pointObjects = PointObjects;
    }
}
}