using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Lean.Pool;
using System;

public class StatusEffect : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI statusText;
    [SerializeField] private Color statusColor = Color.clear;
    [SerializeField] private Color currentStatusColor;
    [SerializeField] private float fadeDuration = 1f;
    //[SerializeField] private int numberOfFrames = UserManager.Instance.gameSetting.gameFPS;
    [SerializeField] private int numberOfFrames = 24;
    private void OnEnable()
    {
        currentStatusColor = statusText.color;
        // StartCoroutine(IEStatusFade());
        //StatusFade();
        LeanTween.delayedCall(1f,()=>{
            try{
                LeanPool.Despawn(gameObject);
            }
            catch(Exception e){
                Debug.LogWarning(e);
            }
        });
    }
    public void StatusFade()
    {
        currentStatusColor = statusText.color;
        LeanTween.delayedCall(0.01f, () =>
        {
            LeanTween.value(1f, 0f, 1).setOnUpdate((float value) =>
            {
            transform.Translate(new Vector3(0f, 1 / (float)numberOfFrames, 0f));
            //transform.localPosition = new Vector3(0f, value, 0f);
            currentStatusColor.a = 1f * value;
            statusText.color = currentStatusColor;
            }).setOnComplete(() =>
            {
            transform.Translate(Vector3.down);
            currentStatusColor = Color.gray;
            LeanPool.Despawn(gameObject);
            });
        });

    }
    public IEnumerator IEStatusFade()
    {
        yield return new WaitForSeconds(0.01f);
        currentStatusColor = statusText.color;
        for (int i = 0; i < numberOfFrames; i++)
        {
            yield return new WaitForSeconds((float)(fadeDuration / numberOfFrames));
            transform.Translate(new Vector3(0f, 1 / (float)numberOfFrames, 0f));
            currentStatusColor.a -= 1 / (float)(255 / 24);
            statusText.color = currentStatusColor;
        }
        transform.Translate(Vector3.down);
        currentStatusColor = Color.gray;
        LeanPool.Despawn(gameObject);
    }
}
