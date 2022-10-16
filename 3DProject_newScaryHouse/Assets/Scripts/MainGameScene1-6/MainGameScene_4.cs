using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGameScene_4 : ISceneState
{
    public MainGameScene_4(SceneStateController Controller) : base(Controller)
    {
        this.StateName = "MainGameScene_4";
    }
    public void StateBegin()
    {

    }
    public void StateUpdate()
    {
        m_Controller.SetState(new MainGameScene_5(m_Controller), "MainGameScene_5");
    }
    public void StateEnd()
    {

    }
}
