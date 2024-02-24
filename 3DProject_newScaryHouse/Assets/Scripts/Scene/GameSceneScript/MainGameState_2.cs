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
        GameObject.Find("RespawnPoint").transform.position = new Vector3(-20, 3.5f, 6.7f);//Json
        GameObject.Find("Player").transform.position = GameObject.Find("RespawnPoint").transform.position;
        MainGame.Instance.InitializePlayerValue();
        MainGame.Instance.Initial(2);
        MainGame.Instance.GetEnemyControlSystem.SetState(new EnemyState_2(MainGame.Instance.GetEnemyControlSystem, MainGame.Instance));
        ActionStorage.Instance.GameLogicUpdateAction = MainGame.Instance.Update;
        MainGame.Instance.SceneTransitionAni(null, true);

        Inventory.ClearAllItem();
        MainGame.Instance.ClearInventorySprite();
    }
    public override void StateUpdate()
    {
        if (ActionStorage.Instance.GameLogicUpdateAction != null)
        {
            ActionStorage.Instance.GameLogicUpdateAction();
        }
        MainGame.Instance.GetGameOptionInterface.Update();
        if (GameLoop.testInt == 3 && sceneChanged == false)
        {
            MainGame.Instance.SceneTransitionAni(() => m_Controller.SetState(new MainGameState_3(m_Controller), "MainGameScene_3"), false);
            sceneChanged = true;
        }
        if (GameLoop.testInt == 99 && sceneChanged == false)
        {
            MainGame.Instance.SceneTransitionAni(() => m_Controller.SetState(new MainMenuState(m_Controller), "MainMenuScene"), false);
            sceneChanged = true;
        }

    }
    public override void StateEnd()
    {

    }
}
