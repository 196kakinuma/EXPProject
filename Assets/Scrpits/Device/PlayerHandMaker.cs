using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


namespace Device
{
    public class PlayerHandMaker : NetworkBehaviour
    {
        [SerializeField]
        GameObject rightHand;
        [SerializeField]
        GameObject leftHand;


        // Use this for initialization
        [ClientCallback]
        void Start ()
        {
            if ( Networks.NetworkInitializer.Instance.cameraType == CameraType.VR )
            {
                if ( hasAuthority )
                {
                    Debug.Log (hasAuthority);
                    CmdCreateHands (this.gameObject);
                }
            }


        }

        [Command]
        private void CmdCreateHands ( GameObject obj )
        {
            Debug.Log ("create hand");
            NetworkServer.SpawnWithClientAuthority (Instantiate (rightHand), obj);
            NetworkServer.SpawnWithClientAuthority (Instantiate (leftHand), obj);
        }


    }
}