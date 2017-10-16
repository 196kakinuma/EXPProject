using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Objects;
using UnityEngine.UI;

namespace Games.GameSystem
{
    public class DoorLockButton : MonoBehaviour, IVRObject
    {
        [SerializeField]
        Door door;

        [SerializeField]
        Text text;

        [SerializeField]
        DoorNetwork dNetwork;


        bool canManipurate = true;
        public bool CanManipurate
        {
            get { return canManipurate; }
            set { canManipurate = value; }
        }

        public void ClickReceive ()
        {
            if ( !CanManipurate ) return;
            door.ClickDoorLockButton ();
        }

        public void HoldReceive ( Vector3 pos )
        {

        }

    }
}