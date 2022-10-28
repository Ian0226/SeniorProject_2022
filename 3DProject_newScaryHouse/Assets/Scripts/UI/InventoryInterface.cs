using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryInterface : IUserInterface
{
    private Image[] inventoryImgs = new Image[5];

    public override void Initialize()
    {
        GameObject canvas = GameObject.Find("Canvas");
        m_RootUI = canvas.transform.Find("InventoryInterface").gameObject;
        GameObject inventoryUIObj = m_RootUI.transform.Find("InventoryUI").gameObject;
        for (int i = 0; i < inventoryImgs.Length; i++)
        {
            inventoryImgs[i] = inventoryUIObj.transform.GetChild(i).GetChild(0).GetComponent<Image>();
        }
    }
    public override void Update()
    {
        if (m_RootUI.activeSelf)
        {
            for (int i = 0; i < Inventory.GetObjs().Count; i++)
            {
                inventoryImgs[i].sprite = Inventory.FindItemSpriteByInt(i);
            }
        }
    }
}
