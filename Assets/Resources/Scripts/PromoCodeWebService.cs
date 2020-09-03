using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;

public class PromoCodeWebService : MonoBehaviour
{
    

    //public User AddUser(User user)
    //{
    //    HttpWebRequest request = (HttpWebRequest)WebRequest.Create("heroku add user link");
    //    request.Method = "POST";
    //    request.ContentType = "application/json";
    //    HttpWebResponse response = (HttpWebResponse)request.GetResponse();
    //    StreamReader reader = new StreamReader(response.GetResponseStream());
    //    string jsonResponse = reader.ReadToEnd();
    //    WeatherInfo info = JsonUtility.FromJson<User>(jsonResponse);
    //    return info;
    //}

    public User AddUser(User user)
    {
        ///<summary>
		/// Post using UnityWebRequest class
		/// </summary>

        AddUserRequest addUserRequest = new AddUserRequest();
        addUserRequest.user = user;
        addUserRequest.securityToken = HashKey.apiKey;

		var jsonString = JsonUtility.ToJson(addUserRequest);
        byte[] byteData = System.Text.Encoding.ASCII.GetBytes(jsonString.ToCharArray());

        UnityWebRequest unityWebRequest = new UnityWebRequest("http://localhost:55376/api/values", "POST");
        unityWebRequest.uploadHandler = new UploadHandlerRaw(byteData);
        unityWebRequest.SetRequestHeader("Content-Type", "application/json");

        DownloadHandlerBuffer downloadHandlerBuffer = new DownloadHandlerBuffer();
        unityWebRequest.downloadHandler = downloadHandlerBuffer;

        unityWebRequest.SendWebRequest();

        if (unityWebRequest.isNetworkError || unityWebRequest.isHttpError)
        {
            Debug.Log(unityWebRequest.error);
            return null;
        }
        else
        {
            Debug.Log("Form upload complete! Status Code: " + unityWebRequest.responseCode);
            string response = unityWebRequest.downloadHandler.text;
            return JsonUtility.FromJson<User>(response);
        }
    }

    public List<PromoCode> GetPromoCodesForUser(string userName)
    {
        ///<summary>
		/// Post using UnityWebRequest class
		/// </summary>

        GetPromoCodeForUserRequest request = new GetPromoCodeForUserRequest();
        request.userName = userName;
        request.securityToken = HashKey.apiKey;

        var jsonString = JsonUtility.ToJson(request);
        byte[] byteData = System.Text.Encoding.ASCII.GetBytes(jsonString.ToCharArray());

        UnityWebRequest unityWebRequest = new UnityWebRequest("http://localhost:55376/api/values", "POST");
        unityWebRequest.uploadHandler = new UploadHandlerRaw(byteData);
        unityWebRequest.SetRequestHeader("Content-Type", "application/json");

        DownloadHandlerBuffer downloadHandlerBuffer = new DownloadHandlerBuffer();
        unityWebRequest.downloadHandler = downloadHandlerBuffer;

        unityWebRequest.SendWebRequest();

        if (unityWebRequest.isNetworkError || unityWebRequest.isHttpError)
        {
            Debug.Log(unityWebRequest.error);
            return null;
        }
        else
        {
            Debug.Log("Form upload complete! Status Code: " + unityWebRequest.responseCode);
            string response = unityWebRequest.downloadHandler.text;
            return JsonUtility.FromJson<List<PromoCode>>(response);
        }
    }

    public class PromoCode
    {
        public int promoCodeId { get; set; }
        public string code { get; set; }
        public string description { get; set; }
        public string currencyType { get; set; }
        public int currencyGiven { get; set; }
        public Boolean hasBeenUsed { get; set; }
    }

    public class User
    {
        public int userId { get; set; }
        public string userName { get; set; }
        public string deviceType { get; set; }
        public string deviceModel { get; set; }
    }

    public class AddUserRequest
    {
        public User user { get; set; }
        public string securityToken { get; set; }
    }

    public class UserUsePromoCodeRequest
    {
        public string userName { get; set; }
        public string promoCode { get; set; }
        public string securityToken { get; set; }
    }

    public class GetPromoCodeForUserRequest
    {
        public string userName;
        public string securityToken;
    }
}
