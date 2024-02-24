using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameEventControlSystem_Scene3 : GameEventControlSystemBase
{
    private Action displayPaperPieceCount;
    public GameEventControlSystem_Scene3(MainGame main) : base(main)
    {
        Initialize();
    }
    public override void Initialize()
    {
        displayPaperPieceCount = mainGame.DisplayPaperPieceCount;
        mainGame.GetInteractiveActionObjController.GetObjComponentByName("Door_Main").Action = MainDoor_Interactive;
        SetActionState(Event_0_DoorOpenHint);
    }
    public override void Update()
    {
        if (displayPaperPieceCount != null)
            displayPaperPieceCount();
    }
    public override void SetActionState(Action action)
    {
        handler = action;
    }
    //物件要觸發的事件
    public void MainDoor_Interactive()
    {
        if(Inventory.PaperPieceCount == 8)
        {
            ChangeScene();
        }
        else
        {
            Debug.Log("顯示字幕");
            mainGame.StartFontControl("actionSentences", 2, 3);
        }
        
    }

    //遊戲流程要觸發的事件
    public void Event_0_DoorOpenHint()
    {
        if(Inventory.PaperPieceCount >= 8)
        {
            MethodDelayExecuteTool.ExecuteDelayedMethod(0.5f, () => mainGame.GetEffectAudioSourceByName("EffectAudio_2_DoorOpen").
                PlayOneShot(mainGame.GetEffectAudioSourceByName("EffectAudio_2_DoorOpen").clip));
            mainGame.StartFontControl("sentences", 14, 16);
            SetActionState(null);
        }
    }

    public void ChangeScene()
    {
        Debug.Log("更改場景 : 場景4");
        ActionStorage.Instance.SetSceneStateNumContainer.Invoke(4);//4為場景4
    }
}
