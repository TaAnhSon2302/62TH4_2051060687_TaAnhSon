using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class LayoutManager : Singleton<LayoutManager>
{
    public GunItem gunItem1;
    public GunItem gunItem2;
    public CharcaterItem mutationItem;
    [SerializeField] private UserSetEquipmentInfor userSetEquipmentInfor = new();
    [SerializeField] public UserGunInformation equipmentSlot1 = new();
    [SerializeField] public UserGunInformation equipmentSlot2 = new();
    private string equipmentSet = "735d1359-08f5-4e98-baa1-406a2120373f1";
    private string mutationId = "735d1359-08f5-4e98-baa1-406a2120373fHEM_JAGUAR";
    public string gunEquipId1;
    public string gunEquipId2;


    private void Start()
    {
        Init(); 
    }
    public void Init()
    {
        userSetEquipmentInfor = DataManager.Instance.UserData.usersetEquipmentInfor.Find(x => x.userEquipmentId == equipmentSet);
        equipmentSlot1 = DataManager.Instance.UserData.userGunInformation.Find(x => x.ownerShipId == userSetEquipmentInfor.gunOwnershipId1);
        equipmentSlot2 = DataManager.Instance.UserData.userGunInformation.Find(x => x.ownerShipId == userSetEquipmentInfor.gunOwnershipId2);
        var mutation = DataManager.Instance.UserData.UserMutationInfor.Find(x => x.ownerShipId == userSetEquipmentInfor.mutationOwnershipId);
        gunItem1.InitIcon(equipmentSlot1);
        gunItem2.InitIcon(equipmentSlot2);
        mutationItem.InitCharIcon(mutation);
        gunEquipId1 = equipmentSlot1.ownerShipId;
        gunEquipId2 = equipmentSlot2.ownerShipId;
        EquipmentManager.Instance.gunOwnedId = "";
    }

    public void OnClickChangeEquipmentSlot1()
    {
        if (EquipmentManager.Instance.gunOwnedId == "")
        {
            return;
        }
        NetworkManager.Instance.StartCoroutine(
            NetworkManager.Instance.CreateWebPostRequest(
                NetworkManager.UpdateEquipmentSet(equipmentSet,mutationId, EquipmentManager.Instance.gunOwnedId,gunEquipId2),
                (string data) =>
                {
                    JSONObject jsonData = new JSONObject(data);
                    DataManager.Instance.UserData.usersetEquipmentInfor.Find(x => x.userEquipmentId == equipmentSet).gunOwnershipId1 = EquipmentManager.Instance.gunOwnedId;
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
                NetworkManager.UpdateEquipmentSet(equipmentSet, mutationId, gunEquipId1, EquipmentManager.Instance.gunOwnedId),
                (string data) =>
                {
                    JSONObject jsonData = new JSONObject(data);
                    DataManager.Instance.UserData.usersetEquipmentInfor.Find(x => x.userEquipmentId == equipmentSet).gunOwnershipId2 = EquipmentManager.Instance.gunOwnedId;
                    Init();
                }));
    }
}
