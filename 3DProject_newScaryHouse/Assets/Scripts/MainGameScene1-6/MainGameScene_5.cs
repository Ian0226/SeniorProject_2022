using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGameScene_5 : ISceneState
{
    public MainGameScene_5(SceneStateController Controller) : base(Controller)
    {
        this.StateName = "MainGameScene_5";
    }
    public void StateBegin()
    {

    }
    public void StateUpdate()
    {
        m_Controller.SetState(new MainGameScene_6(m_Controller), "MainGameScene_6");
    }
    public void StateEnd()
    {

    }
}
