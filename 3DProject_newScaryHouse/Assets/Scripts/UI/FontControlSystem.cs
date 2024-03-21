using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.IO;
using DG.Tweening;
using Unity.CustomTool;

public class FontControlSystem : IUserInterface
{
    private FontControlSystemMono fcsMono = null;
    private bool fontControlStart = false;
    public bool FontControlStart
    {
        get { return fontControlStart; }
    }
    private bool fontControlEnd = false;
    public bool FontControlEnd
    {
        get { return fontControlEnd; }
    }
    private TextMeshProUGUI textDisplay;
    public TextMeshProUGUI TextDisplay
    {
        get { return textDisplay; }
    }

    private int index;
    public int Index
    {
        get { return index; }
    }

    private float typingSpeed;
    public float TypingSpeed
    {
        get { return typingSpeed; }
    }

    private GameObject fcsContainer;

    private GameObject interactiveHint;

    private bool status;
    public bool Status
    {
        get { return status; }
    }

    private string nowActionSentenceType = null;//sentences,actionSentences,hintPlayerSentences
    public string NowActionSentenceType
    {
        get { return nowActionSentenceType; }
        set { nowActionSentenceType = value; }
    }

    //熊熊字幕
    private Transform enemySpeakWordsImg = null;
    public Transform EnemySpeakWordsImg
    {
        get { return enemySpeakWordsImg; }
    }

    //教學字幕UI
    private Transform tutorialInterfaceContainer = null;
    public Transform TutorialInterfaceContainer
    {
        get { return tutorialInterfaceContainer; }
    }

    public Text tutorialInterText;

    public FontControlSystem(MainGame main) : base(main)
    {
        Initialize();
    }
    public override void Initialize()
    {
        fcsContainer = Unity.CustomUITool.UITool.FindUIGameObject("FontControlSystemContainer");
        textDisplay = Unity.CustomUITool.UITool.GetUIComponent<TextMeshProUGUI>(fcsContainer, "TextController");
        interactiveHint = UnityTool.FindChildGameObject(fcsContainer, "InteractiveHintPanel");
        if(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "MainGameScene_1")
        {
            tutorialInterfaceContainer = UnityTool.FindGameObject("TutorialInterface").transform;
            tutorialInterText = Unity.CustomUITool.UITool.GetUIComponent<Text>(tutorialInterfaceContainer.gameObject, "TutorialText");
            tutorialInterfaceContainer.gameObject.SetActive(false);
        }  
        interactiveHint.SetActive(false);
        index = 0;
        typingSpeed = 0.1f;
        fcsMono = fcsContainer.GetComponent<FontControlSystemMono>();
        LoadSentencesFromJson();
        fcsMono.SetFontControlSystem(this);
        fcsContainer.SetActive(false);
        status = false;

        if (UnityTool.FindGameObject("EnemySpeakWordsImg"))
        {
            enemySpeakWordsImg = UnityTool.FindGameObject("EnemySpeakWordsImg").transform;
            enemySpeakWordsImg.gameObject.SetActive(false);
        }
    }
    public bool GetInteractiveHintState()
    {
        bool state = false;
        if (interactiveHint.activeInHierarchy)
        {
            return state = true;
        }
        return state;
    }
    //啟動字幕功能方法
    public void StartFontControl(string sentencesName,int begin,int end)
    {
        fontControlStart = true;
        status = true;
        fcsContainer.SetActive(true);
        nowActionSentenceType = sentencesName;
        fcsMono.SentencesType = sentencesName;

        index = begin;
        fcsMono.BeginSentence = begin;
        fcsMono.EndSentence = end;
        if (sentencesName != null)
            fcsMono.StartACoroutine();
    }
    public void NextSentence()
    {
        interactiveHint.SetActive(false);
        status = false;
        if(fcsMono.EndSentence > fcsMono.GetSentencesLength())
        {
            fcsMono.EndSentence = fcsMono.GetSentencesLength();
        }
        if (index < fcsMono.EndSentence-1)
        {
            index++;
            textDisplay.text = "";

            fcsMono.StartACoroutine();
        }
        else
        {
            interactiveHint.SetActive(false);
            status = false;
            textDisplay.text = "";
            fontControlEnd = true;
            fontControlStart = false;
            Debug.Log("End Sentence");
        }
    }

    public override void Update()
    {
        if(fontControlStart)
        {
            if (textDisplay.text == fcsMono.GetSentence(index))
            {
                interactiveHint.SetActive(true);
                status = true;
            }
            if (status)
            {
                //fcsMono.StatADelayCoroutine(DelayNextSentence(2f));
                //status = false;
                if (Input.GetKeyDown(KeyCode.Space))
                    NextSentence();
            }
        }
    }
    private void LoadSentencesFromJson()
    {
        string path;
        SentencesData data = new SentencesData();
        path = Path.Combine(Directory.GetCurrentDirectory(), "Assets\\StreamingAssets\\SentencesJson.json");

        if (!File.Exists(path))
        {
            path = Path.Combine(Directory.GetCurrentDirectory(), "3DProject_newScaryHouse_Data\\StreamingAssets\\SentencesJson.json");
        }
        var json = File.ReadAllText(path);
        data = JsonUtility.FromJson<SentencesData>(json);
        fcsMono.SetStringArray(data.MainSentences, data.ActionSentences, data.HintSentences);
    }

    //更改熊熊字幕內容(圖片)
    public void SetEnemySpeakWordsImg(Image img)
    {
        enemySpeakWordsImg.GetComponent<Image>().sprite = img.sprite;
    }

    public void DisplayTutorialText(string sentence)
    {
        tutorialInterText.text = sentence;
        tutorialInterfaceContainer.gameObject.SetActive(true);
    }

    private IEnumerator DelayNextSentence(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        NextSentence();
    }
}
public class SentencesData
{
    [SerializeField]
    private string[] mainSentences;
    public string[] MainSentences
    {
        get { return mainSentences; }
    }

    [SerializeField]
    private string[] actionSentences;
    public string[] ActionSentences
    {
        get { return actionSentences; }
    }

    //提示玩家要做什麼的句子
    [SerializeField]
    private string[] hintSentences;
    public string[] HintSentences
    {
        get { return hintSentences; }
    }
}
