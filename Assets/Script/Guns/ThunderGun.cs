using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThunderGun : CellGun
{
    protected override void SetFire()
    {
        base.SetFire();
    }
    protected override IEnumerator OnFire()
    {
        yield return base.OnFire();
    }
    public override void GunRotation()
    {
        base.GunRotation();
    }
}
