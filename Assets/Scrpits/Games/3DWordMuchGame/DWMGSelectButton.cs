using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Objects;
using UnityEngine.UI;

namespace Games.DWordMuchGame
{
    public enum SELECTBUTTON
    {
        UP,
        DOWN
    }


    public class DWMGSelectButton : MonoBehaviour, IVRObject
    {
        [SerializeField]
        SELECTBUTTON selectButton = SELECTBUTTON.UP;

        [SerializeField]
        DWMGCol col;

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