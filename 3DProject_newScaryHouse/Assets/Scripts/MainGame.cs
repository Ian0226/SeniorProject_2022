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
            //�o�Ӥ��ݭn�ǤJMainGame�Ѧ�
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
            //�o�Ӥ��ݭn�ǤJMainGame�Ѧ�
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

    //��ܱоǦr��
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sentence"></param>
    public void DisplayTutorialText(string sentence)
    {
        fontControlSystem.DisplayTutorialText(sentence);
    }
    //�����оǦr��
    public void EndTutorialText()
    {
        fontControlSystem.DisplayTutorialText("");
        fontControlSystem.TutorialInterfaceContainer.gameObject.SetActive(false);
    }

    //���PlayerController�}����enemyStayPointObjs List
    public List<Transform> GetEnemyStayPointObjs()
    {
        return playerController.GetEnemyStayPointObjs();
    }
    //������a���W����v��
    public Transform GetPlayerCamera()
    {
        return playerController.CameraObj;
    }
    //��w���a���ʱ�������Y����@�q�ɶ�
    public void LockPlayerAllControlState(float time)
    {
        if (playerController != null)
            playerController.LockPlayerControlState(time);
            
    }
    //�������Y�ܥؼ�
    public void CameraLookTarget(Transform target)
    {
        if (playerController != null)
            playerController.SetPlayerLookDirection(target);
    }

    //����r���\��status
    public bool GetFontControlEnd()
    {
        return fontControlSystem.FontControlEnd;
    }

    //��������r��
    public Transform GetEnemySpeakWordsImg()
    {
        return fontControlSystem.EnemySpeakWordsImg;
    }

    //��ﺵ���r��
    public void ChangeEnemySpeakWordsImg(UnityEngine.UI.Image img)
    {
        if (fontControlSystem != null)
            fontControlSystem.SetEnemySpeakWordsImg(img);
    }

    //���a�ݨ캵����k
    public void PlayerLookEnemy()
    {
        if (playerController != null)
            playerController.PlayerLookEnemy();
    }

    //�]�wPlayerController�̪�doNotLookEnemyAction
    public void SetDoNotLookEnemyAction(Action action)
    {
        playerController.DoNotLookEnemyAction = action;
    }

    //����ĤH�ʵe
    public Animator GetEnemyAni()
    {
        return GetEnemyControlSystem.State.EnemyAni;
    }

    //�ĤH�欰
    public void EnemyBehaviour()
    {
        if (enemyControlSystem.State != null)
            enemyControlSystem.State.EnemyBehaviour();
    }

    //�]�w���a����A����v���Y���A
    public void SetPlayerControlStateAndCameraState(bool state)
    {
        if (playerController != null)
            playerController.SetPlayerControlStateAndCameraState(state);
    }

    //���ʪ��a�쭫���I
    public void SetPlayerPosToRespawnPoint()
    {
        if (playerController != null)
            playerController.PlayerObj.transform.position = playerController.PlayerRespawnPoint.position;
    }

    //�e���ܷt�ܫG���
    public void SceneTransitionAni(Action action,bool isTransit)
    {
        if (sceneAniEffectControlSystem != null)
            sceneAniEffectControlSystem.SceneTransitionAnime(action,isTransit);
    }

    //�]�w�w������
    public void SetPreviewItem(Transform obj)
    {
        if (interactiveInterface != null)
            interactiveInterface.SetPreviewItem(obj);
    }

    //���InstantiateObjDB����
    public InstantiateObjDB GetInstantiateObjDB()
    {
        return instantiateObjDB;
    }

    //������Ȥ�����n���檺��k
    public void SetPaperPieceCollected(Transform item)
    {
        if (inventoryInterface != null)
            inventoryInterface.CollectPaperPiece(item);
    }

    //�]�w�л\��ӳ�����Panel��Active
    public void SetCoverSceneImageActive(bool state)
    {
        if (sceneAniEffectControlSystem != null)
            sceneAniEffectControlSystem.SetCoverSceneImageActive(state);
    }

    //���GameEventControlSystem_Scene1�̥Ψ��˴����a�O�_�F��оǥؼЪ�inputCount
    public int GetTutorialInputCount()
    {
        return gameEventControlSystem.TutorialInputCount;
    }

    //�]�wGameEventControlSystem_Scene1�̥Ψ��˴����a�O�_�F��оǥؼЪ�inputCount
    public void SetTutorialInputCount(int count)
    {
        gameEventControlSystem.TutorialInputCount = count;
    }

    //���ItemPreviewPanel����
    public GameObject GetItemPreviewPanel()
    {
        return interactiveInterface.ItemPreviewPanel;
    }

    //�����歶��
    public Transform GetGameOptionPanel()
    {
        return gameOptionInterface.GameOptionPanel.transform;
    }

    //�����������ܤ�r
    public GameObject GetDoorLockHintText()
    {
        return interactiveInterface.DoorLockHint;
    }

    //������ƭ���
    public AudioSource GetEffectAudioSourceByInt(int num)
    {
        return scaryEffectControlSystem.GetEffectAudioSourceByInt(num);
    }

    //������ƭ���By�W��
    public AudioSource GetEffectAudioSourceByName(string name)
    {
        return scaryEffectControlSystem.GetEffectAudioSourceByName(name);
    }

    //��ܸH�Ȥ��ƶq
    public void DisplayPaperPieceCount()
    {
        if (inventoryInterface != null)
            inventoryInterface.DisplayPaperPieceCount();
    }

    //����H�Ȥ��r������
    public GameObject GetPaperPieceCountText()
    {
        return inventoryInterface.PaperPieceCountText.gameObject;
    }

    //���SceneAniEffectControlSystem���O�_����������A
    public bool GetSceneTransitionAnimeComplete()
    {
        return sceneAniEffectControlSystem.TransitCompelet;
    }

    //����4�����欰
    public void Scene4_EnemyBehaviour(Transform target)
    {
        if (scaryEffectControlSystem != null)
            scaryEffectControlSystem.EnemyLookTarget(target);
    }

    //�Ұʷw��ĪG
    public void StartVignetteEffect()
    {
        if (scaryEffectControlSystem != null)
            scaryEffectControlSystem.SetVignetteEffect();
    }
    //��������ʵe�����upPoint�Ѽ�
    public RectTransform GetSceneAniEffectControlSystemUpPoint()
    {
        return sceneAniEffectControlSystem.UpPoint;
    }

    //���InteractiveInterface��ViewItemReturnHint
    public GameObject GetViewItemReturnHint()
    {
        return interactiveInterface.ViewItemReturnHint;
    }

    //�O���}���ĪG
    public void LightOpenClose(GameObject bulbObj, GameObject lightObj, bool isOpen)
    {
        scaryEffectControlSystem.LightOpenClose(bulbObj, lightObj, isOpen);
    }

    //����O������
    public GameObject FindLightObjByName(string obj)
    {
        return scaryEffectControlSystem.FindLightObjByName(obj);
    }

    //�C�������ʵe
    public void EndGameAnimeHandler()
    {
        sceneAniEffectControlSystem.EndGameAnimeHandler();
    }

    //�]�w�C�������ʵe�I�����A
    public void SetEndGamePanelState(bool state)
    {
        sceneAniEffectControlSystem.SetEndGamePanelState(state);
    }

    //�]�w�C�������ʵeContainerAction
    public void SetEndGameAnimeHandlerAction(Action action)
    {
        sceneAniEffectControlSystem.EndGameAnimeHandlerAction = action;
    }

    //��^�j�U��k
    public void BackMainMenu()
    {
        ActionStorage.Instance.GameLogicUpdateAction = null;
        ActionStorage.Instance.SetSceneStateNumContainer.Invoke(99);
    }

    //�@�Ǫ��a�ƭȪ�l��
    public void InitializePlayerValue()
    {
        if(playerController != null)
            playerController.InitailPlayerSize();
    }

    //�]�w��Ū���󪺦�m
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
