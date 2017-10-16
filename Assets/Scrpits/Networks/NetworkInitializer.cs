using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;
using IkLibrary.Unity;

public enum PlayerType
{
    HOST,
    CLIENT
}
public enum CameraType
{
    VR,
    MR
}


namespace Networks
{

    public class NetworkInitializer : SingletonMonoBehaviour<NetworkInitializer>
    {
        [SerializeField]
        NetworkManager netManager;

        [SerializeField]
        public PlayerType playerType;

        [SerializeField]
        public CameraType cameraType;

        // Use this for initialization
        void Start ()
        {

            if ( playerType == PlayerType.HOST )
            {
                netManager.StartHost ();
            }
            else if ( playerType == PlayerType.CLIENT )
            {
                netManager.StartClient ();
            }
            else
            {
                DebugCommon.Assert (false, "Error: playerType");
            }
        }

        // Update is called once per frame
        void Update ()
        {

        }
    }
}