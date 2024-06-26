using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLoop : MonoBehaviour
{
    SceneStateController sceneStateController = new SceneStateController();

    //Use to change scene.
    public static int testInt = 0;
    public int testIntInspector;
    void Awake()
    {
        GameObject.DontDestroyOnLoad(this.gameObject);
    }
    void Start()
    {
        sceneStateController.SetState(new StartState(sceneStateController), "");
        Physics.autoSyncTransforms = true;
    }

    void Update()
    {
        testIntInspector = testInt;
        //testInt = testIntInspector;
        //Debug.Log(testInt);
        sceneStateController.StateUpdate();
    }
}
