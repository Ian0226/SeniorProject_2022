using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class EventsStorage : UnityEvent<bool>
{
    private static EventsStorage singleton;
    public static EventsStorage Singleton
    {
        get
        {
            if(singleton == null)
            {
                singleton = new EventsStorage();
            }
            return singleton;
        }
    }
    public CustomEvent onCustomEvent = new CustomEvent();
    public CustomeEventInt onCustomEventInt = new CustomeEventInt();
    private EventsStorage() { }
}
