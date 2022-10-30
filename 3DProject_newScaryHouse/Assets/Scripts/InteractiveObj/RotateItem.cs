using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateItem : MonoBehaviour
{
    private Transform modelTransform;
    private bool isRotate;
    private Vector3 startPoint;
    private Vector3 startAngel;
    private float rotateScale = 1f;

    private void Awake()
    {
        modelTransform = this.transform;
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isRotate)
        {
            isRotate = true;
            startPoint = Input.mousePosition;
            startAngel = modelTransform.eulerAngles;
        }
        if (Input.GetMouseButtonUp(0))
        {
            isRotate = false;
        }
        if (isRotate)
        {
            var currentPoint = Input.mousePosition;
            var x = startPoint.x - currentPoint.x;
            var y = startPoint.y - currentPoint.y;
            var z = startPoint.z - currentPoint.z;

            modelTransform.eulerAngles = startAngel + new Vector3(z * rotateScale, x * rotateScale, y * rotateScale);
        }
    }
}
