using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : InteractableObjBase
{
    private GameObject doorAxis;
    [SerializeField]
    private float rotate;
    //門移動秒數
    [SerializeField]
    private float doorMove;
    [SerializeField]
    private bool doorOpen;
    private Vector3 doorOriginalTransform;

    //門打開的最大角度
    [SerializeField]
    private int doorMaxOpenDegrees;

    //門的方向(true向內 false向外)
    [SerializeField]
    private bool doorDirection;

    //門是否鎖住
    [SerializeField]
    private bool doorLock;

    [Header("音效")]
    [SerializeField]
    private AudioSource doorAudio_1;

    [SerializeField]
    private AudioSource doorAudio_2;

    [SerializeField]
    private AudioSource doorAudio_3;

    [Header("用來解鎖門的物件")]
    [SerializeField]
    private GameObject unlockDoorObj = null;

    private void Start()
    {
        doorAxis = this.gameObject;
        doorOriginalTransform = this.transform.rotation.eulerAngles;
    }

    private void OpenTheDoor()
    {
        if(!doorDirection)
        {
            if (rotate < doorMaxOpenDegrees)
            {
                doorAxis.transform.rotation = Quaternion.Euler(new Vector3(doorOriginalTransform.x, doorAxis.transform.rotation.y + rotate, doorOriginalTransform.z));
                rotate++;
            }
            else
            {
                CancelInvoke();
            }
        }
        else
        {
            if (rotate > -doorMaxOpenDegrees)
            {
                doorAxis.transform.rotation = Quaternion.Euler(new Vector3(doorOriginalTransform.x, doorAxis.transform.rotation.y + rotate, doorOriginalTransform.z));
                rotate--;
            }
            else
            {
                CancelInvoke();
            }
        }
        
    }

    private void CloseTheDoor()
    {
        if(!doorDirection)
        {
            if (rotate > 0)
            {
                doorAxis.transform.rotation = Quaternion.Euler(new Vector3(doorOriginalTransform.x, doorAxis.transform.rotation.y + rotate, doorOriginalTransform.z));
                rotate--;
            }
            else
            {
                CancelInvoke();
            }
        }
        else
        {
            if (rotate < 0)
            {
                doorAxis.transform.rotation = Quaternion.Euler(new Vector3(doorOriginalTransform.x, doorAxis.transform.rotation.y + rotate, doorOriginalTransform.z));
                rotate++;
            }
            else
            {
                CancelInvoke();
            }
        }
        
        
    }
    public override void Interactive()
    {
        if (!doorLock)
        {
            if (doorOpen == false)
            { 
                doorOpen = true;
                InvokeRepeating("OpenTheDoor", doorMove, doorMove);
                doorAudio_1.PlayOneShot(doorAudio_1.clip);
            }
            else if (doorOpen == true)
            {
                doorOpen = false;
                InvokeRepeating("CloseTheDoor", doorMove, doorMove);
                doorAudio_2.PlayOneShot(doorAudio_2.clip);
            }
            else
            {
                //this.transform.eulerAngles = doorOriginalTransform;
            }
        }
        else
        {
            DoorLockInteractiveEffect();
        }
    }

    //門鎖住時的互動效果(待完成)
    private void DoorLockInteractiveEffect()
    {
        doorAudio_3.PlayOneShot(doorAudio_3.clip);
    }

    public void SetDoorLock(bool lockState)
    {
        doorLock = lockState;
    }
    public void ForceInteractiveDoor(bool closeOrOpen)
    {
        if (closeOrOpen)
        {
            doorOpen = true;
            InvokeRepeating("OpenTheDoor", doorMove, doorMove);
            doorAudio_1.PlayOneShot(doorAudio_1.clip);
        }
        else
        {
            doorOpen = false;
            InvokeRepeating("CloseTheDoor", doorMove, doorMove);
            doorAudio_2.PlayOneShot(doorAudio_2.clip);
        }
    }
}
