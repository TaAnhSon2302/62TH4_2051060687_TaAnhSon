using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameStatic;

public class StatusStateShattering : StatusState
{
    [SerializeField] private LayerMask layerToHit;
    public StatusStateShattering(CellsBase cellsBase, int stackAddIn) : base(cellsBase)
    {   
        if(stack>0){
            stack+=stackAddIn;
        }
        else{
            stack+=stackAddIn;
        }
        primaryElement = PrimaryElement.None;
        secondaryElement = SecondaryElement.Shattered;
    }
    public override void Enter()
    {
        base.Enter();
        Shatter();
        statusTimeLeft = 0;
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
    private void Shatter()
    {
        enemyCell.ArmorStrip((int)((float)(enemyCell.currentArmor.armorPoint*stack)*0.09f));
        // Debug.Log("Armor Shattered!");
    }
}
