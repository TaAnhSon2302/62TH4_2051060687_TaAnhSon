using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RainbowText : MonoBehaviour
{
    private TextMeshProUGUI text;
    public float cycleSpeed = 5f;

    private void Start(){
        text = GetComponent<TextMeshProUGUI>();
    }
    private void OnEnable() {
        StartCoroutine(IERainbow());
    }
    private IEnumerator IERainbow()
    {
        float time = 0f;
        while (text == null)
            yield return null;
        while (true)
        {
            float r = Mathf.Sin(time) * 0.5f + 0.5f;
            float b = Mathf.Sin(time + 2f) * 0.5f + 0.5f;
            float g = Mathf.Sin(time + 4f) * 0.5f + 0.5f;
            Color targetColor = new Color(r, b, g, 1f);
            text.color = targetColor;
            time += Time.fixedDeltaTime * cycleSpeed;
            yield return new WaitForSeconds(Time.fixedDeltaTime);
        }
    }
 
}
