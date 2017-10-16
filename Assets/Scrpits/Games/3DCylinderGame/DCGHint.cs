using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Games.DCG
{
    public class DCGHint : MonoBehaviour
    {
        [SerializeField]
        Material material;

        // Use this for initialization
        void Start ()
        {
            DCGMaster.Instance.hintOj = this;
        }

        public void NtInitializeHint ( Color c )
        {
			Debug.Log ("call init cg hint");
            material.color = c;
        }
    }
}