using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Make something DontDestroy.
/// </summary>
public class DoNotDestroy : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(this);
    }
}
