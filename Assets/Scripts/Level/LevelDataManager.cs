using UnityEngine;
using System.Collections.Generic;
using System;
using System.Linq;
using CustomEventBus;
using CustomEventBus.Signals;

namespace Levels{
public class LevelDataManager: IService
{
    private const string PrefabsFilePath = "Levels/";
    private static List<string> PrefabsLevel = new List<string>()
    {
        "Level1",
        "Level2",
        "Level3"
    };
    public int LevelsCount => PrefabsLevel.Count();
    private EventBus _eventBus;
    public void Init()
    {
        _eventBus = ServiceLocator.Current.Get<EventBus>();
    }
    public T GetLevel<T>(int levelId) where T: Level
    {
        var prefabsLevelName = PrefabsLevel[levelId];
        var path = PrefabsFilePath + prefabsLevelName;
        var level = Resources.Load<T>(path);
        if (level == null)
        {
            Debug.LogError("Cant find prefab at path " + path);
        }
        return level;
    }
    public T ShowLevel<T>(int levelId) where T: Level
    {
        var go = GetLevel<T>(levelId);
        if (go == null)
        {
            Debug.LogError("Show level - object not found");
            return null;
        }
        return GameObject.Instantiate(go, LevelHolder);
    }
    public static Transform LevelHolder
    {
        get { return ServiceLocator.Current.Get<LevelHolder>().transform; }
    }
}
}