using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ItemPreviewSystem : IGameSystem
{
    private Transform modelTransform;
    public Transform ModelTransform
    {
        get { return modelTransform; }
    }
    private bool isRotate;
    private Vector3 startPoint;
    private Vector3 startAngel;
    private Vector3 itemOriginTransformPos; //給ReadOnlyObj用的
    public Vector3 ItemOriginTransformPos
    {
        get { return itemOriginTransformPos; }
        set { itemOriginTransformPos = value; }
    }
    private Vector3 itemOriginTransformRot;
    public Vector3 ItemOriginTransformRot
    {
        get { return itemOriginTransformRot; }
        set { itemOriginTransformRot = value; }
    }
    private float rotateScale = 1f;

    //private int keyCode;
    public ItemPreviewSystem(MainGame mGame) : base(mGame)
    {
        Initialize();
    }
    public override void Initialize()
    {
        if(GameObject.Find("ItemPreviewContainer").transform)
            modelTransform = GameObject.Find("ItemPreviewContainer").transform;
    }
    public override void Update()
    {
        if (SceneManager.GetActiveScene().name != "MainMenuScene")
        {
            StartItemPreview();
            ClickItemRaycast();
        }
        if (Input.GetMouseButtonDown(0) && !isRotate)
        {
            isRotate = true;
            startPoint = Input.mousePosition;
            startAngel = modelTransform.eulerAngles;
        }
        if (Input.GetMouseButtonUp(0))
        {
            isRotate = false;
        }
        if (isRotate)
        {
            var currentPoint = Input.mousePosition;
            var x = startPoint.x - currentPoint.x;
            var y = startPoint.y - currentPoint.y;
            var z = startPoint.z - currentPoint.z;

            modelTransform.eulerAngles = startAngel + new Vector3(z * rotateScale, x * rotateScale, y * rotateScale);
        }
    }
    public void SetItemToPreviewPos(GameObject interactiveObj)
    {
        if(interactiveObj.tag == "ReadOnlyItem")
        {
            itemOriginTransformPos = interactiveObj.transform.position;
            itemOriginTransformRot = interactiveObj.transform.eulerAngles;
        }
        else
        {
            itemOriginTransformPos = Vector3.zero;
            itemOriginTransformRot = Vector3.zero;
        }
        //Debug.Log(MainGame.Instance.GetItemPreviewSystem.ItemOriginTransformPos);
        //Debug.Log(MainGame.Instance.GetItemPreviewSystem.ItemOriginTransformRot);
        if (modelTransform.transform.childCount != 0)
        {
            CleanItemPreviewTransform();
        }
        interactiveObj.transform.position = modelTransform.position;
        interactiveObj.transform.eulerAngles = new Vector3(0, 0, 0);
        interactiveObj.transform.SetParent(modelTransform.transform);
        
    }
    public void StartItemPreview()
    {
        if (modelTransform.childCount!= 0)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1) && Inventory.GetObjs().Count >= 1)
            {
                mainGame.SetPreviewItem(modelTransform.GetChild(0));
                mainGame.GetInteractiveInterface.SetItemPreviewPanelStatus(true);
                mainGame.SetReadOnlyItemOriginPos();
                CleanItemPreviewTransform();
                modelTransform.GetChild(0).gameObject.SetActive(true);
                DelayExecutor delayExecutor = new DelayExecutor(0.5f, () =>
                { ActionStorage.Instance.CloseItemPreviewPanelAction = mainGame.GetInteractiveInterface.Hide; });
            }
            if (Input.GetKeyDown(KeyCode.Alpha2) && Inventory.GetObjs().Count >= 2)
            {
                mainGame.SetPreviewItem(modelTransform.GetChild(1));
                mainGame.GetInteractiveInterface.SetItemPreviewPanelStatus(true);
                mainGame.SetReadOnlyItemOriginPos();
                CleanItemPreviewTransform();
                modelTransform.GetChild(1).gameObject.SetActive(true);
                DelayExecutor delayExecutor = new DelayExecutor(0.5f, () =>
                { ActionStorage.Instance.CloseItemPreviewPanelAction = mainGame.GetInteractiveInterface.Hide; });
            }
            if (Input.GetKeyDown(KeyCode.Alpha3) && Inventory.GetObjs().Count >= 3)
            {
                mainGame.SetPreviewItem(modelTransform.GetChild(2));
                mainGame.GetInteractiveInterface.SetItemPreviewPanelStatus(true);
                mainGame.SetReadOnlyItemOriginPos();
                CleanItemPreviewTransform();
                modelTransform.GetChild(2).gameObject.SetActive(true);
                DelayExecutor delayExecutor = new DelayExecutor(0.5f, () =>
                { ActionStorage.Instance.CloseItemPreviewPanelAction = mainGame.GetInteractiveInterface.Hide; });
            }
            if (Input.GetKeyDown(KeyCode.Alpha4) && Inventory.GetObjs().Count >= 4)
            {
                mainGame.SetPreviewItem(modelTransform.GetChild(3));
                mainGame.GetInteractiveInterface.SetItemPreviewPanelStatus(true);
                mainGame.SetReadOnlyItemOriginPos();
                CleanItemPreviewTransform();
                modelTransform.GetChild(3).gameObject.SetActive(true);
                DelayExecutor delayExecutor = new DelayExecutor(0.5f, () =>
                { ActionStorage.Instance.CloseItemPreviewPanelAction = mainGame.GetInteractiveInterface.Hide; });
            }
            if (Input.GetKeyDown(KeyCode.Alpha5) && Inventory.GetObjs().Count == 5)
            {
                mainGame.SetPreviewItem(modelTransform.GetChild(4));
                mainGame.GetInteractiveInterface.SetItemPreviewPanelStatus(true);
                mainGame.SetReadOnlyItemOriginPos();
                CleanItemPreviewTransform();
                modelTransform.GetChild(4).gameObject.SetActive(true);
                DelayExecutor delayExecutor = new DelayExecutor(0.5f, () =>
                { ActionStorage.Instance.CloseItemPreviewPanelAction = mainGame.GetInteractiveInterface.Hide; });
            }
        }
    }
    public void CleanItemPreviewTransform()
    {
        for(int i = 0;i < modelTransform.childCount; i++)
        {
            modelTransform.GetChild(i).gameObject.SetActive(false);
        }
    }
    public void ClickItemRaycast()
    {
        Camera camera = GameObject.Find("ItemPreviewCamera").GetComponent<Camera>();
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hitObj, 20))
        {
            if (Input.GetMouseButtonDown(0) && hitObj.transform.parent.transform.parent!=null && 
                hitObj.transform.parent.transform.parent.name != "ItemPreviewContainer" && hitObj.transform.parent.transform.parent.name != "ItemPreviewSystemGroup")
            {
                RemoveItemFromParent(hitObj.transform.parent);//該專案的互動物件都有一個Parent GameObject作為Container
            }
        }
    }
    public void RemoveItemFromParent(Transform childObj)
    {
        Inventory.SetObj(childObj.gameObject);
        childObj.gameObject.SetActive(false);
        childObj.SetParent(modelTransform);
        childObj.position = modelTransform.position;
    }
    private void DetectNowDisplayItem()
    {
        for (int i = 0; i < modelTransform.childCount; i++)
        {
            if (modelTransform.GetChild(i).gameObject.activeInHierarchy)
            {
                mainGame.SetPreviewItem(modelTransform.GetChild(i));
            }
        }
    }
}
