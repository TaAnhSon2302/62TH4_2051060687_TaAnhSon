using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    private void Start() {
        if(SceneLoadManager.Instance.lastScene == SceneName.Equipment.ToString()||SceneLoadManager.Instance.lastScene == SceneName.Collection.ToString()){

        }
        else
            AudioManager.Instance.StartMainMenuBackGround();
    }
    public void OnCampaignBtnClick(){
        SceneLoadManager.Instance.LoadScene(SceneName.GamePlay, true);
    }
    public void OpenSettingPopup(){
        PopupSetting.Show();
    }
    public void OnCollectionBtnClick()
    {
        SceneLoadManager.Instance.LoadScene(SceneName.Collection);

    }
    public void OnEquipmentBtnClick()
    {
        SceneLoadManager.Instance.LoadScene(SceneName.Equipment);
    }
    public void OnQuitBtnClick(){
        Application.Quit();
    }
}
