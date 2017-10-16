using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Networks;


namespace Device
{
    public class ViceTrackerDevice : NetworkBehaviour
    {

        [SerializeField]
        GameObject tracker;
        [SyncVar]
        public Vector3 pos;
        [SyncVar]
        public Quaternion rot;
        // Use this for initialization
        void Start ()
        {

            if ( NetworkInitializer.Instance.cameraType == CameraType.VR )
                tracker = GameObject.FindGameObjectWithTag ("tracker");
        }

        [ServerCallback]
        // Update is called once per frame
        void Update ()
        {
            //FIX ME:hololensを増やす場合又はVRTrackerが複数人、Trackerが複数になる場合はここを改善
            // if ( !hasAuthority ) return;

            if ( tracker == null )
            {
                tracker = GameObject.FindGameObjectWithTag ("tracker");
            }
            else
            {

                CmdSendTransformData (tracker.transform.position, tracker.transform.rotation);
            }
        }

        [Command]
        void CmdSendTransformData ( Vector3 pos, Quaternion rot )
        {
            this.pos = pos;
            this.rot = rot;

        }
    }
}