using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IEnemyState
{
    protected GameObject enemyObj;
    public GameObject EnemyObj
    {
        get { return enemyObj; }
    }

    protected EnemyControlSystem conrtoller = null;

    protected MainGame mainGame = null;

    protected Animator enemyAni;
    public Animator EnemyAni
    {
        get { return enemyAni; }
    }
    public IEnemyState(EnemyControlSystem controller, MainGame main)
    {
        this.conrtoller = controller;
        this.mainGame = main;
    }

    public virtual void StateInitialize() { }
    public virtual void StateUpdate() { }
    public virtual void EnemyBehaviour(){}
    public virtual void EnemyMove(){}
    public virtual void SetEnemyStayPoint(){}
}
