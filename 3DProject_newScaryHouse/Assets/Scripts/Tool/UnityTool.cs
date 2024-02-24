using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UnityTool 
{
    //�������W������
    public static GameObject FindGameObject(string GameObjectName)
    {
        GameObject pTmpGameObj = GameObject.Find(GameObjectName);
        if (pTmpGameObj == null)
        {
            Debug.LogWarning("�������䤣��GameObject[" + GameObjectName + "]����");
            
            return null;
        }
        return pTmpGameObj;
    }

    //���o�l����
    public static GameObject FindChildGameObject(GameObject Container,string gameObjectName)
    {
        if(Container == null)
        {
            Debug.LogError("NGUICustomTools.GetChild:Container - null");

            return null;
        }

        Transform pGameObjectTF = null;

        //�O���OContainer����
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
                        Debug.LogWarning("Container[" + Container.name + "[�U��X���ƪ�����W��[" + gameObjectName + "]");
                }
            }
        }

        //���S���
        if(pGameObjectTF == null)
        {
            Debug.Log("����[" + Container.name + "]�䤣��l����[" + gameObjectName + "]");

            return null;
        }

        return pGameObjectTF.gameObject;
    }
}
