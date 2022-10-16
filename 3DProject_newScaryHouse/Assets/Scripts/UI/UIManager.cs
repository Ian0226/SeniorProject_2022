using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private GameObject interactiveText;
    public GameObject InteractiveText
    {
        get { return interactiveText; }
        set { interactiveText = value; }
    }
    [SerializeField]
    private Image[] inventoryImgs;

    public void ShowText(bool textStatus)
    {
        interactiveText.SetActive(textStatus);
    }
    private void Update()
    {
        for(int i=0;i< Inventory.GetObjs().Count; i++)
        {
            inventoryImgs[i].sprite = Inventory.FindItemSpriteByInt(i);
        }
    }
}
