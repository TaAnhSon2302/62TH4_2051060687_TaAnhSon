using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ABIL_002 : CellAbility
{
    protected override void Start() {
        base.Start();
        energyConsumption = 25;
    }
    protected override void AbilityBehavior()
    {
        base.AbilityBehavior();
        Debug.Log("ability 2");
    }
}
