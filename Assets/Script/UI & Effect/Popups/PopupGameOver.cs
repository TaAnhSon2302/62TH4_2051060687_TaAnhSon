using UnityEngine;
using System;

public class PopupGameOver : Popups
{
    public static PopupGameOver Instance;
    #region DEFINE VARIABLES
    private Action<bool> _onResult;
    #endregion

    #region FUNCTION
    void InitUI()
    {
    }

    public void OnGoBackButtonClicked()
    {
        SceneLoadManager.Instance.LoadScene(SceneName.MainMenu, true);
        Hide();
    }

   
    #endregion

    #region BASE POPUP 
    static void CheckInstance(Action completed)//
    {
        if (Instance == null)
        {

            var loadAsset = Resources.LoadAsync<PopupGameOver>("Prefab/UI/PopupPrefabs/PopupGameOver" +
                "");
            loadAsset.completed += (result) =>
            {
                var asset = loadAsset.asset as PopupGameOver;
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
