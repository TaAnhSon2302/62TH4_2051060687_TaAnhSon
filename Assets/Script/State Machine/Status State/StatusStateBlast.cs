using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameStatic;

public class StatusStateBlast : StatusState
{
    private float impactField = 3;
    private float impactForce = 30;
    [SerializeField] private LayerMask layerToHit;
    public StatusStateBlast(CellsBase cellsBase, int initDamage, int stackAddIn) : base(cellsBase)
    {   
        if(stack>0){
            stack+=stackAddIn;
        }
        else{
            damagePerTick = initDamage;
            stack+=stackAddIn;
        }
        primaryElement = PrimaryElement.None;
        secondaryElement = SecondaryElement.Blast;
    }
    public override void Enter()
    {
        base.Enter();
        Explode();
        statusTimeLeft = 0;
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        
    }
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        // if(enemyCell!=null)
        //     DamageBurnPerTickToEnemy();
        // else{

        // }
    }
    public override void Exit()
    {
        base.Exit();
    }
    private void Explode()
    {
        layerToHit = LayerMask.GetMask("Enemy","EnemyWraith");
        
        Collider2D[] objects = Physics2D.OverlapCircleAll(enemyCell.transform.position, impactField, layerToHit);
        for (int i = 0; i < objects.Length; i++)
        {
            Vector2 direction = (objects[i].transform.position - this.enemyCell.transform.position).normalized;
            var victim = objects[i].GetComponent<Rigidbody2D>();
            victim.AddForce(direction * impactForce, ForceMode2D.Impulse);
            EnemyCell enemyCell = objects[i].gameObject.GetComponent<EnemyCell>();
            enemyCell.TakeDamage(damagePerTick*stack,5,"Blast!!!");
        }
    }
}
