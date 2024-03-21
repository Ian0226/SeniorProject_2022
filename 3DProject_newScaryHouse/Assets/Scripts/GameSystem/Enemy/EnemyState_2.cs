using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DG.Tweening;

/// <summary>
/// Enemy state in game scene 2,3,in this state enemy if player see enemy to long,enemy will do something bad to player.
/// </summary>
public class EnemyState_2 : IEnemyState
{
    private List<Transform> enemyStayPoint = new List<Transform>();
    private int enemyStayPointNum = 0;

    private float enemyAppearanceFrequency;
    private float enemyAppearanceFrequency_regist;

    private RaycastHit hitObj;

    private List<int> enemyRandomStayPointNums = new List<int>();

    private Transform enemyRestPos;

    private Transform enemyHead;

    private bool enemySpeakWordsImgState = false;

    private AudioSource enemyAudioSource;

    public EnemyState_2(EnemyControlSystem controller, MainGame main) : base(controller, main)
    {
        StateInitialize();
    }
    public override void StateInitialize()
    {
        enemyObj = Unity.CustomTool.UnityTool.FindGameObject("Enemy_State2");
        enemyHead = Unity.CustomTool.UnityTool.FindChildGameObject(enemyObj, "Head").transform;
        enemyAni = enemyHead.GetComponent<Animator>();
        //Debug.Log(enemyObj);
        //enemyAppearanceFrequency = Random.Range(20, 71);
        //test
        enemyAppearanceFrequency = 10;
        enemyAppearanceFrequency_regist = enemyAppearanceFrequency;

        enemyStayPoint = mainGame.GetEnemyStayPointObjs();

        enemyRestPos = Unity.CustomTool.UnityTool.FindGameObject("EnemyRestPos").transform;
        enemyObj.transform.position = enemyRestPos.position;
        enemyAudioSource = enemyObj.GetComponent<AudioSource>();
    }
    public override void StateUpdate()
    {
        enemyStayPoint = mainGame.GetEnemyStayPointObjs();
        enemyHead.LookAt(mainGame.GetPlayerCamera());
        if (Time.timeSinceLevelLoad > enemyAppearanceFrequency)
        {
            //enemyAppearanceFrequency = Random.Range(20, 71);
            //test
            //enemyAppearanceFrequency = 10;
            //Debug.Log("Enemy_Test " + enemyStayPoint.Count);
            EnemyMove();
            if(enemyObj.transform.position != enemyRestPos.position)
            {
                //Debug.Log("Enemy_Test " + enemyStayPoint.Count);
                Transform enemySpeakWordsImg = mainGame.GetEnemySpeakWordsImg();
                if(enemySpeakWordsImgState == false)
                {
                    enemySpeakWordsImg.gameObject.SetActive(true);
                    enemySpeakWordsImgState = true;
                }   
                enemySpeakWordsImg.GetComponent<UnityEngine.UI.Image>().color = new Color(255, 255, 255, 0);
                mainGame.LockPlayerAllControlState(2f);
                mainGame.CameraLookTarget(enemyHead);
                //¶}±Ò­µ®ÄShow_up
                enemyAudioSource.PlayOneShot(enemyObj.GetComponent<AudioObj>().GetAudiosByInt(0));

                DOTween.To(() => enemySpeakWordsImg.GetComponent<UnityEngine.UI.Image>().color.a, x =>
                {
                    enemySpeakWordsImg.GetComponent<UnityEngine.UI.Image>().color = new Color(255, 255, 255, x);
                }, 1, 3).OnComplete(() => { enemySpeakWordsImg.gameObject.SetActive(false);
                    mainGame.SetDoNotLookEnemyAction(mainGame.PlayerLookEnemy);
                });
            }
                
                
            enemyAppearanceFrequency = Time.timeSinceLevelLoad + enemyAppearanceFrequency_regist;
        }
    }
    public override void EnemyBehaviour()
    {
        enemyAudioSource.PlayOneShot(enemyObj.GetComponent<AudioObj>().GetAudiosByInt(1));
        ChooseEnemyBehaviour();
    }
    public void ChooseEnemyBehaviour()
    {
        int num = UnityEngine.Random.Range(0, 2);
        if(mainGame.GetPlayerController.PlayerControlStatus == true)
        {
            switch (num)
            {
                case 0:
                    EnemyBehaviour_0_CatchPlayer();
                    break;
                case 1:
                    EnemyBehaviour_1_DecreasePlayerVision();
                    break;
            }
        }
    }
    public void EnemyBehaviour_0_CatchPlayer()
    {
        mainGame.SetCoverSceneImageActive(true);
        UnityEngine.UI.Image coverPlayerImg = Unity.CustomUITool.UITool.FindUIGameObject("CoverPlayerEyesImg").GetComponent<UnityEngine.UI.Image>();
        mainGame.SetPlayerControlStateAndCameraState(false);
        enemyObj.transform.LookAt(mainGame.GetPlayer().transform);
        enemyObj.transform.DOMove(new Vector3(mainGame.GetPlayerController.EnemyToPlayerPoint.position.x,
            mainGame.GetPlayerController.EnemyToPlayerPoint.position.y, mainGame.GetPlayerController.EnemyToPlayerPoint.position.z), 0.5f).
            OnComplete(() => {
                DOTween.To(() => coverPlayerImg.color.a, x => coverPlayerImg.color = new Color(0, 0, 0, x), 255, 2).OnComplete(() => {
                    mainGame.SetPlayerPosToRespawnPoint();
                    DOTween.To(() => coverPlayerImg.color.a, x => coverPlayerImg.color = new Color(0, 0, 0, x), 0, 2).OnComplete(() => {
                        mainGame.SetPlayerControlStateAndCameraState(true);
                        mainGame.SetCoverSceneImageActive(false);
                    });
                });
            });
    }
    public void EnemyBehaviour_1_DecreasePlayerVision()
    {
        mainGame.StartVignetteEffect();
    }
    public override void EnemyMove()
    {
        mainGame.SetDoNotLookEnemyAction(null);
        if (enemyStayPoint.Count > 1)
        {
            enemyRandomStayPointNums = GenerateRandomNumbers();
            for (int i = 0; i < enemyStayPoint.Count; i++)
            {
                enemyObj.transform.position = enemyStayPoint[enemyRandomStayPointNums[i]].transform.position;
                enemyObj.transform.LookAt(mainGame.GetPlayer().transform);
                if (DetecctPlayeyRayHandler())
                {
                    break;
                }
                else
                {
                    enemyObj.transform.position = enemyRestPos.position;
                    continue;
                }
            }
        }
        else if (enemyStayPoint.Count == 1)
        {
            enemyObj.transform.position = enemyStayPoint[0].position;
            enemyObj.transform.LookAt(mainGame.GetPlayer().transform);
            if (DetecctPlayeyRayHandler())
            {
                enemyObj.transform.position = enemyStayPoint[0].position;
                enemyObj.transform.LookAt(mainGame.GetPlayer().transform);
            }
            else
            {
                enemyObj.transform.position = enemyRestPos.position;
            }
        }
        else
        {
            enemyObj.transform.position = enemyRestPos.position;
        }
    }

    private bool DetecctPlayeyRayHandler()
    {
        bool isHit = false;
        if (Physics.Raycast(enemyObj.transform.position, enemyObj.transform.forward, out hitObj, 100))
        {
            Debug.Log(hitObj.transform.gameObject);
            if (hitObj.transform.gameObject.layer == 3)
            {
                isHit = true;
            }
            else
            {
                isHit = false;
            }
        }
        return isHit;
    }

    public List<int> GenerateRandomNumbers()
    {
        List<int> numbers = new List<int>();

        for (int i = 0; i < enemyStayPoint.Count; i++)
        {
            int newNumber;
            do
            {
                newNumber = UnityEngine.Random.Range(0, enemyStayPoint.Count);
            } while (numbers.Contains(newNumber));

            numbers.Add(newNumber);
        }

        return numbers;
    }
}
