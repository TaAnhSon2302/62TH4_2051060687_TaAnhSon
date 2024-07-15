using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameStatic;

public class StatusStateBurn : StatusState
{
    public StatusStateBurn(CellsBase cellsBase,PrimaryElement triggerElement,int initDamage, int stackAddIn) : base(cellsBase)
    {   
        if(stack>0){
            stack+=stackAddIn;
        }
        else{
            damagePerTick = initDamage/10;
            stack+=stackAddIn;
        }
        primaryElement = PrimaryElement.Fire;
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
            DamageBurnPerTickToEnemy();
        else{

        }
    }
    public override void Exit()
    {
        base.Exit();
    }
    private void DamageBurnPerTickToEnemy(){
        timeBetweenTick -= Time.fixedDeltaTime;
        if(timeBetweenTick<=0){
            timeBetweenTick += TIME_BETWEEN_STATUS_TICK;
            enemyCell.TakeDamage(damagePerTick,0,"Burn "+stack.ToString());
            
        }
    }
}
