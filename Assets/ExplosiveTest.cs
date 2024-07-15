using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveTest : MonoBehaviour
{
    [SerializeField] private float impactField;
    [SerializeField] private float impactForce;
    [SerializeField] private float countdown = 10;
    public LayerMask layerToHit;
    private void Start() {
        explode();
    }
    private void FixedUpdate(){
        countdown-=Time.fixedDeltaTime;
        if(countdown<=0){
            countdown=1;
            explode();
        }
    }
    private void explode()
    {
        Collider2D[] objects = Physics2D.OverlapCircleAll(transform.position, impactField, layerToHit);
        for (int i = 0; i < objects.Length; i++)
        {
            Vector2 direction = (objects[i].transform.position - transform.position).normalized;
            var victim = objects[i].GetComponent<Rigidbody2D>();
            victim.AddForce(direction * impactForce, ForceMode2D.Impulse);
        }
    }
}
