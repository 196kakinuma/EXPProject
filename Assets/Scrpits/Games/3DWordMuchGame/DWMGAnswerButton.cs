using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Objects;

namespace Games.DWordMuchGame
{

    public class DWMGAnswerButton : MonoBehaviour, IVRObject
    {
        public void ClickReceive ()
        {
            DWMGMaster.Instance.Answer ();
        }
        public void HoldReceive ( Vector3 pos )
        {
        }

    }

}