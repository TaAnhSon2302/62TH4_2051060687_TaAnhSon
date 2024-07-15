using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Power Up Data", menuName = "Scriptable Objects/Power Up Data")]
public class PowerUpData : ScriptableObject
{
    public PowerUpRarity rarity = PowerUpRarity.Common;
    public string id;
    public string powerUpName;
    public string description;
    public int maxLv = 5;
    public Image glyph;
    public PowerUp powerUp;
    public virtual string UpdateDescription(int lv){
        return description;
    }

}

