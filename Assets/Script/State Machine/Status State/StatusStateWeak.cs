using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameStatic;

public class StatusStateWeak : StatusState
{
    [SerializeField] private LayerMask layerToHit;
    public StatusStateWeak(CellsBase cellsBase, int stackAddIn) : base(cellsBase)
    {   
        if(stack>0){
            stack+=stackAddIn;
        }
        else if(stack>MAX_STATUS_STACK){
            stack = 10;
        }
        else{
            stack+=stackAddIn;
        }
        primaryElement = PrimaryElement.None;
        secondaryElement = SecondaryElement.Viral;
    }
    public override void Enter()
    {
        base.Enter();
        // Weaken();
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        
    }
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
    public override void Exit()
    {
        base.Exit();
    }
    // private void Weaken()
    // {

    // }
}
