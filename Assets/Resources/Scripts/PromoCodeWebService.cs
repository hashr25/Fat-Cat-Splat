using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;

public class PromoCodeWebService
{
    private string apiURL = "https://fat-cat-splat-api.herokuapp.com/api";

    public IEnumerator DoesUserExist(string userName, Action<bool> callback)
    {
        GetPromoCodeForUserRequest request = new GetPromoCodeForUserRequest();
        request.securityToken = HashKey.apiKey;
        request.userName = userName;

        var jsonString = JsonUtility.ToJson(request);
        byte[] byteData = System.Text.Encoding.ASCII.GetBytes(jsonString.ToCharArray());

        UnityWebRequest unityWebRequest = UnityWebRequest.Post(apiURL + "/userExists", jsonString);

        unityWebRequest.uploadHandler = new UploadHandlerRaw(byteData);
        unityWebRequest.uploadHandler.contentType = "application/json";
        unityWebRequest.SetRequestHeader("Content-Type", "application/json");

        DownloadHandlerBuffer downloadHandlerBuffer = new DownloadHandlerBuffer();
        unityWebRequest.downloadHandler = downloadHandlerBuffer;

        yield return unityWebRequest.SendWebRequest();

        if (unityWebRequest.isNetworkError || unityWebRequest.isHttpError)
        {
            Debug.Log(unityWebRequest.error);
        }
        else
        {
            string response = System.Text.Encoding.ASCII.GetString(unityWebRequest.downloadHandler.data);
            bool userExists;
            Boolean.TryParse(response, out userExists);
            callback(userExists);
            
        }
    }

    public IEnumerator AddUser(User user, Action<User> callback)
    {
        AddUserRequest request = new AddUserRequest();
        request.user = user;
        request.securityToken = HashKey.apiKey;

        var jsonString = JsonUtility.ToJson(request);
        byte[] byteData = System.Text.Encoding.ASCII.GetBytes(jsonString.ToCharArray());

        //UnityWebRequest unityWebRequest = UnityWebRequest.Post(apiURL + "/user", jsonString);
        UnityWebRequest unityWebRequest = new UnityWebRequest(apiURL + "/user", "POST");

        unityWebRequest.uploadHandler = new UploadHandlerRaw(byteData);
        unityWebRequest.uploadHandler.contentType = "application/json";
        unityWebRequest.SetRequestHeader("Content-Type", "application/json");


        DownloadHandlerBuffer downloadHandlerBuffer = new DownloadHandlerBuffer();
        unityWebRequest.downloadHandler = downloadHandlerBuffer;

        yield return unityWebRequest.SendWebRequest();

        if (unityWebRequest.isNetworkError || unityWebRequest.isHttpError)
        {
            Debug.Log(unityWebRequest.error);
        }
        else
        {
            string response = unityWebRequest.downloadHandler.text;
            callback( JsonUtility.FromJson<User>(response));
        }
    }

    public IEnumerator GetPromoCodesForUser(string userName, Action<List<PromoCode>> callback)
    {
        GetPromoCodeForUserRequest request = new GetPromoCodeForUserRequest();
        request.userName = userName;
        request.securityToken = HashKey.apiKey;

        var jsonString = JsonUtility.ToJson(request);
        byte[] byteData = System.Text.Encoding.ASCII.GetBytes(jsonString.ToCharArray());

        UnityWebRequest unityWebRequest = UnityWebRequest.Post(apiURL + "/userPromoCodes", jsonString);

        unityWebRequest.uploadHandler = new UploadHandlerRaw(byteData);
        unityWebRequest.uploadHandler.contentType = "application/json";
        unityWebRequest.SetRequestHeader("Content-Type", "application/json");

        DownloadHandlerBuffer downloadHandlerBuffer = new DownloadHandlerBuffer();
        unityWebRequest.downloadHandler = downloadHandlerBuffer;

        yield return unityWebRequest.SendWebRequest();

        if (unityWebRequest.isNetworkError || unityWebRequest.isHttpError)
        {
            Debug.Log(unityWebRequest.error);
            callback(null);
        }
        else
        {
            string response = unityWebRequest.downloadHandler.text;
            PromoCode[] codes = JsonHelper.FromJsonWithFix<PromoCode>(response);
            callback(new List<PromoCode>(codes));
        }
    }


    public IEnumerator UserUsePromoCode(string userName, string promoCode, Action callback)
    {
        User user = new User();

        UserUsePromoCodeRequest request = new UserUsePromoCodeRequest();
        request.userName = user.userName;
        request.promoCode = promoCode;
        request.securityToken = HashKey.apiKey;

        var jsonString = JsonUtility.ToJson(request);
        byte[] byteData = System.Text.Encoding.ASCII.GetBytes(jsonString.ToCharArray());

        UnityWebRequest unityWebRequest = new UnityWebRequest(apiURL + "/usePromoCode", "POST");

        unityWebRequest.uploadHandler = new UploadHandlerRaw(byteData);
        unityWebRequest.uploadHandler.contentType = "application/json";
        unityWebRequest.SetRequestHeader("Content-Type", "application/json");

        yield return unityWebRequest.SendWebRequest();

        if (unityWebRequest.isNetworkError || unityWebRequest.isHttpError)
        {
            Debug.Log(unityWebRequest.error);
        }
        else
        {
            //string response = unityWebRequest.downloadHandler.text;
            callback();
        }
    }

    [Serializable]
    public class AddUserRequest
    {
        [SerializeField] public User user;
        [SerializeField] public string securityToken;
    }

    [Serializable]
    public class UserUsePromoCodeRequest
    {
        [SerializeField] public string userName;
        [SerializeField] public string promoCode;
        [SerializeField] public string securityToken;
    }

    [Serializable]
    public class GetPromoCodeForUserRequest
    {
        [SerializeField] public string userName;
        [SerializeField] public string securityToken;
    }
}
