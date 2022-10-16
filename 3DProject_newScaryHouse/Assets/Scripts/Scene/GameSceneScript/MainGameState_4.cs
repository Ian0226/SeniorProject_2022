using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGameState_4 : ISceneState
{
    public MainGameState_4(SceneStateController Controller) : base(Controller)
    {
        this.StateName = "MainGameScene_4";
    }
    public override void StateBegin()
    {

    }
    public override void StateUpdate()
    {
        if (GameLoop.testInt == 5)
            m_Controller.SetState(new MainGameState_5(m_Controller), "MainGameScene_5");
    }
    public override void StateEnd()
    {

    }
}
