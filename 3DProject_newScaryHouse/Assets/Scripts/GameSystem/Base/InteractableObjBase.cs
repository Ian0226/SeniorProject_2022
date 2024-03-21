using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Parent of can interactive objects.Provide a Interactive method and mainGame object(contain all game system).
/// </summary>
public class InteractableObjBase : IGameSystem
{
    public InteractableObjBase(MainGame mGame) : base(mGame) { }
    public virtual void Interactive(string objName) { }
}
