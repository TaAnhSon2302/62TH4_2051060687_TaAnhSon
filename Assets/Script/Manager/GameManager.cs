using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Lean.Pool;
using UnityEngine.UI;
using System;
using TMPro;
using Unity.VisualScripting;
using System.Linq;
using Unity.Mathematics;

public class GameManager : Singleton<GameManager>
{
    public CinemachineVirtualCamera virtualCamera;
    public int maximumEnemies = 50;
    public Mutation mutation;
    public Transform powerUpHolder;
    public int maxCardToChoose = 3;
    public List<PowerUpData> listPowerUpDatas = new();
    public List<PowerUpData> listPlayerPowerUpDatas = new();
    public List<PowerUp> listPlayerPowerUps = new();
    public bool isChoosingPowerUp = false;
    public Transform bulletHolder;
    public Action<string> returnPowerIdUpChosen;
    [Space(10)]
    [Header("Game UI")]
    public MutationHealthBar healthBar;
    public Slider xpBar;
    public TextMeshProUGUI currentLvText;
    public int currentLv = 0;
    public int xpRequire;
    public int currentXp;
    [Space(10)]
    [Header("Game State")]
    public StateMachine gameStateMachine;
    public bool isPause = false;
    private void Start()
    {
        Init();
        gameStateMachine.ChangeState(new GameStatePlay());
    }
    private void Init()
    {
        // spawn player's mutation here
        Debug.Log(DataManager.Instance.listMutation.Count);
        //mutation = LeanPool.Spawn(DataManager.Instance.listMutation[0]);
        var mutationSelected = DataManager.Instance.UserData.UserMutationInfor.Find(x => x.ownerShipId == DataManager.Instance.UserData.userSetEquipmentDefault.mutationOwnershipId);
        mutation = LeanPool.Spawn(DataManager.Instance.listMutation.Find(x => x.mutationId == mutationSelected.mutationId));
        EnemySpawner.Instance.playerPosition = mutation.transform;
        virtualCamera.Follow = mutation.transform;
        AudioManager.Instance.StartNormalBattleHematos();
        xpRequire = DataManager.Instance.Data.listIngameLevelConfig[currentLv].xpRequire;
        powerUpHolder.SetParent(mutation.transform);
        powerUpHolder.localPosition = Vector3.zero;
        listPowerUpDatas = Resources.LoadAll<PowerUpData>("Scriptable Object/PowerUps").ToList();
    }
    public void OnPauseClick()
    {
        if (!isPause)
        {
            gameStateMachine.ChangeState(new GameStatePause());
            PopupPauseGamePlay.Show();
        }
        else
        {
            gameStateMachine.ChangeState(new GameStatePlay());
        }
    }
    public void OnObsCollect(int XpObs)
    {
        int surplus = 0;
        while (surplus >= 0)
        {
            surplus = XpObs - (xpRequire - currentXp);
            if (surplus < 0)
            {
                currentXp += XpObs;
            }
            else
            {
                XpObs -= xpRequire - currentXp;
                currentXp += xpRequire - currentXp;
                currentLv++;
                OnLevelUp();
                currentLvText.text = currentLv.ToString();
                xpRequire = DataManager.Instance.Data.listIngameLevelConfig[currentLv].xpRequire;
                currentXp = 0;
            }
        }
        xpBar.value = (float)currentXp/xpRequire;
    }
    public void OnLevelUp(){
        if (!isPause)
        {
            gameStateMachine.ChangeState(new GameStatePause());
            isChoosingPowerUp = true;
            StartCoroutine(IEWaitForChoosingPowerUp());
            PopupChoosePowerUp.Show();
            
        }
    }
    public void CheckIsDead()
    {
        if (mutation.healPoint <= 0)
        {
            gameStateMachine.ChangeState(new GameStateLose());
            Destroy(mutation.gameObject);
            PopupGameOver.Show();
        }
    }
    public IEnumerator IEWaitForChoosingPowerUp(){
        returnPowerIdUpChosen += AddPowerUpToMutation;
        yield return new WaitUntil(()=>!isChoosingPowerUp);
        returnPowerIdUpChosen -= AddPowerUpToMutation;
        gameStateMachine.ChangeState(new GameStatePlay());
    }
    public void AddPowerUpToMutation(string powerUpId){
        // Debug.Log("power up choose: " + powerUpId);
        isChoosingPowerUp = false;
        bool isExistPowerUp = listPowerUpDatas.Exists(x => x.id == powerUpId);
        if (isExistPowerUp){
            PowerUp powerUp = listPowerUpDatas.Find(x=>x.id == powerUpId).powerUp;
            PowerUp temp = LeanPool.Spawn(powerUp,powerUpHolder);
            listPlayerPowerUpDatas.Add(listPowerUpDatas.Find(x => x.id == powerUpId));
            listPlayerPowerUps.Add(temp);
            listPowerUpDatas.Remove(listPowerUpDatas.Find(x => x.id == powerUpId));
        }
        else{
            PowerUp powerUp = listPlayerPowerUps.Find(x=>x.id == powerUpId);
            powerUp.OnLevelUp(powerUp.lv+1);
        }
    }
}
[Serializable]
public class IngameLevelConfigsOOP
{
    public int inGameLv;
    public int xpRequire;
}
[Serializable]
public enum PowerUpRarity{
    Common,
    Uncommon,
    Rare,
    Epic,
    Legendary,
    Corrupted,
}
