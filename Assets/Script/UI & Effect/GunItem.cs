using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

public class GunItem : MonoBehaviour
{
    [SerializeField] public Image icon;
    [SerializeField] private SpriteAtlas gun;
    [SerializeField] Button chooseButton;
    [SerializeField] public string gunId;
    [SerializeField] public string gunOwenredId = "";
    [SerializeField] public string bulletId;
    [SerializeField] public Image selectedBorder;


    //EquipedGun Parameters

    public void InitIcon(UserGunInformation cellgun)
    {
        Sprite sprite = gun.GetSprite(cellgun.gunId);
        gunId = cellgun.gunId;
        gunOwenredId = cellgun.ownerShipId;
        var gunData = DataManager.Instance.Data.listGun.Find(x => x.gunId == gunId);
        bulletId = gunData.bulletId;
        icon.sprite = sprite;
        selectedBorder.enabled = false;
    }
    public void InitEquipIcon(UserGunInformation cellgun)
    {
        Sprite sprite = gun.GetSprite(cellgun.gunId);
        gunId = cellgun.gunId;
        gunOwenredId = cellgun.ownerShipId;
        var gunData = DataManager.Instance.Data.listGun.Find(x => x.gunId == gunId);
        bulletId = gunData.bulletId;
        icon.sprite = sprite;
        selectedBorder.enabled = false;
    }
    public void OnClick()
    {
        EquipmentManager.Instance.bulletId = bulletId;
        EquipmentManager.Instance.gunOwnedId = gunOwenredId;
        EquipmentManager.Instance.OnClickShowInfor(gunId);
        Debug.Log(gunOwenredId);
    }
}
