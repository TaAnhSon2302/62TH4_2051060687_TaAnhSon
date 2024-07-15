using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MutationHealthBar : MonoBehaviour
{
    public Image frame;
    public Image shieldFill;
    public Image healthFill;
    public Image energyFill;
    public TextMeshProUGUI shieldText;
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI energyText;
    private float maxShieldFill;
    private float maxHealthFill;
    private float maxEnergyFill;
    private void Start() {
        maxShieldFill = shieldFill.rectTransform.rect.width;
        maxHealthFill = healthFill.rectTransform.rect.width;
        maxEnergyFill = energyFill.rectTransform.rect.width;
    }
    public void AdjustShield(float percentage,string shield){
        shieldText.text = shield;
        shieldFill.rectTransform.sizeDelta = new Vector2(maxShieldFill * percentage ,shieldFill.rectTransform.rect.height);
        return;
    }
    public void AdjustHealth(float percentage,string health){
        healthText.text = health;
        healthFill.rectTransform.sizeDelta = new Vector2(maxHealthFill * percentage ,healthFill.rectTransform.rect.height);
        return;
    }
    public void AdjustEnergy(float percentage,string energy){
        energyText.text = energy;
        energyFill.rectTransform.sizeDelta = new Vector2(maxEnergyFill * percentage ,energyFill.rectTransform.rect.height);
        return;
    }
}
