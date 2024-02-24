using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetectEnemyColObj : MonoBehaviour
{
    private List<Transform> enemyStayPointObjs = new List<Transform>();

    public List<Transform> GetEnemyStayPointObjs()
    {
        return enemyStayPointObjs;
    }
    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(enemyStayPointObjs.Count + "  " + other);
        if (other.tag == "EnemyStayPoint_State2")
        {
            enemyStayPointObjs.Add(other.transform);
            //Debug.Log(enemyStayPointObjs.Count);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "EnemyStayPoint_State2")
        {
            enemyStayPointObjs.Remove(other.transform);
            //Debug.Log(enemyStayPointObjs.Count);
        }
    }
}
