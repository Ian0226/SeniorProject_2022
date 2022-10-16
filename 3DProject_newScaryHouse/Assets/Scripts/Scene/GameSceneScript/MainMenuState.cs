using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuState : ISceneState
{
    public MainMenuState(SceneStateController Controller) : base(Controller)
    {
        this.StateName = "StartState";
    }
    public override void StateBegin()
    {
        Button startBtn = GameObject.Find("StartButton").GetComponent<Button>();
        if(startBtn != null)
            startBtn.onClick.AddListener(
                                        ()=>OnStartGameBtnClick(startBtn)
                                        );
        
    }
    private void OnStartGameBtnClick(Button btn)
    {
        m_Controller.SetState(new MainGameState_1(m_Controller), "MainGameScene_1");
    }
}
