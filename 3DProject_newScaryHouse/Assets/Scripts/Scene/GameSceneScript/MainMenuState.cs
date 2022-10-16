using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuState : ISceneState
{
    public MainMenuState(SceneStateController Controller) : base(Controller)
    {
        this.StateName = "StartState";
    }
    public override void StateBegin()
    {

    }
    public override void StateUpdate()
    {
        if (GameLoop.testInt == 1)
            m_Controller.SetState(new MainGameState_1(m_Controller), "MainGameScene_1");
    }
    public override void StateEnd()
    {

    }
}
