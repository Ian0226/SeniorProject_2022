using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;
using System.IO;
using TMPro;
using UnityEngine.SceneManagement;
using Unity.CustomUITool;

public class SceneAniEffectControlSystem : IGameSystem
{
    private Action endGameAnimeHandlerAction;
    public Action EndGameAnimeHandlerAction
    {
        get { return endGameAnimeHandlerAction; }
        set { endGameAnimeHandlerAction = value; }
    }
    private Image coverSceneImg;

    private bool transitComplete = false;
    public bool TransitCompelet
    {
        get { return transitComplete; }
    }

    //遊戲結束跑馬燈頁面
    private GameObject endGamePanel;
    private RectTransform downPoint;
    private RectTransform upPoint;
    public RectTransform UpPoint
    {
        get { return upPoint; }
    }
    private GameObject endingText;

    private float instantiateEndingTextTime = 0;
    private float instantiateEndingTextRate = 1.5f;

    string[] endingTexts = new string[] { };
    int sentencesNum = 0;

    public SceneAniEffectControlSystem(MainGame main) : base(main)
    {
        Initialize();
    }
    public override void Initialize()
    {
        coverSceneImg = Unity.CustomTool.UnityTool.FindGameObject("CoverPlayerEyesImg").GetComponent<Image>();
        SetCoverSceneImageActive(false);

        if (UITool.FindUIGameObject("EndGamePanel"))
        {
            endGamePanel = UITool.FindUIGameObject("EndGamePanel");
            endGamePanel.SetActive(false);
        }
        if (UITool.FindUIGameObject("DownPoint"))
        {
            downPoint = UITool.FindUIGameObject("DownPoint").GetComponent<RectTransform>();
        }
        if (UITool.FindUIGameObject("UpPoint"))
        {
            upPoint = UITool.FindUIGameObject("UpPoint").GetComponent<RectTransform>();
        }
        if (Unity.CustomTool.UnityTool.FindGameObject("InstantiateObjDB").GetComponent<InstantiateObjDB>().EndingText)
        {
            endingText = Unity.CustomTool.UnityTool.FindGameObject("InstantiateObjDB").GetComponent<InstantiateObjDB>().EndingText.gameObject;
        }
        if(SceneManager.GetActiveScene().name == "MainGameScene_4")
        {
            LoadTextFromJson();
        }

    }
    public override void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
            endGameAnimeHandlerAction = EndGameAnimeHandler;
        if(endGameAnimeHandlerAction != null)
            endGameAnimeHandlerAction();
    }

    //轉場動畫
    public void SceneTransitionAnime(Action action,bool isTransit)
    {
        SetCoverSceneImageActive(true);
        transitComplete = false;
        if (isTransit)
        {
            coverSceneImg.color = new Color(0, 0, 0, 255);
            MainGame.Instance.SetPlayerControlStateAndCameraState(false);
            DOTween.To(() => coverSceneImg.color.a, x => coverSceneImg.color = new Color(0, 0, 0, x), 0, 3)
                .OnComplete(() => {
                    MainGame.Instance.SetPlayerControlStateAndCameraState(true);
                    transitComplete = true;
                });
        }
        else
        {
            MainGame.Instance.SetPlayerControlStateAndCameraState(false);
            DOTween.To(() => coverSceneImg.color.a, x => coverSceneImg.color = new Color(0, 0, 0, x), 255, 3)
               .OnComplete(() => {
                   action.Invoke();
               }).SetEase(Ease.InExpo);
        }
    }
    public void SetCoverSceneImageActive(bool state)
    {
        coverSceneImg.gameObject.SetActive(state);
    }

    //遊戲結束跑馬燈
    public void EndGameAnimeHandler()
    {
        GameObject endingTextObj;
        if (Time.timeSinceLevelLoad >= instantiateEndingTextTime)
        {
            endingTextObj = GameObject.Instantiate(endingText, downPoint);
            if (sentencesNum < endingTexts.Length)
            {
                endingTextObj.GetComponent<TextMeshProUGUI>().text = endingTexts[sentencesNum];
                sentencesNum++;
            }
            else
            {
                MethodDelayExecuteTool.ExecuteDelayedMethod(1.5f,() => mainGame.BackMainMenu());
            }
            instantiateEndingTextTime = instantiateEndingTextRate + Time.timeSinceLevelLoad;
        }
    }
    public void SetEndGamePanelState(bool state)
    {
        endGamePanel.SetActive(state);
    }
    public void LoadTextFromJson()
    {
        string path;
        EndingTextData data = new EndingTextData();
        path = Path.Combine(Directory.GetCurrentDirectory(), "Assets\\StreamingAssets\\EndingTextSentencesJson.json");

        if (!File.Exists(path))
        {
            path = Path.Combine(Directory.GetCurrentDirectory(), "3DProject_newScaryHouse_Data\\StreamingAssets\\EndingTextSentencesJson.json");
        }
        var json = File.ReadAllText(path);
        data = JsonUtility.FromJson<EndingTextData>(json);
        Debug.Log(data.EndingTextSentences);
        endingTexts = data.EndingTextSentences;
    }

    public class EndingTextData
    {
        [SerializeField]
        private string[] endingTextSentences;
        public string[] EndingTextSentences
        {
            get { return endingTextSentences; }
        }
    }
}
