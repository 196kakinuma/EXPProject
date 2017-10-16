using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Objects;

namespace Games.DWordPushGame
{

    public class DWPGResetButton : MonoBehaviour, IVRObject
    {
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
            DWPGMaster.Instance.ResetAll ();
        }

        public void HoldReceive ( Vector3 pos )
        {

        }
    }
}