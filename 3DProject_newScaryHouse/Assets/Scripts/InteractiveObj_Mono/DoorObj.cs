using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DoorObj : InteractiveObjBase_Mono
{
    [SerializeField]
    private float rotate;
    public float Rotate
    {
        get { return rotate; }
        set { rotate = value; }
    }

    [SerializeField]
    private Transform hintPosObj;
    public Transform HintPosObj
    {
        get { return hintPosObj; }
    }

    //門移動秒數
    [SerializeField]
    private float doorMove;
    public float DoorMove
    {
        get { return doorMove; }
    }

    [SerializeField]
    private bool doorOpen;
    public bool DoorOpen
    {
        get { return doorOpen; }
        set { doorOpen = value; }
    }

    private Vector3 doorOriginalTransform;
    public Vector3 DoorOriginalTransform
    {
        get { return doorOriginalTransform; }
        set { doorOriginalTransform = value; }
    }

    //門打開的最大角度
    [SerializeField]
    private int doorMaxOpenDegrees;
    public int DoorMaxOpenDegrees
    {
        get { return doorMaxOpenDegrees; }
    }

    //門的方向(true向內 false向外)
    [SerializeField]
    private bool doorDirection;
    public bool DoorDirection
    {
        get { return doorDirection; }
    }

    //門是否鎖住
    [SerializeField]
    private bool doorLock;
    public bool DoorLock
    {
        get { return doorLock; }
        set { doorLock = value; }
    }

    [Header("音效")]
    [SerializeField]
    private AudioSource doorAudio_1;
    public AudioSource DoorAudio_1
    {
        get { return doorAudio_1; }
        set { doorAudio_1 = value; }
    }

    [SerializeField]
    private AudioSource doorAudio_2;
    public AudioSource DoorAudio_2
    {
        get { return doorAudio_2; }
        set { doorAudio_2 = value; }
    }

    [SerializeField]
    private AudioSource doorAudio_3;
    public AudioSource DoorAudio_3
    {
        get { return doorAudio_3; }
        set { doorAudio_3 = value; }
    }

    [SerializeField]
    private AudioSource closeDoorAudio_Louder;
    public AudioSource CloseDoorAudio_Louder
    {
        get { return closeDoorAudio_Louder; }
    }

    [SerializeField]
    private AudioSource unlockDoorAudio;
    public AudioSource UnlockDoorAudio
    {
        get { return unlockDoorAudio; }
    }

    [Header("用來解鎖門的物件")]
    [SerializeField]
    private GameObject unlockDoorObj = null;
    public GameObject UnlockDoorObj
    {
        get { return unlockDoorObj; }
    }

    [Header("其他種類門最大打開距離")]
    [SerializeField]
    private float doorOpenMaxInstance;
    public float DoorOpenMaxInstance
    {
        get { return doorOpenMaxInstance; }
    }

    //門是否在移動
    [SerializeField]
    private bool isMove;
    public bool IsMove
    {
        get { return isMove; }
    }

    private Vector3 doorOriginPosition;
    public Vector3 DoorOriginPosition
    {
        get { return doorOriginPosition; }
        set { doorOriginPosition = value; }
    }
    [SerializeField]
    private Animator doorAni;
    public Animator DoorAni
    {
        get { return doorAni; }
    }
    /*private void OpenTheDoor()
    {
        string state = "Open";
        switch (this.gameObject.layer)
        {
            case 8:
                DoorAction_RoomDoor(state);
                break;
            case 9:
                //DoorAction_PullDoor(state);
                break;
            case 10:
                DoorAction_Drawer(state);
                break;
            
        }
    }

    private void CloseTheDoor()
    {
        string state = "Close";
        switch (this.gameObject.layer)
        {
            case 8:
                DoorAction_RoomDoor(state);
                break;
            case 9:
                //DoorAction_PullDoor(state);
                break;
            case 10:
                DoorAction_Drawer(state);
                break;
        }
    }*/
    //房間門 6扇
    private void DoorAction_RoomDoor(string state)
    {
        if(state == "Open")
        {
            if (!doorDirection)
            {
                if (rotate < doorMaxOpenDegrees)
                {
                    isMove = true;
                    SetDoorInteractiveHintPos();
                    this.transform.rotation = Quaternion.Euler(new Vector3(doorOriginalTransform.x,
                        this.transform.rotation.y + rotate, doorOriginalTransform.z));
                    rotate++;
                }
                else
                {
                    isMove = false;
                    EndInvoke();
                }
            }
            else
            {
                if (rotate > -doorMaxOpenDegrees)
                {
                    isMove = true;
                    SetDoorInteractiveHintPos();
                    this.transform.rotation = Quaternion.Euler(new Vector3(doorOriginalTransform.x,
                        this.transform.rotation.y + rotate, doorOriginalTransform.z));
                    rotate--;
                }
                else
                {
                    isMove = false;
                    EndInvoke();
                }
            }
        }
        else
        {
            if (!doorDirection)
            {
                if (rotate > 0)
                {
                    isMove = true;
                    SetDoorInteractiveHintPos();
                    this.transform.rotation = Quaternion.Euler(new Vector3(doorOriginalTransform.x,
                        this.transform.rotation.y + rotate, doorOriginalTransform.z));
                    rotate--;
                }
                else
                {
                    isMove = false;
                    EndInvoke();
                }
            }
            else
            {
                if (rotate < 0)
                {
                    isMove = true;
                    SetDoorInteractiveHintPos();
                    this.transform.rotation = Quaternion.Euler(new Vector3(doorOriginalTransform.x,
                        this.transform.rotation.y + rotate, doorOriginalTransform.z));
                    rotate++;
                }
                else
                {
                    isMove = false;
                    EndInvoke();
                }
            }
        }
    }

    //拉門
    /*private void DoorAction_PullDoor(string state)
    {
        if(this.transform.parent.childCount <= 3)
        {
            if (state == "Open")
            {
                if (this.transform.parent.GetChild(1) == this.transform && this.transform.parent.GetChild(2).GetComponent<DoorObj>().DoorOpen == false)
                {
                    //Debug.Log("1");
                    if (this.transform.localPosition.x >= doorOpenMaxInstance)
                    {
                        this.transform.localPosition -= new Vector3(1, 0, 0) * Time.deltaTime;
                    }
                    else
                    {
                        doorOpen = true;
                        EndInvoke();
                    }
                }
                if (this.transform.parent.GetChild(1) == this.transform && this.transform.parent.GetChild(2).GetComponent<DoorObj>().DoorOpen)
                {
                    //Debug.Log("2");
                    if (this.transform.parent.GetChild(2).localPosition.x >= this.transform.parent.GetChild(2).GetComponent<DoorObj>().DoorOriginPosition.x)
                    {
                        this.transform.parent.GetChild(2).localPosition -= new Vector3(1, 0, 0) * Time.deltaTime;
                    }
                    else
                    {
                        this.transform.parent.GetChild(2).GetComponent<DoorObj>().DoorOpen = false;
                        this.doorOpen = false;
                        EndInvoke();
                    }
                }
                if (this.transform.parent.GetChild(2) == this.transform)
                {
                    //Debug.Log("3");
                    if (this.transform.localPosition.x <= doorOpenMaxInstance)
                    {
                        this.transform.localPosition += new Vector3(1, 0, 0) * Time.deltaTime;
                    }
                    else
                    {
                        doorOpen = true;
                        EndInvoke();
                    }
                }
            }
            else
            {
                if (this.transform.parent.GetChild(1) == this.transform)
                {
                    if (this.transform.localPosition.x <= doorOriginPosition.x && this.transform.parent.GetChild(1) == this.transform)
                    {
                        this.transform.localPosition += new Vector3(1, 0, 0) * Time.deltaTime;
                    }
                    else
                    {
                        doorOpen = false;
                        EndInvoke();
                    }
                }
                if (this.transform.parent.GetChild(2) == this.transform)
                {
                    if (this.transform.localPosition.x >= doorOriginPosition.x && this.transform.parent.GetChild(2) == this.transform)
                    {
                        this.transform.localPosition -= new Vector3(1, 0, 0) * Time.deltaTime;
                    }
                    else
                    {
                        doorOpen = false;
                        EndInvoke();
                    }
                }
            }
        }
        else
        {
            if(state == "Open")
            {
                if (this.transform.localPosition.x >= doorOpenMaxInstance)
                {
                    this.transform.localPosition -= new Vector3(1, 0, 0) * Time.deltaTime;
                }
                else
                {
                    doorOpen = true;
                    EndInvoke();
                }
            }
            else
            {
                if (this.transform.localPosition.x <= doorOriginPosition.x)
                {
                    this.transform.localPosition += new Vector3(1, 0, 0) * Time.deltaTime;
                }
                else
                {
                    doorOpen = false;
                    EndInvoke();
                }
            }
        }
    }*/

    //抽屜
    private void DoorAction_Drawer(string state)
    {
        if (state == "Open")
        {
            if (this.transform.localPosition.z >= doorOpenMaxInstance)
            {
                isMove = true;
                SetDoorInteractiveHintPos();
                this.transform.localPosition -= new Vector3(0, 0, 1) * Time.deltaTime;
            }
            else
            {
                isMove = false;
                EndInvoke();
            }
        }
        else
        {
            if(this.transform.localPosition.z <= doorOriginPosition.z)
            {
                isMove = true;
                SetDoorInteractiveHintPos();
                this.transform.localPosition += new Vector3(0, 0, 1) * Time.deltaTime;
            }
            else
            {
                isMove = false;
                this.transform.localPosition = doorOriginPosition;
                EndInvoke();
            }
        }
    }

    public void StartInvokeRepeating(string methodName)
    {
        InvokeRepeating(methodName, this.DoorMove, this.DoorMove);
    }
    public void EndInvoke()
    {
        CancelInvoke();
    }
    public void SetDoorInteractiveHintPos()
    {
        hintPosition = Unity.CustomTool.UnityTool.FindChildGameObject(this.gameObject, "InteractiveHintPos").transform.position;
    }
}
