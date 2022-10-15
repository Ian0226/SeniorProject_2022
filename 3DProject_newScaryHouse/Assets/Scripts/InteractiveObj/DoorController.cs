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

    

    public override void Interactive()
    {
        if (doorOpen == false)
        {
            doorOpen = true;
            InvokeRepeating("OpenTheDoor", doorMove, doorMove);
        }
        else if (doorOpen == true)
        {
            doorOpen = false;
            InvokeRepeating("CloseTheDoor", doorMove, doorMove);
        }
        else
        {
            //this.transform.eulerAngles = doorOriginalTransform;
        }
    }

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

    
}
