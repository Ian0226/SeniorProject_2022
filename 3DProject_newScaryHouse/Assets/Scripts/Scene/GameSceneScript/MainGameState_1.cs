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
        GameObject.Find("RespawnPoint").transform.position = new Vector3(26.13f, 2.2f, 17.02f);//Json
        GameObject.Find("Player").transform.position = GameObject.Find("RespawnPoint").transform.position;
        MainGame.Instance.InitializePlayerValue();
        MainGame.Instance.Initial(1);//設定GameEventControlSystem,1為遊戲主要場景
        MainGame.Instance.GetEnemyControlSystem.SetState(new EnemyState_1(MainGame.Instance.GetEnemyControlSystem, MainGame.Instance));
        ActionStorage.Instance.GameLogicUpdateAction = MainGame.Instance.Update;
        //GameObject.Find("Canvas").GetComponent<UIManager>().Initialize();
        MainGame.Instance.GetPlayerController.SetPlayerControlStatus(false);
    }
    public override void StateUpdate()
    {
        if (ActionStorage.Instance.GameLogicUpdateAction != null)
        {
            ActionStorage.Instance.GameLogicUpdateAction();
        }
        MainGame.Instance.GetGameOptionInterface.Update();
        if (GameLoop.testInt == 2 && sceneChanged == false)
        {
            MainGame.Instance.SceneTransitionAni(() => m_Controller.SetState(new MainGameState_2(m_Controller), "MainGameScene_2"),false);
            sceneChanged = true;
        }
        if (GameLoop.testInt == 99 && sceneChanged == false)
        {
            MainGame.Instance.SceneTransitionAni(() => m_Controller.SetState(new MainMenuState(m_Controller), "MainMenuScene"), false);
            sceneChanged = true;
        }
        if (MainGame.Instance.GetGameEventControlSystem.PlayerTutorialHandler != null)
            MainGame.Instance.GetGameEventControlSystem.PlayerTutorialHandler();
    }
    public override void StateEnd()
    {

    }
}
