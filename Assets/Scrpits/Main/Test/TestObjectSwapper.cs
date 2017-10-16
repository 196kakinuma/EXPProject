using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Networks;
using UnityEngine.Networking;

namespace Test
{
    public class TestObjectSwapper : MonoBehaviour
    {

        [SerializeField]
        GameObject[] prefabs;
        // Use this for initialization
        void Start ()
        {
            if ( NetworkInitializer.Instance.cameraType == CameraType.VR )
            {
                foreach ( var o in prefabs )
                {
                    NetworkServer.Spawn (Instantiate (o));
                }

            }
        }

        // Update is called once per frame
        void Update ()
        {

        }
    }
}
