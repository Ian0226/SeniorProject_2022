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
    
    [Header("預覽物件頁面")]
    private GameObject itemPreviewPanel = null;

    public void Initialize()
    {
        itemPreviewPanel = GameObject.Find("ItemPreviewPanel");
        itemPreviewPanel.GetComponent<Image>().enabled = false;
    }
    public void SetTextStatus(bool status)
    {
        interactiveText.SetActive(status);
    }
    public void SetItemPreviewPanelStatus(bool status)
    {
        itemPreviewPanel.GetComponent<Image>().enabled = status;
    }
    private void Update()
    {
        for(int i=0;i< Inventory.GetObjs().Count; i++)
        {
            inventoryImgs[i].sprite = Inventory.FindItemSpriteByInt(i);
        }
    }
}
