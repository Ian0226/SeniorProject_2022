using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public static void SetObj(GameObject obj)
    {
        interactiveObjs.Add(obj);
        itemSprites.Add(obj.GetComponent<ItemObj>().GetSprite());
    }

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
