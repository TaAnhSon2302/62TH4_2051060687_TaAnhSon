using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Wave Spawn", menuName = "Scriptable Objects/Spawn/Wave Spawn", order = 1)]
public class WaveSpawn : ScriptableObject
{
    public int waveDuration = 60;
    public List<WaveSpawnRatio> listSpawn = new();
    public bool isLastWave = false;
}
