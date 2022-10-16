using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private static List<GameObject> interactiveObjs = new List<GameObject>();

    public static List<GameObject> GetObjs()
    {
        return interactiveObjs;
    }

    public static void SetObj(GameObject obj)
    {
        interactiveObjs.Add(obj);
    }

    /*public static GameObject FindObj(string objName) 
    {

    }*/
    
    public static GameObject FindObjByInt(int num) 
    {
        return interactiveObjs[num];
    }
}
