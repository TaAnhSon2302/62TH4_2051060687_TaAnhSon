using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Pool;
public class ToxinBullet : Bullet
{
    //public static ToxinBullet instance;
    [SerializeField] public int toxinDamage = 40;
    [SerializeField] public float radius = 25;
    [SerializeField] private ToxinSplash toxinSplash;

    public int GetToxinDamage()
    {
        return toxinDamage;
    }
    public float GetRaidus()
    {
        return radius;
    }
    public void GetToxinStats(int damage, float radius)
    {
        this.toxinDamage = damage;
        this.radius = radius;
    }
    protected override void Awake()
    {
        base.Awake();
        //toxinSplash = Resources.Load<ToxinSplash>("Prefab/Bullet Prefabs/ToxinArea");
        toxinSplash.damage = toxinDamage;
        toxinSplash.transform.localScale = new Vector3(radius, radius, 0);
    }
    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "EnemyCell")
        {
            LeanPool.Spawn(toxinSplash, transform.position, transform.rotation);
            toxinSplash.damage = toxinDamage;
            toxinSplash.transform.localScale = new Vector3(radius, radius,0);
            gameObject.SetActive(false);        
        }
    }
}
