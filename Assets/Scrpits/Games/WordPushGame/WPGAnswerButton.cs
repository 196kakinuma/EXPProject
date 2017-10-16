using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Objects;

namespace Games.WordPushGame
{
    public class WPGAnswerButton : MonoBehaviour, IVRObject
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
            WPGMaster.Instance.Answer ();
        }

        public void HoldReceive ( Vector3 pos )
        {

        }
    }
}