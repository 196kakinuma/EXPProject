using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Objects;
namespace Games.CG
{
    public class CGAnswerButton : MonoBehaviour, IVRObject
    {
        [SerializeField]
        CGMaster master;
        public void ClickReceive ()
        {
            master.Answer ();
        }

        public void HoldReceive ( Vector3 pos )
        {

        }
    }
}