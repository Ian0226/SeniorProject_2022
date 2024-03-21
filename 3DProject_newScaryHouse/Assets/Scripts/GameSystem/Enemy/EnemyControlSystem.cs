using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Enemy state controller,handle enemy state initialize and update 
/// and provide enemy state change method SetState(IEnemyState state).
/// </summary>
public class EnemyControlSystem : IGameSystem
{
    private IEnemyState state;
    public IEnemyState State
    {
        get { return state; }
    }
    
    public EnemyControlSystem(MainGame main) : base(main)
    {

    }
    /// <summary>
    /// Set enemy current state.
    /// </summary>
    /// <param name="state">Enemy state 1 or 2</param>
    public void SetState(IEnemyState state)
    {
        this.state = state;
    }
    public override void Initialize(){}
    public override void Update()
    {
        if(state != null)
        {
            state.StateUpdate();
        }
    }

}
