using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DataManager : Singleton<DataManager>
{
    public UserDataOOP UserData = new();
    public DataManagerOOP Data = new();
    public List<Mutation> listMutation;
    public List<CellGun> listGun;

    private void Start()
    {
        NetworkManager.Instance.GetAbilityDataFromServer();
        NetworkManager.Instance.GetMutationDataFromServer();
        NetworkManager.Instance.GetEnemyDataFromServer();
        NetworkManager.Instance.GetBulletDataFromServer();
        NetworkManager.Instance.GetIngameLevelConfigsFromServer();
        NetworkManager.Instance.GetGunFromServer();
        listMutation = Resources.LoadAll<Mutation>("Prefab/Mutation Prefabs").ToList();
        listGun = Resources.LoadAll<CellGun>("Prefab/Gun Prefabs").ToList();
    }
    public void GetUserInformationData(string data) {
        JSONObject json = new JSONObject(data);
        var listData = json.list;
        foreach (var item in listData) {
            UserData.userInformation.userID = item["id"].str;
            UserData.userInformation.userName = item["userName"].str;
            UserData.userInformation.email = item["email"].str;
        }
    }
    public void GetUserGunInformationData(string data)
    {
        JSONObject json = new JSONObject(data);
        var listData = json.list;
        foreach (var item in listData) {
            UserGunInformation userGunInformation = new UserGunInformation();
            userGunInformation.ownerShipId = item["ownershipId"].str;
            userGunInformation.userId = item["userId"].str;
            userGunInformation.gunId = item["gunId"].str;
            userGunInformation.gunLv = (int)item["gunLv"].n;
            userGunInformation.gunXp = (int)item["gunXp"].n;
            UserData.userGunInformation.Add(userGunInformation);
        }
    }
    public void GetUserEquipedGunInfor(string data)
    {
        JSONObject json = new JSONObject(data);
        var listData = json.list;
        foreach (var item in listData)
        {
            UserSetEquipmentInfor userSetEquipment = new UserSetEquipmentInfor();
            userSetEquipment.userEquipmentId = item["userEquipmentId"].str;
            userSetEquipment.userId = item["userId"].str;
            userSetEquipment.mutationOwnershipId = item["mutationOwnershipId"].str;
            userSetEquipment.gunOwnershipId1 = item["gunOwnershipId1"].str;
            userSetEquipment.gunOwnershipId2 = item["gunOwnershipId2"].str;
            UserData.usersetEquipmentInfor.Add(userSetEquipment);
        }
    }
    public void GetUserMutationInfor(string data)
    {
        JSONObject json = new JSONObject(data);
        var listData = json.list;
        foreach(var item in listData) 
        {
            UserMutaitonInfor userMutaitionInfor = new UserMutaitonInfor();
            userMutaitionInfor.ownerShipId = item["ownershipId"].str;
            userMutaitionInfor.userId = item["userId"].str;
            userMutaitionInfor.mutationId = item["mutationId"].str;
            userMutaitionInfor.mutationLv = (int)item["mutationLv"].n;
            userMutaitionInfor.mutationXp = (int)item["mutationXp"].n;
            UserData.UserMutationInfor.Add(userMutaitionInfor);
        }
    }
    public void GetIngameLevelConfigs(string data) {
        JSONObject json = new JSONObject(data);
        var listData = json.list;
        foreach (var item in listData) {
            IngameLevelConfigsOOP lv = new IngameLevelConfigsOOP();
            lv.inGameLv = (int)item["inGameLv"].n;
            lv.xpRequire = (int)item["xpRequire"].n;
            Data.listIngameLevelConfig.Add(lv);
        }
    }
    public void GetMutationData(string data) {
        JSONObject json = new JSONObject(data);
        var listData = json.list;
        foreach (var item in listData) {
            MutationOOP mutation = new MutationOOP();
            mutation.maxHealth = (int)item["hp"].n;
            mutation.maxEnery = (int)item["mp"].n;
            ArmorType armorType;
            Enum.TryParse(item["cellProtection"].str, out armorType);
            ShieldType shieldType;
            Enum.TryParse(item["shieldType"].str, out shieldType);
            mutation.baseCellProtection.armorType = armorType;
            mutation.baseCellProtection.armorPoint = (int)item["armor"].n;
            mutation.baseCellProtection.shieldType = shieldType;
            mutation.baseCellProtection.shieldPoint = (int)item["shield"].n;
            mutation.moveSpeed = (float)item["moveSpeed"].n;
            mutation.mutationID = item["mutationId"].str;
            mutation.mutationName = item["mutationName"].str;
            Faction faction;
            Enum.TryParse(item["factionId"].str, out faction);
            mutation.faction = faction;
            foreach (var ability in item["mutationAbilities"].list) {
                AbilityOOP tempAbility = new AbilityOOP();
                tempAbility.abilityId = ability["abilityId"].str;
                tempAbility.abilityName = ability["abilityName"].str;
                tempAbility.mutationId = ability["mutationId"].str;
                mutation.mutationAbilities.Add(tempAbility);
            }
            Data.listMutations.Add(mutation);
        }
    }
    public void GetEnemydata(string data) {
        JSONObject json = new JSONObject(data);
        var listData = json.list;
        foreach (var item in listData) {
            EnemyCellOOP enemyCell = new();
            enemyCell.enemyId = item["enemyId"].str;
            enemyCell.enemyName = item["enemyName"].str;
            enemyCell.hp = (int)item["hp"].n;
            enemyCell.mp = (int)item["mp"].n;
            ArmorType armorType;
            Enum.TryParse(item["cellProtection"].str, out armorType);
            ShieldType shieldType;
            Enum.TryParse(item["shieldType"].str, out shieldType);
            enemyCell.cellProtection.armorType = armorType;
            enemyCell.cellProtection.armorPoint = (int)item["armor"].n;
            enemyCell.cellProtection.shieldType = shieldType;
            enemyCell.cellProtection.shieldPoint = (int)item["shield"].n;
            Faction faction;
            Enum.TryParse(item["factionId"].str, out faction);
            enemyCell.faction = faction;
            enemyCell.abilityId = item["abilityId"].str;
            enemyCell.moveSpeed = (float)item["moveSpeed"].n;
            Equipment equipment;
            Enum.TryParse(item["equipment"].str, out equipment);
            enemyCell.equipment = equipment;
            enemyCell.bodyDamage = (int)item["bodyDamage"].n;
            enemyCell.XpObs = (int)item["xpObs"].n;
            Data.listEnemies.Add(enemyCell);
        }
    }
    public void GetAbilityData(string data) {
        JSONObject json = new JSONObject(data);
        var listjson = json.list;
        foreach (var item in listjson) {
            AbilityOOP abilityOOP = new AbilityOOP();
            abilityOOP.abilityId = item["abilityId"].str;
            abilityOOP.abilityName = item["abilityName"].str;
            abilityOOP.mutationId = item["mutationId"].str;
            Data.listAbilities.Add(abilityOOP);
        }
    }
    public void GetBulletData(string data)
    {
        JSONObject json = new JSONObject(data);
        var listjson = json.list;
        foreach (var item in listjson)
        {
            BulletOOP bulletOOP = new BulletOOP();
            bulletOOP.bulletId = item["bulletId"].str;
            bulletOOP.bulletName = item["bulletName"].str;
            bulletOOP.bulletTypeId = item["bulletTypeId"].str;
            bulletOOP.timeExist = (float)item["timeExist"].n;
            bulletOOP.damage = (int)item["damage"].n;
            bulletOOP.bulletSpeed = (int)item["bulletSpeed"].n;
            Elements element = new();
            PrimaryElement primaryElement;
            SecondaryElement secondaryElement;
            Enum.TryParse(item["element"].str, out primaryElement);
            Enum.TryParse(item["element"].str, out secondaryElement);
            element.primaryElement = primaryElement;
            element.secondaryElement = secondaryElement;
            bulletOOP.element.primaryElement = element.primaryElement;
            bulletOOP.element.secondaryElement = element.secondaryElement;
            Data.listBullet.Add(bulletOOP);
        }
    }
    public void GetGunData(string data)
    {
        JSONObject json = new JSONObject(data);
        var listjson = json.list;
        foreach(var item in listjson) 
        {
            CellGunOOP cellGunOOP = new CellGunOOP();
            cellGunOOP.gunId = item["gunId"].str;
            cellGunOOP.gunName = item["gunName"].str;
            cellGunOOP.bulletId = item["bulletId"].str;
            cellGunOOP.fireRate = (float)item["fireRate"].n;
            cellGunOOP.accuracy = (float)item["accuracy"].n;
            cellGunOOP.criticalRate = (float)item["criticalRate"].n;
            cellGunOOP.criticalMultiple = (float)item["criticalMultiple"].n;
            Data.listGun.Add(cellGunOOP);
        }
    }
}
 