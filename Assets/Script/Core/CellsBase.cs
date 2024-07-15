using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

public class CellsBase : MonoBehaviour
{
    [Header("Base Stat")]
    [SerializeField] public int healPoint = 200;
    [SerializeField] protected int maxHealth = 200;
    [SerializeField] protected int maxEnery = 200;
    [SerializeField] public int currentEnery = 200;
    [SerializeField] protected CellProtection baseCellArmor;
    [SerializeField] public CellProtection currentArmor;
    [SerializeField] public float defaultMoveSpeed = 1f;
    [SerializeField] public float moveSpeed = 1f;
    [SerializeField] protected int lv = 1;
    [SerializeField] protected Faction faction;
    [SerializeField] protected Vector2 friction;
    [Space(10)]
    [Header("Status")]
    [SerializeField] protected int currentElementStack = 0;
    [SerializeField] protected PrimaryElement currentPrimaryElement = 0;
    [SerializeField] protected SecondaryElement currentSecondaryElement = 0;
    protected float shieldRechargeDelay = 1;
    protected float shieldRechargeRate = 100;

    
    
    protected virtual void OnEnable(){
        healPoint = maxHealth;
        currentArmor.armorType = baseCellArmor.armorType;
        currentArmor.armorPoint = BioArmorCalculating();
        
    }
    protected virtual void Awake(){
        
    }
    protected virtual void Start(){

    }
    public virtual void OnDead(){
        
    }
    protected int BioArmorCalculating(){
        int armor=0;
        if(baseCellArmor.armorType == ArmorType.Bio){
            armor = baseCellArmor.armorPoint + maxHealth;
        }
        return baseCellArmor.armorPoint;
    }
}
[Serializable]
public enum Faction{
    Hematos,
    Neutroton,
    Cytocell,
    Carcino
}