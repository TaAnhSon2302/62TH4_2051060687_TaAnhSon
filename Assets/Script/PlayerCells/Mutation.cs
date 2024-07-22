using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEditor;
using Unity.VisualScripting;
using System.Linq;

public class Mutation : CellsBase
{
    [SerializeField] public Rigidbody2D playerRigidbody2d;
    [SerializeField] public string mutationId;
    [SerializeField] protected string mutationName;
    [SerializeField] protected StateMachine stateMachine;
    [SerializeField] protected List<CellAbility> mutationAbilities;
    [SerializeField] protected LayerMask layerAffectByShieldPulse;
    [SerializeField] protected int layerObs;
    public float obsCollectRange {get;protected set;} = 5f;
    public float obsCollectRangeAddIn = 0f;
    private float impactField = 10f;
    private float impactForce = 100f;
    protected float pushBackForce = 20f;
    protected float shipAngle = 0f;
    protected bool isMoving = true;
    protected bool isShieldPulseCharged = true;
    protected bool isDelaying = false;
    protected float delayTime = 0;
    public float rotationInterpolation = 0.4f;
    #region Initial & Update
    protected override void Awake()
    {
        base.Awake();
    }
    protected virtual void Update(){
        StateMachineMonitor();
        AbilityTrigger();
        ObsDectector();
    }
    protected virtual void FixedUpdate() {
        isMoving = true;
        if(InputManager.Instance.GetArrowButton() == Vector3.zero){
            isMoving = false;
        }
        PlayerMovement();
        PlayerRotation();
        ShieldRecharge();
        ShieldDelay();
    }
    protected override void Start(){
        base.Start();
        stateMachine = GetComponent<StateMachine>();
        playerRigidbody2d = GetComponent<Rigidbody2D>();
        AddProperties();
        ShowProterties();
        stateMachine.ChangeState(new PlayerStateIdle(this));
        shieldRechargeRate = GameCalculator.ShieldRechargeCalculator(baseCellArmor.shieldPoint);
        layerObs = LayerMask.GetMask("Obs");
        
    }
    #endregion
    #region Function
    protected void AbilityTrigger(){
        if(InputManager.Instance.Ability1Button)
            mutationAbilities[0].AbilityCast();
        if(InputManager.Instance.Ability2Button)
            mutationAbilities[1].AbilityCast();
        if(InputManager.Instance.Ability3Button)
            mutationAbilities[2].AbilityCast();
    }
    protected virtual void Ability1(){
        Debug.Log("Ability 1 triggered!");
    }
    protected virtual void Ability2(){
        Debug.Log("Ability 2 triggered!");

    }
    protected virtual void Ability3(){
        Debug.Log("Ability 3 triggered!");

    }
    public void ObsDectector(){
        float obsCollectRange = this.obsCollectRange + obsCollectRangeAddIn;
        Collider2D []arrayObsDetected = Physics2D.OverlapCircleAll(transform.position,obsCollectRange,layerObs);
        if(arrayObsDetected.Length>0)
            for(int i = 0;i<arrayObsDetected.Length;i++){
                arrayObsDetected[i].GetComponent<XPObs>().StartMovement();
                arrayObsDetected[i].gameObject.layer = LayerMask.NameToLayer("ObsNeutral");
            }
    }
    public void PlayerRotation(){
        Vector2 lookDir = InputManager.Instance.GetArrowButton();
        lookDir.x *= -1;
        if (isMoving)
        {
            shipAngle = Mathf.Atan2(lookDir.x, lookDir.y) * Mathf.Rad2Deg;
        }

        if (playerRigidbody2d.rotation <= -90 && shipAngle >= 90){
            playerRigidbody2d.rotation += 360;
            playerRigidbody2d.rotation = Mathf.Lerp(playerRigidbody2d.rotation, shipAngle, rotationInterpolation);
        }

        if (playerRigidbody2d.rotation >= 90 && shipAngle <= -90){
            playerRigidbody2d.rotation -= 360;
            playerRigidbody2d.rotation = Mathf.Lerp(playerRigidbody2d.rotation, shipAngle, rotationInterpolation);
        }else
        {
            playerRigidbody2d.rotation = Mathf.Lerp(playerRigidbody2d.rotation, shipAngle, rotationInterpolation);
        }
    }
    protected void PlayerMovement(){
        Vector2 moveDirection = InputManager.Instance.GetArrowButton();
        float currentForce = moveSpeed - playerRigidbody2d.velocity.magnitude;
        if(playerRigidbody2d.velocity.magnitude>0){
            friction = playerRigidbody2d.velocity*-1;
            playerRigidbody2d.AddForce(friction*10f);
        }
        if(moveSpeed < playerRigidbody2d.velocity.magnitude) return;
            playerRigidbody2d.AddForce(moveDirection * currentForce *20);


    }
    
    protected void ShieldRecharge(){
        // Debug.Log(isDelaying);
        // Debug.Log(currentArmor.shieldPoint + " + " + baseCellArmor.shieldPoint);
        if (currentArmor.shieldPoint < baseCellArmor.shieldPoint && !isDelaying)
        {
            currentArmor.shieldPoint += (int)(shieldRechargeRate * Time.fixedDeltaTime);
            if (currentArmor.shieldPoint > baseCellArmor.shieldPoint)
                currentArmor.shieldPoint = baseCellArmor.shieldPoint;
            GameManager.Instance.healthBar.AdjustShield((float)currentArmor.shieldPoint / baseCellArmor.shieldPoint, currentArmor.shieldPoint.ToString());
        }
        if(currentArmor.shieldPoint >= baseCellArmor.shieldPoint)
            isShieldPulseCharged = true;
    }
    protected void StateMachineMonitor(){
        if(playerRigidbody2d.velocity==Vector2.zero){
            stateMachine.ChangeState(new PlayerStateIdle(this));
        }
        else if(playerRigidbody2d.velocity!=Vector2.zero){
            stateMachine.ChangeState(new PlayerStateMove(this));
        }
    }
    

    /* +++ ON CONSTRUCTION +++ */
    protected void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "EnemyCell")
        {
            EnemyCell enemyCell = other.gameObject.GetComponent<EnemyCell>();
            TakeDamage(enemyCell.bodyDamage);
            // if(currentArmor.shieldPoint>0){

            //     currentArmor.shieldPoint -= enemyCell.bodyDamage;
            //     if(currentArmor.shieldPoint<0){
            //         OnShieldDelepted();
            //         currentArmor.shieldPoint = 0;
            //     }
            //     delayTime = GameCalculator.ShieldRechargeDelayCalculator(baseCellArmor.shieldPoint - currentArmor.shieldPoint);
            //     GameManager.Instance.healthBar.AdjustShield((float)currentArmor.shieldPoint/baseCellArmor.shieldPoint,currentArmor.shieldPoint.ToString());
            //     EffectManager.Instance.ShowDamageInfict(enemyCell.bodyDamage,4,transform);
            // }
            // else{
            //     int damageTake = GameCalculator.DamageTake(currentArmor,currentArmor.armorPoint,enemyCell.bodyDamage).Item1;
            //     healPoint -= damageTake;
            //     EffectManager.Instance.ShowDamageInfict(damageTake,4,transform);
            //     GameManager.Instance.healthBar.AdjustHealth((float)healPoint/maxHealth,healPoint.ToString());
            // }

            Vector2 collisionDirection = other.contacts[0].normal.normalized;
            //Debug.Log(collisionDirection * pushBackForce);
            playerRigidbody2d.AddForce(collisionDirection * pushBackForce, ForceMode2D.Impulse);
        }
    }
    public void TakeDamage(int damageIncome){
        if(currentArmor.shieldPoint>0){

                currentArmor.shieldPoint -= damageIncome;
                if(currentArmor.shieldPoint<0){
                    OnShieldDelepted();
                    currentArmor.shieldPoint = 0;
                }
                delayTime = GameCalculator.ShieldRechargeDelayCalculator(baseCellArmor.shieldPoint - currentArmor.shieldPoint);
                GameManager.Instance.healthBar.AdjustShield((float)currentArmor.shieldPoint/baseCellArmor.shieldPoint,currentArmor.shieldPoint.ToString());
                EffectManager.Instance.ShowDamageInfict(damageIncome,4,transform);
            }
            else{
                int damageTake = GameCalculator.DamageTake(currentArmor,currentArmor.armorPoint,damageIncome).Item1;
                healPoint -= damageTake;
                EffectManager.Instance.ShowDamageInfict(damageTake,4,transform);
                GameManager.Instance.healthBar.AdjustHealth((float)healPoint/maxHealth,healPoint.ToString());
            }

    }
    protected void OnShieldDelepted(){
        if(isShieldPulseCharged){
            Collider2D[] objects = Physics2D.OverlapCircleAll(transform.position,impactField,layerAffectByShieldPulse);
            foreach(Collider2D obj in objects){
                Vector2 direction = (obj.transform.position - transform.position).normalized;
                var victim = obj.GetComponent<Rigidbody2D>();
                victim.AddForce(direction*impactForce,ForceMode2D.Impulse);
            }
        }
        isShieldPulseCharged = false;
    }
    protected void ShieldDelay()
    {
        if (delayTime > 0)
        {
            isDelaying = true;
            delayTime -= Time.fixedDeltaTime;
        }
        else
        {
            isDelaying = false;
        }
    }
    protected void GetRotation()
    {
        
    }
    protected void AddProperties(){
        if(DataManager.Instance.Data.listMutations.Exists(x => x.mutationID == mutationId)){
            MutationOOP mutationOOP = DataManager.Instance.Data.listMutations.Find(x => x.mutationID == mutationId);
            mutationName = mutationOOP.mutationName;
            maxHealth = mutationOOP.maxHealth;
            maxEnery = mutationOOP.maxEnery;
            baseCellArmor = mutationOOP.baseCellProtection;
            currentArmor = new CellProtection(baseCellArmor);
            moveSpeed = mutationOOP.moveSpeed;
            defaultMoveSpeed = mutationOOP.moveSpeed;
            lv = mutationOOP.lv;
            currentArmor.shieldPoint = baseCellArmor.shieldPoint;
            healPoint = maxHealth;
            currentEnery = maxEnery;
            foreach (var ability in mutationOOP.mutationAbilities)
            {
                string customComponentName = ability.abilityId;
                Type customComponentType = Type.GetType(customComponentName);
                CellAbility cellAbility = (CellAbility)gameObject.AddComponent(customComponentType);
                cellAbility.mutation = this;
                mutationAbilities.Add(cellAbility);
            }
        }
    }
    protected void ShowProterties(){
        GameManager.Instance.healthBar.shieldText.text = baseCellArmor.shieldPoint.ToString();
        GameManager.Instance.healthBar.healthText.text = maxHealth.ToString();
        GameManager.Instance.healthBar.energyText.text = maxEnery.ToString();
    }
}
#endregion

[Serializable]
public class MutationOOP
{
    [Header("Base Stat")]
    public string mutationID;
    public string mutationName;
    public int maxHealth = 200;
    public int maxEnery = 200;
    public CellProtection baseCellProtection;
    public float moveSpeed = 1f;
    public int lv = 1;
    public List<AbilityOOP> mutationAbilities;
    public Faction faction;
    public MutationOOP()
    {
        baseCellProtection = new CellProtection();
        mutationAbilities = new List<AbilityOOP>();
    }
}