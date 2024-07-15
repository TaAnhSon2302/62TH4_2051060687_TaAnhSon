using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

public class MutationItem : MonoBehaviour
{
    [SerializeField] public Image icon;
    [SerializeField] private SpriteAtlas mutation;
    [SerializeField] private string mutationId;

    public void InitIcon(MutationOOP _data)
    {
        Sprite sprite = mutation.GetSprite(_data.mutationID);
        mutationId = _data.mutationID;
        icon.sprite = sprite;
    }
}
