using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;
using static GameStatic;
using UnityEngine.Video;

public partial class NetworkManager : Singleton<NetworkManager>
{
    private AccessToken accessToken = new AccessToken();
    APIRequest apiRequest = new();
    void Start()
    {
        // UserDataOOP user = new UserDataOOP();
        // user.userId = "userID1234";
        // user.userName = "exampleUser";
        // user.password = "examplePassword";
        // user.credit = 100;

        // PostNewUserToServer(user);
    }
    #region Get
    public void GetIngameLevelConfigsFromServer(){
        StartCoroutine(CreateWebGetRequest(HOST + GET_INGAME_LEVEL_CONFIGS,(string data)=>{
            DataManager.Instance.GetIngameLevelConfigs(data);
        }));
    }
    public void GetMutationDataFromServer()
    {
        StartCoroutine(CreateWebGetRequest(HOST + GET_MUTATION_API, (string data) =>
        {
            DataManager.Instance.GetMutationData(data);
        }));
    }
    public void GetEnemyDataFromServer(){
        StartCoroutine(CreateWebGetRequest(HOST + GET_ENEMY_API,(string data) => 
        {
            DataManager.Instance.GetEnemydata(data);
        }));
    }
    public void GetAbilityDataFromServer()
    {
        StartCoroutine(CreateWebGetRequest(HOST + GET_ABILITY_API, (string data) =>
        {
            DataManager.Instance.GetAbilityData(data);
        }));
    }
    public void GetBulletDataFromServer()
    {
        StartCoroutine(CreateWebGetRequest(HOST + GET_BULLET_API, (string data) =>
        {
            DataManager.Instance.GetBulletData(data);
        }));
    }
    public void GetUserInformationFromServer(string email){
        StartCoroutine(CreateWebGetRequest(HOST + GET_USER_INFORMATION + email, (string data) =>
        {
            DataManager.Instance.GetUserInformationData(data);
        }));
    }
    public void GetUserGunFromServer(string userId)
    {
        StartCoroutine(CreateWebGetRequest(HOST + GET_USER_GUN + userId, (string data) => 
        {
            DataManager.Instance.GetUserGunInformationData(data);
        }));
    }
    public void GetGunFromServer()
    {
        StartCoroutine(CreateWebGetRequest(HOST + GET_GUN_API, (string data) =>
        {
            DataManager.Instance.GetGunData(data);   
        }));
    }
    public void GetUserEquipedGunFromServer(string userId)
    {
        StartCoroutine(CreateWebGetRequest(HOST + GET_USER_EQUIPED_GUN + userId , (string data) =>
        {
            DataManager.Instance.GetUserEquipedGunInfor(data);
            //Debug.Log(DataManager.Instance.UserData.usersetEquipmentInfor.Count);
            DataManager.Instance.UserData.userSetEquipmentDefault = DataManager.Instance.UserData.usersetEquipmentInfor[0];
        
        }));
    }
    public void GetUserMutattionFromServer(string userId)
    {
        StartCoroutine(CreateWebGetRequest(HOST + GET_USER_MUTATION + userId, (string data) =>
        {
            DataManager.Instance.GetUserMutationInfor(data);
        }));
    }
    #endregion

    #region Post
    public void PostRequestSignUp(UserLogin userLogin,Action onComplete, Action<string> onFail){
        APIRequest aPIRequest = new();
        apiRequest.url = HOST + POST_SIGNUP_REQUEST;
        string jsonData = JsonConvert.SerializeObject(userLogin);
        apiRequest.body = jsonData;
        StartCoroutine(CreateWebPostRequest(apiRequest,(string data)=>{
            onComplete?.Invoke();
        },(data)=>{
            onFail?.Invoke(data);
        },false));
    }
    public void PostRequestLogin(UserLogin userLogin,Action onComplete,Action onFail,Action onUnauthorized){
        APIRequest aPIRequest = new();
        apiRequest.url = HOST + POST_LOGIN_REQUEST;
        string jsonData = JsonConvert.SerializeObject(userLogin);
        apiRequest.body = jsonData;
        StartCoroutine(CreateWebPostRequest(apiRequest,(string data)=>{
            JSONObject json = new JSONObject(data);
            accessToken.accessToken = json["accessToken"].str;
            accessToken.refreshToken = json["refreshToken"].str;
            onComplete?.Invoke();
        },(data)=>{
            onFail?.Invoke();
        },false,onUnauthorized:()=>{
            onUnauthorized?.Invoke();
        }));
    }
    //public void PostNewUserToServer(UserDataOOP newUser)
    //{
    //    APIRequest apiRequest = new();
    //    apiRequest.url = HOST + "/api/Users";
    //    string jsonData = JsonConvert.SerializeObject(newUser);
    //    apiRequest.body = jsonData;
    //    StartCoroutine(CreateWebPostRequest(apiRequest, (string data) =>
    //    {
    //        Debug.Log("done");
    //    }));
    //}
    public static APIRequest UpdateEquipmentSet(string equipSetId, string mutationId, string gun1 = null , string gun2  =null)
    {
        APIRequest apiRequest = new APIRequest();
        apiRequest.url = HOST + "/api/UserEquipment/UpdateUserEquipment";
       // var currentSet = DataManager.Instance.UserData.usersetEquipmentInfor.Find(x => x.userEquipmentId == equipSetId);
        var data = new
        {
            userEquipmentId = equipSetId,
            mutationOwnershipId = mutationId,
            gunOwnershipId1 = gun1,
            gunOwnershipId2 = gun2,
        };
        apiRequest.body = JsonConvert.SerializeObject(data);
        return apiRequest;
    }
    public static APIRequest AddNewEquipmentSet(string userId)
    {
        APIRequest apiRequest = new APIRequest();
        apiRequest.url = HOST + GameStatic.ADD_USER_EQUIPSET;
        var data = new
        {
            userId = userId,
        };
        apiRequest.body= JsonConvert.SerializeObject(data);
        return apiRequest;
    }
    #endregion
}