using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Power Up Data Clean map", menuName = "Scriptable Objects/Power Up Data/Clean map")]
public class PowerUpData_CleanMap : PowerUpData
{
    public MagneticUpgrades[] magneticUpgrades = new MagneticUpgrades[99];
    public override string UpdateDescription(int lv){
        base.UpdateDescription(lv);
        description = $"Sweep all nonboss enemy on map instantly";
        return description;
    }
}

