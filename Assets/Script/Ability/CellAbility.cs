using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;


public class CellAbility : MonoBehaviour
{
    public Mutation mutation;
    public string abilityId;
    public string abilityName;
    public string mutationId;
    public int energyConsumption = 25;
    public int strength = 100; 
    public int duration = 100;
    public int range = 100;
    public int efficiency = 100;
    public AbilityOrder abilityOrder = AbilityOrder.Skill1;
    public CellAbility(){}
    public CellAbility(AbilityOOP abilityOOP){
        abilityId = abilityOOP.abilityId;
        abilityName = abilityOOP.abilityName;
        mutationId = abilityOOP.mutationId;
    }
    protected virtual void Start(){
        abilityId = GetType().Name;
        abilityName = DataManager.Instance.Data.listAbilities.Find(x => x.abilityId == abilityId).abilityName;
        mutationId =  DataManager.Instance.Data.listAbilities.Find(x => x.abilityId == abilityId).mutationId;
    }

    public virtual void AbilityCast(){
        mutation.currentEnery -= energyConsumption;
        AbilityBehavior();
    }
    protected virtual void AbilityBehavior(){

    }
}
[Serializable]
public class AbilityOOP{
    public string abilityId;
    public string abilityName;
    public string mutationId;
    
}
public enum AbilityOrder{
    Skill1,
    Skill2,
    Skill3,
    Skill4,
    Skill5,
    Ultimate,
}