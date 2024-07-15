using UnityEngine;
public class GameStatePause : GameState{
    public GameStatePause(){
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
        Debug.Log("game pause");
    }
}