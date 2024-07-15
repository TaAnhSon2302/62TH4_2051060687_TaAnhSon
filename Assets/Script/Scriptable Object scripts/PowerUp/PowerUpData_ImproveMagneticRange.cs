using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Power Up Data Improve Magnetic Range", menuName = "Scriptable Objects/Power Up Data/Improve Magnetic Range")]
public class PowerUpData_ImproveMagneticRange : PowerUpData
{
    public MagneticUpgrades[] magneticUpgrades = new MagneticUpgrades[10];
    public override string UpdateDescription(int lv){
        base.UpdateDescription(lv);
        description = $"Increase obs collect radius to <color=#00ff00>{magneticUpgrades[lv].radius}%</color>";
        return description;
    }
}
[Serializable]
public class MagneticUpgrades{
    public float radius;
}
