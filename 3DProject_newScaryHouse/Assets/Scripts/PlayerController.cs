using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed;
    public float MoveSpeed
    {
        get { return moveSpeed; }
        set { moveSpeed = value; }
    }
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
    [SerializeField]
    private float interactiveRange;
    private GameObject nowInteractiveObj;
    private CharacterController playerCtrl;

    private Ray ray;
    private RaycastHit hitObj;
    

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
        ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        if (Physics.Raycast(ray, out hitObj, interactiveRange) )
        {
            hitObj.transform.SendMessage("HitByRaycast", gameObject, SendMessageOptions.DontRequireReceiver);
            //Debug.DrawLine(ray.origin, hitObj.point, Color.yellow);
            //print(hitObj.transform.name);
        }
        if(Physics.Raycast(ray, out hitObj, interactiveRange) && hitObj.transform.gameObject != null && Input.GetKeyDown(KeyCode.E))
        {
            PlayerInteractive();
        }
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
        //¥ª¥k²¾°Ê
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
                hitObj.transform.GetComponentInParent<DoorController>().Interactive();
                break;
        }
    }
}
