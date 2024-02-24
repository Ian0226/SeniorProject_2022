using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEventControlSystem_Scene4 : GameEventControlSystemBase
{
    public GameEventControlSystem_Scene4(MainGame main) : base(main)
    {
        Initialize();
    }
    public override void Initialize()
    {
        SetActionState(Event_1_EndGame);
    }
    public override void SetActionState(Action action)
    {
        handler = action;
    }
    public void Event_1_EndGame()
    {
        if(mainGame.GetPlayerController.NowCollisionObj != null && mainGame.GetPlayerController.NowCollisionObj.name == "EventTriggerCollider_1")
        {
            mainGame.Scene4_EnemyBehaviour(UnityTool.FindGameObject("Player").transform);
            MethodDelayExecuteTool.ExecuteDelayedMethod(2f,() => mainGame.SetEndGamePanelState(true));
            mainGame.SetEndGameAnimeHandlerAction(mainGame.EndGameAnimeHandler);
            SetActionState(null);
        }
    }
}
