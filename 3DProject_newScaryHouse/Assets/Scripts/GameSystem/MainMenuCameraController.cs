using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuCameraController
{
    private Transform camera;
    public MainMenuCameraController() : base()
    {
        Initialize();
    }
    public void Initialize()
    {
        camera = Unity.CustomTool.UnityTool.FindGameObject("Main Camera").transform;
    }
    public void Update()
    {
        RotateCamera();
    }
    public void RotateCamera()
    {
        camera.rotation = Quaternion.Euler(0, camera.transform.rotation.eulerAngles.y + 0.01f, 0);
    }
}
