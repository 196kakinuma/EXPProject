using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Objects.Test
{
    public class TestCubeScript : MonoBehaviour, IVRObject
    {
        [SerializeField]
        Material material;

        [SerializeField]
        TestCubeColorScript colorScript;
        // Use this for initialization
        void Start ()
        {

        }

        // Update is called once per frame
        void Update ()
        {

        }

        public void ClickReceive ()
        {
            colorScript.CmdColorChange ();

        }

        public void HoldReceive ( Vector3 pos )
        {

        }

    }
}