using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Games.GameSystem;
using IkLibrary.Unity;
using Objects;
using System.Linq;
using System;

namespace Games.WordPushGame
{
    public class WPGMaster : SingletonMonoBehaviour<WPGMaster>, IKeyLockGameMaster
    {

        [SerializeField]
        WPGNetworkTransform netTransform;

        public WPGWordButton[] wpgWordButtons;
        public WPGWordButton[] currentWpgButton;
        public WPGAnswerButton answerButton;
        public WPGResetButton resetButton;
        [HideInInspector]
        public WPGCalender calenderObj;


        //問題系
        [SerializeField]
        WPGQuestion question;
        List<string> questionList;
        [SerializeField]
        WPGAnswer answer;
        List<int> answerList;
        [SerializeField]
        WPGHint calender;
        int month = 1;
        int day = 1;

        int randNum = 1;


        //答えを格納する
        List<int> clientAnswerList;
        /// <summary>
        /// 正解した後の不要な弄りをカットする為に使用
        /// </summary>
        bool operationAuthority = false;

        Door currentDoor;


        // Use this for initialization
        void Start ()
        {
            if ( Networks.NetworkInitializer.Instance.cameraType != CameraType.VR ) return;


            //ゲーム開始前に入力が入った場合のエラーを排除
            clientAnswerList = new List<int> ();



        }

        public void SetOperationAuthority ( bool b )
        {
            operationAuthority = b;
        }
        /// <summary>
        /// ゲーム開始時に呼ばれる
        /// </summary>
        public void Prepare ()
        {
            ResetAll ();
            netTransform.CmdSetActive (false);

        }

        /// <summary>
        /// ゲームをデータから読み込み問題を作成、準備
        /// </summary>
        /// <returns></returns>
        public IEnumerator Initialize ( Door d )
        {
            Debug.Log ("WPG Question init!!!!!!!!!!");
            netTransform.CmdSetActive (true);
            //準備前でもボタンなどは前後できるため.
            ResetAll ();

            //ランダムを生成
            randNum = UnityEngine.Random.Range (0, answer.sheets[0].list.Count);

            //問題と正解を読み込む
            InitializeQuestion ();

            InitializeAnswer ();

            InitializeHint ();
            InitializeButtons ();

            currentDoor = d;
            PrepareMove ();


            yield return true;
        }

        public void Clear ()
        {
            currentDoor = null;
            ResetAll ();
            netTransform.CmdSetActive (false);

        }
        #region INIT
        private void InitializeQuestion ()
        {
            questionList = new List<string> ();
            for ( int i = 0; i < question.sheets[0].list.Count; i++ )
            {
                //問題
                switch ( 1 )
                {
                    case 0:
                        questionList.Add (question.sheets[0].list[i].one);
                        break;
                    case 1:
                        questionList.Add (question.sheets[0].list[i].two);
                        break;
                }


            }
        }

        private void InitializeAnswer ()
        {
            answerList = new List<int> ();
            answerList.Add (answer.sheets[0].list[randNum].answer1);
            answerList.Add (answer.sheets[0].list[randNum].answer2);
            answerList.Add (answer.sheets[0].list[randNum].answer3);
            answerList.Add (answer.sheets[0].list[randNum].answer4);
            answerList.Add (answer.sheets[0].list[randNum].answer5);
            answerList.Add (answer.sheets[0].list[randNum].answer6);
        }

        private void InitializeHint ()
        {
            month = calender.sheets[0].list[randNum].Month;
            day = calender.sheets[0].list[randNum].Day;
            netTransform.CmdSetCalender (month, day);
        }
        private void InitializeButtons ()
        {
            var array = GetRandomIntArrayFromButtonLength ();
            currentWpgButton = new WPGWordButton[wpgWordButtons.Length];
            //array = new int[5] { 0, 1, 2, 3, 4 }; デバッグ用
            for ( int i = 0; i < wpgWordButtons.Length; i++ )
            {
                wpgWordButtons[array[i]].InitializeButtonInfo (questionList[i], i);
                netTransform.CmdSetWord (wpgWordButtons[array[i]].buttonNum, wpgWordButtons[array[i]].word);
                currentWpgButton[i] = wpgWordButtons[array[i]];
            }
        }


        private int[] GetRandomIntArrayFromButtonLength ()
        {
            int[] array = new int[wpgWordButtons.Length];
            for ( int i = 0; i < array.Length; i++ )
                array[i] = i;

            return array.OrderBy (i => Guid.NewGuid ()).ToArray ();
        }
        #endregion

        /// <summary>
        /// すべてのボタンを元の位置に戻す。
        /// 押していない状態にする
        /// </summary>
        public void ResetAll ()
        {
            for ( int i = 0; i < wpgWordButtons.Length; i++ )
            {
                netTransform.CmdPullMove (i);
            }
            clientAnswerList.Clear ();

        }

        /// <summary>
        /// 現在の状況で回答する
        /// </summary>
        public void Answer ()
        {
            if ( !operationAuthority ) return;

            bool correction = true;
            if ( GameSettings.Instance.debug ) correction = true;
            else if ( clientAnswerList.Count != answerList.Count ) correction = false;

            if ( correction )
            {
                for ( int i = 0; i < clientAnswerList.Count; i++ )
                {
                    if ( clientAnswerList[i] != answerList[i] )
                    {
                        correction = false;
                        break;
                    }
                }
            }

            if ( correction )
            {
                Debug.Log ("answer is correct!!");
                currentDoor.KeyLock = true;
                ExitRoom ();
            }
            else
            {
                ResetAll ();
                Debug.Log ("missed");
            }

        }

        /// <summary>
        /// クライアントのボタン入力をここで受け取り、リストに保持する
        /// </summary>
        public void ReceiveUserResponse ( int i )
        {
            if ( !operationAuthority ) return;
            if ( clientAnswerList.Contains (i) ) return;
            clientAnswerList.Add (i);
            netTransform.CmdPushMove (i);

        }

        #region MOVING
        [SerializeField]
        float moveDistance = 2;
        public void PrepareMove ()
        {
            netTransform.CmdPrepareMove (currentDoor.keyLockGamePosition.position, currentDoor.keyLockGamePosition.forward);
        }

        public void NtPrepareMove ( Vector3 pos, Vector3 forward )
        {
            pos = new Vector3 (pos.x, pos.y - moveDistance, pos.z);
            this.transform.position = pos;
            this.transform.forward = forward;
        }

        public void AppearRoom ()
        {
            netTransform.CmdAppearRoom ();
        }

        public void NtAppearRoom ()
        {
            Debug.Log ("appear");

            //表示する

            //アニメーション
            var pos = transform.position;
            this.transform.position = new Vector3 (pos.x, pos.y + moveDistance, pos.z);
            SetOperationAuthority (true);

        }

        public void ExitRoom ()
        {
            currentDoor.SetButtonActive (true);
            currentDoor.SetButtonWord (false);
            netTransform.CmdExitRoom ();
            //表示を隠す
            Clear ();

        }

        public void NtExitRoom ()
        {
            Debug.Log ("exit");
            SetOperationAuthority (false);

            //あにめーしょん
            var pos = transform.position;
            transform.position = new Vector3 (pos.x, pos.y - moveDistance, pos.z);

        }
        #endregion


    }

}