using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControlSystem : IGameSystem
{
    private IEnemyState state;
    public IEnemyState State
    {
        get { return state; }
    }
    private bool m_RunBegin = false;
    
    public EnemyControlSystem(MainGame main) : base(main)
    {
        Initialize();
    }
    public void SetState(IEnemyState state)
    {
        this.state = state;
    }
    public override void Initialize()
    {
        if(state != null && m_RunBegin == false)
        {
            state.StateInitialize();
            m_RunBegin = true;
        }
    }
    public override void Update()
    {
        if(state != null)
        {
            state.StateUpdate();
        }
    }

}
