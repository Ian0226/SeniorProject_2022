using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UITool 
{
    private static GameObject m_CanvasObj = null; //�����W��2D�e������

    //��M���w�bCanvas�e���U��UI����
    public static GameObject FindUIGameObject(string UIName)
    {
        if (m_CanvasObj == null)
            m_CanvasObj = UnityTool.FindGameObject("Canvas");
        if (m_CanvasObj == null)
            return null;
        return UnityTool.FindChildGameObject(m_CanvasObj, UIName);
    }

    //���oUI����
    public static T GetUIComponent<T>(GameObject Container,string UIName) where T : UnityEngine.Component
    {
        //��X�l����
        GameObject ChildGameObject = UnityTool.FindChildGameObject(Container, UIName);
        if (ChildGameObject == null)
            return null;

        T tempObj = ChildGameObject.GetComponent<T>();
        if(tempObj == null)
        {
            Debug.LogWarning("����[" + UIName + "]���O[" + typeof(T) + "]");
            return null;
        }
        return tempObj;
    }
}
