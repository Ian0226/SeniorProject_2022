using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGameState_5 : ISceneState
{
    public MainGameState_5(SceneStateController Controller) : base(Controller)
    {
        this.StateName = "MainGameScene_5";
    }
    public override void StateBegin()
    {

    }
    public override void StateUpdate()
    {
        if (GameLoop.testInt == 6)
            m_Controller.SetState(new MainGameState_6(m_Controller), "MainGameScene_6");
    }
    public override void StateEnd()
    {

    }
}
