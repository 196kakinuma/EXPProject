using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace Games.WordPushGame
{
    public class WPGButtonNetworker : NetworkBehaviour
    {
        WPGWordButton button;

        [Command]
        public void CmdInitializer ( string word )
        {

        }

        [ClientRpc]
        void RpcInitText ( string word )
        {

        }


    }
}