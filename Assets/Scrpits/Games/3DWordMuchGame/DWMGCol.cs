using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Games.DWordMuchGame
{
    public class DWMGCol : MonoBehaviour
    {
        [SerializeField]
        Text text;

        [Range (0, 4)]
        public int staticColNum = 0;


        int currentNum = 0;

        bool operationAuthority;
        public bool OperationAuthority
        {
            get { return operationAuthority; }
            set { operationAuthority = value; }
        }

        //毎回リロードされる値
        List<string> textList;
        int colNum;

        public void Initialize ( int colNum, List<string> textL )
        {
            this.colNum = colNum;
            textList = textL;
            currentNum = Random.Range (0, textL.Count);
            //currentNum = staticColNum; デバッグ用
            SetText ();
        }

        public int GetSelectNumber ()
        {
            return currentNum;
        }

        public void TextUp ()
        {
            if ( !OperationAuthority ) return;
            CurrentNumUp ();
            SetText ();
        }

        void CurrentNumUp ()
        {
            if ( currentNum - 1 < 0 ) currentNum = textList.Count - 1;
            else currentNum--;
        }



        public void TextDown ()
        {
            if ( !OperationAuthority ) return;
            CurrentNumDown ();
            SetText ();
        }

        void CurrentNumDown ()
        {
            if ( currentNum + 1 >= textList.Count ) currentNum = 0;
            else currentNum++;
        }

        void SetText ()
        {
            DWMGMaster.Instance.SetColText (colNum, textList[currentNum]);
        }

        public void NtSetText ( string text )
        {
            this.text.text = text;
        }

    }
}