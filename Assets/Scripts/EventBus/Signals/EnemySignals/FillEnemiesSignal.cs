using System.Collections.Generic;
namespace CustomEventBus.Signals{
public class FillEnemiesSignal{
    public List<EnemyObject> enemyObjects;
    public FillEnemiesSignal(List<EnemyObject> EnemyObjects){
        enemyObjects = EnemyObjects;
    }
}
}