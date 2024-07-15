using System.Collections;
using UnityEngine;
using Lean.Pool;
using static GameStatic;
using System;
using Random = UnityEngine.Random;
public class Bullet : MonoBehaviour
{

    [SerializeField] protected float bulletSpeed = 20f;
    [SerializeField] public int damage = 20;
    [SerializeField] public CellGun cellGun;
    [SerializeField] protected Elements elements;
    [SerializeField] protected float timeExist = 2f;
    [SerializeField] protected bool isProjectile = true;
    [SerializeField] protected Rigidbody2D rigidbody2d;
    [SerializeField] protected BoxCollider2D bulletCollider2D;
    [SerializeField] protected TrailRenderer bulletTrail;
    [SerializeField] protected SpriteRenderer sprite;
    [SerializeField] protected string bulletId;
    [SerializeField] protected string bulletTypeId;
    [SerializeField] protected string bulletName;
    [SerializeField] protected bool isExplosive = false;
    [SerializeField] protected float impactField = 2f;
    [SerializeField] protected float impactForce = 15f;
    [SerializeField] protected float countdown = 10;
    [SerializeField] protected LayerMask layerToHit;
    [SerializeField] protected bool isPenetration = false;

    public float Speed
    {
        get
        {
            return bulletSpeed;
        }
        set
        {
            bulletSpeed = value;
        }
    }
    public Elements Elements {get => elements;}
    protected virtual void Awake(){
        rigidbody2d = GetComponent<Rigidbody2D>();
        if(rigidbody2d == null){
            rigidbody2d = gameObject.AddComponent<Rigidbody2D>();
        }
        rigidbody2d.gravityScale = 0;
        AddProperties();
    }
    public void BulletMovement()
    {

    }
    private void Start() {

       
    }
    public void AddProperties()
    {
        if(DataManager.Instance.Data.listBullet.Exists(x => x.bulletId == this.bulletId))
        {
            BulletOOP bulletOOP = DataManager.Instance.Data.listBullet.Find(x => x.bulletId == this.bulletId);
            this.bulletName = bulletOOP.bulletName;
            this.bulletSpeed = bulletOOP.bulletSpeed;
            this.damage = bulletOOP.damage;
            this.bulletTypeId = bulletOOP.bulletTypeId;
            this.timeExist = bulletOOP.timeExist;
           this.elements.primaryElement = bulletOOP.element.primaryElement;
            //this.elements.secondaryElement = bulletOOP.element.secondaryElement;
        }
    }
    
    public virtual void SetBullet(Transform gunPosition,Vector3 targetPosition, float accuracy)
    {
        float spreadAngle = GUN_MAX_SPREAD_ANGLE - GUN_MAX_SPREAD_ANGLE / 100 * accuracy;
        float randomAngle = Random.Range(-spreadAngle, spreadAngle + 1);
        Vector2 bulletDirection = targetPosition - gunPosition.position;
        Quaternion quaternion = Quaternion.Euler(0f,0f,randomAngle);
        bulletDirection = quaternion * bulletDirection;
        bulletDirection.Normalize();
        transform.rotation = quaternion * gunPosition.rotation;
        bulletCollider2D.enabled = true;
        if(bulletTrail!=null)
            bulletTrail.Clear();
        sprite.color = Color.white;
        rigidbody2d.AddForce(bulletDirection * bulletSpeed,ForceMode2D.Impulse);
        LeanTween.delayedCall(timeExist, () =>
        {
            try{
                LeanPool.Despawn(gameObject);
            }
            catch(Exception e){
                Debug.LogWarning(e);
            }
        });
    }
    protected virtual void Explode()
    {
        Collider2D[] objects = Physics2D.OverlapCircleAll(transform.position, impactField, layerToHit);
        for (int i = 0; i < objects.Length; i++)
        {
            Vector2 direction = (objects[i].transform.position - transform.position).normalized;
            var victim = objects[i].GetComponent<Rigidbody2D>();
            victim.AddForce(direction * impactForce, ForceMode2D.Impulse);
            EnemyCell enemyCell = objects[i].gameObject.GetComponent<EnemyCell>();
            (float,int) critical = GameCalculator.CriticalManager(cellGun);
            enemyCell.TakeDamage((int)(critical.Item1*damage),critical.Item2);
            enemyCell.SetStatusMachine(elements.primaryElement,(int)(critical.Item1 * damage),1);
            bulletCollider2D.enabled = false;
        }
    }
    protected virtual void OnCollisionEnter2D(Collision2D collision2D) {
        if(collision2D.gameObject.tag=="EnemyCell" && gameObject.tag != "EnemyBullet"){
            if(!isExplosive){
                EnemyCell enemyCell = collision2D.gameObject.GetComponent<EnemyCell>();
                (float,int) critical = GameCalculator.CriticalManager(cellGun);
                enemyCell.TakeDamage((int)(critical.Item1*damage),critical.Item2);
                enemyCell.SetStatusMachine(elements.primaryElement,(int)(critical.Item1 * damage),1);
                bulletCollider2D.enabled = false;
            }
            else{
                Explode();
            }
        }
        else if(collision2D.gameObject.CompareTag("Player") && gameObject.tag == "EnemyBullet"){
            collision2D.gameObject.GetComponent<Mutation>().TakeDamage(damage);
        }
    }
    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "EnemyCell")
        {
            EnemyCell enemyCell = collision.gameObject.GetComponent<EnemyCell>();
            (float, int) critical = GameCalculator.CriticalManager(cellGun);
            enemyCell.TakeDamage((int)(critical.Item1 * damage), critical.Item2);
            enemyCell.SetStatusMachine(elements.primaryElement, (int)(critical.Item1 * damage), 1);
        }
    }
}
[Serializable]
public class BulletOOP
{
    public string bulletId;
    public string bulletName;
    public string bulletTypeId;
    public int damage;
    public float timeExist;
    public int bulletSpeed;
    public Elements element = new();
}
