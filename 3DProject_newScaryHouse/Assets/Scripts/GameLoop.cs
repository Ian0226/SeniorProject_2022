using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLoop : MonoBehaviour
{
    SceneStateController sceneStateController = new SceneStateController();
    void Awake()
    {
        GameObject.DontDestroyOnLoad(this.gameObject);
    }
    void Start()
    {
        sceneStateController.SetState(new StartScene(sceneStateController), "");
    }

    // Update is called once per frame
    void Update()
    {
        sceneStateController.StateUpdate();
    }
}
