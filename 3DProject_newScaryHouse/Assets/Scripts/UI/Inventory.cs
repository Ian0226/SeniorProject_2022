using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private static List<GameObject> interactiveObjs;

    public static List<GameObject> GetObj()
    {
        return interactiveObjs;
    }

    public static void SetObj(GameObject obj)
    {
        interactiveObjs.Add(obj);
    }

    public static void FindObj(string ItemController)
    {

    }

    public static GameObject FindObjbyInt(int num)
    {
        return interactiveObjs[num];
    }
}
