using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The parent of all game systems.
/// </summary>
public class IGameSystem
{
    /// <summary>
    /// �򤶪����O�����C
    /// </summary>
    protected MainGame mainGame = null;
    public IGameSystem(MainGame mGame)
    {
        mainGame = mGame;
    }
    public virtual void Initialize(){ }

    public virtual void Release() { }

    public virtual void Update() { }
}
