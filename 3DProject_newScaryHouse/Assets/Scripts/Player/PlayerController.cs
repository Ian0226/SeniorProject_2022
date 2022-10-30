using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //移動速度
    [SerializeField]
    private float moveSpeed;
    public float MoveSpeed
    {
        get { return moveSpeed; }
        set { moveSpeed = value; }
    }
    //滑鼠靈敏度
    [SerializeField]
    private float mouseSensitivity;
    public float MouseSensitivity
    {
        get { return mouseSensitivity; }
        set { mouseSensitivity = value; }
    }
    private float xRotate = 0f;
    [SerializeField]
    private float gravity;
    private Transform playerTransform;
    private Transform mainCameraTransform;
    //射線長度
    [SerializeField]
    private float interactiveRange;
    //玩家當前互動的物件
    private GameObject nowInteractiveObj;
    //玩家當前碰撞到的物件
    private GameObject nowCollisionObj;

    private CharacterController playerCtrl;

    private Ray ray;
    //射線打到的物件
    private RaycastHit hitObj;

    private bool isCollide;

    [SerializeField]
    private UIManager uiManager;

    private bool playerControlStatus;

    private void Awake()
    {
        uiManager.UIInitialize();
        EventsStorage.Singleton.onCustomEvent.AddListener(SetPlayerControlStatus);
    }
    private void Start()
    {
        playerControlStatus = true;
        playerTransform = GetComponent<Transform>();
        mainCameraTransform = Camera.main.GetComponent<Transform>();
        playerCtrl = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
    }
    private void Update()
    {
        uiManager.UIUpdate();
        if (playerControlStatus)
        {
            Move();
            CameraCtrl();
            ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
            if (Physics.Raycast(ray, out hitObj, interactiveRange))
            {
                //hitObj.transform.SendMessage("HitByRaycast", gameObject, SendMessageOptions.DontRequireReceiver);
                nowInteractiveObj = hitObj.transform.gameObject;
                Debug.DrawLine(ray.origin, hitObj.point, Color.yellow);
                DetectInteractiveObj();
                //print(hitObj.transform.name);
            }
            else
            {
                nowInteractiveObj = null;
                uiManager.GetInteractiveInterface().SetTextStatus(false);
            }
            //判斷互動UI是否已彈出
            if (uiManager.GetInteractiveInterface().InteractiveHint.activeInHierarchy == true)
            {
                //點擊E互動
                if (Input.GetKeyDown(KeyCode.E))
                {
                    //調用互動方法
                    PlayerInteractive(hitObj.transform.gameObject);
                }

            }
        }
        else
        {

        }
        Debug.DrawLine(ray.origin, hitObj.point, Color.yellow);
        //print(hitObj.transform.gameObject.layer + " " + nowInteractiveObj.name.ToString() + " " + hitObj.transform.name);
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
            z1 += moveSpeed * Time.deltaTime;
        }
        playerCtrl.Move(transform.TransformDirection(new Vector3(x1, y1, z1)));
    }

    private void CameraCtrl()
    {
        float x = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float y = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        playerTransform.Rotate(Vector3.up * x);

        xRotate -= y;
        xRotate = Mathf.Clamp(xRotate, -70f, 60f);
        mainCameraTransform.localRotation = Quaternion.Euler(xRotate, 0f, 0f);
    }
    //玩家互動方法
    private void PlayerInteractive(GameObject obj)
    {
        switch(obj.tag)
        {
            case "Door":
                nowInteractiveObj.GetComponentInParent<DoorController>().Interactive();
                break;
            case "Item":
                uiManager.GetInteractiveInterface().SetItemPreviewPanelStatus(true);
                nowInteractiveObj.GetComponent<ItemController>().Interactive();
                GameObject itemContainer = GameObject.Find("ItemContainer");
                nowInteractiveObj.transform.position = itemContainer.transform.position;
                nowInteractiveObj.transform.SetParent(itemContainer.transform);
                //Destroy(nowInteractiveObj);
                break;
        }
    }
    private void DetectInteractiveObj()
    {
        
        if (hitObj.transform.gameObject.layer == 6 && nowInteractiveObj!=null)
        {
            uiManager.GetInteractiveInterface().SetTextStatus(true);
        }
        else
        {
            uiManager.GetInteractiveInterface().SetTextStatus(false);
        }
    }
    private void SetPlayerControlStatus(bool status)
    {
        playerControlStatus = status;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        switch (other.gameObject.name)
        {
            case "EventTriggerCollider_1":
                EventsStorage.Singleton.onCustomEventInt.Invoke(1);
                Destroy(other.gameObject);
                break;
        }
    }
}
