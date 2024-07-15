using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp_006 : PowerUp
{
    private void Awake() {
        powerUpType = PowerUpType.Instant;
    }
    protected override void Start() {
        base.Start();
        OnBurst();
    }
    public override void OnLevelUp(int lv)
    {
        this.lv = lv;
        OnBurst();
    }

    protected override void OnFire()
    {
    }
    private void OnBurst(){
        // Debug.Log("nuke all them");
        UpdateManager.Instance.DestroyAllCell();
    }
}
