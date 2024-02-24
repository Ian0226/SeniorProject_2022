using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UITool 
{
    private static GameObject m_CanvasObj = null; //場景上的2D畫布物件

    //找尋限定在Canvas畫布下的UI介面
    public static GameObject FindUIGameObject(string UIName)
    {
        if (m_CanvasObj == null)
            m_CanvasObj = UnityTool.FindGameObject("Canvas");
        if (m_CanvasObj == null)
            return null;
        return UnityTool.FindChildGameObject(m_CanvasObj, UIName);
    }

    //取得UI元件
    public static T GetUIComponent<T>(GameObject Container,string UIName) where T : UnityEngine.Component
    {
        //找出子物件
        GameObject ChildGameObject = UnityTool.FindChildGameObject(Container, UIName);
        if (ChildGameObject == null)
            return null;

        T tempObj = ChildGameObject.GetComponent<T>();
        if(tempObj == null)
        {
            Debug.LogWarning("元件[" + UIName + "]不是[" + typeof(T) + "]");
            return null;
        }
        return tempObj;
    }
}
