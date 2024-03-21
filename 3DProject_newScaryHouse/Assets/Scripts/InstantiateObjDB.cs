using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Component on InstantiateObjDB,store some object that need to instantiate in the scene.
/// </summary>
public class InstantiateObjDB : MonoBehaviour
{
    /// <summary>
    /// �ĤG�ӳ����}�l�n�����Ȥ��C
    /// </summary>
    [SerializeField]
    private GameObject[] paperPieces;
    public GameObject[] PaperPieces
    {
        get { return paperPieces; }
    }

    /// <summary>
    /// �C�������ɪ������r���C
    /// </summary>
    [SerializeField]
    private Transform endingText;
    public Transform EndingText
    {
        get { return endingText; }
    }
}
