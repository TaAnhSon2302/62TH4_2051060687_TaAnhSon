using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentManager : Singleton<EquipmentManager>
{
    public GunItem gunItem;
    [SerializeField] private Transform itemHolder;
    [SerializeField] private List<GunItem> gunItems = new();
    [SerializeField] private List<UserGunInformation> userGunInformations = new();
    public string bulletId;
    [SerializeField] private TextMeshProUGUI gunName;
    [SerializeField] private TextMeshProUGUI fireRate;
    [SerializeField] private TextMeshProUGUI accuracy;
    [SerializeField] private TextMeshProUGUI critRate;
    [SerializeField] private TextMeshProUGUI critMultiple;
    [SerializeField] private TextMeshProUGUI bulletName;
    [SerializeField] public string gunOwnedId;

    
    private void Start()
    {
        userGunInformations = DataManager.Instance.UserData.userGunInformation;
        for (int i = 0; i < userGunInformations.Count; i++)
        {
            var gunSprite = Instantiate(gunItem,itemHolder);
            gunSprite.InitIcon(userGunInformations[i]);
            //Debug.Log(userGunInformation[i].gunId);
            gunItems.Add(gunSprite);
            var button = gunSprite.GetComponent<Button>();
        }
    }
    public void OnClickShowInfor(string id)
    {
        var gun = DataManager.Instance.Data.listGun.Find(x => x.gunId == id);
        var bullet = DataManager.Instance.Data.listBullet.Find(x => x.bulletId == bulletId);
        var gunOwnedId = userGunInformations.Find(x => x.ownerShipId == this.gunOwnedId);
        Debug.Log(gunOwnedId.ownerShipId);
        gunName.text = $"Name: {gun.gunName}";
        fireRate.text = $"Fire rate: {gun.fireRate}";
        accuracy.text = $"Accuracy: {gun.accuracy}";
        critRate.text = $"Crite rate: {gun.criticalRate}";
        critMultiple.text = $"Crit damage: {gun.criticalMultiple}";
        bulletName.text = $"Bullet: {bullet.bulletName}";
        for (int i = 0; i < gunItems.Count; i++)
        {
          if (gunItems[i].gunOwenredId == gunOwnedId.ownerShipId)
            {
                gunItems[i].selectedBorder.enabled = true;
            }
            else
            {
                gunItems[i].selectedBorder.enabled = false;
            }
        }
    }
    public void OnClickBackToMenu()
    {
        SceneLoadManager.Instance.LoadScene(SceneName.MainMenu);
    }
}
