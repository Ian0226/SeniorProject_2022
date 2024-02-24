using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace PWCameraShake
{
    public class PlayerWalkCameraShake : MonoBehaviour
    {
        //
        private GameObject CameraContainer;

        //
        private Vector3 DefaultPosInfluence = new Vector3(0.1f, 0.1f, 0.1f);

        //
        private Vector3 DefaultRotInfluence = new Vector3(1, 1, 1);

        //
        private Vector3 DefaultRotLimit = new Vector3(10, 10, 10);

        //
        private Vector3 DefaultPosLimit = new Vector3(10, 10, 10);

        //
        private bool ShakeState = false;

        //
        private Action Handler;

        private void Awake()
        {
            CameraContainer = this.gameObject;
        }
        private void Update()
        {
            if (ShakeState)
            {
                
            }
            else
            {

            }
        }

        private void ShakeHandler()
        {
            if(CameraContainer.transform.position.y < -DefaultPosLimit.y)
            {

            }
            else
            {

            }

            if(CameraContainer.transform.position.y > DefaultPosLimit.y)
            {

            }
            else
            {

            }
        }
        public void StartShake()
        {
            ShakeState = true;
        }
        public void StopShake()
        {
            ShakeState = false;
        }
    }
}

