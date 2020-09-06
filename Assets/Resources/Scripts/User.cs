using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class User
{
    public int userId;
    public string userName;
    public string deviceType;
    public string deviceModel;

    public User()
    {
        userName = SystemInfo.deviceUniqueIdentifier;
        deviceType = SystemInfo.deviceType.ToString();
        deviceModel = SystemInfo.deviceModel;
    }
}
