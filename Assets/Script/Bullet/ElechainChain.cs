using System.Collections;
using System.Collections.Generic;
using Lean.Pool;
using UnityEngine;

public class ElechainChain : MonoBehaviour
{
    public SpriteRenderer sprite;
    public Transform jumpFromTarget;
    public Transform jumpToTarget;
    public LayerMask layerMask;
    public float scanRadius = 4f;
    public float jumpRate = 100;
    public int damage = 0;
    [SerializeField] private List<Transform> listEnemiesMarked;
    private void OnEnable()
    {
        listEnemiesMarked = new();
        jumpRate = 100;
    }
    public void InitChain(int damage,float jumpRate){
        this.damage = damage;
        this.jumpRate = jumpRate;
    }
    public void Jump(Transform jumpFromTarget, Transform jumpToTarget)
    {
        LeanTween.value(1f, 0, 0.1f)
        .setOnStart(() =>
        {
            listEnemiesMarked.Add(jumpToTarget);
            float distance = Vector3.Distance(jumpFromTarget.position, jumpToTarget.position);
            sprite.size = new Vector2(distance / 6, sprite.size.y);
            Vector3 direction = jumpToTarget.position - jumpToTarget.position;
            transform.position = (jumpFromTarget.position + jumpToTarget.position) / 2f;
            float angleRadians = Mathf.Atan2(jumpToTarget.position.y - jumpFromTarget.position.y, jumpToTarget.position.x - jumpFromTarget.position.x);
            transform.rotation = Quaternion.Euler(0, 0, Mathf.Rad2Deg * angleRadians);
            var enemy = jumpToTarget.GetComponent<EnemyCell>();
            enemy.TakeDamage(damage,0);
            enemy.SetStatusMachine(PrimaryElement.Electric,damage,1);
            this.jumpFromTarget = jumpToTarget;
            PrepareToNextJump();
        })
        .setOnUpdate((float value) =>
        {
            sprite.color = new Color(1, 1, 1, 1 * value);
        })
        .setOnComplete(() =>
        {
        });
    }

    public void PrepareToNextJump()
    {
        int randomRate = UnityEngine.Random.Range(0, 101);
        if (randomRate < jumpRate)
        {
            Collider2D[] enemyArray = Physics2D.OverlapCircleAll(jumpFromTarget.position, scanRadius, layerMask);
            if (enemyArray.Length == 0)
            {
                LeanPool.Despawn(this);
                return;
            }
            var nearestEnemy = enemyArray[0];
            for (int i = 1; i < enemyArray.Length; i++)
            {
                bool canJump = true;
                foreach (var enemy in listEnemiesMarked)
                {
                    if (enemyArray[i].gameObject == enemy.gameObject)
                    {
                        canJump = false;
                        break;
                    }
                }
                if (canJump == false) continue;
                if (Vector2.Distance(enemyArray[i].transform.position, jumpFromTarget.position) < Vector2.Distance(nearestEnemy.transform.position, jumpFromTarget.position))
                {
                    nearestEnemy = enemyArray[i];
                }
            }
            if(nearestEnemy == enemyArray[0]){
                LeanPool.Despawn(this);
                return;
            }
            LeanTween.delayedCall(0.1f, () => { Jump(jumpFromTarget, nearestEnemy.transform); });
        }
        else
            LeanPool.Despawn(this);
    }
}
