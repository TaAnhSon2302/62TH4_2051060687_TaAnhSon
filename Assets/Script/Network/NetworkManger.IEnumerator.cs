using System.Collections;
using System.Collections.Generic;
using System;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;

public partial class NetworkManager {
    //string apiUrl = "https://localhost:7121/api/Mutations";

    public IEnumerator CreateWebGetRequest(string requestAPI,Action<string> onComplete = null, Action onFail = null){
        using(UnityWebRequest webRequest = UnityWebRequest.Get(requestAPI)){
            webRequest.SetRequestHeader("Authorization","Bearer " + accessToken.accessToken);
            yield return webRequest.SendWebRequest();
            if(webRequest.result != UnityWebRequest.Result.Success){
                Debug.Log("Error while fetch from API: " + requestAPI + " " + webRequest.error);
                onFail?.Invoke();
            }
            else{
                string data = webRequest.downloadHandler.text;
                Debug.Log(data);
                onComplete?.Invoke(data);
            }
        }
    }
    public IEnumerator CreateWebPostRequest(APIRequest apiRequest,Action<string> onComplete = null,Action<string> onFail = null,bool isRequireAuthorize = true,Action onUnauthorized = null)
    {
        
        using (UnityWebRequest request = UnityWebRequest.PostWwwForm(apiRequest.url, apiRequest.body))
        {
            Debug.Log(apiRequest.body);
            request.SetRequestHeader("Content-Type", "application/json");
            byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(apiRequest.body);
            request.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
            request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            if(isRequireAuthorize)
                request.SetRequestHeader("Authorization","Bearer " + accessToken.accessToken);
            yield return request.SendWebRequest();
            Debug.Log(request.responseCode);
            long responseCode = request.responseCode;
            if (request.result != UnityWebRequest.Result.Success)
            {
                if(responseCode == 401){
                    Debug.Log("Error: Unauthorized");
                    onUnauthorized?.Invoke();
                }
                else{
                    string data = request.downloadHandler.text;
                    Debug.Log(data);
                    onFail?.Invoke(data);
                }
            }
            else
            {
                if(responseCode == 200){
                    string data = request.downloadHandler.text;
                    Debug.Log("Post complete! " + data);
                    onComplete?.Invoke(data);
                }
                else {
                    Debug.Log(request.downloadHandler.text);
                }
            }
        }
    }
}

public class APIRequest{
    public string url;
    public string body;
}