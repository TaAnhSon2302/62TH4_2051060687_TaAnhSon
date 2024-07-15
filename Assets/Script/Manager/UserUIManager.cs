using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static GameStatic;

public class UserUIManager : Singleton<UserUIManager>
{
    public List<Color> listUIColor;
    public Color currentUIColor = USER_UI_COLOR_CYAN;
    private void Start() {
        currentUIColor = USER_UI_COLOR_CYAN;
        listUIColor = new List<Color>(){
            CRITICAL_TIER_5_COLOR,
            USER_UI_COLOR_BLUE,
            USER_UI_COLOR_CYAN,
            USER_UI_COLOR_PURPLE,
        };
    }
    public void ChangeUIColor(Color color){
        currentUIColor = color;
    }
    public Color GetCurrentUIColor(){
        return currentUIColor;
    }   
    public void TransformStringByRandom(TextMeshProUGUI inputString, string outputString, float time){
        StartCoroutine(IETransformStringByRandom(inputString,outputString,time));
    }
    public IEnumerator IETransformStringByRandom(TextMeshProUGUI inputString, string outputString, float time)
    {
        char[] charArray = inputString.text.ToCharArray();
        while (time > 0)
        {
            yield return new WaitForFixedUpdate();
            for (int i = 0; i < charArray.Length; i++)
            {
                if ((int)charArray[i] == 32) continue;
                else if((int)charArray[i]<91)
                    charArray[i] = (char)Random.Range(65, 91);
                else
                    charArray[i]= (char)Random.Range(97,123);
            }
            time -= Time.fixedDeltaTime;
            inputString.text = string.Concat(charArray);
        }
        inputString.text = outputString;
    }
}
