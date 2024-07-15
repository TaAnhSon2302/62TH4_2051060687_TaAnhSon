using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Power Up data Toxin Plash", menuName = "Scriptable Objects/Power Up Data/Toxin Plash")]
public class PowerUpData_ToxinPlash : PowerUpData
{
    public ToxinPlashUpgrades[] toxinPlashUpgrades = new ToxinPlashUpgrades[6];
    public override string UpdateDescription(int lv)
    {
        base.UpdateDescription(lv);
        description = $"Create <color=#00ff00>{toxinPlashUpgrades[lv].plashAmount}</color> plash(s) dealing <color=#00ff00>{toxinPlashUpgrades[lv].damage}</color> damge for enemies in <color=#00ff00>{toxinPlashUpgrades[lv].raidus}</color>";
        return description ;
    }
}
[Serializable]
public class ToxinPlashUpgrades
{
    public float raidus;
    public int damage;
    public int plashAmount;
}
