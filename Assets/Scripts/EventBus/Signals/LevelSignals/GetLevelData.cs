namespace CustomEventBus.Signals
{
    public class GetLevelData
    {
        public readonly LevelData LevelData;

        public GetLevelData(LevelData levelData)
        {
            LevelData = levelData;
        }
    }
}