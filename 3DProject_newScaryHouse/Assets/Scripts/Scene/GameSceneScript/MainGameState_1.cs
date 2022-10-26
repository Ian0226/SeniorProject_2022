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
        GameObject.Find("RespawnPoint").transform.position = new Vector3(26.13f, 3, 17.02f);
        GameObject.Find("Player").transform.position = GameObject.Find("RespawnPoint").transform.position;
        GameObject.Find("Canvas").GetComponent<UIManager>().Initialize();
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
