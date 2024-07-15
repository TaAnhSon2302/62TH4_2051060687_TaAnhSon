

public class GameState : State
{
    protected float timeScale;
    protected virtual void Start(){
        LogicUpdate();
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        
    }
}
