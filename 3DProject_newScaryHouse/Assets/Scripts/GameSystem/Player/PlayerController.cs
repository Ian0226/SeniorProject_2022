using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DG.Tweening;

public class PlayerController : IGameSystem
{
    private GameObject playerObj;
    public GameObject PlayerObj
    {
        get { return playerObj; }
    }
    private Transform specialItemHoldPos;
    private Transform playerHand;

    private bool specialItemIsUsing = false;
    #region Camera
    //攝影機
    private Transform cameraObj;
    public Transform CameraObj
    {
        get { return cameraObj; }
    }
    private Transform camera2;
    public Transform Camera2
    {
        get { return camera2; }
    }
    //CameraContainer
    private Transform cameraContainer;
    #endregion

    Vector3 cameraOriginPos;
    Vector3 cameraOriginRot;
    private Action endViewItemHandler;
    //攝影機控制狀態
    private bool cameraControlState;
    
    private Quaternion cameraOriginRotate;
    private float originOffsetX;
    private float cameraLookAtTargetRotate;
    //移動速度
    private float moveSpeed;
    public float MoveSpeed
    {
        get { return moveSpeed; }
        set { moveSpeed = value; }
    }
    //滑鼠靈敏度
    private float mouseSensitivity;
    public float MouseSensitivity
    {
        get { return mouseSensitivity; }
        set { mouseSensitivity = value; }
    }
    private float xRotate = 0f;
    private float gravity;
    private Transform playerTransform;
    private Transform mainCameraTransform;
    //射線長度
    private float interactiveRange;
    //玩家當前互動的物件(射線打到)
    private GameObject nowInteractiveObj;
    public GameObject NowInteractiveObj
    {
        get { return nowInteractiveObj; }
        set { nowInteractiveObj = value; }
    }
    //玩家當前互動的物件(按E)
    private GameObject interactiveObj;
    public GameObject InteractiveObj
    {
        get { return interactiveObj; }
        set { interactiveObj = value; }
    }
    //玩家當前碰撞到的物件
    private GameObject nowCollisionObj;
    public GameObject NowCollisionObj
    {
        get { return nowCollisionObj; }
        set { nowCollisionObj = value; }
    }

    private CharacterController playerCtrl;

    private Ray ray;
    //射線打到的物件
    private RaycastHit hitObj;

    private bool playerControlStatus;
    public bool PlayerControlStatus
    {
        get { return playerControlStatus;}
    }

    private ColliderDetectionTool colliderDetectionTool;

    //是否衝刺
    private bool isSprint;

    //是否蹲下
    private bool isCrouch;

    private float playerCrouchHeight;

    private float playerOriginHeight;

    private Transform playerDetectEnemyColObj;
    private PlayerDetectEnemyColObj _playerDetectEnemyColObj;

    private Action playerCameraLootAtHandler;

    private float cameraRotateOffsetX = 0;

    private Action playerMissonAction = null;

    private Action doNotLookEnemyAction = null;
    public Action DoNotLookEnemyAction
    {
        get { return doNotLookEnemyAction; }
        set { doNotLookEnemyAction = value; }
    }

    //玩家是否看到敵人
    private bool isPlayerLookEnemy = false;
    //玩家看到敵人幾秒
    private float playerLookEnemyTime = 0;

    private Transform playerRespawnPoint;
    public Transform PlayerRespawnPoint
    {
        get { return playerRespawnPoint; }
    }

    private float baseStepSpeed = 0.6f;
    private float crouchStepMultiplier = 1.5f;
    private float sprintStepMultiplier = 0.6f;
    private float footstepsTimer = 0f;

    private float GetCurrentOffset => baseStepSpeed * (isCrouch ? crouchStepMultiplier : isSprint ? sprintStepMultiplier : 1.0f);

    private AudioSource playerFootstepsAudioSource;
    private AudioClip playerFootstepsAudioClip;

    private Transform enemyToPlayerPoint;
    public Transform EnemyToPlayerPoint
    {
        get { return enemyToPlayerPoint; }
    }

    ItemHintController itemHintController = null;

    public PlayerController(MainGame main) : base(main)
    {
        Initialize();
    }

    public override void Initialize()
    {
        moveSpeed = 3;
        mouseSensitivity = GameSettingParamStorage.MouseSensitivity;
        gravity = 15;
        interactiveRange = 7;

        playerObj = UnityTool.FindGameObject("Player");
        GameObject.DontDestroyOnLoad(playerObj);
        specialItemHoldPos = UnityTool.FindChildGameObject(playerObj, "SpecialItemHoldPos").transform;
        playerHand = UnityTool.FindChildGameObject(playerObj, "PlayerHand").transform;
        Inventory.SpecialItem = specialItemHoldPos.GetChild(0).gameObject;
        Inventory.SpecialItem.SetActive(false);
        cameraObj = UnityTool.FindChildGameObject(playerObj, "PlayerMainCameraContainer").transform.GetChild(0);
        camera2 = UnityTool.FindChildGameObject(playerObj, "PlayerMainCameraContainer").transform.GetChild(1);
        camera2.gameObject.SetActive(false);
        cameraControlState = true;
        ActionStorage.Instance.SetPlayerControlStatusAction = SetPlayerControlStatus;
        playerTransform = playerObj.GetComponent<Transform>();
        playerCtrl = playerObj.GetComponent<CharacterController>();
        colliderDetectionTool = playerObj.GetComponent<ColliderDetectionTool>();
        playerControlStatus = true;
        mainCameraTransform = Camera.main.GetComponent<Transform>();
        Cursor.lockState = CursorLockMode.Locked;
        playerOriginHeight = playerTransform.localScale.y;
        playerCrouchHeight = playerOriginHeight / 2;
        playerObj.transform.localEulerAngles = new Vector3(0, 180, 0);

        if (UnityTool.FindGameObject("PlayerDetectEnemyCol"))
        {
            playerDetectEnemyColObj = UnityTool.FindGameObject("PlayerDetectEnemyCol").transform;
            _playerDetectEnemyColObj = playerDetectEnemyColObj.GetComponent<PlayerDetectEnemyColObj>();
        }
        playerRespawnPoint = UnityTool.FindGameObject("RespawnPoint").transform;

        playerFootstepsAudioSource = playerObj.GetComponent<AudioSource>();
        playerFootstepsAudioClip = playerFootstepsAudioSource.clip;

        enemyToPlayerPoint = UnityTool.FindChildGameObject(playerObj, "EnemyToPlayerPoint").transform;

        cameraContainer = UnityTool.FindChildGameObject(playerObj, "PlayerMainCameraContainer").transform;
    }

    int layerMask;
    public override void Update()
    {
        mouseSensitivity = GameSettingParamStorage.MouseSensitivity;
        if (endViewItemHandler != null)
            endViewItemHandler();
        if(camera2.gameObject.activeInHierarchy)
            ViewItemRaycastHandler();
        if (mainGame.GetGameEventControlSystem.Handler != null)
            mainGame.GetGameEventControlSystem.Handler.Invoke();
        //mainGame.GetGameEventControlSystem.PlayerMissionHandler.Invoke();
        if (playerMissonAction != null)
            playerMissonAction();
        if (playerDetectEnemyColObj != null)
        {
            playerDetectEnemyColObj.transform.position = playerObj.transform.position;
            //限制射線擊中目標
            layerMask = ~(1 << playerDetectEnemyColObj.gameObject.layer | 1 << LayerMask.NameToLayer("LimitArea") |
                1 << LayerMask.NameToLayer("EnemyStayPoint_State2"));
        }
        /*if (playerCameraLootAtHandler != null)
        {
            playerCameraLootAtHandler.Invoke();
            playerCameraLootAtHandler = null;
            Debug.Log("origin" + cameraOriginRotate);
        }
        else
        {
            //Debug.Log("origin" + cameraOriginRotate + "now" + cameraObj.localEulerAngles);
            //cameraObj.rotation = cameraOriginRotate;
        }*/

        if (playerControlStatus)
        {
            Move();
            if(cameraControlState)
                CameraCtrl();
            ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
            if (Physics.Raycast(ray, out hitObj, interactiveRange, layerMask))
            {
                //hitObj.transform.SendMessage("HitByRaycast", gameObject, SendMessageOptions.DontRequireReceiver);
                nowInteractiveObj = hitObj.transform.gameObject;
                Debug.DrawLine(ray.origin, hitObj.point, Color.yellow);
                DetectInteractiveObj();
                //Debug.Log(hitObj.transform.name);
                if (doNotLookEnemyAction != null)
                    doNotLookEnemyAction();
            }
            else
            {
                nowInteractiveObj = null;
                mainGame.GetInteractiveInterface.SetTextStatus(false);
            }
            //判斷互動UI是否已彈出
            if (mainGame.GetInteractiveInterface.InteractiveHint.activeInHierarchy == true)
            {
                //點擊E互動
                if (Input.GetKeyDown(KeyCode.E))
                {
                    //調用互動方法
                    PlayerInteractive(hitObj.transform.gameObject);
                    interactiveObj = hitObj.transform.gameObject;
                }

            }
            if (Inventory.SpecialItem != null && Input.GetKeyDown(KeyCode.F))
            {
                UseSpecialItem();
            }
        }
        else
        {

        }
        if (colliderDetectionTool.Collider != null)
        {
            switch (colliderDetectionTool.Collider.gameObject.tag)
            {
                case "EventTrigger":
                    nowCollisionObj = colliderDetectionTool.Collider.gameObject;
                    break;
            }
        }
        Debug.DrawLine(ray.origin, hitObj.point, Color.yellow);
        //print(hitObj.transform.gameObject.layer + " " + nowInteractiveObj.name.ToString() + " " + hitObj.transform.name);
    }
    //初始化玩家大小
    public void InitailPlayerSize()
    {
        if(playerObj != null)
            playerObj.transform.localScale = new Vector3(1, 1.3f, 1);
    }
    private void Move()
    {
        float x1 = 0, y1 = 0, z1 = 0;
        y1 -= gravity * Time.deltaTime;
        if (Input.GetKey(KeyCode.W))
        {
            z1 += moveSpeed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            z1 -= moveSpeed * Time.deltaTime;
        }
        //左右移動
        if (Input.GetKey(KeyCode.A))
        {
            x1 -= moveSpeed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            x1 += moveSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.LeftShift))
        {
            if (Input.GetKey(KeyCode.W))
            {
                z1 += moveSpeed * Time.deltaTime * 1.3f;
                isSprint = true;
            }
            //z1 += moveSpeed * Time.deltaTime;
        }
        else
        {
            isSprint = false;
        }
        if (Input.GetKey(KeyCode.LeftControl))
        {
            isCrouch = true;
        }
        else
        {
            isCrouch = false;
        }
        if (isCrouch)
        {
            //playerObj.transform.localScale = new Vector3(playerObj.transform.localScale.x, playerCrouchHeight,
                //playerObj.transform.localScale.z);
            playerObj.transform.DOScale(playerCrouchHeight, 1f);
            //.SetEase(Ease.InQuint)
        }
        else
        {
            //playerObj.transform.localScale = new Vector3(playerObj.transform.localScale.x, playerOriginHeight,
                //playerObj.transform.localScale.z);
            playerObj.transform.DOScale(playerOriginHeight, 1f);
        }
        playerCtrl.Move(playerObj.transform.TransformDirection(new Vector3(x1, y1, z1)));

        HandleFootsteps();
        HandleShakeCamera();
    }
    //玩家移動腳步聲與鏡頭晃動
    public void HandleFootsteps()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            footstepsTimer -= Time.deltaTime;

            if (footstepsTimer <= 0f)
            {
                playerFootstepsAudioSource.PlayOneShot(playerFootstepsAudioClip);

                footstepsTimer = GetCurrentOffset;
            }
        }
    }
    public void HandleShakeCamera()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {

                EZCameraShake.CameraShaker.Instance.ShakeOnce(0.7f, 0.6f, 0f, 0.5f);
            }
            else
            {

                EZCameraShake.CameraShaker.Instance.ShakeOnce(0.35f, 0.35f, 0f, 0.5f);
            }
        }
    }

    private void CameraCtrl()
    {
        //Debug.Log(mainCameraTransform.localEulerAngles.x);
        float x = Input.GetAxis("Mouse X")* mouseSensitivity * Time.deltaTime;
        float y = Input.GetAxis("Mouse Y")* mouseSensitivity * Time.deltaTime;
        //Debug.Log(y);

        playerTransform.Rotate(Vector3.up * x);

        xRotate -= y;

        //Debug.Log(cameraRotateOffsetX);
        //xRotate = Mathf.Clamp(xRotate, -70f, 60f);
        if (originOffsetX > 0 && cameraLookAtTargetRotate < 0)
        {
            xRotate = Mathf.Clamp(xRotate, -70f + cameraRotateOffsetX, 60f + cameraRotateOffsetX);
            mainCameraTransform.localRotation = Quaternion.Euler(xRotate - cameraRotateOffsetX, 0f, 0f);
        }
        else if(originOffsetX > 0 && cameraLookAtTargetRotate > 0 && originOffsetX > cameraLookAtTargetRotate)
        {
            xRotate = Mathf.Clamp(xRotate, -70f + cameraRotateOffsetX, 60f + cameraRotateOffsetX);
            mainCameraTransform.localRotation = Quaternion.Euler(xRotate - cameraRotateOffsetX, 0f, 0f);
        }
        else if(originOffsetX > 0 && cameraLookAtTargetRotate > 0 && originOffsetX < cameraLookAtTargetRotate)
        {
            xRotate = Mathf.Clamp(xRotate, -70f - cameraRotateOffsetX, 60f - cameraRotateOffsetX);
            mainCameraTransform.localRotation = Quaternion.Euler(xRotate + cameraRotateOffsetX, 0f, 0f);
        }
        else if(originOffsetX < 0 && cameraLookAtTargetRotate > 0)
        {
            xRotate = Mathf.Clamp(xRotate, -70f - cameraRotateOffsetX, 60f - cameraRotateOffsetX);
            mainCameraTransform.localRotation = Quaternion.Euler(xRotate + cameraRotateOffsetX, 0f, 0f);
        }
        else if(originOffsetX < 0 && cameraLookAtTargetRotate < 0 && originOffsetX > cameraLookAtTargetRotate)
        {
            xRotate = Mathf.Clamp(xRotate, -70f + cameraRotateOffsetX, 60f + cameraRotateOffsetX);
            mainCameraTransform.localRotation = Quaternion.Euler(xRotate - cameraRotateOffsetX, 0f, 0f);
        }
        else if(originOffsetX < 0 && cameraLookAtTargetRotate < 0 && originOffsetX < cameraLookAtTargetRotate)
        {
            xRotate = Mathf.Clamp(xRotate, -70f - cameraRotateOffsetX, 60f - cameraRotateOffsetX);
            mainCameraTransform.localRotation = Quaternion.Euler(xRotate + cameraRotateOffsetX, 0f, 0f);
        }
        else
        {
            xRotate = Mathf.Clamp(xRotate, -70f, 60f);
            mainCameraTransform.localRotation = Quaternion.Euler(xRotate, 0f, 0f);
        }
        
        //Debug.Log(mainCameraTransform.localEulerAngles);
    }
    //玩家互動方法
    private void PlayerInteractive(GameObject obj)
    {
        if(obj.tag == "Door")
        {
            mainGame.DoorController.Interactive(nowInteractiveObj.transform.parent.name);
            if (mainGame.GetGameEventControlSystem.PlayerTutorialHandler != null && mainGame.GetGameEventControlSystem.PlayerTutorialHandler.Method.Name.Equals("Tutorial_5_Interactive"))
            {
                mainGame.SetTutorialInputCount(mainGame.GetTutorialInputCount() + 1);
            }
        }
        else if(obj.transform.parent != null && obj.transform.parent.tag == "Item")
        {
            if (mainGame.GetGameEventControlSystem.PlayerTutorialHandler != null && mainGame.GetGameEventControlSystem.PlayerTutorialHandler.Method.Name.Equals("Tutorial_5_Interactive"))
            {
                mainGame.SetTutorialInputCount(mainGame.GetTutorialInputCount() + 2);
            }
            mainGame.SetPreviewItem(nowInteractiveObj.transform.parent);
            mainGame.GetInteractiveInterface.Show();
            mainGame.ItemController.Interactive(nowInteractiveObj.transform.parent.name);
            mainGame.GetItemPreviewSystem.SetItemToPreviewPos(nowInteractiveObj.transform.parent.gameObject);
            DelayExecutor delayExecutor = new DelayExecutor(0.5f, () =>
            { ActionStorage.Instance.CloseItemPreviewPanelAction = mainGame.GetInteractiveInterface.Hide; });
        }
        else if(obj.tag == "ReadOnlyItem")
        {
            if (mainGame.GetGameEventControlSystem.PlayerTutorialHandler != null && mainGame.GetGameEventControlSystem.PlayerTutorialHandler.Method.Name.Equals("Tutorial_5_Interactive"))
            {
                mainGame.SetTutorialInputCount(mainGame.GetTutorialInputCount() + 2);
            }
            if(nowInteractiveObj.transform.parent == null)
            {
                mainGame.SetPreviewItem(nowInteractiveObj.transform);
                mainGame.GetItemPreviewSystem.SetItemToPreviewPos(nowInteractiveObj);
            }
            else
            {
                Debug.Log(nowInteractiveObj.transform.parent);
                mainGame.SetPreviewItem(nowInteractiveObj.transform.parent);
                mainGame.GetItemPreviewSystem.SetItemToPreviewPos(nowInteractiveObj.transform.parent.gameObject);
            }
            mainGame.GetInteractiveInterface.Show();
            
            DelayExecutor delayExecutor = new DelayExecutor(0.5f, () =>
            { ActionStorage.Instance.CloseItemPreviewPanelAction = mainGame.GetInteractiveInterface.Hide; });
        }
        else if (obj.tag == "InteractiveActionItem")
        {
            if (mainGame.GetGameEventControlSystem.PlayerTutorialHandler != null && mainGame.GetGameEventControlSystem.PlayerTutorialHandler.Method.Name.Equals("Tutorial_5_Interactive"))
            {
                mainGame.SetTutorialInputCount(mainGame.GetTutorialInputCount() + 1);
            }
            if(mainGame.GetInteractiveActionObjController.GetObjComponentByName(nowInteractiveObj.name).Action!=null)
                mainGame.GetInteractiveActionObjController.GetObjComponentByName(nowInteractiveObj.name).Action.Invoke();
        }
        else if(obj.transform.parent.tag == "BigInteractiveObject")
        {
            StartViewObjectHandler(obj.transform.parent.Find("CameraPoint"),obj.transform.parent.Find("Lock02"));
            mainGame.GetInteractiveInterface.SetTextStatus(false);
        }
        /*else if(obj.transform.parent != null && obj.transform.parent.name == "SmartPhone")
        {
            Inventory.SpecialItem = obj.transform.parent.gameObject;
            obj.transform.parent.gameObject.SetActive(false);
        }*/
        else if(obj.transform.parent.tag == "PaperPiece")
        {
            mainGame.SetPaperPieceCollected(obj.transform.parent);
        }
    }
    private void DetectInteractiveObj()
    {
        
        if (mainGame.GetPlayerController.NowInteractiveObj && mainGame.GetPlayerController.NowInteractiveObj.transform.parent)
        {
            if (mainGame.GetPlayerController.NowInteractiveObj.transform.parent.GetComponent<ItemHintController>() != null)
            {
                itemHintController = mainGame.GetPlayerController.NowInteractiveObj.transform.parent.GetComponent<ItemHintController>();
            }
        }
        if (hitObj.transform.gameObject.layer == 6 && nowInteractiveObj!=null)
        {
            if(nowInteractiveObj.tag == "Door" && mainGame.DoorController.GetDoor().IsMove == true)
            {
                
                mainGame.GetInteractiveInterface.SetTextStatus(false);
                
            }
            else
            {
                mainGame.GetInteractiveInterface.SetTextStatus(true);
                mainGame.GetInteractiveInterface.SetInteractiveHintPos();
                if (itemHintController != null)
                {
                    if (itemHintController.IsOnHint() == false)
                    {
                        itemHintController.ShowHint();
                    }
                }
            }
        }
        else
        {
            mainGame.GetInteractiveInterface.SetTextStatus(false);
            if (itemHintController != null)
            {
                if (itemHintController.IsOnHint())
                {
                    itemHintController.CloseHint();
                }
            }
        }
    }
    public void SetPlayerControlStatus(bool status)
    {
        playerControlStatus = status;
    }
    //使用特殊道具
    public void UseSpecialItem()
    {
        //Debug.Log("Use" + Inventory.SpecialItem);
        if(specialItemIsUsing == false)
        {
            Transform specialItem = Inventory.SpecialItem.transform;
            specialItem.localPosition = Vector3.zero;
            playerHand.transform.localEulerAngles = new Vector3(30, playerHand.transform.localEulerAngles.y, playerHand.transform.localEulerAngles.z);
            playerHand.transform.DOLocalRotate(new Vector3(0, 0, 0), 0.3f);
            specialItem.gameObject.SetActive(true);
            specialItem.GetChild(2).gameObject.SetActive(true);
            specialItem.transform.position = specialItemHoldPos.position;
            //specialItem.transform.SetParent(specialItemHoldPos);
            specialItem.transform.localRotation = Quaternion.Euler(0, -180, 0);

            specialItemIsUsing = true;
        }
        else
        {
            Inventory.SpecialItem.gameObject.SetActive(false);

            specialItemIsUsing = false;
        }
    }

    public List<Transform> GetEnemyStayPointObjs()
    {
        return _playerDetectEnemyColObj.GetEnemyStayPointObjs();
    }
    public void SetPlayerLookDirection(Transform target)
    {
        cameraRotateOffsetX = 0;
        originOffsetX = 0;
        cameraLookAtTargetRotate = 0;
        //float originOffsetX = mainCameraTransform.localEulerAngles.x;
        originOffsetX = mainCameraTransform.localEulerAngles.x;
        playerTransform.DOLookAt(new Vector3(target.position.x, playerTransform.position.y, target.position.z), 0.5f);
        //Debug.Log(mainCameraTransform.localEulerAngles.x);
        cameraObj.DOLookAt(target.position, 0.5f).OnComplete(() => {
            CalCameraOffset(originOffsetX);
            //Debug.Log(cameraRotateOffsetX);
            //Debug.Log("移動前RotationX : " + originOffsetX + " + 偏移植 : " + cameraRotateOffsetX);
            //Debug.Log("移動後RotationX : " + mainCameraTransform.localEulerAngles.x);
            //Debug.Log("xRotate : " + xRotate + " " + "xRotate + cameraRotateOffsetX : " + mainCameraTransform.localRotation.x);
        });

    }
    public void LockPlayerControlState(float time)
    {
        cameraControlState = false;
        playerControlStatus = false;
        DelayExecutor delayExecutor = new DelayExecutor(time, () => {
            cameraControlState = true;
            playerControlStatus = true;
        });
    }
    public void SetPlayerControlStateAndCameraState(bool state)
    {
        cameraControlState = state;
        playerControlStatus = state;
    }
    //計算鏡頭移動至目標後旋轉角度與移動前差值
    public void CalCameraOffset(float x)
    {
        cameraLookAtTargetRotate = mainCameraTransform.localEulerAngles.x;
        cameraRotateOffsetX = Mathf.Abs(cameraLookAtTargetRotate - x);
        if(cameraRotateOffsetX > 180)
        {
            cameraRotateOffsetX = 360 - cameraRotateOffsetX;
        }
        if(x > 180)
        {
            x = -(360 - x);
        }
        originOffsetX = x;
        //Debug.Log("鏡頭移動至目標後 : " + cameraLookAtTargetRotate + "鏡頭移動至目標前 : " + x);
        //Debug.Log("差值 : " + cameraRotateOffsetX);
        //Debug.Log("xRotate : " + xRotate);
    }
    public void PlayerLookEnemy()
    {
        if (CheckPlayerLookTarget(hitObj.transform, "Enemy"))
        {
            playerLookEnemyTime += 1 * Time.deltaTime;
            mainGame.GetEnemyAni().SetBool("isShack", true);
            if (playerLookEnemyTime > 1.5f)
            {
                Debug.Log("看到敵人，執行懲罰");
                mainGame.EnemyBehaviour();
                mainGame.GetEnemyAni().SetBool("isShack", false);
                playerLookEnemyTime = 0;
            }
        }
        else
        {
            playerLookEnemyTime = 0;
        }
    }
    public bool CheckPlayerLookTarget(Transform playerLookObj,string targetTag)
    {
        bool isLook = false;
        if(playerLookObj.tag == targetTag)
        {
            isLook = true;
        }
        else
        {
            isLook = false;
        }
        return isLook;
    }

    public void StartViewObjectHandler(Transform cameraPos,Transform lookTargetObj)
    {
        cameraOriginPos = camera2.transform.position;
        cameraOriginRot = camera2.transform.eulerAngles;
        camera2.gameObject.SetActive(true);
        cameraObj.gameObject.SetActive(false);
        camera2.transform.DOMove(cameraPos.position, 2f).OnComplete(() => { mainGame.GetViewItemReturnHint().SetActive(true);
            endViewItemHandler = EndViewObjectHandler;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        });
        camera2.transform.DODynamicLookAt(lookTargetObj.position, 2f);
        //camera2.transform.rotation.eulerAngles.Set(0,0,0);
        playerControlStatus = false;
    }
    public void EndViewObjectHandler()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            mainGame.GetViewItemReturnHint().SetActive(false);
            camera2.transform.DOMove(cameraOriginPos, 2f).OnComplete(() => {
                cameraObj.gameObject.SetActive(true);
                camera2.gameObject.SetActive(false);
                playerControlStatus = true;
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                endViewItemHandler = null;
            });
            camera2.transform.DORotate(cameraOriginRot, 2f);
        }
    }
    private bool clickState = false;
    public void ViewItemRaycastHandler()
    {
        Camera camera = camera2.GetComponent<Camera>();
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hitObj, 20))
        {
            if(hitObj.transform.gameObject != null && hitObj.transform.parent != null)
            {
                if (hitObj.transform.tag == "Lock" && clickState == false)
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        hitObj.transform.parent.parent.GetComponent<InteractiveActionObj>().ActionWithParam(hitObj.transform);
                        clickState = true;
                        MethodDelayExecuteTool.ExecuteDelayedMethod(0.3f, () => clickState = false);
                    }
                }
            }
        }
    }
}
