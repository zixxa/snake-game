using System.Collections.Generic;
using System.Linq;
using CustomEventBus;
using CustomEventBus.Signals;
public class BodyLinker
{
    private EventBus _eventBus;
    private List<PointPrefabData> pointObjects;
    private List<BodyPrefabData> bodyObjects;
    public Dictionary<string, Body> linksByBodyAndPoints = new Dictionary<string, Body>();
    public Body GetBodyByPoint(Point point) => linksByBodyAndPoints[point.color.code];

    public void OnFillPoints(FillPointsSignal signal)
    {
        pointObjects = signal.pointObjects;
    }

    public void OnFillBodies(FillBodiesSignal signal)
    {
        bodyObjects = signal.bodyObjects;
    }

    public void OnInit()
    {
        linksByBodyAndPoints = Enumerable.Range(0, pointObjects.Count()
        ).ToDictionary(
            x => pointObjects[x].color.code,
            x => bodyObjects.Where(i => i.color == pointObjects[x].color).Last().prefab
        );

    }
}