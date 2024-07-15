using System.Collections;
using System.Collections.Generic;
using Lean.Pool;
using UnityEngine;

public class FireBall : Bullet
{
    protected override void OnCollisionEnter2D(Collision2D other)
    {
        base.OnCollisionEnter2D(other);
        gameObject.SetActive(false);
        EffectManager.Instance.ShowFireBlashVFX(transform);
    }
}
