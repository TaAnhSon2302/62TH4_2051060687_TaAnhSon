using System;
using System.Collections;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    [SerializeField] private string currentStateName;
    [SerializeField] private string currentStateStatusName;
    private State currentState;
    public StatusState currentStatusState {get;private set;}
    public void StateMachineUpdate() {
        currentState?.LogicUpdate();
        currentStatusState?.LogicUpdate();
    }
    public void StateMachineFixedUpdate() {
        currentState?.PhysicsUpdate();
        currentStatusState?.PhysicsUpdate();
    }
    public void ChangeStatusState(StatusState newStatusState){
        int initDamage = -1;
        // if (newStatusState.CurrentStatusLevel() == 2)
        //     return;
        if(currentStatusState!=null){
            initDamage = currentStatusState.damagePerTick;
            newStatusState.stack+=currentStatusState.stack;
            if(newStatusState.stack>newStatusState.maxStack)
                newStatusState.stack = newStatusState.maxStack;
            currentStatusState.Exit();
        }
        currentStatusState = newStatusState;
        currentStatusState.Initialize(this);
        try{
            currentStatusState.Enter();
        }
        catch(Exception e){
            string exception = e.ToString();
            StartCoroutine(IEWaitForEnemyLoad());
            IEnumerator IEWaitForEnemyLoad(){
                while(currentStatusState.enemyCell==null)
                    yield return null;
                currentStatusState.Enter();
            }
        }
        currentStatusState.damagePerTick = initDamage > currentStatusState.damagePerTick ? initDamage : currentStatusState.damagePerTick;
        currentStateStatusName = currentStatusState.ToString();
    }
    public void ChangeState(PlayerState newState)
    {
        if (currentState != null)
        {
            currentState.Exit();
        }

        currentState = newState;
        currentState.Initialize(this);
        currentState.Enter();
        currentStateName = currentState.ToString();
    }
    public void ChangeState(EnemyState newState)
    {
        if (currentState != null)
        {
            currentState.Exit();
        }

        currentState = newState;
        currentState.Initialize(this);
        currentState.Enter();
        currentStateName = currentState.ToString();
    }
    public void ChangeState(GameState newState)
    {
        if (currentState != null)
        {
            currentState.Exit();
        }

        currentState = newState;
        currentState.Initialize(this);
        currentState.Enter();
        currentState.LogicUpdate();
        currentStateName = currentState.ToString();
    }
}
