using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleSceneDirector : MonoBehaviour
{
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // ゲーム共通ディレクター
    //*|***|***|***|***|***|***|***|***|***|***|***|
    PlaySceneDirectorIndex m_directorIndex;
    PlayerDirectorIndex m_playerIndex;
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 星のいかり
    //*|***|***|***|***|***|***|***|***|***|***|***|
    [SerializeField]
    bool m_startMoveFlag = false;
    [SerializeField]
    bool m_startEndFlag = false;
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // コントロールを管理しているクラス
    //*|***|***|***|***|***|***|***|***|***|***|***|
    PlayerController m_playerController;
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // ファイル名
    //*|***|***|***|***|***|***|***|***|***|***|***|

    void Awake()
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // プレイヤーコントローラーを作成せよ。
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_playerController = new PlayerController();
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // ゲーム共通ディレクター登録
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_directorIndex = PlaySceneDirectorIndex.GetInstance();
        m_directorIndex.AllReset();
        m_playerIndex = PlayerDirectorIndex.GetInstance();
        m_playerIndex.AllReset();
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 更新
    //*|***|***|***|***|***|***|***|***|***|***|***|
    void Update()
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 操作更新
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_playerController.Update();

        if ((m_playerController.ChackStartTrigger()) || (m_playerController.ChackAttack()))
        {
            m_startMoveFlag = true;
        }
        if ((Input.GetKeyDown(KeyCode.Space)))
        {
            m_startMoveFlag = true;
        }

        //if (m_startEndFlag == true)
        //{
        //    //当たった後の挙動
        //    EndMove();
        //}
        //else
        //{
        //    if (m_startMoveFlag == true)
        //    {
        //        //ロケットに追尾
        //        TrackingMove();
        //    }
        //    else
        //    {
        //        //(待機状態)ふわふわ浮かぶ
        //        StayFloatMove();
        //    }
        //}
    }


}
