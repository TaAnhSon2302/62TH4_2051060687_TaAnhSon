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
    [SerializeField] public string mutationId;
    [SerializeField] public string mutationOwnedId="";
    [SerializeField] public Image selecteBorder;
    public void InitIcon(EnemyCellOOP _data)
    {
        Sprite sprite = enemy.GetSprite(_data.enemyId);
        enemyId = _data.enemyId;
        icon.sprite = sprite;
        var button = GetComponent<Button>();
        button.onClick.AddListener(OnClick);
        selecteBorder.enabled = false;
    }    
    public void InitCharIcon(UserMutaitonInfor _data)
    {
        if (_data == null)
        {
            icon.sprite = mutation.GetSprite("questionMark");
            return;
        }
        Sprite sprite = mutation.GetSprite(_data.mutationId);   

        icon.sprite = sprite;
        mutationId = _data.mutationId;
        mutationOwnedId = _data.ownerShipId;
        var button = GetComponent<Button>();
        button.onClick.AddListener(OnClickMutation);
        selecteBorder.enabled = false;
    }
    public void OnClick()
    {
        ColletionManager.Instance.OnClickShowInfor(enemyId);
    }
    public void OnClickMutation()
    {
        EquipmentManager.Instance.mutationOwnedId = mutationOwnedId;
        EquipmentManager.Instance.OnClickMutaitonSelected(mutationOwnedId);
        Debug.LogFormat(mutationOwnedId);
    }
}
