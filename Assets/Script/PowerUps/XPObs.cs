using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Lean.Pool;

public class XPObs : MonoBehaviour
{
    [SerializeField] Collision2D collision;
    // [SerializeField] Rigidbody2D rb;
    [SerializeField] SpriteRenderer model;
    [SerializeField] int xPContain = 0;
    [SerializeField] float moveSpeed = 0f;
    [SerializeField] float acceleration = 0.01f;
    [SerializeField] float fadeTime = 0.3f;
    public bool isPulling = false;

    private void Start()
    {

    }
    private void OnEnable()
    {
        
    }
    public void StartMovement()
    {
        isPulling = true;
        StartCoroutine(IEOnObsMove());
    }
    public void OnConsumption()
    {
        Vector2 temp = transform.localScale;
        Color color = model.color;
        isPulling = false;
        LeanTween.value(gameObject, 0, 1, fadeTime).setOnStart(() =>
        {
            GameManager.Instance.OnObsCollect(xPContain);
        }).setOnUpdate((float value) =>
        {
            transform.localScale = temp * (value + 2);
            model.color = new Color(model.color.r, model.color.g, model.color.b, 1f - value);
        }).setOnComplete(() =>
        {
            moveSpeed = 0;
            transform.localScale = temp;
            model.color = color;
            gameObject.layer = LayerMask.NameToLayer("Obs");
            LeanPool.Despawn(gameObject);
        });
    }
    public IEnumerator IEOnObsMove()
    {
        while (isPulling)
        {
            yield return new WaitForEndOfFrame();
            if (Time.timeScale > 0)
            {
                Vector2 moveDirection = GameManager.Instance.mutation.transform.position - transform.position;
                if (moveDirection.magnitude < 1)
                {
                    OnConsumption();
                    
                }
                moveDirection.Normalize();
                // rb.velocity = moveDirection * (moveSpeed += acceleration);

                moveSpeed += acceleration * Time.deltaTime;
                float moveDistance = moveSpeed;
                transform.Translate(moveDirection * moveDistance, Space.World);
            }

        }
        // while (isPulling)
        // {
        //     yield return new WaitForFixedUpdate();
        //     Vector2 moveDirection = GameManager.Instance.mutation.transform.position - transform.position;
        //     if(moveDirection.magnitude<1){
        //         OnConsumption();
        //         GameManager.Instance.OnObsCollect(xPContain);
        //     }
        //     moveDirection.Normalize();
        //     rb.velocity = moveDirection * (moveSpeed += acceleration);
        // }
        // rb.velocity = Vector3.zero;
    }
    // private void OnTriggerEnter2D(Collider2D other)
    // {
    //     if (other.gameObject.tag == "Player")
    //     {
    //         OnConsumption();
    //         GameManager.Instance.OnObsCollect(xPContain);
    //     }
    // }
}
