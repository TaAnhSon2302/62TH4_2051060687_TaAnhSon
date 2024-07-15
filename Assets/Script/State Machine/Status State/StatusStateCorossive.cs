using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameStatic;

public class StatusStateCorrosive : StatusState
{
    [SerializeField] private LayerMask layerToHit;
    public StatusStateCorrosive(CellsBase cellsBase, int stackAddIn) : base(cellsBase)
    {   
        if(stack>0){
            stack+=stackAddIn;
        }
        else{
            stack+=stackAddIn;
        }
        primaryElement = PrimaryElement.None;
        secondaryElement = SecondaryElement.Corrosive;
    }
    public override void Enter()
    {
        base.Enter();
        Crossive();
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
    private void Crossive()
    {
        enemyCell.ArmorStrip((int)((float)(enemyCell.currentArmor.armorPoint*stack)*0.09f));
        // Debug.Log("Armor Shattered!");
    }
}
