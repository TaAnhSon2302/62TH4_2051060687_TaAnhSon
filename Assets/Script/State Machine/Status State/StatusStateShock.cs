using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameStatic;

public class StatusStateShock : StatusState
{
    public StatusStateShock(CellsBase cellsBase,PrimaryElement triggerElement,int initDamage, int stackAddIn, bool isOverrideMaxStack = false) : base(cellsBase)
    {
        maxStack = isOverrideMaxStack ? UNLIMITED_STATUS_STACK : MAX_STATUS_STACK;
        if (stack>0){
            stack+=stackAddIn;
        }
        else if(stack >= maxStack){
            stack = maxStack;
        }
        else{
            damagePerTick = initDamage/10;
            stack+=stackAddIn;
        }
        primaryElement = PrimaryElement.Electric;
    }
    public override void Enter()
    {
        base.Enter();
        EnemyShock();
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        
    }
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        if(enemyCell!=null)
            DamageShockPerTickToEnemy();
        else{

        }
    }
    public override void Exit()
    {
        base.Exit();
    }
    private void DamageShockPerTickToEnemy(){
        timeBetweenTick -= Time.fixedDeltaTime;
        if(timeBetweenTick<=0){
            timeBetweenTick += TIME_BETWEEN_STATUS_TICK;
            enemyCell.TakeDamage(damagePerTick,0,"Shock "+stack.ToString());
            stack--;
        }
    }
    private void EnemyShock(){
        if(stack>=10){
            enemyCell.stateMachine.ChangeState(new EnemyStateShock(enemyCell,(float)stack/10));
        }
    }
}
