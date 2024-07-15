using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

public class CharcaterItem : MonoBehaviour
{
    [SerializeField] public Image icon;
    [SerializeField] private SpriteAtlas enemy;
    [SerializeField] Button chooseButton;
    [SerializeField] private string enemyId;

    [SerializeField] private SpriteAtlas mutation;
    [SerializeField] private string mutationId;
    public void InitIcon(EnemyCellOOP _data)
    {
        Sprite sprite = enemy.GetSprite(_data.enemyId);
        enemyId = _data.enemyId;
        icon.sprite = sprite;
       
    }    
    public void InitCharIcon(UserMutaitonInfor _data)
    {
        Sprite sprite = mutation.GetSprite(_data.mutationId);   
        icon.sprite = sprite;
        mutationId = _data.mutationId;
    }
    public void OnClick()
    {
        ColletionManager.Instance.OnClickShowInfor(enemyId);
    }
}
