using UnityEngine;
using CustomEventBus;
using CustomEventBus.Signals;
public class Head: Segment
{
    public bool enemyTouched;
    public Head()
    {
        enemyTouched = false;
    }
    void GameOnStart(GameStartSignal signal)
    {
    }
}