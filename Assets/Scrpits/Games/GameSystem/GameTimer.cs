using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IkLibrary.Unity;

namespace Games.GameSystem
{
    public class GameTimer : SingletonMonoBehaviour<GameTimer>
    {
        [SerializeField]
        GameMaster master;

        float startTime;
        float finishTime;


        public void GameStart ()
        {
            startTime = Time.time;

        }

        public float GetTime ()
        {
            if ( master.IsPlaying )
                return Time.time - startTime;
            else
                return finishTime;
        }

        public void GameFinish ()
        {
            finishTime = Time.time - startTime;
            Debug.Log ("finishTime =" + finishTime);
        }

        public bool IsGameEnd ()
        {
            if ( GameSettings.Instance.GameDuration < GetTime () ) return true;
            else return false;
        }

    }
}