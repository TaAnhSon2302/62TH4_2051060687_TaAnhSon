using UnityEngine;

public class EnemyStateShock : EnemyState
{
    float duration;
    public EnemyStateShock(EnemyCell enemyCell,float duration) : base(enemyCell)
    {
        this.duration = duration;
    }
    public override void Enter()
    {
        base.Enter();
        enemyCell.isRestrict = true;
        enemyCell.moveSpeed = 0;
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        duration -= Time.fixedDeltaTime;
        if(duration<=0){
            enemyCell.stateMachine.ChangeState(new EnemyStateMove(enemyCell));
            enemyCell.isRestrict = false;
        }
    }
}
