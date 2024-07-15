using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HUDManager : Singleton<HUDManager>
{
    public TextMeshProUGUI FPSCounter;
    float oneSec = 1f;
    private void Update()
    {
        oneSec -= Time.deltaTime;
        if (oneSec <= 0)
        {
            FPSCounter.text = "FPS: " + ((int)(1 / Time.deltaTime)).ToString();
            oneSec = 1f;
        }
    }
}
