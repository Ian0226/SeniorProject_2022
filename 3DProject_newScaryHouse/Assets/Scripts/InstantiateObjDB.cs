using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateObjDB : MonoBehaviour
{
    [SerializeField]
    private GameObject[] paperPieces;
    public GameObject[] PaperPieces
    {
        get { return paperPieces; }
    }

    [SerializeField]
    private Transform endingText;
    public Transform EndingText
    {
        get { return endingText; }
    }
    public void Awake()
    {

    }
}
