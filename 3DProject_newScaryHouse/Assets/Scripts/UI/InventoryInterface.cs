using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryInterface : IUserInterface
{
    private Image[] inventoryImgs = new Image[5];

    
    private GameObject[] paperPieceCollectedPosition = new GameObject[8];

    private Image specialItemImg;

    private Text paperPieceCountText = null;
    public Text PaperPieceCountText
    {
        get { return paperPieceCountText; }
    }

    public InventoryInterface(MainGame main) : base(main)
    {
        Initialize();
    }
    public override void Initialize()
    {
        m_RootUI = UITool.FindUIGameObject("InventoryInterface");
        GameObject inventoryUIObj = m_RootUI.transform.Find("InventoryUI").gameObject;
        specialItemImg = m_RootUI.transform.Find("Field_SpecialItem").GetChild(0).GetComponent<Image>();

        if (UnityTool.FindGameObject("PaperPieceCountText"))
        {
            paperPieceCountText = UnityTool.FindGameObject("PaperPieceCountText").GetComponent<Text>();
            paperPieceCountText.gameObject.SetActive(false);
        }
            

        if (GameObject.FindGameObjectWithTag("PaperPieceCollectedPosition"))
        {
            Transform paperPieceCollectedPositionObj = GameObject.Find("PaperPieceCollectedPositionGroup").transform;
            for (int i = 0; i < paperPieceCollectedPositionObj.childCount; i++)
            {
                paperPieceCollectedPosition[i] = paperPieceCollectedPositionObj.GetChild(i).gameObject;
            }
            //paperPieceCollectedPosition = GameObject.FindGameObjectsWithTag("PaperPieceCollectedPosition");
            if(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "MainGameScene_2")
            {
                foreach (GameObject obj in paperPieceCollectedPosition)
                {
                    obj.SetActive(false);
                }
            }
            
        }
        
        for (int i = 0; i < inventoryImgs.Length; i++)
        {
            inventoryImgs[i] = inventoryUIObj.transform.GetChild(i).GetChild(0).GetComponent<Image>();
        }
    }
    public override void Update()
    {
        if (m_RootUI.activeSelf)
        {
            for(int i = 0; i < inventoryImgs.Length; i++)
            {
                if(inventoryImgs[i].sprite == null)
                {
                    inventoryImgs[i].gameObject.SetActive(false);
                }
                else
                {
                    inventoryImgs[i].gameObject.SetActive(true);
                }
                
            }
            for (int i = 0; i < Inventory.GetObjs().Count; i++)
            {
                inventoryImgs[i].sprite = Inventory.FindItemSpriteByInt(i);
            }

            if(Inventory.SpecialItem != null)
            {
                specialItemImg.gameObject.SetActive(true);

                specialItemImg.sprite = Inventory.GetSpecialItemSprite();
            }
            else
            {
                specialItemImg.gameObject.SetActive(false);
                specialItemImg.sprite = null;
            }
        }
    }
    public void ClearInventorySprite()
    {
        for (int i = 0; i < inventoryImgs.Length; i++)
        {
            inventoryImgs[i].sprite = null;
        }
    }

    public void DisplayPaperPieceCount()
    {
        paperPieceCountText.gameObject.SetActive(true);
        paperPieceCountText.text = Inventory.PaperPieceCount + " / " + 8;
    }

    private void ShowPaperPieceOnPhotoFrame(Transform item)
    {
        string itemName = item.gameObject.name.Substring(0, item.gameObject.name.Length - 7);
        char lastChar = itemName.Substring(itemName.Length - 1, 1)[0];
        
        int itemNum = int.Parse(lastChar.ToString());
        
        Debug.Log(item.name + " " + itemName + " " + itemNum);
        
        Inventory.AddPaperPieces(itemNum, item);
        item.gameObject.SetActive(false);
        //Inventory.GetPaperPieceByInt(itemNum).transform.position = paperPieceCollectedPosition[itemNum].transform.position;
        paperPieceCollectedPosition[itemNum].SetActive(true);
    }

    public void CollectPaperPiece(Transform item)
    {
        ShowPaperPieceOnPhotoFrame(item);
        Inventory.PaperPieceCount++;
    }
}
