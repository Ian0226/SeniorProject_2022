using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderDetectionTool : MonoBehaviour
{
    private Collider _collider = null;
    public Collider Collider
    {
        get { return _collider; }
        set { _collider = value; }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other)
            _collider = other;
    }
    private void OnTriggerExit(Collider other)
    {
        other = null;
    }
}
