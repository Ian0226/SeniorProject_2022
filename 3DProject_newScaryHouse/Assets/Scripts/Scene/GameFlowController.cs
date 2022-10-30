using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameFlowController : MonoBehaviour
{
    private void Awake()
    {
        EventsStorage.Singleton.onCustomEventInt.AddListener(EventsControl);
    }
    public void EventsControl(int num)
    {
        switch(num)
        {
            case 1:
                DoorController door = GameObject.Find("DoorAxis_5").GetComponent<DoorController>();
                door.ForceInteractiveDoor(false);
                door.SetDoorLock(true);
                break;
        }
    }
}
