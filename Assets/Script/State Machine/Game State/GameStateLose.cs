using UnityEngine;
public class GameStateLose : GameState{
    public GameStateLose()
    {
        timeScale = 0;
    }
    public override void Enter()
    {
        base.Enter();
        GameManager.Instance.isPause = true;
    }
    public override void LogicUpdate(){
        base.LogicUpdate();
    }
}