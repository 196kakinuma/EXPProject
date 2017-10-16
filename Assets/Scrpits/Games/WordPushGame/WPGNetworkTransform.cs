using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Objects;

namespace Games.WordPushGame
{
    public class WPGNetworkTransform : NetworkBehaviour
    {
        [SerializeField]
        WPGMaster master;


        #region Button
        [Command]
        public void CmdPushMove ( int i )
        {
            Debug.Log ("cmd ");
            RpcPushMove (i);
        }

        [ClientRpc]
        void RpcPushMove ( int i )
        {
            Debug.Log ("Rpc ");
            master.currentWpgButton[i].PushMove ();

        }

        [Command]
        public void CmdPullMove ( int i )
        {
            RpcPullMove (i);
        }

        [ClientRpc]
        void RpcPullMove ( int i )
        {
            master.wpgWordButtons[i].ResetPosition ();

        }

        [Command]
        public void CmdSetWord ( int i, string text )
        {
            RpcSetWord (i, text);
        }

        [ClientRpc]
        void RpcSetWord ( int i, string text )
        {
            master.currentWpgButton[i].SetWord (text);
        }
        #endregion
        [Command]
        public void CmdSetCalender ( int month, int day )
        {
            RpcSetCalender (month, day);
        }

        [ClientRpc]
        void RpcSetCalender ( int month, int day )
        {
            master.calenderObj.SetCalender (month, day);
        }

        [Command]
        public void CmdSetActive ( bool b )
        {
            RpcSetActive (b);
        }

        [ClientRpc]
        void RpcSetActive ( bool b )
        {
            gameObject.SetActive (b);
        }
        #region Move
        [Command]
        public void CmdPrepareMove ( Vector3 pos, Vector3 forward )
        {
            RpcPrepareMove (pos, forward);
        }
        [ClientRpc]
        void RpcPrepareMove ( Vector3 pos, Vector3 forward )
        {
            master.NtPrepareMove (pos, forward);
        }

        [Command]
        public void CmdAppearRoom ()
        {
            RpcAppearRoom ();
        }

        [ClientRpc]
        void RpcAppearRoom ()
        {
            master.NtAppearRoom ();
        }

        [Command]
        public void CmdExitRoom ()
        {
            RpcExitRoom ();
        }
        [ClientRpc]
        void RpcExitRoom ()
        {
            master.NtExitRoom ();
        }
        #endregion
    }
}