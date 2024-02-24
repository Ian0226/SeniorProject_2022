using System;
using UnityEngine;

public class GameEventControlSystem_Scene2 : GameEventControlSystemBase
{
    private bool changeScene = false;
    private Action displayPaperPieceCount;

    float degree;
    bool rotate;
    int[] nowNumber = new int[3] { 0, 0, 0 };
    bool unLockState = false;
    public GameEventControlSystem_Scene2(MainGame main) : base(main)
    {
        Initialize();
    }
    public override void Initialize()
    {
        rotate = true;
        SetActionState(Event_Initial);
        //設定物件的Action
        mainGame.GetInteractiveActionObjController.GetObjComponentByName("Door_Main").Action = MainDoor_Interactive;
    }

    //物件要觸發的事件
    public void MainDoor_Interactive()
    {
        mainGame.StartFontControl("actionSentences", 2, 3);
        Debug.Log("顯示字幕");
    }

    public override void Update()
    {
        if (displayPaperPieceCount != null)
            displayPaperPieceCount();
        if (Inventory.PaperPieceCount >= 4 && changeScene == false)
        {
            changeScene = true;
            ChangeScene();
        }
    }
    public override void SetActionState(Action action)
    {
        handler = action;
    }
    public void ChangeScene()
    {
        Debug.Log("更改場景 : 場景3");
        ActionStorage.Instance.SetSceneStateNumContainer.Invoke(3);//3為場景3
    }

    //遊戲流程要觸發的事件
    public void Event_Initial()
    {
        if (mainGame.GetSceneTransitionAnimeComplete() == true)
        {
            mainGame.ForceInteractiveDoor(true, "DoorAxis_4", false);
            mainGame.StartFontControl("sentences", 8, 11);
            SetActionState(Event_1_FindSecretRoom);
        }
    }
    public void Event_1_FindSecretRoom()
    {
        if (mainGame.GetPlayerController.NowCollisionObj != null && mainGame.GetPlayerController.NowCollisionObj.name == "EventTriggerCollider_2")
        {
            mainGame.GetPaperPieceCountText().SetActive(true);
            mainGame.StartFontControl("sentences", 11, 14);
            displayPaperPieceCount = mainGame.DisplayPaperPieceCount;
            SetActionState(null);
        }
    }
}
