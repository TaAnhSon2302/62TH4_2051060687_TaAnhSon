using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameStatic;

public class StatusStateNormal : StatusState
{
    public StatusStateNormal(CellsBase cellsBase) : base(cellsBase)
    {   

    }
    public override void Enter()
    {
        base.Enter();
        ResetState();
        enemyCell.moveSpeed = enemyCell.defaultMoveSpeed;
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        // Debug.Log("this is normal state");
    }
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        
    }
}
