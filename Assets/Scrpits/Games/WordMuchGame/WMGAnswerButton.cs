using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Objects;

namespace Games.WordMuchGame
{

    public class WMGAnswerButton : MonoBehaviour, IVRObject
    {
        public void ClickReceive ()
        {
            WMGMaster.Instance.Answer ();
        }
        public void HoldReceive ( Vector3 pos )
        {
            Debug.Log ("Hold On !!!!!!!!!!!!!!!!" + pos);
        }

    }

}