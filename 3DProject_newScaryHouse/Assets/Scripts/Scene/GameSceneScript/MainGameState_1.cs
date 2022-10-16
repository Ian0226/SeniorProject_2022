using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGameState_1 : ISceneState
{
    public MainGameState_1(SceneStateController Controller) : base(Controller)
    {
        this.StateName = "MainGameScene_1";
    }
    public override void StateBegin()
    {

    }
    public override void StateUpdate()
    {
        if (GameLoop.testInt == 2)
            m_Controller.SetState(new MainGameState_2(m_Controller), "MainGameScene_2");
    }
    public override void StateEnd()
    {

    }
}
