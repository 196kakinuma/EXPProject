using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Objects;
using UnityEngine.UI;

namespace Games.DWordPushGame
{
    public class DWPGWordButton : MonoBehaviour, IVRObject
    {
        [SerializeField]
        Text text;

        public int buttonNum;
        public string word;
        public Vector3 initPos;


        void Start ()
        {
            initPos = transform.localPosition;
        }

        /// <summary>
        /// ゲームを行うVRクライアントでのみ呼ばれる
        /// 情報を保持するために
        /// </summary>
        /// <param name="text"></param>
        /// <param name="num"></param>
        public void InitializeButtonInfo ( string text, int num )
        {
            buttonNum = num;
            this.word = text;
            initPos = transform.localPosition;

        }

        /// <summary>
        /// Rpcによりサーバから全クライアントにコールされる
        /// </summary>
        /// <param name="word"></param>
        public void SetWord ( string word )
        {
            Debug.Log ("word set");
            this.text.text = word;
        }


        public void ClickReceive ()
        {
            Debug.Log ("button click");
            DWPGMaster.Instance.ReceiveUserResponse (buttonNum);
        }

        public void HoldReceive ( Vector3 pos )
        {

        }

        public void PushMove ()
        {
            transform.localPosition = new Vector3 (transform.localPosition.x, transform.localPosition.y, transform.localPosition.z - 0.1f);
        }

        public void ResetPosition ()
        {
            transform.localPosition = initPos;
        }

        public void SelectEffect ()
        {

        }
    }
}