using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGameState_3 : ISceneState
{
    public MainGameState_3(SceneStateController Controller) : base(Controller)
    {
        this.StateName = "MainGameScene_3";
    }
    public override void StateBegin()
    {

    }
    public override void StateUpdate()
    {
        if (GameLoop.testInt == 4)
            m_Controller.SetState(new MainGameState_4(m_Controller), "MainGameScene_4");
    }
    public override void StateEnd()
    {

    }
}
