using System.Collections.Generic;
namespace CustomEventBus.Signals{
public class FillBodiesSignal{
    public List<BodyPrefabData> bodyObjects;
    public FillBodiesSignal(List<BodyPrefabData> BodyObjects){
        bodyObjects = BodyObjects;
    }
}
}