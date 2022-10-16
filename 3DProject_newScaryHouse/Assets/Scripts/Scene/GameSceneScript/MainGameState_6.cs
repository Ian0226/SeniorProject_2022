using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGameState_6 : ISceneState
{
    public MainGameState_6(SceneStateController Controller) : base(Controller)
    {
        this.StateName = "MainGameScene_5";
    }
    public override void StateBegin()
    {

    }
    public override void StateUpdate()
    {
        if (GameLoop.testInt == 0)
            m_Controller.SetState(new MainMenuState(m_Controller), "MainMenuScene");
    }
    public override void StateEnd()
    {

    }
}
