using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GameEventControlSystem_Scene1 : GameEventControlSystemBase
{
    int[] nowNumber = new int[3] { 0, 0, 0 };
    bool unLockState = false;

    //The interactive state
    private bool interactiveState = false;

    public GameEventControlSystem_Scene1(MainGame main) : base(main)
    {
        Initialize();
    }
    public override void Initialize()
    {
        Event_Initial();

        handler = Event_1_EndFirstSentence;
        playerMissionHandler = Mission_1_Move;
        //�]�w����Action
        mainGame.GetInteractiveActionObjController.GetObjComponentByName("Painting_1").Action = Painting_1_ChangeScene;
        mainGame.GetInteractiveActionObjController.GetObjComponentByName("Lockbox").ActionWithParam = ItemLockBox_1_Interactive;

    }
    public override void SetActionState(Action action)
    {
        handler = action;
    }

    //����nĲ�o���ƥ�
    public void Painting_1_ChangeScene()
    {
        if (Inventory.FindObj("Item_2_Emerald"))
        {
            //����}�����éж�����
            MethodDelayExecuteTool.ExecuteDelayedMethod(0.5f, () => mainGame.GetEffectAudioSourceByName("EffectAudio_1_SecretRoomOpen").
                    PlayOneShot(mainGame.GetEffectAudioSourceByName("EffectAudio_1_SecretRoomOpen").clip));
            GameObject painting = mainGame.GetInteractiveActionObjController.GetObjComponentByName("Painting_1").transform.GetChild(0).gameObject;
            painting.transform.parent.gameObject.layer = LayerMask.NameToLayer("Default");
            painting.GetComponent<MeshRenderer>().material = mainGame.GetInteractiveActionObjController.GetObjComponentByName("Painting_1").ChangedMaterial;
            ActionStorage.Instance.SetSceneStateNumContainer.Invoke(2);//
        }
        else
        {
            if (!mainGame.GetFontStart())
            {
                //Debug.Log("��ܦr��");
                mainGame.StartFontControl("actionSentences", 0, 2);
            }
        }
    }
    public void ItemLockBox_1_Interactive(Transform obj)
    {
        Transform lock01, lock02, lock03;

        int[] password = new int[3] { 2, 7, 4 };
        if (obj.tag == "Lock")
        {
            switch (obj.parent.name)
            {
                case "Lock01":
                    lock01 = obj.parent;
                    if (nowNumber[0] < 9)
                    {
                        nowNumber[0]++;
                    }
                    else
                    {
                        nowNumber[0] = 0;
                    }
                    lock01.GetComponent<Animator>().SetInteger("Number", nowNumber[0]);
                    obj.parent.parent.Find("AudioContainer").GetComponent<AudioSource>().
                        PlayOneShot(obj.parent.parent.Find("AudioContainer").GetComponent<AudioObj>().GetAudiosByInt(1));

                    break;
                case "Lock02":
                    lock02 = obj.parent;
                    if (nowNumber[1] < 9)
                    {
                        nowNumber[1]++;
                    }
                    else
                    {
                        nowNumber[1] = 0;
                    }
                    lock02.GetComponent<Animator>().SetInteger("Number", nowNumber[1]);
                    obj.parent.parent.Find("AudioContainer").GetComponent<AudioSource>().
                        PlayOneShot(obj.parent.parent.Find("AudioContainer").GetComponent<AudioObj>().GetAudiosByInt(1));

                    break;
                case "Lock03":
                    lock03 = obj.parent;
                    if (nowNumber[2] < 9)
                    {
                        nowNumber[2]++;
                    }
                    else
                    {
                        nowNumber[2] = 0;
                    }
                    lock03.GetComponent<Animator>().SetInteger("Number", nowNumber[2]);
                    obj.parent.parent.Find("AudioContainer").GetComponent<AudioSource>().
                        PlayOneShot(obj.parent.parent.Find("AudioContainer").GetComponent<AudioObj>().GetAudiosByInt(1));

                    break;
            }

            //degree = obj.transform.localEulerAngles.x - 36f;
            //obj.transform.localRotation = Quaternion.Euler(new Vector3(degree, 0, 0));
            //Debug.Log(obj.transform.localEulerAngles.x + " " + degree);
            //obj.transform.DORotate(new Vector3(obj.transform.rotation.x - 36f, 0, 0), 0.3f);

            if (nowNumber[0] == password[0] && nowNumber[1] == password[1] && nowNumber[2] == password[2])
            {
                unLockState = true;
                obj.parent.parent.Find("Cover").GetComponent<Animator>().SetBool("CoverOpen", true);
                obj.parent.parent.Find("AudioContainer").GetComponent<AudioSource>().
                    PlayOneShot(obj.parent.parent.Find("AudioContainer").GetComponent<AudioObj>().GetAudiosByInt(0));
            }
            
            

        }
    }

    //���a�о�Action
    public void Tutorial_1_Move()
    {
        mainGame.DisplayTutorialText("WASD移動");
        if (Input.GetKeyDown(KeyCode.W))
        {
            tutorialInputCount++;
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            tutorialInputCount++;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            tutorialInputCount++;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            tutorialInputCount++;
        }

        if(tutorialInputCount >= 4)
        {
            playerTutorialHandler = Tutorial_2_Crouch;
        }
    }
    public void Tutorial_2_Crouch()
    {
        tutorialInputCount = 0;
        mainGame.DisplayTutorialText("Ctrl蹲下");
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            playerTutorialHandler = Tutorial_3_ShowOptionPanel;
        }
    }
    public void Tutorial_3_ShowOptionPanel()
    {
        tutorialInputCount = 0;
        mainGame.DisplayTutorialText("ESC開啟選單");
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            playerTutorialHandler = Tutorial_3_CloseOptionPanel;
        }
    }
    public void Tutorial_3_CloseOptionPanel()
    {
        mainGame.DisplayTutorialText("ESC關閉選單");
        if (mainGame.GetGameOptionPanel().gameObject.activeInHierarchy == false)
        {
            tutorialInputCount = 0;
            playerTutorialHandler = Tutorial_4_Sprint;
        }
    }
    public void Tutorial_4_Sprint()
    {
        mainGame.DisplayTutorialText("Shift跑步");
        if (Input.GetKey(KeyCode.LeftShift))
        {
            tutorialInputCount = 0;
            playerTutorialHandler = Tutorial_5_Interactive;
        }
    }
    public void Tutorial_5_Interactive()
    {
        mainGame.DisplayTutorialText("E互動");
        if(tutorialInputCount == 1)
        {
            mainGame.EndTutorialText();
            playerTutorialHandler = Tutorial_5Dot5_Interactive;
        }
        else if(tutorialInputCount >= 2)
        {
            playerTutorialHandler = Tutorial_6_MouseDragPreviewItem;
        }
    }
    public void Tutorial_5Dot5_Interactive()
    {
        if(mainGame.GetItemPreviewPanel().activeInHierarchy)
            playerTutorialHandler = Tutorial_6_MouseDragPreviewItem;
    }
    public void Tutorial_6_MouseDragPreviewItem()
    {
        tutorialInputCount = 0;
        mainGame.DisplayTutorialText("滑鼠右鍵拖曳以選轉物體");
        if (Input.GetMouseButtonUp(0))
        {
            playerTutorialHandler = Tutorial_7_InteractiveItemInItemPreviewPanel;
        }
    }
    public void Tutorial_7_InteractiveItemInItemPreviewPanel()
    {
        tutorialInputCount = 0;
        mainGame.DisplayTutorialText("滑鼠右鍵點擊與物件物動");
        if(mainGame.GetItemPreviewPanel().activeInHierarchy && Inventory.GetObjs().Count >= 2)
        {
            playerTutorialHandler = Tutorial_8_ClosePreviewPanel;
        }
    }
    public void Tutorial_8_ClosePreviewPanel()
    {
        mainGame.DisplayTutorialText("E鍵關閉物件預覽頁面");
        if (tutorialInputCount >= 1)
        {
            tutorialInputCount = 0;
            mainGame.EndTutorialText();
            playerTutorialHandler = Tutorial_8Dot5_ClosePreviewPanel;
        }
        
    }
    public void Tutorial_8Dot5_ClosePreviewPanel()
    {
        if(mainGame.GetItemPreviewPanel().activeInHierarchy && Inventory.GetObjs().Count > 1)
        {
            tutorialInputCount = 0;
            playerTutorialHandler = Tutorial_9_ChangePreviewItemInPreviewPanel;
        }
    }
    public void Tutorial_9_ChangePreviewItemInPreviewPanel()
    {
        mainGame.DisplayTutorialText("數字鍵1~5選擇物件進行預覽");
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            tutorialInputCount++;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            tutorialInputCount++;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            tutorialInputCount++;
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            tutorialInputCount++;
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            tutorialInputCount++;
        }
        
        if(tutorialInputCount >= 1)
        {
            tutorialInputCount = 0;
            mainGame.EndTutorialText();
            playerTutorialHandler = Tutorial_9Dot5_ChangePreviewItemInPreviewPanel;
        }

    }
    public void Tutorial_9Dot5_ChangePreviewItemInPreviewPanel()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            playerTutorialHandler = Tutorial_10_ChoosePreviewItem;
        }
    }
    public void Tutorial_10_ChoosePreviewItem()
    {
        mainGame.DisplayTutorialText("數字鍵1~5選擇物件");
        if (!mainGame.GetItemPreviewPanel().activeInHierarchy)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                tutorialInputCount++;
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                tutorialInputCount++;
            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                tutorialInputCount++;
            }
            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                tutorialInputCount++;
            }
            if (Input.GetKeyDown(KeyCode.Alpha5))
            {
                tutorialInputCount++;
            }
        }
        

        if (tutorialInputCount >= 2)
        {
            tutorialInputCount = 0;
            playerTutorialHandler = Tutorial_11_UsePhone;
        }
    }
    public void Tutorial_11_UsePhone()
    {
        tutorialInputCount = 0;
        if (!mainGame.GetItemPreviewPanel().activeInHierarchy)
        {
            mainGame.DisplayTutorialText("F使用特殊物品");
            if (Input.GetKeyDown(KeyCode.F))
            {
                playerTutorialHandler = Tutorial_12_UnUsePhone;
            }
        }
    }
    public void Tutorial_12_UnUsePhone()
    {
        tutorialInputCount = 0;
        mainGame.DisplayTutorialText("收起特殊物品");
        if (Input.GetKeyDown(KeyCode.F))
        {
            mainGame.EndTutorialText();
            playerTutorialHandler = null;
        }
    }

    public void Mission_1_Move()
    {

    }

    public void Event_Initial()
    {
        mainGame.StartFontControl("sentences", 0, 6);
    }
 
    public void Event_1_EndFirstSentence()
    {
        if (mainGame.GetFontControlEnd())
        {
            //Debug.Log("Test_1");
            mainGame.GetPlayerController.SetPlayerControlStatus(true);
            playerTutorialHandler = Tutorial_1_Move;
            SetActionState(Event_2_HintLight);
        }
    }
    public void Event_2_HintLight()
    {
        if (mainGame.GetPlayerController.NowCollisionObj != null && mainGame.GetPlayerController.NowCollisionObj.name == "EventTriggerCollider_2")
        {
            mainGame.LightOpenClose(mainGame.FindLightObjByName("Bulb_LivingroomLamp_1"), mainGame.FindLightObjByName("SpotLight_LivingroomLamp_1"), true);
            MethodDelayExecuteTool.ExecuteDelayedMethod(10f, () => mainGame.LightOpenClose(mainGame.FindLightObjByName("Bulb_LivingroomLamp_1"), mainGame.FindLightObjByName("SpotLight_LivingroomLamp_1"), false));
            SetActionState(Event_3_CloseDoor);
        }
    }
    public void Event_3_CloseDoor()
    {
        if(mainGame.GetPlayerController.NowCollisionObj!=null && mainGame.GetPlayerController.NowCollisionObj.name== "EventTriggerCollider_1")
        {
            if (mainGame.DoorController.GetDoorObjByName("DoorAxis_5").DoorOpen == true)
            {
                
                mainGame.ForceInteractiveDoor(false, "DoorAxis_5", true);//�����Ĥ�����
                mainGame.DoorController.SetDoorLock(true, "DoorAxis_5");
                mainGame.StartFontControl("sentences", 6, 7);
            }
            else
            {
                mainGame.DoorController.GetDoorObjByName("DoorAxis_5").DoorLock = true;
            }
            GameObject.Destroy(mainGame.GetPlayerController.NowCollisionObj);
            SetActionState(Event_4_UnlockDoor);
        }
    }
    public void Event_4_UnlockDoor()
    {
        if(mainGame.GetPlayerController.InteractiveObj!=null && mainGame.GetPlayerController.InteractiveObj.name == "Pic_A_Body")
        {
            mainGame.DoorController.SetDoorLock(false, "DoorAxis_5");
            mainGame.GetEnemyControlSystem.State.SetEnemyStayPoint();
            //SetActionState(Event_4_ChangeScene);
        }
    }
}
