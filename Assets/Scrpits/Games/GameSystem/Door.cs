using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
namespace Games.GameSystem
{


    public class Door : MonoBehaviour
    {
        [SerializeField]
        GameObject doorWood;

        [SerializeField]
        Image windowImage;

        [SerializeField]
        DoorNetwork network;

        [SerializeField]
        Text lockText;

        [SerializeField]
        Text buttonText;

        [SerializeField]
        DoorLockButton button;

        DoorManager manager;

        /// <summary>
        /// 敵が去った後の空白時間
        /// </summary>
        float blankTime = 0;
        public float BlankTime
        {
            get { return blankTime; }
            set { blankTime = value; }
        }

        public Transform keyLockGamePosition;

        int doorNum;
        public int DoorNum
        {
            get { return doorNum; }
            private set { doorNum = value; }
        }
        bool visit;
        public bool Visit
        {
            get { return visit; }
            private set { visit = value; }
        }

        Enemy.Enemy enemy;
        public Enemy.Enemy VisitEnemy
        {
            get { return enemy; }
            set { enemy = value; }
        }

        bool keyLock = false;
        public bool KeyLock
        {
            get { return keyLock; }
            set
            {
                network.CmdSetLockText (value);
                if ( value == true && VisitEnemy != null ) //扉をロックするときで、エネミーがいるとき
                    VisitEnemy.SetLockDoorStayTime ();
                keyLock = value;
            }
        }

        /// <summary>
        /// ゲーム開始前に毎回呼ばれる
        /// </summary>
        /// <param name="doorNum"></param>
        public void Initialize ( int doorNum, DoorManager man )
        {
            manager = man;
            DoorNum = doorNum;
            VisitEnemy = null;
            Visit = false;
            keyLock = false;
        }

        /// <summary>
        /// ロックされているの表示
        /// </summary>
        public void NtSetLockText ( bool b )
        {
            if ( b )
            {
                lockText.text = "LOCKED";
                lockText.color = Color.red;
            }
            else
            {
                lockText.text = "OPEN";
                lockText.color = Color.green;
            }
        }

        public void NtSetButtonText ( bool isLock )
        {
            if ( isLock )
            {
                buttonText.text = "OPEN";
            }
            else
            {
                buttonText.text = "LOCK";
            }
        }

        public void ClickDoorLockButton ()
        {
            if ( KeyLock )
            {
                KeyLock = false;
                network.CmdSetButtonText (false);
                Debug.Log ("key lockOut time:" + BlankTime);
            }
            else //問題を出す
            {
                if ( manager.AppearKeyLockGame (this) )
                {
                    //操作不能に
                    SetButtonActive (false);
                }
            }
        }

        public void SetButtonActive ( bool b )
        {
            button.CanManipurate = b;
        }

        public void SetButtonWord ( bool isLock )
        {
            network.CmdSetButtonText (true);
        }

        #region WINDOW

        public void SetImageActive ( Enemy.EnemyType type )
        {
            network.CmdSetWindowsImageActive (type);
        }

        public void NtSetImageActive ( Enemy.EnemyType type )
        {
            windowImage.gameObject.SetActive (true);
        }

        public void SetImageNonActive ( Enemy.EnemyType type )
        {
            network.CmdSetWindowImageNonActive (type);
        }

        public void NtSetImageNonActive ( Enemy.EnemyType type )
        {
            Debug.Log ("お化け非表示");
            windowImage.gameObject.SetActive (false);
        }
        #endregion
    }
}