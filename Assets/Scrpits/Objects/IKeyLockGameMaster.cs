using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Games.GameSystem;
namespace Objects
{

    public interface IKeyLockGameMaster
    {

        void SetOperationAuthority ( bool b );
        /// <summary>
        /// ゲーム開始時に呼ばれる
        /// </summary>
        void Prepare ();
        IEnumerator Initialize ( Door d );
        /// <summary>
        /// そのゲーム毎に紐づく物をnullにする
        /// </summary>
        void Clear ();

        void ResetAll ();

        void Answer ();

        #region MOVING
        /// <summary>
        /// ドアの下にセット
        /// </summary>
        void PrepareMove ();

        void NtPrepareMove ( Vector3 pos, Vector3 forward );

        /// <summary>
        /// 問題を解くボタン押して,登場させる
        /// </summary>
        void AppearRoom ();

        void NtAppearRoom ();
        /// <summary>
        /// 問題が正解したら,画面外に出して非アクティブ化
        /// </summary>
        void ExitRoom ();

        void NtExitRoom ();

        #endregion
    }
}