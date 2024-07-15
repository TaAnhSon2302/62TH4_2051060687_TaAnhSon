using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PowerUp : MonoBehaviour
{
    public string id;
    public Mutation mutation;
    [SerializeField] protected PowerUpType powerUpType = PowerUpType.Weapon;
    [SerializeField] protected float timeCharge = 1f;
    [SerializeField] protected float countdown = 0f;
    [SerializeField] protected int damage = 0;
    [SerializeField] protected int modifiedDamage;
    [SerializeField] protected int multishot;
    [SerializeField] public int lv = 0;
    [SerializeField] protected float scanRadius = 10f;
    [SerializeField] protected int[] layerMaskInt;
    [SerializeField] protected LayerMask layerMask;
    protected virtual void Start()
    {
        mutation = GetComponentInParent<Mutation>();
        lv = 0;
        modifiedDamage = damage;
        multishot = 1;
    }
    protected virtual void FixedUpdate()
    {
        FixedTimeCountdown();
        if (countdown <= 0)
        {
            OnFire();
        }
    }
    protected virtual void FixedTimeCountdown()
    {
        if (countdown <= 0)
        {
            countdown = timeCharge;
        }
        countdown -= Time.fixedDeltaTime;
    }
    protected abstract void OnFire();
    public abstract void OnLevelUp(int lv);
}

[Serializable]
public enum PowerUpType
{
    StartUp,
    Equipment,
    Weapon,
    Instant,
}
