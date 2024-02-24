using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Item")
        {
            Debug.Log("1");
        }
        else if (other.tag == "Player")
        {
            Debug.Log("2");
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Item")
        {
            Debug.Log("1");
        }
        else if (other.tag == "Player")
        {
            Debug.Log("2");
        }
    }
}
