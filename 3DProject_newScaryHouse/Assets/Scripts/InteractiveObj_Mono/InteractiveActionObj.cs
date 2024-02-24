using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveActionObj : MonoBehaviour
{
    private Action action;
    public Action Action
    {
        get { return action; }
        set { action = value; }
    }

    private Action<Transform> actionWithParam;
    public Action<Transform> ActionWithParam
    {
        get { return actionWithParam; }
        set { actionWithParam = value; }
    }

    [Header("�p�G���ݭn�󴫧���")]
    [SerializeField]
    private Material changedMaterial;
    public Material ChangedMaterial
    {
        get { return changedMaterial; }
        set { changedMaterial = value; }
    }
}
