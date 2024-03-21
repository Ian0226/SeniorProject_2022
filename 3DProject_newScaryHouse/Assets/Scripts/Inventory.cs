using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Inventory class,store the items that player collected,give some operations on player inventory panel.
/// </summary>
public class Inventory : MonoBehaviour
{
    private static List<GameObject> interactiveObjs = new List<GameObject>();
    private static List<Sprite> itemSprites = new List<Sprite>();

    private static GameObject specialItem;
    public static GameObject SpecialItem
    {
        get { return specialItem; }
        set { specialItem = value; }
    }
    private static Transform[] paperPieces = new Transform[8];

    private static int paperPieceCount = 0;
    public static int PaperPieceCount
    {
        get { return paperPieceCount; }
        set { paperPieceCount = value; }
    }

    public static Transform GetPaperPieceByInt(int num)
    {
        return paperPieces[num];
    }
    public static void ClearPaperPieces()
    {
        for(int i = 0; i < paperPieces.Length; i++)
        {
            paperPieces[i] = null;
        }
    }
    public static void AddPaperPieces(int num,Transform item)
    {
        paperPieces[num] = item;
    }
    public static List<GameObject> GetObjs()
    {
        return interactiveObjs;
    }

    /// <summary>
    /// Store object into player inventory.
    /// </summary>
    /// <param name="obj">The gameObject want to store.</param>
    public static void SetObj(GameObject obj)
    {
        interactiveObjs.Add(obj);
        itemSprites.Add(obj.GetComponent<ItemObj>().GetSprite());
    }

    /// <summary>
    /// Find object in interactiveObjs list by object name,and return it.
    /// </summary>
    /// <param name="objName">The object name that want to find.</param>
    /// <returns>The gameObject want to find.</returns>
    public static GameObject FindObj(string objName) 
    {
        GameObject container = null;
        foreach(GameObject obj in interactiveObjs)
        {
            if (objName == obj.name)
            {
                container = obj;
                break;
            }
            else
            {
                container = null;
            }
        }
        return container;
    }
    
    public static GameObject FindObjByInt(int num) 
    {
        return interactiveObjs[num];
    }
    public static Sprite FindItemSpriteByInt(int num)
    {
        return itemSprites[num];
    }

    /// <summary>
    /// Handle remove item in interactiveObjs list. 
    /// </summary>
    /// <param name="name">The item name you want to clear.</param>
    public static void ClearItemByName(string name)
    {
        for(int i=0;i< interactiveObjs.Count;i++)
        {
            if (interactiveObjs[i].name == name)
            {
                Destroy(interactiveObjs[i]);
                interactiveObjs.RemoveAt(i);
                itemSprites.RemoveAt(i);
                MainGame.Instance.ClearInventorySprite();
                break;
            }
        }
        for (int i = 0; i < interactiveObjs.Count; i++)
            Debug.Log(interactiveObjs[i] + "" + itemSprites[i]);
    }

    public static void ClearAllItem()
    {
        interactiveObjs.Clear();
        itemSprites.Clear();
    }

    public static Sprite GetSpecialItemSprite()
    {
        return specialItem.GetComponent<ItemObj>().GetSprite();
    }
}
