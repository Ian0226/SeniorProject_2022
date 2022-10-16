using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGameScene_1 : ISceneState
{
    public MainGameScene_1(SceneStateController Controller) : base(Controller)
    {
        this.StateName = "MainGameScene_1";
    }
    public void StateBegin()
    {

    }
    public void StateUpdate()
    {
        m_Controller.SetState(new MainGameScene_2(m_Controller), "MainGameScene_2");
    }
    public void StateEnd()
    {

    }
}
