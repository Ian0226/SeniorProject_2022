using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Component on InstantiateObjDB,store some object that need to instantiate in the scene.
/// </summary>
public class InstantiateObjDB : MonoBehaviour
{
    /// <summary>
    /// 第二個場景開始要收集紙片。
    /// </summary>
    [SerializeField]
    private GameObject[] paperPieces;
    public GameObject[] PaperPieces
    {
        get { return paperPieces; }
    }

    /// <summary>
    /// 遊戲結束時的結尾字幕。
    /// </summary>
    [SerializeField]
    private Transform endingText;
    public Transform EndingText
    {
        get { return endingText; }
    }
}
