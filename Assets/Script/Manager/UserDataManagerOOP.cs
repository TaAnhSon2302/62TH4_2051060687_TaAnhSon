using System;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;



[Serializable]
public class UserDataOOP
{
    public UserInformation userInformation = new();
    public List<UserGunInformation> userGunInformation = new();
    public List<UserSetEquipmentInfor> usersetEquipmentInfor = new();
    public List<UserMutaitonInfor> UserMutationInfor = new();
    public UserSetEquipmentInfor userSetEquipmentDefault = new();
}
[Serializable]
public class UserLogin{
    public string email;
    public string password;
    public UserLogin(){}
    public UserLogin(string email,string password){
        this.email = email;
        this.password = password;
    }
}
[Serializable]
public class UserInformation
{
    public string userID;
    public string userName;
    public string email;
}

[Serializable]
public class UserGunInformation
{
    public string ownerShipId;
    public string userId;
    public string gunId;
    public int gunLv;
    public int gunXp;
}
[Serializable]
public class UserSetEquipmentInfor
{
    public string userEquipmentId;
    public string userId;
    public string mutationOwnershipId;
    public string gunOwnershipId1;
    public string gunOwnershipId2;
}
[Serializable]
public class UserMutaitonInfor
{
    public string ownerShipId;
    public string userId;
    public string mutationId;
    public int mutationLv;
    public int mutationXp;
}


[Serializable]
public class AccessToken{
    public string accessToken;
    public string refreshToken;
}
