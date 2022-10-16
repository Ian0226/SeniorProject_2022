using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGameScene_2 : ISceneState
{
    public MainGameScene_2(SceneStateController Controller) : base(Controller)
    {
        this.StateName = "MainGameScene_2";
    }
    public void StateBegin()
    {

    }
    public void StateUpdate()
    {
        m_Controller.SetState(new MainGameScene_3(m_Controller), "MainGameScene_3");
    }
    public void StateEnd()
    {

    }
}
