using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractiveInterface : IUserInterface
{
    //互動提示
    private GameObject interactiveHint;
    public GameObject InteractiveHint
    {
        get { return interactiveHint; }
        set { interactiveHint = value; }
    }
    //門鎖住時的提示
    private GameObject doorLockHint;
    public GameObject DoorLockHint
    {
        get { return doorLockHint; }
    }
    //預覽物件頁面
    private GameObject itemPreviewPanel = null;
    public GameObject ItemPreviewPanel
    {
        get { return itemPreviewPanel; }
    }
    private Text itemDescriptText = null;
    private Transform nowDisplayItem = null;
    private Button itemPreviewPanelExitBtn = null;
    private RectTransform hintBox;

    private GameObject viewItemReturnHint;
    public GameObject ViewItemReturnHint
    {
        get { return viewItemReturnHint; }
    }
    public GameObject hintForItem;

    public InteractiveInterface(MainGame main) : base(main)
    {
        Initialize();
    }
    public override void Initialize()
    {
        m_RootUI = UITool.FindUIGameObject("InteractiveInterface");
        interactiveHint = m_RootUI.transform.Find("InteractiveHint").gameObject;
        doorLockHint = m_RootUI.transform.Find("LockHintText").gameObject;
        itemPreviewPanel = m_RootUI.transform.Find("ItemPreviewPanel").gameObject;
        hintBox = m_RootUI.GetComponent<RectTransform>();
        viewItemReturnHint = m_RootUI.transform.Find("Hint").gameObject;
        hintForItem = m_RootUI.transform.Find("HintForItem").gameObject;
        //itemPreviewPanelExitBtn = UITool.GetUIComponent<Button>(itemPreviewPanel, "ExitBtn");
        itemDescriptText = UnityTool.FindChildGameObject(itemPreviewPanel, "ItemDescriptText").GetComponent<Text>(); 
        /*itemPreviewPanelExitBtn.onClick.AddListener(() => {
        {
                if (m_MainGame.GetGameEventControlSystem.PlayerTutorialHandler.Method.Name.Equals("Tutorial_8_ClosePreviewPanel"))
                {
                    SetItemPreviewPanelStatus(false);
                    m_MainGame.SetTutorialInputCount(m_MainGame.GetTutorialInputCount() + 1);
                } 
            }
        });*/
        //itemPreviewPanelExitBtn.onClick.AddListener(() => { ActionStorage.Instance.CloseItemPreviewPanelAction = null; });
        //itemPreviewPanelExitBtn.onClick.AddListener(() => SetReadOnlyItemPos());
        itemPreviewPanel.SetActive(false);
        interactiveHint.SetActive(false);
        hintForItem.SetActive(false);
    }
    public void SetTextStatus(bool status)
    {
        interactiveHint.SetActive(status);
    }
    public void SetHintForItemStatus(bool status)
    {
        hintForItem.SetActive(status);
    }
    public void SetItemPreviewPanelStatus(bool status)
    {
        //Debug.Log(nowDisplayItem);
        itemPreviewPanel.SetActive(status);
        if (itemPreviewPanel.activeInHierarchy)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            if(nowDisplayItem != null)
            {
                itemDescriptText.text = nowDisplayItem.GetComponent<ItemObj>().ItemDescription;
            }
            else
            {
                itemDescriptText.text = "";
            }
            SetTextStatus(false);
            ActionStorage.Instance.SetPlayerControlStatusAction.Invoke(false);
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            ActionStorage.Instance.SetPlayerControlStatusAction.Invoke(true);
        }
    }
    public void SetReadOnlyItemPos()
    {
        for (int i=0;i< MainGame.Instance.GetItemPreviewSystem.ModelTransform.childCount; i++)
        {
            if (MainGame.Instance.GetItemPreviewSystem.ModelTransform.GetChild(i).tag == "ReadOnlyItem")
            {
                Transform obj = MainGame.Instance.GetItemPreviewSystem.ModelTransform.GetChild(i);
                obj.SetParent(null);
                Debug.Log(MainGame.Instance.GetItemPreviewSystem.ItemOriginTransformPos);
                obj.position = MainGame.Instance.GetItemPreviewSystem.ItemOriginTransformPos;
                obj.rotation = Quaternion.Euler(MainGame.Instance.GetItemPreviewSystem.ItemOriginTransformRot);
                break;
            }
        }
    }
    public void SetInteractiveHintPos()
    {
        Vector2 pos;
        if (m_MainGame.GetPlayerController.NowInteractiveObj != null)
        {
            //將提示設置到物件上的對應位置
            if (m_MainGame.GetPlayerController.NowInteractiveObj.transform.parent.tag == "Item" || m_MainGame.GetPlayerController.NowInteractiveObj.transform.parent.tag == "ReadOnlyItem" 
                || m_MainGame.GetPlayerController.NowInteractiveObj.transform.parent.tag == "PaperPiece" || m_MainGame.GetPlayerController.NowInteractiveObj.transform.parent.tag == "BigInteractiveObject")
            {
                /*Vector2 screenPos;
                if (m_MainGame.GetPlayerController.NowInteractiveObj.transform.parent == null)
                {
                    screenPos = Camera.main.WorldToScreenPoint(m_MainGame.GetPlayerController.
                    NowInteractiveObj.GetComponent<ItemObj>().HintPosition);
                }
                else
                {
                    screenPos = Camera.main.WorldToScreenPoint(m_MainGame.GetPlayerController.
                    NowInteractiveObj.transform.parent.GetComponent<ItemObj>().HintPosition);
                }

                if (RectTransformUtility.ScreenPointToLocalPointInRectangle(hintBox, screenPos, Camera.main, out pos))
                {
                    interactiveHint.transform.position = pos;
                }*/

                interactiveHint.transform.position = hintForItem.GetComponent<RectTransform>().position;
                //interactiveHint.transform.position = Camera.main.WorldToScreenPoint(m_MainGame.GetPlayerController.
                //NowInteractiveObj.GetComponent<ItemObj>().HintPosition);
                //RectTransformUtility.
            }
            else if (m_MainGame.GetPlayerController.NowInteractiveObj.tag == "Door")
            {
                DoorObj door = m_MainGame.GetPlayerController.NowInteractiveObj.transform.parent.GetComponent<DoorObj>();
                door.SetDoorInteractiveHintPos();
                pos = RectTransformUtility.WorldToScreenPoint(Camera.main, door.HintPosition);
                interactiveHint.transform.position = pos;
                //interactiveHint.transform.position = Camera.main.WorldToScreenPoint(door.HintPosition);
            }
        }
    }

    public override void Update()
    {
        if (ActionStorage.Instance.CloseItemPreviewPanelAction != null)
            ActionStorage.Instance.CloseItemPreviewPanelAction();
    }
    public override void Hide()
    {
        if(itemPreviewPanel != null)
        {
            if (itemPreviewPanel.activeInHierarchy)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    m_MainGame.SetTutorialInputCount(m_MainGame.GetTutorialInputCount() + 1);
                    SetItemPreviewPanelStatus(false);
                    SetReadOnlyItemPos();
                    ActionStorage.Instance.CloseItemPreviewPanelAction = null;
                }
            }
        }
    }
    public override void Show()
    {
        SetItemPreviewPanelStatus(true);
    }
    public void SetPreviewItem(Transform obj)
    {
        nowDisplayItem = obj;
    }
}
