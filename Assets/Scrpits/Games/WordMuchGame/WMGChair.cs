using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Games.WordMuchGame
{
    public class WMGChair : MonoBehaviour
    {
        [SerializeField]
        Material chairMat;
        void Start ()
        {
            WMGMaster.Instance.chairobj = this;
        }

        public void NtSetChairMatColor ( Color matColor )
        {
            chairMat.color = matColor;
        }
    }
}