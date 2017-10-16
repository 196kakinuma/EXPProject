using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Games.CG
{
    public class CGHint : MonoBehaviour
    {
        [SerializeField]
        Material material;

        // Use this for initialization
        void Start ()
        {
            CGMaster.Instance.hintOj = this;
        }

        public void NtInitializeHint ( Color c )
        {
			Debug.Log ("set hint color ");
            material.color = c;
        }
    }
}