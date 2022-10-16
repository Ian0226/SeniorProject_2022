using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ISceneState 
{
    private string stateName;
    public string StateName
    {
        get { return stateName; }
        set { stateName = value; }
    }
    protected SceneStateController m_Controller = null;
    public ISceneState(SceneStateController Controller)
    {
        m_Controller = Controller;
    }
    public void StateBegin()
    {

    }
    public void StateEnd()
    {

    }
    public void StateUpdate()
    {

    }
}
