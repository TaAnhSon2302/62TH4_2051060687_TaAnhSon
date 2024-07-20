using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Pool;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] public GameObject slot1;
    [SerializeField] public GameObject slot2;
    public CellGun cellgun1;
    public CellGun cellgun2;
    private UserSetEquipmentInfor equipmentSet = DataManager.Instance.UserData.usersetEquipmentInfor[0];
    [SerializeField] public UserGunInformation equipmentSlot1 = new();
    [SerializeField] public UserGunInformation equipmentSlot2 = new();
    [SerializeField] private MutationJaguar mutaiton;
    public StateMachine gameStateMachine;
    void Start()
    {
        equipmentSlot1 = DataManager.Instance.UserData.userGunInformation.Find(x => x.ownerShipId == equipmentSet.gunOwnershipId1);
        equipmentSlot2 = DataManager.Instance.UserData.userGunInformation.Find(x => x.ownerShipId == equipmentSet.gunOwnershipId2);
        if(equipmentSlot1 != null)
        {
            cellgun1 = DataManager.Instance.listGun.Find(x => x.gunId == equipmentSlot1.gunId);
            CellGun checkIsfirtgun1 = Instantiate(cellgun1, slot1.transform);
            checkIsfirtgun1.isFirstGun = true;
        }
        else { return; }
        if(equipmentSlot2 != null)
        {
            cellgun2 = DataManager.Instance.listGun.Find(x => x.gunId == equipmentSlot2.gunId);
            CellGun checkIsfirtgun2 = Instantiate(cellgun2, slot2.transform);
            checkIsfirtgun2.isFirstGun = false;
        }
        else { return; }
     
       
        mutaiton = GetComponent<MutationJaguar>();
    }

    // Update is called once per frame
    void Update()
    {
        GameManager.Instance.CheckIsDead();
    }
}
