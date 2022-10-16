using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartScene : ISceneState
{
    public StartScene(SceneStateController Controller):base(Controller)
    {
        this.StateName = "StartState";
    }
    public void StateBegin()
    {

    }
    public void StateUpdate()
    {
        m_Controller.SetState(new MainMenuScene(m_Controller), "MainMenuScene");
    }
    public void StateEnd()
    {

    }
}
