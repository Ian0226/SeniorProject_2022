using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObjBase : IGameSystem
{
    public InteractableObjBase(MainGame mGame) : base(mGame) { }
    public virtual void Interactive(string objName) { }
}
