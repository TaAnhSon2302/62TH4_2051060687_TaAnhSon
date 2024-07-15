using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameStatic;

public class StatusStatePoisoned : StatusState
{
    public StatusStatePoisoned(CellsBase cellsBase,PrimaryElement triggerElement,int initDamage, int stackAddIn) : base(cellsBase)
    {   
        if(stack>0){
            stack+=stackAddIn;
        }
        else{
            damagePerTick = initDamage/10;
            stack+=stackAddIn;
        }
        primaryElement = PrimaryElement.Toxin;
    }
    public override void Enter()
    {
        base.Enter();
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        
    }
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        if(enemyCell!=null)
            DamagePoisonedPerTickToEnemy();
        else{

        }
    }
    public override void Exit()
    {
        base.Exit();
    }
    private void DamagePoisonedPerTickToEnemy(){
        timeBetweenTick -= Time.fixedDeltaTime;
        if(timeBetweenTick<=0){
            timeBetweenTick += TIME_BETWEEN_STATUS_TICK;
            enemyCell.TakeDamage(damagePerTick,0,"Poisoned "+stack.ToString());
            
        }
    }
}
