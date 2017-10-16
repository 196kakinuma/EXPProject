using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
namespace Games.GameSystem
{
    public class StartButtonHandler : NetworkBehaviour
    {
        [SerializeField]
        StartButton button;
        Vector3 initPos;

        [ClientCallback]
        void Start ()
        {
            initPos = gameObject.transform.position;

        }

        #region DOWN
        [Command]
        public void CmdClickStartButton ( bool tutorial )
        {
            RpcClickButton (tutorial);
        }

        [ClientRpc]
        void RpcClickButton ( bool tutorial )
        {
            button.SetEnabled (false);
            var c = StartCoroutine (SlideOut ());
            if ( Networks.NetworkInitializer.Instance.cameraType == CameraType.VR && Networks.NetworkInitializer.Instance.playerType == PlayerType.HOST )
            {
                if ( !tutorial )
                    StartCoroutine (GameMaster.Instance.StartGame (c));
                else
                    StartCoroutine (GameMaster.Instance.TutorialStart (c));
            }
        }

        IEnumerator SlideOut ()
        {
            Debug.Log ("coroutine");
            while ( gameObject.transform.position.y > -1 )
            {
                gameObject.transform.position = new Vector3 (transform.position.x, transform.position.y - 0.01f, transform.position.z);
                yield return new WaitForEndOfFrame ();
            }
        }
        #endregion

        #region UP
        [Command]
        public void CmdResetStartButton ()
        {
            RpcResetStartButton ();
        }

        [ClientRpc]
        void RpcResetStartButton ()
        {
            transform.position = initPos;
            button.SetEnabled (true);
        }
        #endregion

    }
}