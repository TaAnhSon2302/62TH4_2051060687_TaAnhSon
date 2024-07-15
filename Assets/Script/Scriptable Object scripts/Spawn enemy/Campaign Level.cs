using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Campaign Level", menuName = "Scriptable Objects/Spawn/Campaign Level", order = 2)]
public class CampaignLevel : ScriptableObject
{
    public List<WaveSpawn> waves =new();
}
