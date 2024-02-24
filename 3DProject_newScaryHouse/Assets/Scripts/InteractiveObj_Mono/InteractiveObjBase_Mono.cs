using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveObjBase_Mono : MonoBehaviour
{
    [SerializeField]
    protected Vector3 hintPosition;
    public Vector3 HintPosition
    {
        get { return hintPosition; }
        set { hintPosition = value; }
    }

    [SerializeField]
    protected string itemDescription;
    public string ItemDescription
    {
        get { return itemDescription; }
        set { itemDescription = value; }
    }

    [SerializeField]
    private Transform hintPointTransform;
    public Transform HintPointTransform
    {
        get { return hintPointTransform; }
    }
}
