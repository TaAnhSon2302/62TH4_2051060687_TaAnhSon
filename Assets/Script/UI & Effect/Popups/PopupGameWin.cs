using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class PopupGameWin : Popups
{
    public static PopupGameWin Instance;
    #region DEFINE VARIABLES
    private Action<bool> _onResult;
    #endregion

    #region FUNCTION
    void InitUI()
    {
    }

    public void OnGoBackButtonClicked()
    {
        GameManager.Instance.isPause = false;
        Time.timeScale = 1;
        Hide();
        SceneLoadManager.Instance.LoadScene(SceneName.MainMenu, true);

    }

    #endregion

    #region BASE POPUP 
    static void CheckInstance(Action completed)//
    {
        if (Instance == null)
        {

            var loadAsset = Resources.LoadAsync<PopupGameWin>("Prefab/UI/PopupPrefabs/PopupGameWin" +
                "");
            loadAsset.completed += (result) =>
            {
                var asset = loadAsset.asset as PopupGameWin;
                if (asset != null)
                {
                    Instance = Instantiate(asset,
                        CanvasPopup3.transform,
                        false);

                    if (completed != null)
                    {
                        completed();
                    }
                }
            };

        }
        else
        {
            if (completed != null)
            {
                completed();
            }
        }
    }

    public static void Show()//
    {

        CheckInstance(() =>
        {
            Instance.Appear();
            Instance.InitUI();
        });

    }

    public static void Hide()
    {
        //if (GameStatic.IS_ANIMATING ) return;
        //Debug.Log("close");
        Instance.Disappear();
    }
    public override void Appear()
    {
        IsLoadBoxCollider = false;
        base.Appear();
        //Background.gameObject.SetActive(true);
        Panel.gameObject.SetActive(true);
    }
    public void Disappear()
    {
        //Background.gameObject.SetActive(false);
        base.Disappear(() => {
            Panel.gameObject.SetActive(false);
        });
    }

    public override void Disable()
    {
        base.Disable();
    }

    public override void NextStep(object value = null)
    {
    }
    #endregion

}

