using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UnityTool 
{
    //找到場景上的物件
    public static GameObject FindGameObject(string GameObjectName)
    {
        GameObject pTmpGameObj = GameObject.Find(GameObjectName);
        if (pTmpGameObj == null)
        {
            Debug.LogWarning("場景中找不到GameObject[" + GameObjectName + "]物件");
            
            return null;
        }
        return pTmpGameObj;
    }

    //取得子物件
    public static GameObject FindChildGameObject(GameObject Container,string gameObjectName)
    {
        if(Container == null)
        {
            Debug.LogError("NGUICustomTools.GetChild:Container - null");

            return null;
        }

        Transform pGameObjectTF = null;

        //是不是Container本身
        if (Container.name == gameObjectName)
        {
            pGameObjectTF = Container.transform;
        }
        else
        {
            Transform[] allChildren = Container.transform.GetComponentsInChildren<Transform>();
            foreach(Transform child in allChildren)
            {
                if(child.name == gameObjectName)
                {
                    if (pGameObjectTF == null)
                        pGameObjectTF = child;
                    else
                        Debug.LogWarning("Container[" + Container.name + "[下找出重複的元件名稱[" + gameObjectName + "]");
                }
            }
        }

        //都沒找到
        if(pGameObjectTF == null)
        {
            Debug.Log("元件[" + Container.name + "]找不到子元件[" + gameObjectName + "]");

            return null;
        }

        return pGameObjectTF.gameObject;
    }
}
