using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Objects;
using UnityEngine.UI;

namespace Games.WordMuchGame
{
    public enum SELECTBUTTON
    {
        UP,
        DOWN
    }


    public class WMGSelectButton : MonoBehaviour, IVRObject
    {
        [SerializeField]
        SELECTBUTTON selectButton = SELECTBUTTON.UP;

        [SerializeField]
        WMGCol col;

        public void ClickReceive ()
        {
            if ( selectButton == SELECTBUTTON.UP ) col.TextUp ();
            else col.TextDown ();
        }

        public void HoldReceive ( Vector3 pos )
        {

        }
    }
}