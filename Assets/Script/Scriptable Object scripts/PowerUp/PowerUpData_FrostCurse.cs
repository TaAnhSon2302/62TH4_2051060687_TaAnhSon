using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Power Up Data Frost Curse", menuName = "Scriptable Objects/Power Up Data/Frost Curse")]
public class PowerUpData_FrostCurse : PowerUpData
{
    public FrostCurseUpgrades[] frostCurseUpgrades = new FrostCurseUpgrades[6];

    public override string UpdateDescription(int lv){
        base.UpdateDescription(lv);
        description = $"Form a <color=#00ff00>{frostCurseUpgrades[lv].radius}</color> m ice field around mutation, cause <color=#00ff00>{frostCurseUpgrades[lv].damage}%</color> DPS per <color=#00ff00>{frostCurseUpgrades[lv].delay}s</color>";
        return description;
    }
}
[Serializable]
public class FrostCurseUpgrades{
    public int damage;
    public float radius;
    public float delay;
    
}