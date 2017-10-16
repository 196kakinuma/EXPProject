using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Games.DWordMuchGame
{
    public class DWMGChair : MonoBehaviour
    {
        [SerializeField]
        Material chairMat;
        void Start ()
        {
            DWMGMaster.Instance.chairobj = this;
        }

        public void NtSetChairMatColor ( Color matColor )
        {
            chairMat.color = matColor;
        }
    }
}