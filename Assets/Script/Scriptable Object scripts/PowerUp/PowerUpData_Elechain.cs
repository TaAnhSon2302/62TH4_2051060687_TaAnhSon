using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Power Up Data Elechain", menuName = "Scriptable Objects/Power Up Data/Elechain")]
public class PowerUpData_Elechain : PowerUpData
{
    public ElechainlUpgrades[] elechainlUpgrades = new ElechainlUpgrades[6];

    public override string UpdateDescription(int lv){
        base.UpdateDescription(lv);
        description = $"Fire a chain of electricity to nearest enemy, cause <color=#00ff00>{elechainlUpgrades[lv].damage}%</color> damage. Every enemy inflicted have <color=#00ff00>{elechainlUpgrades[lv].jumpRate}%</color> chance to jump to other.";
        return description;
    }
}
[Serializable]
public class ElechainlUpgrades{
    public int damage;
    public int jumpRate;
    
}