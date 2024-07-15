using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class CharcaterAssetManager : Singleton<CharcaterAssetManager>
{
    [SerializeField] private SpriteAtlas characterIcons;

    public Sprite GetIcon(string name)
    {
        return characterIcons.GetSprite(name);
    }
}
