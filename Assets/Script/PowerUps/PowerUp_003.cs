using System.Collections;
using System.Collections.Generic;
using Lean.Pool;
using UnityEngine;

public class PowerUp_003 : PowerUp
{
    // [SerializeField] private float jumpRate = 60;
    [SerializeField] private ElechainChain elechainChain;
    public override void OnLevelUp(int lv)
    {
        this.lv = lv;
        PowerUpData_Elechain powerUpData = (PowerUpData_Elechain)GameManager.Instance.listPlayerPowerUpDatas.Find(x => x.id == id);
        modifiedDamage = (int)((float)damage / powerUpData.elechainlUpgrades[0].damage * powerUpData.elechainlUpgrades[this.lv].damage);
        float jumpRate = powerUpData.elechainlUpgrades[this.lv].jumpRate;
    }

    protected override void OnFire()
    {
        Collider2D[] enemyArray = Physics2D.OverlapCircleAll(mutation.transform.position, scanRadius, layerMask);
        if(enemyArray.Length == 0) return;
        var nearestEnemy = enemyArray[0];
        for (int i = 1; i < enemyArray.Length; i++)
        {
            if (Vector2.Distance(enemyArray[i].transform.position, mutation.transform.position) < Vector2.Distance(nearestEnemy.transform.position, mutation.transform.position))
            {
                nearestEnemy = enemyArray[i];
            }
        }
        PowerUpData_Elechain powerUpData = (PowerUpData_Elechain)GameManager.Instance.listPlayerPowerUpDatas.Find(x => x.id == id);
        ElechainChain chain = LeanPool.Spawn(elechainChain,GameManager.Instance.bulletHolder);
        chain.InitChain(powerUpData.elechainlUpgrades[lv].damage,powerUpData.elechainlUpgrades[lv].jumpRate);
        chain.Jump(transform,nearestEnemy.transform);
    }
}
