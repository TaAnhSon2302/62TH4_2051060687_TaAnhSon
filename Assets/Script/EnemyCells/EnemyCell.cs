using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Lean.Pool;
using TMPro;
using System;
using static GameCalculator;
using Unity.VisualScripting;

public class EnemyCell : CellsBase
{
    [Header("Enemy Properties")]
    [SerializeField] protected Rigidbody2D rigidbody2d;
    [SerializeField] protected Collider2D collider2d;
    [SerializeField] public bool isBoss = false;
    [SerializeField] public int bodyDamage { get; protected set; } = 0;
    [SerializeField] protected int XpObs;
    [SerializeField] public bool isRestrict = false;
    [SerializeField] protected int index;
    [SerializeField] protected Equipment equipment;
    [SerializeField] public StateMachine stateMachine;
    [SerializeField] protected Animator animator;
    [SerializeField] protected GameObject destroyAnimation;
    [Space(10)]
    [Header("UI")]
    [SerializeField] protected string enemyId;
    [SerializeField] public string enemyName;
    [SerializeField] public Slider healthBar;
    [SerializeField] public Slider shieldBar;
    [SerializeField] protected TextMeshProUGUI healthText;
    [SerializeField] protected SpriteRenderer model;
    [Space(10)]
    [Header("Attack")]
    [SerializeField] protected EnemyMeleeController meleeController;
    [SerializeField] public MeleeRangeRenderer meleeRangeRenderer;
    [SerializeField] protected EnemyRangeController rangeController;
    [Space(10)]
    [Header("Wave")]
    [SerializeField] public int wave;
    [SerializeField] public bool dotStatus = false;
    #region Initial & Update
    protected override void Start()
    {
        base.Start();
        destroyAnimation.SetActive(false);
        meleeController.gameObject.SetActive(equipment == Equipment.Melee ? true : false);
        rangeController.gameObject.SetActive(equipment == Equipment.Range ? true : false);
        meleeRangeRenderer.radius = meleeController.detectedRange;
    }
    protected override void OnEnable()
    {
        base.OnEnable();
        AddProperties();
        UpdateManager.Instance.AddCellToPool(this);
        index = UpdateManager.Instance.poolIndex;
        Reset();
    }
    private void Reset()
    {
        healPoint = maxHealth;
        currentArmor.armorType = baseCellArmor.armorType;
        currentArmor.armorPoint = BioArmorCalculating();
        model.color = new Color(1, 1, 1, 1);
        collider2d.enabled = true;
        SetStatusMachine(PrimaryElement.None);
        meleeController.gameObject.SetActive(equipment == Equipment.Melee ? true : false);
    }
    public void CellUpdate()
    {
        // healthText.text = healPoint.ToString();
        stateMachine.StateMachineUpdate();
        Spawner.Instance.Reposition(transform);
        if (equipment == Equipment.Melee)
        {
            meleeRangeRenderer?.DrawArc();
            meleeController?.SlashCheck();
        }
    }
    public void CellFixedUpdate()
    {
        // need to upgrade to grand overshield (may be overguard for health too)
        healthBar.value = (float)healPoint / (float)maxHealth;
        if (baseCellArmor.shieldPoint != 0)
            shieldBar.value = (float)currentArmor.shieldPoint / (float)baseCellArmor.shieldPoint;
        else
            shieldBar.value = 0;
        movement();
        StateMachineMonitor();
        stateMachine.StateMachineFixedUpdate();
        currentElementStack = stateMachine.currentStatusState.stack;
        currentPrimaryElement = stateMachine.currentStatusState.primaryElement;
        currentSecondaryElement = stateMachine.currentStatusState.secondaryElement;
        if (healPoint <= 0)
        {
            stateMachine.ChangeState(new EnemyStateDestroy(this));
        }
    }
    protected void AddProperties()
    {
        if (DataManager.Instance.Data.listEnemies.Exists(x => x.enemyId == this.enemyId))
        {
            EnemyCellOOP enemyCellOOP = DataManager.Instance.Data.listEnemies.Find(x => x.enemyId == this.enemyId);
            maxHealth = enemyCellOOP.hp;
            enemyName = enemyCellOOP.enemyName;
            healPoint = maxHealth;
            moveSpeed = enemyCellOOP.moveSpeed;
            defaultMoveSpeed = enemyCellOOP.moveSpeed;
            baseCellArmor = new CellProtection(enemyCellOOP.cellProtection);
            currentArmor = new CellProtection(baseCellArmor);
            faction = enemyCellOOP.faction;
            equipment = enemyCellOOP.equipment;
            bodyDamage = enemyCellOOP.bodyDamage;
            XpObs = enemyCellOOP.XpObs;
        }
    }
    #endregion
    #region Function
    public void movement()
    {
        Vector3 moveDirection = (GameManager.Instance.mutation.transform.position - transform.position).normalized;
        // if (equipment == Equipment.Range)
        // {
        //     Vector3 distance = (GameManager.Instance.mutation.transform.position - transform.position).normalized * rangeController.range;
        //     moveDirection = (moveDirection - (distance + transform.position)).normalized;
        // }
        if(equipment == Equipment.Range){
            float directionMagnitude = (GameManager.Instance.mutation.transform.position - transform.position).magnitude;
            moveDirection *= directionMagnitude > rangeController.range ? 1 : -1;
            if(Mathf.Abs(directionMagnitude- rangeController.range)<rangeController.range/10){
                moveDirection = Vector3.zero;
            }
        }
        // rigidbody2d.MovePosition((Vector2)transform.position + ((Vector2)moveDirection * moveSpeed * Time.deltaTime));
        // rigidbody2d.velocity = moveDirection * moveSpeed;
        float currentForce = moveSpeed - rigidbody2d.velocity.magnitude;
        if (rigidbody2d.velocity.magnitude > 0)
        {
            friction = rigidbody2d.velocity * -1;
            rigidbody2d.AddForce(friction * 10f * rigidbody2d.mass);
        }
        if (moveSpeed < rigidbody2d.velocity.magnitude) return;
        rigidbody2d.AddForce(moveDirection * currentForce * 20 * rigidbody2d.mass);
    }
    public void TakeDamage(int damageIncome, int criticalTier, string status = null, bool isToShieldOnly = false)
    {
        (int, int) damageTaken;
        if (stateMachine.currentStatusState.secondaryElement == SecondaryElement.Viral)
            damageIncome += (int)(0.3f * (float)damageIncome * stateMachine.currentStatusState.stack);
        damageTaken = DamageTake(currentArmor, BioArmorCalculating(), damageIncome);
        currentArmor.armorPoint -= damageTaken.Item2;
        if (currentArmor.shieldPoint > 0)
        {
            currentArmor.shieldPoint -= damageTaken.Item1;
            criticalTier = 6;
        }
        else
        {
            if (!isToShieldOnly)
            {
                healPoint -= damageTaken.Item1;
            }
        }
        EffectManager.Instance.ShowDamageInfict(damageTaken.Item1, criticalTier, transform, status);
    }
    public void ArmorStrip(int Amount)
    {
        if (currentArmor.armorType == ArmorType.None)
            return;
        else if (currentArmor.armorType == ArmorType.Alloy)
        {
            currentArmor.armorPoint -= Amount;
        }
        else if (currentArmor.armorType == ArmorType.Bio)
        {
            currentArmor.armorPoint -= Amount;
        }
    }
    public override void OnDead()
    {
        base.OnDead();
        moveSpeed = 0;
        UpdateManager.Instance.RemoveCellFromPool(index);
        rigidbody2d.velocity = Vector3.zero;
        collider2d.enabled = false;
        destroyAnimation.SetActive(true);
        animator.SetTrigger("Destroy");
        model.color = new Color(0, 0, 0, 0);
        EffectManager.Instance.SpawnObs(gameObject, XpObs);
        stateMachine.ChangeStatusState(new StatusStateNormal(this));
        LeanTween.delayedCall(1f, () =>
        {
            try
            {
                EnemySpawner.Instance.OnEnemyDestroy(this);
                LeanPool.Despawn(this);
            }
            catch (Exception e)
            {
                Debug.LogWarning(e);
            }
        });
    }
    #endregion
    #region State Machine
    public void SetStatusMachine(PrimaryElement element, int damageIncome = 0, int stack = 0, bool isOverrideMaxStack = false)
    {
        if (element == PrimaryElement.None)
            stateMachine.ChangeStatusState(new StatusStateNormal(this));
        if (stateMachine.currentStatusState != null && stateMachine.currentStatusState.CurrentStatusLevel() == 2) return;
        switch (element)
        {
            case PrimaryElement.Fire:
                stateMachine.ChangeStatusState(ElementReact(stateMachine.currentStatusState, new StatusStateBurn(this, PrimaryElement.Fire, damageIncome, stack)));
                break;
            case PrimaryElement.Ice:
                stateMachine.ChangeStatusState(ElementReact(stateMachine.currentStatusState, new StatusStateFreeze(this, PrimaryElement.Ice, stack, isOverrideMaxStack)));
                break;
            case PrimaryElement.Toxin:
                stateMachine.ChangeStatusState(ElementReact(stateMachine.currentStatusState, new StatusStatePoisoned(this, PrimaryElement.Toxin, damageIncome, stack)));
                break;
            case PrimaryElement.Electric:
                stateMachine.ChangeStatusState(ElementReact(stateMachine.currentStatusState, new StatusStateShock(this, PrimaryElement.Electric, damageIncome, stack, isOverrideMaxStack)));
                break;
            default:
                // if(stateMachine.currentStatusState!= null && stateMachine.currentStatusState.secondaryElement!= SecondaryElement.None)
                //     break;
                stateMachine.ChangeStatusState(new StatusStateNormal(this));
                break;
        }
    }
    protected void StateMachineMonitor()
    {
        if (isRestrict) return;
        if (rigidbody2d.velocity == Vector2.zero)
        {
            stateMachine.ChangeState(new EnemyStateIdle(this));
        }
        else if (rigidbody2d.velocity != Vector2.zero)
        {
            stateMachine.ChangeState(new EnemyStateMove(this));
        }

    }
    #endregion
    // protected void Init(){
    //     foreach(var enemy in DataManager.Instance.Data.listEnemies){
    //         if(enemyId == enemy.enemyId){

    //         }
    //     }
    // }


    #region Component gadget
    [ContextMenu("AutoFillFields")]
    public void AutoFillFields()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        collider2d = GetComponent<Collider2D>();
        stateMachine = GetComponent<StateMachine>();
        model = GetComponent<SpriteRenderer>();
        meleeController = GetComponentInChildren<EnemyMeleeController>();
        animator = GetComponentInChildren<Animator>();
    }
    #endregion
}

[Serializable]
public class EnemyCellOOP
{
    public string enemyId;
    public string enemyName;
    public int hp;
    public int mp;
    public CellProtection cellProtection;
    public float moveSpeed;
    public string abilityId;
    public Faction faction;
    public Equipment equipment;
    public int XpObs;
    public EnemyCellOOP()
    {
        cellProtection = new CellProtection();
    }
    public int bodyDamage;
}
[Serializable]
public enum Equipment
{
    None,
    Melee,
    Range
}