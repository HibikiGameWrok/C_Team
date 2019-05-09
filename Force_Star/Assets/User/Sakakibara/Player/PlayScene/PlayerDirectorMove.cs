using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class PlayerDirector : MonoBehaviour
{
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // プレイヤー情報
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // コントローラー情報を転換せよ
    //*|***|***|***|***|***|***|***|***|***|***|***|
    DebugPlayerController m_controller;
    [SerializeField]
    public int m_animeNum = 0;
    private bool m_nextMirror = false;

    //*|***|***|***|***|***|***|***|***|***|***|***|
    // プレイヤー情報
    //*|***|***|***|***|***|***|***|***|***|***|***|
    void AwakePlayer()
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // プレイヤーコントローラーを作成せよ。
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_controller = new DebugPlayerController();
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // プレイヤーを飾り付けよ。
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_playerMove = m_playerCenter.AddComponent<PlayerMove>();
        m_playerMove.LinkController(m_controller);


        m_nextMirror = false;

    }

    //*|***|***|***|***|***|***|***|***|***|***|***|
    // プレイヤー情報
    //*|***|***|***|***|***|***|***|***|***|***|***|
    void UpdatePlayer()
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 操作更新
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_controller.Update();
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // アニメ更新
        //*|***|***|***|***|***|***|***|***|***|***|***|
        AnimeStudyFrild();
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // アニメの制御
    //*|***|***|***|***|***|***|***|***|***|***|***|
    void AnimeStudyFrild()
    {
        bool groundFlag = m_playerMove.GetGroundFlag();

        bool addPowerFlag = m_playerMove.GetAddPowerFlag();
        bool moveingPowerFlag = m_playerMove.GetMoveingPowerFlag();
        bool reverseArrowFlag = m_playerMove.GetReverseArrowFlag();
        bool rightArrow = m_playerMove.GetRightArrowFlag();
        m_myAnime.speed = 1;

        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 攻撃中判定
        //*|***|***|***|***|***|***|***|***|***|***|***|
        bool attackFlag = false;


        m_myAnime.SetInteger("MoveEnum", 0);
        if (m_controller.ChackStickMove())
        {
            m_myAnime.SetInteger("MoveEnum", 1);
        }
        if (m_controller.ChackAttack())
        {
            m_myAnime.SetInteger("MoveEnum", 2);
            attackFlag = true;
        }
        if (m_controller.ChackJump())
        {
            m_myAnime.SetInteger("MoveEnum", 3);
        }
        if (m_controller.ChackStart())
        {
            m_myAnime.SetInteger("MoveEnum", 4);
        }
        m_myAnime.SetBool("ClipLand", groundFlag);
        m_myAnime.SetBool("addPowerFlag", addPowerFlag);
        m_myAnime.SetBool("moveingPowerFlag", moveingPowerFlag);
        m_myAnime.SetBool("reverseArrowFlag", reverseArrowFlag);

        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 体の向きを反映
        //*|***|***|***|***|***|***|***|***|***|***|***|
        if (m_nextMirror)
        {
            m_playerCenter.transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else
        {
            m_playerCenter.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        m_nextMirror = rightArrow;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 攻撃中判定を反映
        //*|***|***|***|***|***|***|***|***|***|***|***|
        {
            m_playerMove.SetAttackFlag(attackFlag);
        }
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // プレイヤー情報取得
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public Vector3 GetPlayerPositon()
    {
        return m_playerMove.GetPosition();
    }
}
