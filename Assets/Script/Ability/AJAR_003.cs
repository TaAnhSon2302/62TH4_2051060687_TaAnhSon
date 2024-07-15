using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AJAR_003 : CellAbility
{
    protected override void Start() {
        base.Start();
        energyConsumption = 25;
    }
    protected override void AbilityBehavior()
    {
        base.AbilityBehavior();
        Debug.Log(this.GetType().Name);
        mutation.moveSpeed*=10;
        LeanTween.delayedCall(3f,()=>{
            mutation.moveSpeed/=10;
        });
    }
}
