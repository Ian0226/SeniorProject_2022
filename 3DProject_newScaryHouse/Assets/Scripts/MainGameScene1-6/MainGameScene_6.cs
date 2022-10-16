using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGameScene_6 : ISceneState
{
    public MainGameScene_6(SceneStateController Controller) : base(Controller)
    {
        this.StateName = "MainGameScene_6";
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
