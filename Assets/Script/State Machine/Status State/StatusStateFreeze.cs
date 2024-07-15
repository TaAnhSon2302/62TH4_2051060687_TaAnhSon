using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameStatic;

public class StatusStateFreeze : StatusState
{
    public StatusStateFreeze(CellsBase cellsBase,PrimaryElement triggerElement, int stackAddIn, bool isOverrideMaxStack = false) : base(cellsBase)
    {   
        if(isOverrideMaxStack)
            maxStack = UNLIMITED_STATUS_STACK;
        else    
            maxStack = MAX_STATUS_STACK;
            stack+=stackAddIn;
        primaryElement = PrimaryElement.Ice;
    }
    public override void Enter()
    {
        base.Enter();
        if(enemyCell!=null)
            SlowPerTickToEnemy();
        else{

        }
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        
    }
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        if(enemyCell!=null)
            CountDown();
        else{

        }
    }
    public override void Exit()
    {
        base.Exit();
        enemyCell.moveSpeed = enemyCell.defaultMoveSpeed;
    }
    private void CountDown(){
        timeBetweenTick -= Time.fixedDeltaTime;
        if(timeBetweenTick<=0){
            stack--;
            timeBetweenTick += TIME_BETWEEN_STATUS_TICK;
            SlowPerTickToEnemy();
        }
    }
    private void SlowPerTickToEnemy(){
        enemyCell.moveSpeed = enemyCell.defaultMoveSpeed;
        enemyCell.moveSpeed *= 1 - (0.09f*stack);
        if(enemyCell.moveSpeed<0)
            enemyCell.moveSpeed = 0;
    }
}
