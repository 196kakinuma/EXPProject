﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

namespace Games.WordPushGame
{
    public class WPGCalender : MonoBehaviour
    {
        [SerializeField]
        Text text;

        void Start ()
        {
            WPGMaster.Instance.calenderObj = this;
            Debug.Log ("Calender Init");
        }
        public void SetCalender ( int month, int day )
        {
            text.text = month.ToString () + "/" + day.ToString ();
        }

    }
}