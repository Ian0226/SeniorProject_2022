using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : InteractableObjBase
{
    private GameObject doorAxis;
    [SerializeField]
    private float rotate;
    //�����ʬ��
    [SerializeField]
    private float doorMove;
    [SerializeField]
    private bool doorOpen;
    private Vector3 doorOriginalTransform;

    //�����}���̤j����
    [SerializeField]
    private int doorMaxOpenDegrees;

    //������V(true�V�� false�V�~)
    [SerializeField]
    private bool doorDirection;

    //���O�_���
    [SerializeField]
    private bool doorLock;

    [Header("����")]
    [SerializeField]
    private AudioSource doorAudio_1;

    [SerializeField]
    private AudioSource doorAudio_2;

    [SerializeField]
    private AudioSource doorAudio_3;

    [Header("�ΨӸ����������")]
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
                //�}������
                doorAudio_1.PlayOneShot(doorAudio_1.clip);
                doorOpen = true;
                InvokeRepeating("OpenTheDoor", doorMove, doorMove);
            }
            else if (doorOpen == true)
            {
                //��������
                doorAudio_2.PlayOneShot(doorAudio_2.clip);
                doorOpen = false;
                InvokeRepeating("CloseTheDoor", doorMove, doorMove);
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

    //�����ɪ����ʮĪG(�ݧ���)
    private void DoorLockInteractiveEffect()
    {
        doorAudio_3.PlayOneShot(doorAudio_3.clip);
    }

    public void SetDoorLock(bool lockState)
    {
        doorLock = lockState;
    }
}
