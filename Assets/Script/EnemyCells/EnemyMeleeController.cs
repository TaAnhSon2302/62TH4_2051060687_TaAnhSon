using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMeleeController : MonoBehaviour
{
    [SerializeField] private EnemyCell enemyCell;
    [SerializeField] public EnemyCell EnemyCell{get => enemyCell;}
    [SerializeField] private Transform slashAxis;
    [SerializeField] private Transform sword;
    [SerializeField] private float slashEdge = 120;
    [SerializeField] private float slashDuration = 0.5f;
    [SerializeField] private float chargeDuration = 1f;
    [SerializeField] private float cooldownDuration = 1f;
    [SerializeField] public float detectedRange = 2f;
    private bool isCooldown = false;
    private bool isCharging = false;
    
    private void Awake() {
        enemyCell = GetComponentInParent<EnemyCell>();
    }
    private void Start() {
        sword.gameObject.SetActive(false);
    }
    public void SlashCheck(){
        if(isCooldown) return;
        Vector3 direction =  GameManager.Instance.mutation.transform.position - enemyCell.transform.position;
        float magnitude = direction.magnitude;
        direction.Normalize();
        float edge = Mathf.Atan2(direction.y,direction.x)*Mathf.Rad2Deg;
        slashAxis.rotation=Quaternion.Euler(slashAxis.position.x,slashAxis.position.y,edge - 90);
        if(magnitude<detectedRange){
            if(!isCharging)
                StartCoroutine(IEChargeToAttack());
        }
    }
    public void SlashSlash(){
        isCooldown = true;
        sword.gameObject.SetActive(true);
        slashAxis.rotation = Quaternion.Euler(slashAxis.position.x, slashAxis.position.y, slashAxis.eulerAngles.z + slashEdge/2);
        StartCoroutine(IEMoveSword());
        StartCoroutine(IEWaitForCooldown());
    }
    public IEnumerator IEChargeToAttack(){
        isCharging = true;
        float chargeDuration = this.chargeDuration;
        enemyCell.meleeRangeRenderer.projectileChargeLine.gameObject.SetActive(true);
        while(chargeDuration>0){
            float t = Mathf.Clamp01((this.chargeDuration  - chargeDuration)/ this.chargeDuration);
            enemyCell.meleeRangeRenderer.DrawChargeArc(t);
            yield return new WaitForSeconds(Time.fixedDeltaTime);
            chargeDuration -= Time.fixedDeltaTime;
        }
        enemyCell.meleeRangeRenderer.projectileChargeLine.gameObject.SetActive(false);
        SlashSlash();
    }
    public IEnumerator IEMoveSword(){
        float duration = slashDuration;
        while(duration>=0){
            slashAxis.rotation = Quaternion.Euler(slashAxis.position.x, slashAxis.position.y, slashAxis.eulerAngles.z - slashEdge / (slashDuration/Time.fixedDeltaTime));
            yield return new WaitForFixedUpdate();
            duration -= Time.fixedDeltaTime;
        }
        yield return new WaitForSeconds(0.01f);
        sword.gameObject.SetActive(false);
    }
    public IEnumerator IEWaitForCooldown(){
        float cooldownDuration = this.cooldownDuration;
        while(cooldownDuration>0){
            yield return new WaitForFixedUpdate();
            cooldownDuration-= Time.fixedDeltaTime;;
        }
        isCooldown = false;
        isCharging = false;
    }
}
