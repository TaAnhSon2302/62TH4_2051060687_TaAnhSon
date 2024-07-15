using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Pool;

public class ShurikenGun : CellGun
{
    [SerializeField] private int pelletCount = 5;
    [SerializeField] private float spreadAngle = 45f;
    protected override void SetFire()
    {
        if (isGunReady)
        {
            isGunReady = false;
            StartCoroutine(OnFire());
        }
    }
    protected override IEnumerator OnFire()
    {
        float angleIncrement = spreadAngle / (pelletCount - 1);
        float startAngle = -spreadAngle / 2f;
        AudioManager.Instance.PlayGunFire();
        for (int i = 0; i < pelletCount; i++)
        {
            float currentAngle = startAngle + i * angleIncrement;
            Vector2 direction = Quaternion.Euler(0, 0, currentAngle) * transform.up;
            Quaternion bulletRotation = Quaternion.Euler(0f, 0f, startAngle + i * angleIncrement);
            Bullet bullet = LeanPool.Spawn(bulletPrefab, transform.position, transform.rotation, GameManager.Instance.bulletHolder);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.velocity = direction * bullet.Speed;
            bullet.cellGun = this;
            bullet.SetBullet(transform, InputManager.Instance.mouseWorldPosition, accuracy);
        }
        yield return new WaitForSeconds(1 / fireRate);
        isGunReady = true;
    }
    public override void GunRotation()
    {
        base.GunRotation();
    }
}
