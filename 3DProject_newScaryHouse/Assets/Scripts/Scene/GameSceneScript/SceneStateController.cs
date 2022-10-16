using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneStateController
{
    private ISceneState state;
    private bool m_RunBegin = false;

    public SceneStateController()
    {

    }
    public void SetState(ISceneState State, string LoadSceneName)
    {
        m_RunBegin = false;
        LoadScene(LoadSceneName);
        if (state != null)
        {
            state.StateEnd();
        }
        state = State;
    }
    public void StateUpdate()
    {
        if (Application.isLoadingLevel)
        {
            return;
        }
        if (state != null && m_RunBegin == false)
        {
            state.StateBegin();
            m_RunBegin = true;
        }
        if (state != null)
        {
            state.StateUpdate();
        }
    }
    public void LoadScene(string LoadSceneName)
    {
        if (LoadSceneName == null || LoadSceneName.Length == 0)
        {
            return;
        }
        Application.LoadLevel(LoadSceneName);
    }
}
