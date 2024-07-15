using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Pool;
using UnityEngine.UIElements;

public class IceGun : CellGun
{
    // Start is called before the first frame update
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
