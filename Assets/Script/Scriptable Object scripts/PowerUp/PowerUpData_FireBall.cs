using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Power Up Data Fire Ball", menuName = "Scriptable Objects/Power Up Data/Fire Ball")]
public class PowerUpData_FireBall : PowerUpData
{
    public FireBallUpgrades[] fireBallUpgrades = new FireBallUpgrades[6];

    public override string UpdateDescription(int lv){
        base.UpdateDescription(lv);
        description = $"Launch a fire<color=#00ff00> {fireBallUpgrades[lv].ballsAmount} </color>ball(s) per second to enemies, cause <color=#00ff00>{fireBallUpgrades[lv].damage}%</color> DPS explosive damage.";
        return description;
    }
}
[Serializable]
public class FireBallUpgrades{
    public int damage;
    public int ballsAmount;
    
}