using UnityEngine;

public class EnemyState : State
{
    protected EnemyCell enemyCell;
    public EnemyState(EnemyCell enemyCell){
        this.enemyCell = enemyCell;
    }
}
