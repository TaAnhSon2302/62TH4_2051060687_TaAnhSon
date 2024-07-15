using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMeleeSlash : MonoBehaviour
{
    [SerializeField] private EnemyMeleeController enemyMeleeController;
    [SerializeField] private float pushBackForce = 20f;
    [SerializeField] public Collider2D collider2d;
    private void Awake() {
        collider2d = GetComponent<Collider2D>();
    }
    private void OnTriggerEnter2D(Collider2D other) {
        // Debug.Log("your are slashed!");
        if(other.CompareTag("Player")){
            Rigidbody2D playerRigidBody = other.GetComponent<Rigidbody2D>();
            other.GetComponent<Mutation>().TakeDamage(enemyMeleeController.EnemyCell.bodyDamage * 2);
            playerRigidBody.AddForce((other.transform.position - enemyMeleeController.transform.position).normalized*pushBackForce,ForceMode2D.Impulse);
        }
    }
}
