using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : InteractableObjBase
{
    private GameObject doorAxis;
    [SerializeField]
    private float rotate;
    [SerializeField]
    private float doorMove;
    [SerializeField]
    private bool doorOpen;
    private Vector3 doorOriginalTransform;

    private void Start()
    {
        doorAxis = this.gameObject;
        doorOriginalTransform = this.transform.rotation.eulerAngles;
    }
    private void Update()
    {
        
    }

    private void OpenTheDoor()
    {
        if (rotate < 90)
        {
            doorAxis.transform.rotation = Quaternion.Euler(new Vector3(0, doorAxis.transform.rotation.y + rotate, 0));
            rotate++;
        }
        else
        {
            CancelInvoke();
        }
        
    }

    private void CloseTheDoor()
    {
        if (rotate > 0)
        {
            doorAxis.transform.rotation = Quaternion.Euler(new Vector3(0, doorAxis.transform.rotation.y + rotate, 0));
            rotate--;
        }
        else
        {
            CancelInvoke();
        }
        
    }

    public void Interactive()
    {
        if(doorOpen == false)
        {
            doorOpen = true;
            InvokeRepeating("OpenTheDoor", doorMove, doorMove);
        }
        else if(doorOpen == true)
        {
            doorOpen = false;
            InvokeRepeating("CloseTheDoor", doorMove, doorMove);
        }
        else
        {
            //this.transform.eulerAngles = doorOriginalTransform;
        }
    }
}
