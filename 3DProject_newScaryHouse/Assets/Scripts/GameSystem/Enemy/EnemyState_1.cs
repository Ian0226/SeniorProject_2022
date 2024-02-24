using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState_1 : IEnemyState
{
    private List<GameObject> enemyStayPoint = new List<GameObject>();

    private int enemyStayPointNum = 0;
    public EnemyState_1(EnemyControlSystem controller,MainGame main) : base(controller, main)
    {
        StateInitialize();
    }
    public override void StateInitialize()
    {
        enemyStayPoint.AddRange(GameObject.FindGameObjectsWithTag("EnemyStayPoint"));
        string[] strs = new string[enemyStayPoint.Count];
        for(int i = 0; i < strs.Length; i++)
        {
            strs[i] = enemyStayPoint[i].name;
        }
        Array.Sort(strs);
        for(int i = 0; i < strs.Length; i++)
        {
            enemyStayPoint[i] = GameObject.Find(strs[i]);
        }
        enemyObj = GameObject.Find("Enemy");
    }
    public override void StateUpdate()
    {
        EnemyBehaviour();
    }
    public override void EnemyBehaviour()
    {
        EnemyMove();
    }
    public override void EnemyMove()
    {
        enemyObj.transform.position = enemyStayPoint[enemyStayPointNum].transform.position;
    }
    public override void SetEnemyStayPoint()
    {
        if(enemyStayPointNum < enemyStayPoint.Count-1)
            enemyStayPointNum++;
    }
}
