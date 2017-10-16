using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Device
{
    public class CameraSetter : MonoBehaviour
    {
        Camera camera;
        // Use this for initialization
        void Start ()
        {
            camera = GetComponent<Camera> ();
            switch ( Networks.NetworkInitializer.Instance.cameraType )
            {
                case CameraType.VR:
                    break;
                case CameraType.MR:
                    camera.clearFlags = CameraClearFlags.SolidColor;
                    camera.backgroundColor = Color.black;
                    break;
            }
        }

        // Update is called once per frame
        void Update ()
        {

        }
    }
}