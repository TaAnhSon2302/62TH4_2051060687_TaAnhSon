using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Pool;
using JetBrains.Annotations;
using Unity.VisualScripting;

public class PowerUp_004 : PowerUp
{
    [SerializeField] private ToxinBullet bullet;
    protected override void Start()
    {
        base.Start();
    }
    // private void FixedUpdate()
    // {
    //     FixedTimeCountdown();
    //     if (countdown <= 0)
    //     {
    //         OnFire();
    //     }
    // }
    protected override void OnFire()
    {
        Collider2D[] enemyArray = Physics2D.OverlapCircleAll(mutation.transform.position, scanRadius, layerMask);
        if (enemyArray.Length >= multishot)
        {
            // for (int i = 1; i < enemyArray.Length; i++)
            // {
            //     if (Vector2.Distance(enemyArray[i].transform.position, mutation.transform.position) < Vector2.Distance(nearestEnemy.transform.position, mutation.transform.position))
            //     {
            //         nearestEnemy = enemyArray[i];
            //     }
            // }
            for (int i = 0; i < multishot; i++)
            {
                var nearestEnemy = enemyArray[i];
                LeanTween.delayedCall(1f / multishot * i, () => {
                    ToxinBullet spawnedBullet = LeanPool.Spawn(bullet, transform.position, Quaternion.identity, GameManager.Instance.bulletHolder);
                    //spawnedBullet.damage = modifiedDamage;
                    Vector3 distance = nearestEnemy.transform.position - transform.position;
                    distance.Normalize();
                    float rotateZ = Mathf.Atan2(distance.y, distance.x) * Mathf.Rad2Deg;
                    transform.rotation = Quaternion.Euler(0f, 0f, rotateZ + 90);
                    spawnedBullet.GetToxinStats(modifiedDamage, scanRadius);
                    spawnedBullet.SetBullet(transform, nearestEnemy.transform.position, 100f);
                   // Debug.Log(modifiedDamage);
                    //Debug.Log(scanRadius);
                });
            }
        } 
    }
    public override void OnLevelUp(int lv)
    {
        this.lv = lv;
        PowerUpData_ToxinPlash powerUPdata = (PowerUpData_ToxinPlash)GameManager.Instance.listPlayerPowerUpDatas.Find(x => x.id == id);
        multishot = powerUPdata.toxinPlashUpgrades[this.lv].plashAmount;
        modifiedDamage = powerUPdata.toxinPlashUpgrades[this.lv].damage;
        scanRadius = powerUPdata.toxinPlashUpgrades[this.lv].raidus;
    }
}
