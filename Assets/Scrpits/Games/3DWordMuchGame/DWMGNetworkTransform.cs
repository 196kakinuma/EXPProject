using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

using Objects;
namespace Games.DWordMuchGame
{
    public class DWMGNetworkTransform : NetworkBehaviour
    {
        [SerializeField]
        DWMGMaster master;


        [Command]
        public void CmdSetChairMatColor ( Color color )
        {
            RpcSetChairMatColor (color);
        }
        [ClientRpc]
        void RpcSetChairMatColor ( Color color )
        {
            master.chairobj.NtSetChairMatColor (color);
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

        [Command]
        public void CmdSetText ( int colNum, string text )
        {
            RpcSetText (colNum, text);
        }

        [ClientRpc]
        void RpcSetText ( int colNum, string text )
        {
            master.NtSetColText (colNum, text);
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