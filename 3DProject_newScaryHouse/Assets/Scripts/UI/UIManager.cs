using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private GameObject interactiveText;

    public void ShowText(bool textStatus)
    {
        interactiveText.SetActive(textStatus);
    }
}
