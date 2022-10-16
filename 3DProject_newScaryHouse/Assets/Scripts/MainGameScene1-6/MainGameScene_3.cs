using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGameScene_3 : ISceneState
{
    public MainGameScene_3(SceneStateController Controller) : base(Controller)
    {
        this.StateName = "MainGameScene_3";
    }
    public void StateBegin()
    {

    }
    public void StateUpdate()
    {
        m_Controller.SetState(new MainGameScene_4(m_Controller), "MainGameScene_4");
    }
    public void StateEnd()
    {

    }
}
