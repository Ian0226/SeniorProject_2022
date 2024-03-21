using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.CustomTool;

public class MainMenuState : ISceneState
{
    private GameOptionInterface gameOptionInterface = null;
    private MainMenuCameraController cameraControlSystem = null;
    public MainMenuState(SceneStateController Controller) : base(Controller)
    {
        this.StateName = "StartState";
    }
    public override void StateBegin()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        //When player return to menu,destroy some don't destroy object.
        if(UnityTool.FindGameObject("Player"))
            GameObject.Destroy(UnityTool.FindGameObject("Player"));
        if (UnityTool.FindGameObject("PaperPieceCollectedPosition_0"))
            GameObject.Destroy(UnityTool.FindGameObject("PaperPieceCollectedPosition_0"));
        if (GameObject.FindGameObjectWithTag("PaperPiecePosition"))
            GameObject.Destroy(GameObject.FindGameObjectWithTag("PaperPiecePosition").transform.parent.gameObject);
        if(UnityTool.FindGameObject("PaperPieceCollectedPositionGroup"))
            GameObject.Destroy(UnityTool.FindGameObject("PaperPieceCollectedPositionGroup"));
        if (UnityTool.FindGameObject("MethodDelay"))
            GameObject.Destroy(UnityTool.FindGameObject("MethodDelay"));
        if (GameObject.FindGameObjectWithTag("PaperPiece"))
        {
            foreach (GameObject obj in GameObject.FindGameObjectsWithTag("PaperPiece"))
                GameObject.Destroy(obj);
        }

        gameOptionInterface = new GameOptionInterface();
        cameraControlSystem = new MainMenuCameraController();
        Button startBtn = GameObject.Find("StartButton").GetComponent<Button>();
        if(startBtn != null)
            startBtn.onClick.AddListener(
                                        ()=>OnStartGameBtnClick(startBtn)
                                        );

        Button openOptionBtn = GameObject.Find("OptionButton").GetComponent<Button>();
        if (openOptionBtn != null)
            openOptionBtn.onClick.AddListener(
                                             ()=>OpenOptionPanel()
                                             );

        Button exitBtn = GameObject.Find("ExitButton").GetComponent<Button>();
        if (exitBtn != null)
            exitBtn.onClick.AddListener(
                                       () => ExitGame()
                                       );

        Inventory.ClearPaperPieces();
        Inventory.PaperPieceCount = 0;
        Inventory.ClearAllItem();
        
    }
    public override void StateUpdate()
    {
        cameraControlSystem.Update();
    }
    private void OnStartGameBtnClick(Button btn)
    {
        ActionStorage.Instance.SetSceneStateNumContainer.Invoke(1);
        m_Controller.SetState(new MainGameState_1(m_Controller), "MainGameScene_1");
    }

    //ÂIÀ»Ä~Äò¹CÀ¸«ö¶s
    private void ContinueGame(int sceneNum)
    {
        switch (sceneNum)
        {
            case 1:
                m_Controller.SetState(new MainGameState_1(m_Controller), "MainGameScene_1");
                break;
            case 2:
                m_Controller.SetState(new MainGameState_2(m_Controller), "MainGameScene_2");
                break;
            case 3:
                m_Controller.SetState(new MainGameState_3(m_Controller), "MainGameScene_3");
                break;
            case 4:
                m_Controller.SetState(new MainGameState_4(m_Controller), "MainGameScene_4");
                break;
        }
    }

    private void OpenOptionPanel()
    {
        gameOptionInterface.OpenGameOption();
    }
    private void ExitGame()
    {
        Application.Quit();
    }
}
