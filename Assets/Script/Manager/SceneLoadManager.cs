using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadManager : Singleton<SceneLoadManager>
{
    [SerializeField] private Animator sceneLoadAnimator;
    [SerializeField] private float waitBeforeLoad = 0.5f;
    private const string NameAnimStartLoad = "start";
    public string lastScene = "";
    private void Start() {
        sceneLoadAnimator.enabled = false;
    }
    public void LoadScene(SceneName sceneName,bool isStopMusic = false){
        if(isStopMusic)
            AudioManager.Instance.StopCurrentMusic();
        sceneLoadAnimator.enabled = true;
        lastScene = SceneManager.GetActiveScene().name;
        StartCoroutine(IEOnSceneLoad(sceneName));
        // SceneManager.LoadScene(sceneName.ToString());
    }
    private IEnumerator IEOnSceneLoad(SceneName sceneName)
    {
        sceneLoadAnimator.SetTrigger(NameAnimStartLoad);
        yield return new WaitForSeconds(waitBeforeLoad);
        SceneManager.LoadScene(sceneName.ToString());
    }
}
public enum SceneName{
    MainMenu,
    Launch,
    GamePlay,
    Collection,
    Equipment,
}
