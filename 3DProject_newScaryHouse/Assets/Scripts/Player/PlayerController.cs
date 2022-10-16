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

    private void Start()
    {
        playerTransform = GetComponent<Transform>();
        mainCameraTransform = Camera.main.GetComponent<Transform>();
        playerCtrl = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        
    }
    private void Update()
    {
        Move();
        CameraCtrl();

        ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width /2, Screen.height /2, 0));
        if(Physics.Raycast(ray, out hitObj, interactiveRange))
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
            uiManager.ShowText(false);
        }
        //判斷互動UI是否已彈出
        if (uiManager.InteractiveText.activeInHierarchy == true)
        {
            //點擊E互動
            if (Input.GetKeyDown(KeyCode.E))
            {
                //調用互動方法
                PlayerInteractive(hitObj.transform.gameObject);
            }
               
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
                nowInteractiveObj.GetComponent<ItemController>().Interactive();
                Destroy(nowInteractiveObj);
                break;
        }
    }
    private void DetectInteractiveObj()
    {
        
        if (hitObj.transform.gameObject.layer == 6 && nowInteractiveObj!=null)
        {
            uiManager.ShowText(true);
        }
        else
        {
            uiManager.ShowText(false);
        }
    }
    /*
    private void OnTriggerEnter(Collider other)
    {
        nowCollisionObj = other.gameObject;
        switch(other.tag)
        {
            case "DoorCollider":
                isCollide = true;
                break;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        switch (other.tag)
        {
            case "DoorCollider":
                if(Physics.Raycast(ray, out hitObj, interactiveRange) && hitObj.transform.gameObject.tag == "Door")
                {
                    uiManager.ShowText(true);
                }
                else
                {
                    uiManager.ShowText(false);
                }
                break;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        nowCollisionObj = null;
        nowInteractiveObj = null;
        switch (other.tag)
        {
            case "DoorCollider":
                isCollide = false;
                uiManager.ShowText(false);
                break;
        }
    }
    */
}
