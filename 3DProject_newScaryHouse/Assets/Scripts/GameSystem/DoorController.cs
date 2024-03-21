using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

/// <summary>
/// Store all door to array in game scene,handle door operations.
/// </summary>
public class DoorController : InteractableObjBase
{
    private int doorNum;
    private GameObject[] doorAxises;
    private DoorObj[] doorObjs;

    public DoorController(MainGame mGame) : base(mGame)
    {
        Initialize();
    }
    public override void Initialize()
    {
        doorAxises = GameObject.FindGameObjectsWithTag("DoorAxis");
        //Sort by door number.
        string[] strs = new string[doorAxises.Length];
        for(int i = 0; i < strs.Length; i++)
        {
            strs[i] = doorAxises[i].name;
        }
        Array.Sort(strs);
        for(int i = 0; i < strs.Length; i++)
        {
            doorAxises[i] = GameObject.Find(strs[i]);
        }

        doorObjs = new DoorObj[doorAxises.Length];
        for (int i = 0; i < doorObjs.Length; i++)
        {
            doorObjs[i] = doorAxises[i].GetComponent<DoorObj>();
        }
    }

    public override void Update()
    {
        //Set door hint UI position.
        doorObjs[doorNum].HintPosition = doorObjs[doorNum].HintPosObj.position;
    }

    /// <summary>
    /// Handle door interactive.
    /// </summary>
    /// <param name="objName">The door name you want to interactive.</param>
    public override void Interactive(string objName)
    {
        FindObjByName(objName);
        if (!doorObjs[doorNum].DoorLock)
        {
            if (doorObjs[doorNum].DoorOpen == false)
            {
                doorObjs[doorNum].DoorOriginPosition = doorObjs[doorNum].transform.localPosition;
                doorObjs[doorNum].DoorOpen = true;
                //doorObjs[doorNum].StartInvokeRepeating("OpenTheDoor");
                doorObjs[doorNum].DoorAni.SetBool("DoorOpen",true);
                doorObjs[doorNum].DoorAudio_1.PlayOneShot(doorObjs[doorNum].DoorAudio_1.clip);//Open audio.
            }
            else
            {
                doorObjs[doorNum].DoorOpen = false;
                //doorObjs[doorNum].StartInvokeRepeating("CloseTheDoor");
                doorObjs[doorNum].DoorAni.SetBool("DoorOpen", false);
                doorObjs[doorNum].DoorAudio_2.PlayOneShot(doorObjs[doorNum].DoorAudio_2.clip);//Close audio.
            }
        }
        else
        {
            if(doorObjs[doorNum].UnlockDoorObj == null)
            {
                DoorLockInteractiveEffect();
                return;
            }

            if (!Inventory.FindObj(doorObjs[doorNum].UnlockDoorObj.name))
            {
                DoorLockInteractiveEffect();
                return;
            }

            doorObjs[doorNum].DoorLock = false;
            Inventory.ClearItemByName(doorObjs[doorNum].UnlockDoorObj.name);
            doorObjs[doorNum].UnlockDoorAudio.PlayOneShot(doorObjs[doorNum].UnlockDoorAudio.clip);
            MainGame.Instance.GetDoorLockHintText().SetActive(true);
            MainGame.Instance.GetDoorLockHintText().GetComponent<Text>().text = "成功解鎖";
            MethodDelayExecuteTool.ExecuteDelayedMethod(0.5f, () => MainGame.Instance.GetDoorLockHintText().SetActive(false));
            //Debug.Log(Inventory.FindObj(doorObjs[doorNum].UnlockDoorObj.name));
            //Debug.Log(doorObjs[doorNum].UnlockDoorObj.name);
        } 
    }
    
    /// <summary>
    /// Door lock effect when door is locked.
    /// </summary>
    private void DoorLockInteractiveEffect()
    {
        int value = 0;
        MainGame.Instance.GetDoorLockHintText().SetActive(true);
        MainGame.Instance.GetDoorLockHintText().GetComponent<Text>().text = "已上鎖";
        DOTween.To(() => value, x => value = x, 10, 0.5f).OnComplete(() =>
        {
            value = 0;
            MainGame.Instance.GetDoorLockHintText().SetActive(false);

        });
        doorObjs[doorNum].DoorAudio_3.PlayOneShot(doorObjs[doorNum].DoorAudio_3.clip);
    }

    public void SetDoorLock(bool lockState,string doorName)
    {
        GetDoorObjByName(doorName).DoorLock = lockState;
    }

    /// <summary>
    /// Call this when player trigger close door event.
    /// </summary>
    /// <param name="doorStatus">True is door open,false is close.</param>
    /// <param name="doorName">The door you want to control.</param>
    /// <param name="playAudio">The door audio.</param>
    public void ForceInteractiveDoor(bool doorStatus,string doorName,bool playAudio)
    {
        FindObjByName(doorName);
        if (doorStatus)
        {
            doorObjs[doorNum].DoorOpen = true;
            //doorObjs[doorNum].StartInvokeRepeating("OpenTheDoor");
            doorObjs[doorNum].DoorAni.SetBool("DoorOpen", true);
            if(playAudio)
                doorObjs[doorNum].DoorAudio_1.PlayOneShot(doorObjs[doorNum].DoorAudio_1.clip);
        }
        else
        {
            doorObjs[doorNum].DoorOpen = false;
            //doorObjs[doorNum].StartInvokeRepeating("CloseTheDoor");
            if (playAudio)
            {
                doorObjs[doorNum].DoorAni.SetBool("DoorOpen", false);
                doorObjs[doorNum].DoorAni.SetBool("CloseDoorVigorously", true);
            }
            doorObjs[doorNum].DoorAudio_2.PlayOneShot(doorObjs[doorNum].CloseDoorAudio_Louder.clip);
        }
    }
    public void FindObjByName(string name)
    {
        for(int i=0;i < doorAxises.Length; i++)
        {
            if(doorAxises[i].name == name)
            {
                doorNum = i;
            }
        }
    }
    public DoorObj GetDoor()
    {
        return doorObjs[doorNum];
    }
    public DoorObj GetDoorByInt(int i)
    {
        return doorObjs[i];
    }
    public DoorObj GetDoorObjByName(string containerName)
    {
        GameObject obj = new GameObject();
        for(int i = 0; i < doorObjs.Length; i++)
        {
            if (doorObjs[i].name == containerName)
            {
                obj = doorObjs[i].gameObject;
                break;
            }
            else
            {
                obj = null;
                continue;
            }
        }
        //Find nothing.
        if(obj == null)
        {
            Debug.LogWarning("Can't find door by this name : " + containerName);
        }
        return obj.GetComponent<DoorObj>();
    }
}
