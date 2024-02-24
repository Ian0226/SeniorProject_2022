using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IGameSystem
{
    protected MainGame mainGame = null;
    public IGameSystem(MainGame mGame)
    {
        mainGame = mGame;
    }
    public virtual void Initialize(){ }

    public virtual void Release() { }

    public virtual void Update() { }
}
