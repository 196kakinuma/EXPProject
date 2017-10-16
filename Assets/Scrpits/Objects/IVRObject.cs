using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Objects
{
    public interface IVRObject
    {
        void ClickReceive ();

        void HoldReceive ( Vector3 pos );
    }
}