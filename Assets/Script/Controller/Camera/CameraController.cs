using System;
using Controller;
using Entity.Cameras;
using Script.Controller.Input;
using UnityEngine;

namespace Script.Controller.Camera
{
    public class CameraController : RootController
    {
        //Private-props
        private float _fixedPosition;

        //Awake
        private void Start()
        {
            FixPos();
            SpawnCamera.Triggered += HandleChangeCamera;
            InputController.OnStop += HandleUpdateCamera;
        }

        //Handlers
        private void HandleChangeCamera(GameObject spawn)
        {
            Debug.Log(spawn.tag);
            if (transform.position == GameObject.FindGameObjectWithTag("CC" + spawn.tag).transform.position) return;
            transform.position = GameObject.FindGameObjectWithTag("CC" + spawn.tag).transform.position;
            transform.rotation = GameObject.FindGameObjectWithTag("CC" + spawn.tag).transform.rotation;
            Debug.Log(spawn.tag);
        }

        private void HandleUpdateCamera()
        {
            FixPos();
        }

        //Private-methods
        private void FixPos()
        {
            if (Math.Abs(_fixedPosition - this.transform.eulerAngles.y) > 0)
            {
                _fixedPosition = this.transform.eulerAngles.y;
            }
        }

        //Getter & Setter
        public float GetFixedPosition()
        {
            return this._fixedPosition;
        }

        public void SetFixedPosition(float position)
        {
            this._fixedPosition = position;
        }
    }
}