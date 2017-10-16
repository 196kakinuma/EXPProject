using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
namespace Games.GameSystem
{
    public class FinishButtonHandler : NetworkBehaviour
    {
        [SerializeField]
        FinishButton button;
        Vector3 initPos;


        #region DOWN
        [Command]
        public void CmdClickFinishButton ()
        {
            RpcClickButton ();
        }

        [ClientRpc]
        void RpcClickButton ()
        {
            button.SetEnabled (false);
            gameObject.SetActive (false);
            if ( Networks.NetworkInitializer.Instance.cameraType == CameraType.VR && Networks.NetworkInitializer.Instance.playerType == PlayerType.HOST )
            {
                GameMaster.Instance.FinishGame ();
            }
        }

        #endregion

        #region UP
        [Command]
        public void CmdAppearFinishButton ()
        {
            RpcAppearFinishButton ();
        }

        [ClientRpc]
        void RpcAppearFinishButton ()
        {
            button.SetEnabled (true);
            gameObject.SetActive (true);
        }
        [Command]
        public void CmdDisappearButton ()
        {
            this.gameObject.SetActive (false);
        }

        [ClientRpc]
        void RpcDisappearButton ()
        {
            this.gameObject.SetActive (true);
        }
        #endregion

    }
}