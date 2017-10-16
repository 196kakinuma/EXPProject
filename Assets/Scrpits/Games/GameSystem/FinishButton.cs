using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Objects;
namespace Games.GameSystem
{
    public class FinishButton : MonoBehaviour, IVRObject
    {
        [SerializeField]
        FinishButtonHandler handler;

        bool enabled = true;

        public void SetEnabled ( bool b )
        {
            enabled = b;
        }
        public void ClickReceive ()
        {
            if ( !enabled ) return;

            handler.CmdClickFinishButton ();
        }

        public void HoldReceive ( Vector3 pos )
        {

        }


    }
}