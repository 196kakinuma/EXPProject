using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace Games.Enemy
{
    public enum EnemyType
    {
        RED,
        BLUE,
        GREEN
    }

    public class Enemy
    {
        //移動周期
        float cycle;
        public float Cycle
        {
            get { return cycle; }
            private set { cycle = value; }
        }
        //侵入街時間
        float stayTime;
        public float StayTime
        {
            get { return stayTime; }
            private set { stayTime = value; }
        }

        float afterLockStaryTime;
        public float AfterLockStayTime
        {
            get { return afterLockStaryTime; }
            private set { afterLockStaryTime = value; }
        }


        //敵の色
        EnemyType type;
        public EnemyType Type
        {
            get { return type; }
            private set { type = value; }
        }



        float nextCheckTime;
        public float NextCheckTime
        {
            get { return nextCheckTime; }
            private set { nextCheckTime = value; }
        }

        /// <summary>
        /// ゲーム毎に新しく生成される
        /// </summary>
        /// <param name="cycle"></param>
        /// <param name="stayTime"></param>
        /// <param name="type"></param>
        public Enemy ( float cycle, float stayTime, float afterTime, EnemyType type )
        {
            this.Cycle = cycle;
            this.StayTime = stayTime;
            this.Type = type;
            this.AfterLockStayTime = afterTime;
            nextCheckTime = Cycle;
        }

        /// <summary>
        /// チェックの際にその部屋にはいることができたか
        /// </summary>
        /// <param name="roomIn"></param>
        public void SetNextCheck ( bool roomIn )
        {
            if ( roomIn ) nextCheckTime += StayTime;
            else NextCheckTime += Cycle;
        }

        /// <summary>
        /// ドアをロックスたらその後の待ち時間を減らす
        /// </summary>
        public void SetLockDoorStayTime ()
        {
            NextCheckTime = Games.GameSystem.GameTimer.Instance.GetTime () + AfterLockStayTime;
        }
    }
}