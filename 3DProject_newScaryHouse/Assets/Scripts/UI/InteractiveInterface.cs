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
    //預覽物件頁面
    private GameObject itemPreviewPanel = null;
    private Button itemPreviewPanelExitBtn = null;
    public override void Initialize()
    {
        GameObject canvas = GameObject.Find("Canvas");
        m_RootUI = canvas.transform.Find("InteractiveInterface").gameObject;
        interactiveHint = m_RootUI.transform.Find("InteractiveHint").gameObject;
        itemPreviewPanel = m_RootUI.transform.Find("ItemPreviewPanel").gameObject;
        itemPreviewPanelExitBtn = itemPreviewPanel.transform.Find("ExitBtn").gameObject.GetComponent<Button>();
        itemPreviewPanelExitBtn.onClick.AddListener(() => SetItemPreviewPanelStatus(false));
        itemPreviewPanel.SetActive(false);
    }
    public void SetTextStatus(bool status)
    {
        interactiveHint.SetActive(status);
    }
    public void SetItemPreviewPanelStatus(bool status)
    {
        itemPreviewPanel.SetActive(status);
    }
    public override void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SetItemPreviewPanelStatus(false);
        }
        if(itemPreviewPanel.activeInHierarchy)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            SetTextStatus(false);
            EventsStorage.Singleton.onCustomEvent.Invoke(false);
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            EventsStorage.Singleton.onCustomEvent.Invoke(true);
        }
    }
}
