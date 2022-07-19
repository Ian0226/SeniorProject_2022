using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObjBase : MonoBehaviour
{
    private string command;
    public string Command
    {
        get { return command; }
        set { command = value; }
    }
}
