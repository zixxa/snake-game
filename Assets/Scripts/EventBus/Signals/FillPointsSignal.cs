using System.Collections.Generic;
namespace CustomEventBus.Signals{
public class FillPointsSignal{
    public List<PointObject> pointsObjects;
    public FillPointsSignal(List<PointObject> PointsObjects){
        pointsObjects = PointsObjects;
    }
}
}