using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Objects;

namespace Games.DWordPushGame
{
    public class DWPGAnswerButton : MonoBehaviour, IVRObject
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
            DWPGMaster.Instance.Answer ();
        }

        public void HoldReceive ( Vector3 pos )
        {

        }
    }
}