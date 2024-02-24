using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEventControlSystemBase : IGameSystem
{
    protected Queue<Action> gameEvents = new Queue<Action>();

    protected Action handler;
    public Action Handler
    {
        get { return handler; }
        set { handler = value; }
    }

    protected int tutorialInputCount = 0;
    public int TutorialInputCount
    {
        get { return tutorialInputCount; }
        set { tutorialInputCount = value; }
    }

    protected Action playerMissionHandler;
    public Action PlayerMissionHandler
    {
        get { return playerMissionHandler; }
        set { playerMissionHandler = value; }
    }

    protected Action playerTutorialHandler;
    public Action PlayerTutorialHandler
    {
        get { return playerTutorialHandler; }
        set { playerTutorialHandler = value; }
    }

    public GameEventControlSystemBase(MainGame main) : base(main){}

    public virtual void SetActionState(Action action) { }
}
