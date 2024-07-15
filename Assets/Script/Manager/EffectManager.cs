using System.Collections;
using System.Collections.Generic;
using static GameStatic;
using UnityEngine;
using Lean.Pool;
using TMPro;
using System.Linq;
using Unity.Collections;

public class EffectManager : Singleton<EffectManager>
{
    public Color damage = Color.red;
    public Color heal = Color.green;
    public StatusEffect statusEffect;
    public GameObject effectHolder;
    public List<XPObs> listXpObs = new();
    public List<int> listXpPerObs = new(){1000,500,200,100,50,20,10,1};
    public GameObject FireBlashVFX;
    private void Start()
    {
        // listXpObs = new();
        // listXpObs = Resources.LoadAll<XPObs>("Prefab/Obs").ToList();
        FireBlashVFX = Resources.Load<GameObject>("Prefab/UI/VFX/Fire_blash_VFX");
    }
    public void ShowFireBlashVFX(Transform transform){
         Vector2 temp;
        temp = new Vector2(transform.position.x, transform.position.y);
        GameObject fireballVFX = LeanPool.Spawn(FireBlashVFX, temp, Quaternion.identity,effectHolder.transform);
        LeanTween.delayedCall(1.5f, () => { LeanPool.Despawn(fireballVFX); });
    }
    public void ShowDamageInfict(int damage, int criticalTier, Transform transform, string status = null)
    {
        Vector2 temp;
        temp = new Vector2(transform.position.x + Random.Range(-0.5f, 0.51f), transform.position.y);
        StatusEffect statusEffect = LeanPool.Spawn(this.statusEffect, temp, Quaternion.identity, effectHolder.transform);
        if (status != null)
            statusEffect.statusText.text = damage.ToString() + " " + status;
        else
        {
            statusEffect.statusText.text = damage.ToString();
        }
        switch (criticalTier)
        {
            case 0:
                statusEffect.statusText.color = CRITICAL_TIER_0_COLOR;
                break;
            case 1:
                statusEffect.statusText.color = CRITICAL_TIER_1_COLOR;
                break;
            case 2:
                statusEffect.statusText.color = CRITICAL_TIER_2_COLOR;
                break;
            case 3:
                statusEffect.statusText.color = CRITICAL_TIER_3_COLOR;
                break;
            case 4:
                statusEffect.statusText.color = CRITICAL_TIER_4_COLOR;
                break;
            case 5:
                statusEffect.statusText.color = CRITICAL_TIER_5_COLOR;
                break;
            case 6:
                statusEffect.statusText.color = SHIELD_DAMAGE_COLOR;
                break;
            default:
                statusEffect.statusText.color = CRITICAL_TIER_5_COLOR;
                break;
        }
    }
    public void TransformStringByRandom(TextMeshProUGUI inputString, string outputString, float time)
    {
        StartCoroutine(IETransformStringByRandom(inputString, outputString, time));
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
                else if ((int)charArray[i] < 91)
                    charArray[i] = (char)Random.Range(65, 91);
                else
                    charArray[i] = (char)Random.Range(97, 123);
            }
            time -= Time.fixedDeltaTime;
            inputString.text = string.Concat(charArray);
        }
        inputString.text = outputString;
    }
    public void SpawnObs(GameObject objectSpawn, int amount)
    {
        int divide;
        for(int i = 0;i<listXpPerObs.Count;i++){
            divide = amount / listXpPerObs[i];
            if (divide > 0){
                for (int j = 0; j < divide; j++){
                    LeanPool.Spawn(listXpObs[i], EnemySpawner.Instance.SetTargetCyclePos(0.1f, objectSpawn.transform.position), Quaternion.identity, effectHolder.transform);

                }
                amount -= divide * listXpPerObs[i];
            }
        }
    }
}
