using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Objects;
using IkLibrary.Unity;
using Games.GameSystem;
using System.Linq;



namespace Games.DWordMuchGame
{

    public class DWMGMaster : SingletonMonoBehaviour<DWMGMaster>, IKeyLockGameMaster
    {
        [SerializeField]
        DWMGNetworkTransform netTransform;

        /// <summary>
        /// 普通の並び順のcol 不変
        /// </summary>
        [SerializeField]
        DWMGCol[] cols;

        /// <summary>
        /// 毎ゲーム変更される
        /// </summary>
        DWMGCol[] currentCols;

        //問題系
        [SerializeField]
        WMGQuestion[] question;
        List<List<string>> questionList;
        [SerializeField]
        WMGAnswer answer;
        List<int> answerList;

        [SerializeField]
        public DWMGChair chairobj;


        int randNum = 0;

        bool operationAuthority = false;

        Door currentDoor;
        // Use this for initialization
        void Start ()
        {
            if ( Networks.NetworkInitializer.Instance.cameraType != CameraType.VR ) return;

        }


        public void SetOperationAuthority ( bool b )
        {
            operationAuthority = b;
            foreach ( var col in cols )
            {
                col.OperationAuthority = this.operationAuthority;
            }
        }
        /// <summary>
        /// ゲーム開始時に呼ばれる
        /// </summary>
        public void Prepare ()
        {
            ResetAll ();
            netTransform.CmdSetActive (false);
        }
        public IEnumerator Initialize ( Door d )
        {
            Debug.Log ("WPG Question init!!!!!!!!!!");
            netTransform.CmdSetActive (true);
            //準備前でもボタンなどは前後できるため.
            ResetAll ();

            //FIXED:ME ランダムを生成
            randNum = UnityEngine.Random.Range (0, answer.sheets[0].list.Count);

            //問題と正解を読み込む
            InitializeQuestion ();

            InitializeAnswer ();

            InitializeHint ();

            InitializeCol ();

            currentDoor = d;
            PrepareMove ();


            yield return true;
        }

        #region INIT
        private void InitializeQuestion ()
        {
            questionList = new List<List<string>> ();
            var question = this.question[0];
            for ( int j = 0; j < question.sheets[0].list.Count; j++ )
            {
                var texts = new List<string> ();
                for ( int i = 0; i < question.sheets[0].list.Count; i++ )
                {
                    switch ( j )
                    {
                        case 0:
                            texts.Add (question.sheets[0].list[i].Col1);
                            break;
                        case 1:
                            texts.Add (question.sheets[0].list[i].Col2);
                            break;
                        case 2:
                            texts.Add (question.sheets[0].list[i].Col3);
                            break;
                        case 3:
                            texts.Add (question.sheets[0].list[i].Col4);
                            break;
                        case 4:
                            texts.Add (question.sheets[0].list[i].Col5);
                            break;
                    }

                }
                questionList.Add (texts);
            }
        }

        private void InitializeAnswer ()
        {
            answerList = new List<int> ();
            answerList.Add (answer.sheets[0].list[randNum].Answer1);
            answerList.Add (answer.sheets[0].list[randNum].Answer2);
            answerList.Add (answer.sheets[0].list[randNum].Answer3);
            answerList.Add (answer.sheets[0].list[randNum].Answer4);
            answerList.Add (answer.sheets[0].list[randNum].Answer5);
        }

        private void InitializeHint ()
        {
            switch ( randNum )
            {
                case 0:
                    netTransform.CmdSetChairMatColor (Color.red);
                    break;
                case 1:
                    netTransform.CmdSetChairMatColor (Color.blue);
                    break;
                case 2:
                    netTransform.CmdSetChairMatColor (Color.green);
                    break;
                case 3:
                    netTransform.CmdSetChairMatColor (Color.yellow);
                    break;
                case 4:
                    netTransform.CmdSetChairMatColor (Color.white);
                    break;
            }
        }

        private void InitializeCol ()
        {
            var array = GetRandomIntArrayFromColLength ();
            currentCols = new DWMGCol[cols.Length];
            //array = new int[5] { 0, 1, 2, 3, 4 }; デバッグ用
            for ( int i = 0; i < cols.Length; i++ )
            {
                currentCols[i] = cols[array[i]];
                currentCols[i].Initialize (i, questionList[i]);
            }
        }

        private int[] GetRandomIntArrayFromColLength ()
        {
            int[] array = new int[cols.Length];
            for ( int i = 0; i < array.Length; i++ )
                array[i] = i;

            return array.OrderBy (i => Guid.NewGuid ()).ToArray ();
        }


        #endregion
        /// <summary>
        /// そのゲーム毎に紐づく物をnullにする
        /// </summary>
        public void Clear ()
        {
            currentDoor = null;
            ResetAll ();
            netTransform.CmdSetActive (false);
        }

        public void ResetAll ()
        {

        }

        public void Answer ()
        {
            if ( !operationAuthority ) return;
            bool correction = CheckAnswer ();

            if ( correction || GameSettings.Instance.debug )
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

        private bool CheckAnswer ()
        {
            for ( int i = 0; i < currentCols.Length; i++ )
            {
                if ( !( currentCols[i].GetSelectNumber () == answerList[i] ) )
                    return false;
            }
            return true;
        }

        /// <summary>
        /// 文字を変更するメソッドをRpcにする
        /// </summary>
        public void SetColText ( int colNum, string text )
        {
            //すべてのクライアントがcurrentcolを知っているわけではない
            netTransform.CmdSetText (currentCols[colNum].staticColNum, text);
        }

        public void NtSetColText ( int colNum, string text )
        {
            cols[colNum].NtSetText (text);
        }



        #region MOVING
        [SerializeField]
        float moveDistance = 2;
        /// <summary>
        /// ドアの下にセット
        /// </summary>
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

        /// <summary>
        /// 問題を解くボタン押して,登場させる
        /// </summary>
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
        /// <summary>
        /// 問題が正解したら,画面外に出して非アクティブ化
        /// </summary>
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