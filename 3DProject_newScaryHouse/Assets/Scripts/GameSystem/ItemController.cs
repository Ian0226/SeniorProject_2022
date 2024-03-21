using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Store all item in scene,and handle there operations.
/// </summary>
public class ItemController : InteractableObjBase
{
    private int itemNum;
    private List<GameObject> itemObj = new List<GameObject>();
    private List<Sprite> itemSprite = new List<Sprite>();
    public ItemController(MainGame mGame) : base(mGame)
    {
        Initialize();
    }
    public override void Initialize()
    {
        itemObj.AddRange(GameObject.FindGameObjectsWithTag("Item"));
        for(int i = 0; i < itemObj.Count; i++)
        {
            Debug.Log(itemObj[i]);
            itemSprite.Add(itemObj[i].transform.GetComponent<ItemObj>().GetSprite());
        }
    }
    public override void Interactive(string objName)
    {
        FindObjByName(objName);
        Inventory.SetObj(itemObj[itemNum]);
    }
    public Sprite GetSpriteByInt()
    {
        return itemSprite[itemNum];
    }
    public void FindObjByName(string name)
    {
        for(int i = 0; i < itemObj.Count; i++)
        {
            if(itemObj[i].name == name)
            {
                itemNum = i;
            }
        }
    }
}
