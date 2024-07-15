using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThunderBullet : Bullet
{
    protected override void Awake()
    {
        base.Awake();
    }
    public override void SetBullet(Transform gunPosition, Vector3 targetPosition, float accuracy)
    {
        base.SetBullet(gunPosition, targetPosition, accuracy);

    }
    protected override void OnCollisionEnter2D(Collision2D collision2D)
    {
        base.OnCollisionEnter2D(collision2D);
    }
}
