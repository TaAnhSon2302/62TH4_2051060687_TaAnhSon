using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class LayoutManager : Singleton<LayoutManager>
{
    public GunItem gunItem1;
    public GunItem gunItem2;
    public CharcaterItem mutationItem;
    //[SerializeField] private UserSetEquipmentInfor userSetEquipmentInfor = new();
    [SerializeField] public UserGunInformation equipmentSlot1 = new();
    [SerializeField] public UserGunInformation equipmentSlot2 = new();
    private UserSetEquipmentInfor equipmentSet = DataManager.Instance.UserData.userSetEquipmentDefault;
    private string mutationId ;
    public string gunEquipId1;
    public string gunEquipId2;
    public int currentSet = 0;

    private void Start()
    {
        Init(); 
    }
    public void Init()
    {
        mutationId = equipmentSet.mutationOwnershipId;
        equipmentSlot1 = DataManager.Instance.UserData.userGunInformation.Find(x => x.ownerShipId == equipmentSet.gunOwnershipId1);
        equipmentSlot2 = DataManager.Instance.UserData.userGunInformation.Find(x => x.ownerShipId == equipmentSet.gunOwnershipId2);
        var mutation = DataManager.Instance.UserData.UserMutationInfor.Find(x => x.ownerShipId == equipmentSet.mutationOwnershipId);
        gunItem1.InitEquipIcon(equipmentSlot1);
        gunItem2.InitEquipIcon(equipmentSlot2);
        if (equipmentSlot1 != null)
        {
            gunEquipId1 = equipmentSlot1.ownerShipId;
        }
        else
        {
            gunEquipId1 = null;
        }
        if (equipmentSlot2 != null)
        {
            gunEquipId2 = equipmentSlot2.ownerShipId;
        }
        else
        {
            gunEquipId2 = null;
        }
        mutationItem.InitCharIcon(mutation);
        EquipmentManager.Instance.gunOwnedId = "";
        EquipmentManager.Instance.mutationOwnedId = "";
    }

    public void OnClickChangeEquipmentSlot1()
    {
        if (EquipmentManager.Instance.gunOwnedId == "")
        {
            return;
        }
        NetworkManager.Instance.StartCoroutine(
            NetworkManager.Instance.CreateWebPostRequest(
                NetworkManager.UpdateEquipmentSet(equipmentSet.userEquipmentId,mutationId, EquipmentManager.Instance.gunOwnedId,gunEquipId2),
                (string data) =>
                {
                    JSONObject jsonData = new JSONObject(data);
                    DataManager.Instance.UserData.usersetEquipmentInfor.Find(x => x.userEquipmentId == equipmentSet.userEquipmentId).gunOwnershipId1 = EquipmentManager.Instance.gunOwnedId;
                    Init();
                }));
    }
    public void OnClickChangeEquipmentSlot2()
    {
        if (EquipmentManager.Instance.gunOwnedId == "")
        {
            return;
        }
        NetworkManager.Instance.StartCoroutine(
            NetworkManager.Instance.CreateWebPostRequest(
                NetworkManager.UpdateEquipmentSet(equipmentSet.userEquipmentId, mutationId, gunEquipId1, EquipmentManager.Instance.gunOwnedId),
                (string data) =>
                {
                    JSONObject jsonData = new JSONObject(data);
                    DataManager.Instance.UserData.usersetEquipmentInfor.Find(x => x.userEquipmentId == equipmentSet.userEquipmentId).gunOwnershipId2 = EquipmentManager.Instance.gunOwnedId;
                    Init();
                }));
    }
    public void OnClickChangeMutaion()
    {
        if(EquipmentManager.Instance.mutationOwnedId == " ")
        {
            return;
        }
        NetworkManager.Instance.StartCoroutine(
           NetworkManager.Instance.CreateWebPostRequest(
               NetworkManager.UpdateEquipmentSet(equipmentSet.userEquipmentId, EquipmentManager.Instance.mutationOwnedId,gunEquipId1,gunEquipId2),
               (string data) =>
               {
                   JSONObject jsonData = new JSONObject(data);
                   DataManager.Instance.UserData.usersetEquipmentInfor.Find(x => x.userEquipmentId == equipmentSet.userEquipmentId).mutationOwnershipId = EquipmentManager.Instance.mutationOwnedId;
                   Init();
               }));
    }
    public void OnClickPreviousSet()
    {
        if(currentSet == 0)
        {
            return;
        }
        currentSet--;
        equipmentSet = DataManager.Instance.UserData.usersetEquipmentInfor[currentSet];
        Init();
    }
    public void OnClickNextSet()
    {
        if (currentSet == DataManager.Instance.UserData.usersetEquipmentInfor.Count)
        {
            return;
        }
        currentSet++;
        equipmentSet = DataManager.Instance.UserData.usersetEquipmentInfor[currentSet];
        Init();
    }
    public void ConfirmUseThisSet()
    {
        DataManager.Instance.UserData.userSetEquipmentDefault = equipmentSet;
        Debug.Log(equipmentSet.userEquipmentId);
    }
}
