using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Lean.Common;
using System;
using UnityEngine.Purchasing.MiniJSON;
using System.Linq;

public class LoginManager : Singleton<LoginManager>
{
    public TMP_InputField userEmail;
    public TMP_InputField userPassword;
    public Transform loginPanel;
    public TextMeshProUGUI notification;
    public RectTransform confirmPassword;
    public TMP_InputField confirmUserPassword;
    public Button submitBtn;
    public Button startBtn;
    public Transform signUpPanel;
    public Button backButton;
    public Image startFog;
    private TextMeshProUGUI submitText;
    private Vector2 sizeOftextField;
    private float sizeOfNotification;
    private bool isOnTransform = false;
    private bool isSignIn = true;

    private void Start() {
        Init();
    }
    private void Init(){
        #if UNITY_EDITOR
            userEmail.text = "qwerty@gmail.com";
            userPassword.text = "Abc@123";
        #endif
        RectTransform rect = (RectTransform)(userPassword.transform);
        sizeOftextField = new Vector2(rect.sizeDelta.x,rect.sizeDelta.y);
        sizeOfNotification = notification.fontSize;
        submitText = submitBtn.GetComponentInChildren<TextMeshProUGUI>();
        
        LeanTween.value(gameObject, 1, 0, 3.5f).setOnStart(() =>
        {
            startFog.gameObject.SetActive(true);
        }).setOnUpdate((float value) =>
        {
            startFog.color = new Color(0f,0f,0f,value);
        }).setOnComplete(() =>
        {
            startFog.gameObject.SetActive(false);
        });
    }
    public void OnSubmitBtnClick()
    {
        TextMeshProUGUI logInText = submitBtn.GetComponentInChildren<TextMeshProUGUI>();
        if(isOnTransform) return;
        if(isSignIn){
            submitBtn.interactable = false;
            logInText.color = new Color(1, 1, 1, 0.5f);
            notification.text = "";
            NetworkManager.Instance.PostRequestLogin(new UserLogin(userEmail.text, userPassword.text), () =>
            {
                loginPanel.gameObject.SetActive(false);
                startBtn.gameObject.SetActive(true);
                NetworkManager.Instance.GetUserInformationFromServer(userEmail.text);
            }, () =>
            {
                submitBtn.interactable = true;
                logInText.color = new Color(1, 1, 1, 1);
                notification.text = " Can not connect to server";

            }, () =>
            {
                submitBtn.interactable = true;
                logInText.color = new Color(1, 1, 1, 1);
                notification.text = " Email or password is incorrect";

            });
        }
        else
        {
            submitBtn.interactable = false;
            logInText.color = new Color(1, 1, 1, 0.5f);
            notification.text = "";
            if(userPassword.text!=confirmUserPassword.text){
                submitBtn.interactable = true;
                logInText.color = new Color(1, 1, 1, 1);
                notification.text = "Confirmation password does not match";
            }
            else{
                NetworkManager.Instance.PostRequestSignUp(new UserLogin(userEmail.text, userPassword.text),()=>{
                    submitBtn.interactable = true;
                    logInText.color = new Color(1, 1, 1, 1);
                    OnBackClick();
                },(data)=>{
                    JSONObject json = new JSONObject(data);
                    var listErrors = json["errors"].list;
                    if(listErrors[0].ToString() == $"[\"Email '{userEmail.text}' is invalid.\"]")
                        notification.text = "Invalid email";
                    else if(listErrors[0].ToString() ==  "[\"Passwords must be at least 6 characters.\"]")
                        notification.text = "Passwords must be at least 6 characters.";
                    else if(listErrors[0].ToString() == "[\"Passwords must have at least one digit ('0'-'9').\"]"||
                        listErrors[0].ToString() == "[\"Passwords must have at least one lowercase ('a'-'z').\"]"||
                        listErrors[0].ToString() == "[\"Passwords must have at least one uppercase ('A'-'Z').\"]"||
                        listErrors[0].ToString() == "[\"Passwords must have at least one non alphanumeric character.\"]")
                        notification.text = "Password require at least an uppercase, number and special character";
                    else{
                        notification.text = "Email is already taken";
                    }
                    submitBtn.interactable = true;
                    logInText.color = new Color(1, 1, 1, 1);

                });
            }
        }
    }
    public void NotificationOutAnim()
    {
        LeanTween.value(gameObject, 0, 1, 0.4f).setOnStart(() =>
        {
            notification.fontSize = 0;
        }).setOnUpdate((float value) =>
        {
            notification.fontSize = sizeOfNotification*value;
        }).setEaseOutQuad().setOnComplete(() =>
        {
        });
    }
    public void NotificationInAnim()
    {
        LeanTween.value(gameObject, 0, 1, 0.4f).setOnStart(() =>
        {
        }).setOnUpdate((float value) =>
        {
            notification.fontSize = sizeOfNotification*value;
        }).setEaseInQuad().setOnComplete(() =>
        {
        });
    }
    public void OnRegisterClick(){
        float time = 0.4f;
        Vector2 tempSize = sizeOftextField;
        
        if(notification.gameObject.activeSelf){
            
        }
        LeanTween.value(gameObject, 0, 1, time).setOnStart(() =>
        {
            notification.text = "";
            isOnTransform = true;
            confirmPassword.sizeDelta = Vector2.zero;
            UserUIManager.Instance.TransformStringByRandom(submitText,"Sign Up",time);
        }).setOnUpdate((float value) =>
        {
            confirmPassword.sizeDelta = tempSize*value;
        }).setEaseOutQuad().setOnComplete(() => {
            isOnTransform = false;
            isSignIn = false;
        });


        signUpPanel.gameObject.SetActive(false);
        backButton.gameObject.SetActive(true);
    }
    public void OnBackClick(){
        float time = 0.4f;
        Vector2 tempSize = sizeOftextField;
        LeanTween.value(gameObject, 1, 0, time).setOnStart(() =>
        {
            UserUIManager.Instance.TransformStringByRandom(submitText,"Sign In",time);
            userPassword.text = "";
            isOnTransform = true;
        }).setOnUpdate((float value) =>
        {
            confirmPassword.sizeDelta = tempSize*value;
        }).setEaseInQuad().setOnComplete(() => {
            isOnTransform = false;
            isSignIn = true;
            confirmUserPassword.text = "";
        });
        signUpPanel.gameObject.SetActive(true);
        backButton.gameObject.SetActive(false);
    }
    public void OnStartClick(){
        /* #if UNITY_EDITOR
             SceneLoadManager.Instance.LoadScene(SceneName.GamePlay);
         #else */
        NetworkManager.Instance.GetUserGunFromServer(DataManager.Instance.UserData.userInformation.userID);
        NetworkManager.Instance.GetUserEquipedGunFromServer(DataManager.Instance.UserData.userInformation.userID);
        NetworkManager.Instance.GetUserMutattionFromServer(DataManager.Instance.UserData.userInformation.userID);
        SceneLoadManager.Instance.LoadScene(SceneName.MainMenu,true);
        //  #endif
    }
    // private IEnumerator IEInputFieldAnimation(float from,float to,float time,Action onStart =null, Action onFinish = null){
    //     while (time>0){
    //         yield return new WaitForFixedUpdate();
    //     }
    // }
}
