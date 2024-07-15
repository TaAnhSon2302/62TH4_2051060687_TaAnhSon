using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Game Setting", menuName = "Scriptable Objects/Game Setting")]
public class GameSetting : ScriptableObject
{
    public int gameFPS = 24;
    public float gameVolume = 1f;
    public float sfxVolume = 1f;

}
