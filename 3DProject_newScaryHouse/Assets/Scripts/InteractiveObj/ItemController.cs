using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : InteractableObjBase
{
    [SerializeField]
    private Sprite itemSprite;
    public override void Interactive() 
    {
        Inventory.SetObj(this.gameObject);
    }
    public Sprite GetSprite()
    {
        return itemSprite;
    }
}
