using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGameState_2 : ISceneState
{
    public MainGameState_2(SceneStateController Controller) : base(Controller)
    {
        this.StateName = "MainGameScene_2";
    }
    public override void StateBegin()
    {

    }
    public override void StateUpdate()
    {
        if (GameLoop.testInt == 3)
            m_Controller.SetState(new MainGameState_3(m_Controller), "MainGameScene_3");
    }
    public override void StateEnd()
    {

    }
}
