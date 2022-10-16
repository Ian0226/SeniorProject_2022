using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartState : ISceneState
{
    public StartState(SceneStateController Controller) : base(Controller)
    {
        this.StateName = "StartState";
    }
    public override void StateBegin()
    {

    }
    public override void StateUpdate()
    {
        m_Controller.SetState(new MainMenuState(m_Controller), "MainMenuScene");
    }
    public override void StateEnd()
    {

    }
}
