using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// Contain all game systems,is mediator for all systems,system use this to communicate with other system.
/// </summary>
public class MainGame
{
    private static MainGame _instance;
    public static MainGame Instance
    {
        get { if (_instance == null)
                _instance = new MainGame();
            return _instance;
        }
    }

    private PlayerController playerController = null;
    public PlayerController GetPlayerController
    {
        get { return playerController; }
    }
    private ItemPreviewSystem itemPreviewSystem = null;
    public ItemPreviewSystem GetItemPreviewSystem
    {
        get { return itemPreviewSystem; }
    }
    private DoorController doorController = null;
    public DoorController DoorController
    {
        get { return doorController; }
    }
    private ItemController itemController = null;
    public ItemController ItemController
    {
        get { return itemController; }
    }

    private EnemyControlSystem enemyControlSystem = null;
    public EnemyControlSystem GetEnemyControlSystem
    {
        get { return enemyControlSystem; }
    }

    private GameEventControlSystemBase gameEventControlSystem = null;
    public GameEventControlSystemBase GetGameEventControlSystem
    {
        get { return gameEventControlSystem; }
    }

    //UIInterface
    private GameOptionInterface gameOptionInterface = null;
    public GameOptionInterface GetGameOptionInterface
    {
        get { return gameOptionInterface; }
    }
    private InteractiveInterface interactiveInterface = null;
    public InteractiveInterface GetInteractiveInterface
    {
        get { return interactiveInterface; }
    }
    private InventoryInterface inventoryInterface = null;
    private FontControlSystem fontControlSystem = null;
    private InteractiveActionObjController interactiveActionObjController = null;
    public InteractiveActionObjController GetInteractiveActionObjController
    {
        get { return interactiveActionObjController; }
    }
    private SceneAniEffectControlSystem sceneAniEffectControlSystem = null;

    private InstantiateObjDB instantiateObjDB = null;

    private TaskItemGenerateSystem taskItemGenerateSystem = null;

    private ScaryEffectControlSystem scaryEffectControlSystem = null;

    private MainGame() { }
    public void Initial(int sceneNum)
    {
        if(sceneNum != 4)
        {
            instantiateObjDB = Unity.CustomTool.UnityTool.FindGameObject("InstantiateObjDB").GetComponent<InstantiateObjDB>();

            playerController = new PlayerController(this);
            itemPreviewSystem = new ItemPreviewSystem(this);
            doorController = new DoorController(this);
            itemController = new ItemController(this);
            enemyControlSystem = new EnemyControlSystem(this);


            fontControlSystem = new FontControlSystem(this);
            //這個不需要傳入MainGame參考
            gameOptionInterface = new GameOptionInterface();
            interactiveInterface = new InteractiveInterface(this);
            inventoryInterface = new InventoryInterface(this);
            interactiveActionObjController = new InteractiveActionObjController(this);
            sceneAniEffectControlSystem = new SceneAniEffectControlSystem(this);
            if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "MainGameScene_2")
                taskItemGenerateSystem = new TaskItemGenerateSystem(this);
            scaryEffectControlSystem = new ScaryEffectControlSystem(this);
        }
        else
        {
            playerController = new PlayerController(this);
            fontControlSystem = new FontControlSystem(this);
            //這個不需要傳入MainGame參考
            gameOptionInterface = new GameOptionInterface();
            interactiveInterface = new InteractiveInterface(this);
            inventoryInterface = new InventoryInterface(this);
            interactiveActionObjController = new InteractiveActionObjController(this);
            sceneAniEffectControlSystem = new SceneAniEffectControlSystem(this);
            scaryEffectControlSystem = new ScaryEffectControlSystem(this);
        }
        

        switch (sceneNum)
        {
            case 1:
                gameEventControlSystem = new GameEventControlSystem_Scene1(this);
                break;
            case 2:
                gameEventControlSystem = new GameEventControlSystem_Scene2(this);
                break;
            case 3:
                gameEventControlSystem = new GameEventControlSystem_Scene3(this);
                break;
            case 4:
                gameEventControlSystem = new GameEventControlSystem_Scene4(this);
                break;
        }
    }
    public void Update()
    {
        if(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name != "MainGameScene_4")
        {
            playerController.Update();
            itemPreviewSystem.Update();
            doorController.Update();
            enemyControlSystem.Update();

            fontControlSystem.Update();
            interactiveInterface.Update();
            inventoryInterface.Update();
            interactiveActionObjController.Update();

            gameEventControlSystem.Update();
        }
        else
        {
            playerController.Update();

            fontControlSystem.Update();
            interactiveInterface.Update();
            inventoryInterface.Update();
            interactiveActionObjController.Update();

            gameEventControlSystem.Update();
            sceneAniEffectControlSystem.Update();
        }
    }

    /// <summary>
    /// Get the player gameObject in the scene.
    /// </summary>
    /// <returns>Return gameObject.</returns>
    public GameObject GetPlayer()
    {
        return playerController.PlayerObj;
    }

    /// <summary>
    /// Set door state,use on some game event.
    /// </summary>
    /// <param name="doorStatus">True is open,false is close.</param>
    /// <param name="doorName">The door object name you want to interactive.</param>
    /// <param name="playAudio">The door audio.</param>
    public void ForceInteractiveDoor(bool doorStatus, string doorName, bool playAudio)
    {
        if (doorController != null)
            doorController.ForceInteractiveDoor(doorStatus, doorName, playAudio);
    }

    /// <summary>
    /// Get the enemy gameObject in the scene.
    /// </summary>
    /// <returns>Return gameObject.</returns>
    public GameObject GetEnemy()
    {
        return enemyControlSystem.State.EnemyObj;
    }
    
    /// <summary>
    /// Clear all inventory sprites,when new scene begin,call this.
    /// </summary>
    public void ClearInventorySprite()
    {
        inventoryInterface.ClearInventorySprite();
    }

    /// <summary>
    /// Using font conrtoller system to display font.
    /// </summary>
    /// <param name="senName">The sentence you want to display,a sentence is a string array.</param>
    /// <param name="beginSen">The string in current sentence that begin to display.</param>
    /// <param name="endSen">The string in current sentence that at last.</param>
    public void StartFontControl(string senName,int beginSen,int endSen)
    {
        fontControlSystem.StartFontControl(senName,beginSen,endSen);
    }

    //顯示教學字幕
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sentence"></param>
    public void DisplayTutorialText(string sentence)
    {
        fontControlSystem.DisplayTutorialText(sentence);
    }
    //關閉教學字幕
    public void EndTutorialText()
    {
        fontControlSystem.DisplayTutorialText("");
        fontControlSystem.TutorialInterfaceContainer.gameObject.SetActive(false);
    }

    //獲取PlayerController腳本的enemyStayPointObjs List
    public List<Transform> GetEnemyStayPointObjs()
    {
        return playerController.GetEnemyStayPointObjs();
    }
    //獲取玩家身上的攝影機
    public Transform GetPlayerCamera()
    {
        return playerController.CameraObj;
    }
    //鎖定玩家移動控制及鏡頭控制一段時間
    public void LockPlayerAllControlState(float time)
    {
        if (playerController != null)
            playerController.LockPlayerControlState(time);
            
    }
    //移動鏡頭至目標
    public void CameraLookTarget(Transform target)
    {
        if (playerController != null)
            playerController.SetPlayerLookDirection(target);
    }

    //獲取字幕功能status
    public bool GetFontControlEnd()
    {
        return fontControlSystem.FontControlEnd;
    }

    //獲取熊熊字幕
    public Transform GetEnemySpeakWordsImg()
    {
        return fontControlSystem.EnemySpeakWordsImg;
    }

    //更改熊熊字幕
    public void ChangeEnemySpeakWordsImg(UnityEngine.UI.Image img)
    {
        if (fontControlSystem != null)
            fontControlSystem.SetEnemySpeakWordsImg(img);
    }

    //玩家看到熊熊方法
    public void PlayerLookEnemy()
    {
        if (playerController != null)
            playerController.PlayerLookEnemy();
    }

    //設定PlayerController裡的doNotLookEnemyAction
    public void SetDoNotLookEnemyAction(Action action)
    {
        playerController.DoNotLookEnemyAction = action;
    }

    //獲取敵人動畫
    public Animator GetEnemyAni()
    {
        return GetEnemyControlSystem.State.EnemyAni;
    }

    //敵人行為
    public void EnemyBehaviour()
    {
        if (enemyControlSystem.State != null)
            enemyControlSystem.State.EnemyBehaviour();
    }

    //設定玩家控制狀態跟攝影鏡頭狀態
    public void SetPlayerControlStateAndCameraState(bool state)
    {
        if (playerController != null)
            playerController.SetPlayerControlStateAndCameraState(state);
    }

    //移動玩家到重生點
    public void SetPlayerPosToRespawnPoint()
    {
        if (playerController != null)
            playerController.PlayerObj.transform.position = playerController.PlayerRespawnPoint.position;
    }

    //畫面變暗變亮轉場
    public void SceneTransitionAni(Action action,bool isTransit)
    {
        if (sceneAniEffectControlSystem != null)
            sceneAniEffectControlSystem.SceneTransitionAnime(action,isTransit);
    }

    //設定預覽物件
    public void SetPreviewItem(Transform obj)
    {
        if (interactiveInterface != null)
            interactiveInterface.SetPreviewItem(obj);
    }

    //獲取InstantiateObjDB物件
    public InstantiateObjDB GetInstantiateObjDB()
    {
        return instantiateObjDB;
    }

    //收集到紙片物件要執行的方法
    public void SetPaperPieceCollected(Transform item)
    {
        if (inventoryInterface != null)
            inventoryInterface.CollectPaperPiece(item);
    }

    //設定覆蓋整個場景的Panel的Active
    public void SetCoverSceneImageActive(bool state)
    {
        if (sceneAniEffectControlSystem != null)
            sceneAniEffectControlSystem.SetCoverSceneImageActive(state);
    }

    //獲取GameEventControlSystem_Scene1裡用來檢測玩家是否達到教學目標的inputCount
    public int GetTutorialInputCount()
    {
        return gameEventControlSystem.TutorialInputCount;
    }

    //設定GameEventControlSystem_Scene1裡用來檢測玩家是否達到教學目標的inputCount
    public void SetTutorialInputCount(int count)
    {
        gameEventControlSystem.TutorialInputCount = count;
    }

    //獲取ItemPreviewPanel物件
    public GameObject GetItemPreviewPanel()
    {
        return interactiveInterface.ItemPreviewPanel;
    }

    //獲取選單頁面
    public Transform GetGameOptionPanel()
    {
        return gameOptionInterface.GameOptionPanel.transform;
    }

    //獲取門鎖住的提示文字
    public GameObject GetDoorLockHintText()
    {
        return interactiveInterface.DoorLockHint;
    }

    //獲取恐怖音效
    public AudioSource GetEffectAudioSourceByInt(int num)
    {
        return scaryEffectControlSystem.GetEffectAudioSourceByInt(num);
    }

    //獲取恐怖音效By名稱
    public AudioSource GetEffectAudioSourceByName(string name)
    {
        return scaryEffectControlSystem.GetEffectAudioSourceByName(name);
    }

    //顯示碎紙片數量
    public void DisplayPaperPieceCount()
    {
        if (inventoryInterface != null)
            inventoryInterface.DisplayPaperPieceCount();
    }

    //獲取碎紙片字幕物件
    public GameObject GetPaperPieceCountText()
    {
        return inventoryInterface.PaperPieceCountText.gameObject;
    }

    //獲取SceneAniEffectControlSystem的是否完成轉場狀態
    public bool GetSceneTransitionAnimeComplete()
    {
        return sceneAniEffectControlSystem.TransitCompelet;
    }

    //場景4熊熊行為
    public void Scene4_EnemyBehaviour(Transform target)
    {
        if (scaryEffectControlSystem != null)
            scaryEffectControlSystem.EnemyLookTarget(target);
    }

    //啟動暈邊效果
    public void StartVignetteEffect()
    {
        if (scaryEffectControlSystem != null)
            scaryEffectControlSystem.SetVignetteEffect();
    }
    //獲取場景動畫控制器的upPoint參數
    public RectTransform GetSceneAniEffectControlSystemUpPoint()
    {
        return sceneAniEffectControlSystem.UpPoint;
    }

    //獲取InteractiveInterface的ViewItemReturnHint
    public GameObject GetViewItemReturnHint()
    {
        return interactiveInterface.ViewItemReturnHint;
    }

    //燈光開關效果
    public void LightOpenClose(GameObject bulbObj, GameObject lightObj, bool isOpen)
    {
        scaryEffectControlSystem.LightOpenClose(bulbObj, lightObj, isOpen);
    }

    //獲取燈光物件
    public GameObject FindLightObjByName(string obj)
    {
        return scaryEffectControlSystem.FindLightObjByName(obj);
    }

    //遊戲結尾動畫
    public void EndGameAnimeHandler()
    {
        sceneAniEffectControlSystem.EndGameAnimeHandler();
    }

    //設定遊戲結尾動畫背景狀態
    public void SetEndGamePanelState(bool state)
    {
        sceneAniEffectControlSystem.SetEndGamePanelState(state);
    }

    //設定遊戲結尾動畫ContainerAction
    public void SetEndGameAnimeHandlerAction(Action action)
    {
        sceneAniEffectControlSystem.EndGameAnimeHandlerAction = action;
    }

    //返回大廳方法
    public void BackMainMenu()
    {
        ActionStorage.Instance.GameLogicUpdateAction = null;
        ActionStorage.Instance.SetSceneStateNumContainer.Invoke(99);
    }

    //一些玩家數值初始化
    public void InitializePlayerValue()
    {
        if(playerController != null)
            playerController.InitailPlayerSize();
    }

    //設定唯讀物件的位置
    public void SetReadOnlyItemOriginPos()
    {
        interactiveInterface.SetReadOnlyItemPos();
    }

    //Get interactive hint state in hierarchy
    public bool GetInteractiveHintState()
    {
        return fontControlSystem.GetInteractiveHintState();
    }

    //Get font start
    public bool GetFontStart()
    {
        return fontControlSystem.FontControlStart;
    }
}
