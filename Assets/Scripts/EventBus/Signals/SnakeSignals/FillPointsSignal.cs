using System.Collections.Generic;
namespace CustomEventBus.Signals{
public class FillBodiesSignal{
    public List<BodyObject> bodyObjects;
    public FillBodiesSignal(List<BodyObject> BodyObjects){
        bodyObjects = BodyObjects;
    }
}
}