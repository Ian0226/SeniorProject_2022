using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveActionObjController : InteractableObjBase
{
    private List<GameObject> itemObjs = new List<GameObject>();
    private List<InteractiveActionObj> itemObjComponents = new List<InteractiveActionObj>();
    public InteractiveActionObjController(MainGame main) : base(main)
    {
        Initialize();
    }
    public override void Initialize()
    {
        itemObjs.AddRange(GameObject.FindGameObjectsWithTag("InteractiveActionItem"));
        itemObjs.AddRange(GameObject.FindGameObjectsWithTag("BigInteractiveObject"));
        foreach(GameObject obj in itemObjs)
        {
            itemObjComponents.Add(obj.gameObject.GetComponent<InteractiveActionObj>());
        }
    }
    public InteractiveActionObj GetObjComponentByName(string name)
    {
        GameObject obj = new GameObject();
        for(int i = 0;i < itemObjComponents.Count; i++)
        {
            if(itemObjComponents[i].gameObject.name == name)
            {
                obj = itemObjComponents[i].gameObject;
                break;
            }
            else
            {
                obj = null;
                continue;
            }
        }
        return obj.GetComponent<InteractiveActionObj>();
    }
}
