using UnityEngine;

public abstract class State
{
  protected StateMachine stateMachine;
  protected bool isExitingState = true;
  public void Initialize(StateMachine stateMachine)
  {
    this.stateMachine = stateMachine;
  }
  public virtual void Enter()
  {
    isExitingState = false;
  }
  public virtual void Exit()
  {
    isExitingState = true;
  }
  public virtual void LogicUpdate()
  {
  }
  public virtual void PhysicsUpdate()
  {
  }
}
