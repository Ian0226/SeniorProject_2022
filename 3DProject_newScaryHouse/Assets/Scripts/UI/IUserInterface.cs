using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IUserInterface
{
    protected GameObject m_RootUI;
    private bool m_bActive;

    public bool IsVisible()
    {
        return m_bActive;
    }
    public virtual void Show()
    {
        m_RootUI.SetActive(true);
        m_bActive = true;
    }
    public virtual void Hide()
    {
        m_RootUI.SetActive(false);
        m_bActive = false;
    }
    public virtual void Initialize()
    {}
    public virtual void Release()
    {}
    public virtual void Update()
    {}
}
