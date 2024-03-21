using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOptionInterface
{
    private GameObject gameOptionPanel;
    public GameObject GameOptionPanel
    {
        get { return gameOptionPanel; }
    }
    private Button returnButton;
    private Button BackMainMenuButton;

    private AudioController audioController = null;
    private Slider mouseSensitivitySlider;

    public GameOptionInterface()
    {
        Initialize();
    }
    public void Initialize()
    {
        if (SceneManager.GetActiveScene().name == "MainMenuScene")
            gameOptionPanel = GameObject.Find("Canvas").transform.Find("GameOptionInterface").gameObject;
        else
            gameOptionPanel = Unity.CustomTool.UnityTool.FindGameObject("GameOptionInterface");
        returnButton = Unity.CustomUITool.UITool.GetUIComponent<Button>(gameOptionPanel, "ReturnButton");
        audioController = new AudioController();
        mouseSensitivitySlider = Unity.CustomUITool.UITool.GetUIComponent<Slider>(gameOptionPanel, "MouseSlider");
        if (returnButton != null)
            returnButton.onClick.AddListener(
                                            () => ClosePanel()
                                            );
        BackMainMenuButton = Unity.CustomUITool.UITool.GetUIComponent<Button>(gameOptionPanel, "BackMainMenuButton");
        if (BackMainMenuButton != null)
            BackMainMenuButton.onClick.AddListener(
                                                  () => BackMainMenu()
                                                  );
        
        gameOptionPanel.SetActive(false);
        mouseSensitivitySlider.onValueChanged.AddListener(
                                                         (Slider) => ChangeMouseSensitivity()
                                                         );
    }
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && gameOptionPanel.activeInHierarchy != true)
        {
            OpenGameOption();
        }
        else if(Input.GetKeyDown(KeyCode.Escape) && gameOptionPanel.activeInHierarchy == true)
        {
            ClosePanel();
        }
    }
    public void OpenGameOption()
    {
        if (SceneManager.GetActiveScene().name != "MainMenuScene")
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        ActionStorage.Instance.GameLogicUpdateAction = null;
        gameOptionPanel.SetActive(true);
    }
    public void ClosePanel()
    {
        if (SceneManager.GetActiveScene().name != "MainMenuScene")
        {
            if (MainGame.Instance.GetInteractiveInterface.ItemPreviewPanel.activeInHierarchy != true)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }

        ActionStorage.Instance.GameLogicUpdateAction = MainGame.Instance.Update;
        gameOptionPanel.SetActive(false);
    }
    public void ChangeMouseSensitivity()
    {
        GameSettingParamStorage.MouseSensitivity = mouseSensitivitySlider.value;
    }
    public void BackMainMenu()
    {
        ClosePanel();
        ActionStorage.Instance.GameLogicUpdateAction = null;
        //SceneManager.LoadScene(1);
        ActionStorage.Instance.SetSceneStateNumContainer.Invoke(99);
    }
    
}
