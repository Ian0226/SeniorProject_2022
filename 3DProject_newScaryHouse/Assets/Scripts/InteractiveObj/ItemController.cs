using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemController : InteractableObjBase
{
    public Image image;
    public override void Interactive()
    {
        Inventory.SetObj(this.gameObject);
        Debug.Log(Inventory.FindObjbyInt(0));
    }
}
