using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //���ʳt��
    [SerializeField]
    private float moveSpeed;
    public float MoveSpeed
    {
        get { return moveSpeed; }
        set { moveSpeed = value; }
    }
    //�ƹ��F�ӫ�
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
    //�g�u����
    [SerializeField]
    private float interactiveRange;
    //���a���e���ʪ�����
    private GameObject nowInteractiveObj;
    //���a���e�I���쪺����
    private GameObject nowCollisionObj;

    private CharacterController playerCtrl;

    private Ray ray;
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
            //nowInteractiveObj = hitObj.transform.gameObject;
            Debug.DrawLine(ray.origin, hitObj.point, Color.yellow);
            //print(hitObj.transform.name);
        }
        if (uiManager.InteractiveText.activeInHierarchy == true)
        {
            if (Input.GetKeyDown(KeyCode.E) && nowCollisionObj != null)
            {
                nowInteractiveObj = nowCollisionObj;
                PlayerInteractive();
            }
               
        }
        Debug.DrawLine(ray.origin, hitObj.point, Color.yellow);
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
        //���k����
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

    private void PlayerInteractive()
    {
        switch(hitObj.transform.gameObject.tag)
        {
            case "Door":
                nowCollisionObj.GetComponentInChildren<DoorController>().Interactive();
                break;
        }
    }
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
}