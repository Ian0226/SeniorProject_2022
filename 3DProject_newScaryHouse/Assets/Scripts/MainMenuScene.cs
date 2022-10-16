using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuScene : ISceneState
{
    public MainMenuScene(SceneStateController Controller) : base(Controller)
    {
        this.StateName = "MainMenuScene";
    }
    public void StateBegin()
    {

    }
    public void StateUpdate()
    {
        m_Controller.SetState(new MainGameScene_1(m_Controller), "MainGameScene_1");
    }
    public void StateEnd()
    {

    }
}
