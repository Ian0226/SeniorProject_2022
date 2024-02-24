using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObj : InteractiveObjBase_Mono
{
    [SerializeField]
    private Sprite itemSprite;

    public Sprite GetSprite()
    {
        return itemSprite;
    }
}
