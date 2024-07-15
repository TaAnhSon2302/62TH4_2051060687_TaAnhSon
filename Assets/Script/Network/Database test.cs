using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;

public class Databasetest : MonoBehaviour
{
    [System.Serializable]
    public class User
    {
        public string userId;
        public string userName;
        public string password;
        public int credit;
    }

    void Start()
    {
        // UserDataOOP user = new UserDataOOP();
        // user.userId = "userID123";
        // user.userName = "exampleUser";
        // user.password = "examplePassword";
        // user.credit = 100;
        // APIRequest request = new();
        // request.url = "https://localhost:7121/api/Users";
        // string jsonData = JsonConvert.SerializeObject(user);
        // request.body = jsonData;
        // StartCoroutine(PostUser(request));
    }

    IEnumerator PostUser(APIRequest apiRequest)
    {
        
        using (UnityWebRequest request = UnityWebRequest.PostWwwForm(apiRequest.url, apiRequest.body))
        {
            request.SetRequestHeader("Content-Type", "application/json");
            byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(apiRequest.body);
            request.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
            request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();

            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(request.error);
            }
            else
            {
                Debug.Log("User post complete! " + request.downloadHandler.text);
            }
        }
    }
}
