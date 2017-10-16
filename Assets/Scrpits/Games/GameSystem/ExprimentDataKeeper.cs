using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IkLibrary.Unity;

namespace Games.GameSystem
{
    public class ExprimentDataKeeper : SingletonMonoBehaviour<ExprimentDataKeeper>
    {

        public string day;
        public string UserName;
        public string memo = "";

        string extension = ".scv";
        ExcelWriter eWriter;

        List<string> expGameNums;
        List<float> expTimes;
        List<string> expSituations;
        // Use this for initialization
        void Start ()
        {
            eWriter = new ExcelWriter ();
        }


        /// <summary>
        /// 実験前にそのユーザの情報を格納するファイルを生成
        /// </summary>
        public void InitializeNewFile ()
        {
            eWriter.InitializeFile (name);
            eWriter.InitWriteUserInfo (day, name);
            expGameNums = new List<string> ();
            expTimes = new List<float> ();
            expSituations = new List<string> ();

        }

        /// <summary>
        /// 書き込む実験データを受け取る
        /// </summary>
        /// <param name="gameNum"></param>
        /// <param name="time"></param>
        /// <param name="situation"></param>
        public void SetExperimentData ( KeyGames game, float time, string situation )
        {
            expGameNums.Add (game.ToString ());
            expTimes.Add (time);
            expSituations.Add (situation);
        }


        /// <summary>
        /// 実験後のデータ書き出し用
        /// </summary>
        public void AllWriteDownExcel ()
        {
            foreach ( var a in expGameNums )
                eWriter.WriteWord (a);
            eWriter.WriteNewLine ();
            foreach ( var a in expTimes )
                eWriter.WriteWord (a.ToString ());
            eWriter.WriteNewLine ();
            foreach ( var a in expSituations )
                eWriter.WriteWord (a);
            eWriter.WriteNewLine ();
        }
    }
}