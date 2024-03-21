using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 宣告一些遊戲中會使用到的Action，此類別為Singleton，使用Instance獲取該類別物件。
/// </summary>
public class ActionStorage
{
    private static ActionStorage _instance;
    public static ActionStorage Instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = new ActionStorage();
            }
            return _instance;
        }
    }

    private Action<bool> setPlayerControlStatusAction; //Contain PlayerController.SetPlayerControlStatus() method
    public Action<bool> SetPlayerControlStatusAction
    {
        get { return setPlayerControlStatusAction; }
        set { setPlayerControlStatusAction = value; }
    }

    /// <summary>
    /// Use to change scene.
    /// </summary>
    private Action<int> setSceneStateNumContainer = (int num) => { GameLoop.testInt = num; };
    public Action<int> SetSceneStateNumContainer
    {
        get { return setSceneStateNumContainer; }
        set { setSceneStateNumContainer = value; }
    }

    private Action closeItemPreviewPanelAction;
    public Action CloseItemPreviewPanelAction
    {
        get { return closeItemPreviewPanelAction; }
        set { closeItemPreviewPanelAction = value; }
    }

    /// <summary>
    /// Contain gameLogic method,which contains all game system's update.
    /// </summary>
    private Action gameLogicUpdateAction;
    public Action GameLogicUpdateAction
    {
        get { return gameLogicUpdateAction; }
        set { gameLogicUpdateAction = value; }
    }


    private ActionStorage() { }

}
