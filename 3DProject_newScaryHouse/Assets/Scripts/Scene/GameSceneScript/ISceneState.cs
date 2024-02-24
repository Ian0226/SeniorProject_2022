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
    protected bool sceneChanged;
    protected SceneStateController m_Controller = null;
    public ISceneState(SceneStateController Controller)
    {
        m_Controller = Controller;
    }
    public virtual void StateBegin()
    {

    }
    public virtual void StateEnd()
    {

    }
    public virtual void StateUpdate()
    {

    }
}
