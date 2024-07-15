using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameStatic;

public class StatusStateSuperConductive : StatusState
{
    [SerializeField] private LayerMask layerToHit;
    public StatusStateSuperConductive(CellsBase cellsBase, int initDamage, int stackAddIn) : base(cellsBase)
    {
        if (stack > 0)
        {
            stack += stackAddIn;
        }
        else
        {
            stack += stackAddIn;
            damagePerTick = initDamage;
        }
        primaryElement = PrimaryElement.None;
        secondaryElement = SecondaryElement.SuperConductive;
    }
    public override void Enter()
    {
        base.Enter();
        // Debug.Log("super conductive triggered");
        SuperConductive();
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
    private void SuperConductive()
    {
        // enemyCell.TakeDamage(damagePerTick * GameCalculator.CalculateFactorial(stack), 4, "Super conductive");
        enemyCell.TakeDamage(damagePerTick * stack * 5, 4, "Super conductive");
        // Debug.Log("Armor SuperConductive!");
    }

}
