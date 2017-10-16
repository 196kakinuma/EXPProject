using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace Games.CG
{
    public class CGNetworkTransform : NetworkBehaviour
    {
        [SerializeField]
        CGMaster master;

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
        public void CmdKnobMove ( int i, float y, Color c )
        {
            RpcKnobMove (i, y, c);
        }

        [ClientRpc]
        public void RpcKnobMove ( int i, float y, Color c )
        {
            master.NtKnobMove (i, y, c);
        }

        [Command]
        public void CmdSetHint ( Color c )
        {
            RpcSetHint (c);
        }
        [ClientRpc]
        void RpcSetHint ( Color c )
        {
            Debug.Log ("Set Hint");
            master.hintOj.NtInitializeHint (c);
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