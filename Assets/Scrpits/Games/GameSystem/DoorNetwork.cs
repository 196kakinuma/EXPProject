using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

namespace Games.GameSystem
{
    public class DoorNetwork : NetworkBehaviour
    {
        [SerializeField]
        Door door;


        [Command]
        public void CmdSetWindowsImageActive ( Enemy.EnemyType type )
        {
            RpcSetWindowImageActive (type);
        }

        [ClientRpc]
        void RpcSetWindowImageActive ( Enemy.EnemyType type )
        {
            door.NtSetImageActive (type);
        }

        [Command]
        public void CmdSetWindowImageNonActive ( Enemy.EnemyType type )
        {
            RpcSetWindowImageNonActive (type);
        }

        [ClientRpc]
        void RpcSetWindowImageNonActive ( Enemy.EnemyType type )
        {
            door.NtSetImageNonActive (type);
        }

        [Command]
        public void CmdSetLockText ( bool b )
        {

            RpcSetLockText (b);
        }

        [ClientRpc]
        void RpcSetLockText ( bool b )
        {
            door.NtSetLockText (b);
        }

        [Command]
        public void CmdSetButtonText ( bool isLock )
        {
            RpcSetButtonText (isLock);
        }

        [ClientRpc]
        void RpcSetButtonText ( bool isLock )
        {
            door.NtSetButtonText (isLock);
        }
    }
}