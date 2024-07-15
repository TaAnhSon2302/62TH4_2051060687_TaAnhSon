using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PowerUp_005 : PowerUp
{
    private void Awake() {
        powerUpType = PowerUpType.StartUp;
    }
    public override void OnLevelUp(int lv)
    {
        
        this.lv = lv;
        PowerUpData_ImproveMagneticRange powerUpData = (PowerUpData_ImproveMagneticRange)GameManager.Instance.listPlayerPowerUpDatas.Find(x=>x.id == id);
        mutation.obsCollectRangeAddIn = mutation.obsCollectRange * (powerUpData.magneticUpgrades[lv].radius/100);
    }

    protected override void OnFire()
    {
        return;
    }
}
