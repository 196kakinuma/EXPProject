using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Objects;
namespace Games.DCG
{
    public class DCGAnswerButton : MonoBehaviour, IVRObject
    {
        [SerializeField]
        DCGMaster master;
        public void ClickReceive ()
        {
            master.Answer ();
        }

        public void HoldReceive ( Vector3 pos )
        {

        }
    }
}