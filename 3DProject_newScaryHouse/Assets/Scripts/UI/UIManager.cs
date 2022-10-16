using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private GameObject interactiveText;

    public GameObject InteractiveText
    {
        get { return interactiveText; }
        set { interactiveText = value; }
    }

    public void ShowText(bool textStatus)
    {
        interactiveText.SetActive(textStatus);
    }
}
