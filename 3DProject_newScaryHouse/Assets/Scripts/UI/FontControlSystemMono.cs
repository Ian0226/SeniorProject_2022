using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FontControlSystemMono : MonoBehaviour
{
    private FontControlSystem fontControlSystem = null;
    private int beginSentence = 0;
    public int BeginSentence
    {
        get { return beginSentence; }
        set { beginSentence = value; }
    }
    private int endSentence = 0;
    public int EndSentence
    {
        get { return endSentence; }
        set { endSentence = value; }
    }
    [SerializeField]
    private string[] sentences;//遊戲流程的主要字幕
    public string[] Sentences
    {
        get { return sentences; }
    }
    [SerializeField]
    private string[] actionSentences;//被特定事件觸發的字幕
    public string[] ActionSentences
    {
        get { return actionSentences; }
    }
    [SerializeField]
    private string[] hintPlayerSentences;//提示玩家要做什麼事的字幕
    public string[] HintPlayerSentences
    {
        get { return hintPlayerSentences; }
    }

    private string sentencesType;
    public string SentencesType
    {
        get { return sentencesType; }
        set { sentencesType = value; }
    }
    public void SetFontControlSystem(FontControlSystem fcs)
    {
        fontControlSystem = fcs;
    }
    
    IEnumerator TypeMainSentences()
    {
        foreach (char letter in sentences[fontControlSystem.Index].ToCharArray())
        {
            fontControlSystem.TextDisplay.text += letter;
            yield return new WaitForSeconds(fontControlSystem.TypingSpeed);
        }
    }
    IEnumerator TypeActionSentences()
    {
        foreach (char letter in actionSentences[fontControlSystem.Index].ToCharArray())
        {
            fontControlSystem.TextDisplay.text += letter;
            yield return new WaitForSeconds(fontControlSystem.TypingSpeed);
        }
    }
    IEnumerator TypeHintSentences()
    {
        foreach (char letter in hintPlayerSentences[fontControlSystem.Index].ToCharArray())
        {
            fontControlSystem.TextDisplay.text += letter;
            yield return new WaitForSeconds(fontControlSystem.TypingSpeed);
        }
    }
    public string GetSentence(int index)
    {
        string[] sen = new string[0];
        switch (sentencesType)
        {
            case "sentences":
                sen = sentences;
                break;
            case "actionSentences":
                sen = actionSentences;
                break;
            case "hintPlayerSentences":
                sen = hintPlayerSentences;
                break;
        }
        return sen[index];
    }
    public int GetSentencesLength()
    {
        int i = 0;
        switch (sentencesType)
        {
            case "sentences":
                i = sentences.Length;
                break;
            case "actionSentences":
                i = actionSentences.Length;
                break;
            case "hintPlayerSentences":
                i = hintPlayerSentences.Length;
                break;
        }
        return i;
    }
    public void StartACoroutine()
    {
        switch (sentencesType)
        {
            case "sentences":
                StartCoroutine(TypeMainSentences());
                break;
            case "actionSentences":
                StartCoroutine(TypeActionSentences());
                break;
            case "hintPlayerSentences":
                StartCoroutine(TypeHintSentences());
                break;
        }
        


    }
    public void StatADelayCoroutine(IEnumerator IEnumeAction)
    {
        StartCoroutine(IEnumeAction);
    }

    public void SetStringArray(string[] main,string[] actionSen,string[] hintSen)
    {
        sentences = main;
        actionSentences = actionSen;
        hintPlayerSentences = hintSen;
    }
}
