using System.Collections.Generic;
namespace CustomEventBus.Signals{
public class FillEnemiesSignal{
    public List<EnemyData> enemyObjects;
    public FillEnemiesSignal(List<EnemyData> EnemyObjects){
        enemyObjects = EnemyObjects;
    }
}
}