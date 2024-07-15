using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PowerUpCard : MonoBehaviour
{
    [SerializeField] private PowerUpData powerUpData;
    [SerializeField] private Button cardButton; 
    [SerializeField] private TextMeshProUGUI cardTitle;
    [SerializeField] private Image cardImage;
    [SerializeField] private TextMeshProUGUI cardDescription;
    [SerializeField] private PopupChoosePowerUp popupChoosePowerUp;

    public void InitCard(PowerUpData powerUpData,PopupChoosePowerUp popupChoosePowerUp,int lv = 0){
        cardTitle.text = powerUpData.powerUpName;
        cardDescription.text = powerUpData.UpdateDescription(lv);
        this.powerUpData = powerUpData;
        this.popupChoosePowerUp = popupChoosePowerUp;
    }
    public void OnCardClick(){
        GameManager.Instance.returnPowerIdUpChosen?.Invoke(powerUpData.id);
        popupChoosePowerUp.Disappear();
    }
}
