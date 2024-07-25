using UnityEngine;
public class GameStateWin : GameState{
    public GameStateWin()
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
        Time.timeScale = timeScale;
        InputManager.Instance.isOnPauseState = true;
    }
}