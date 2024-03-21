using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Parent of EnemyState.
/// </summary>
public class IEnemyState
{
    /// <summary>
    /// Enemy model object.
    /// </summary>
    protected GameObject enemyObj;
    public GameObject EnemyObj
    {
        get { return enemyObj; }
    }

    /// <summary>
    /// Enemy state controller.
    /// </summary>
    protected EnemyControlSystem conrtoller = null;

    protected MainGame mainGame = null;

    /// <summary>
    /// Animator controller of enemy.
    /// </summary>
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
