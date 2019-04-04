﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//*|***|***|***|***|***|***|***|***|***|***|***|
// コントローラー情報を転換せよ
//*|***|***|***|***|***|***|***|***|***|***|***|
public class DebugPlayerController 
{
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // スティック情報
    // 動いたか？
    //*|***|***|***|***|***|***|***|***|***|***|***|
    bool m_stickMove;
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // スティック情報
    // 動きの量
    //*|***|***|***|***|***|***|***|***|***|***|***|
    Vector2 m_stickPower;
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // スティック情報
    // 動きの方向
    //*|***|***|***|***|***|***|***|***|***|***|***|
    float m_stickAngle;
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 攻撃コード
    //*|***|***|***|***|***|***|***|***|***|***|***|
    bool m_flagAttackKey;
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // ジャンプコード
    //*|***|***|***|***|***|***|***|***|***|***|***|
    bool m_flagJumpKey;
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 踏みつけコード
    //*|***|***|***|***|***|***|***|***|***|***|***|
    bool m_flagTrampleKey;
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // スタートコード
    //*|***|***|***|***|***|***|***|***|***|***|***|
    bool m_flagStartKey;

    //*|***|***|***|***|***|***|***|***|***|***|***|
    // コンストラクタ
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public DebugPlayerController()
    {
        ResetCommond();
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // コマンドリセット
    //*|***|***|***|***|***|***|***|***|***|***|***|
    void ResetCommond()
    {
        m_stickPower = Vector2.zero;
        m_stickAngle = 0.0f;
        m_stickMove = false;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 攻撃コード
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_flagAttackKey = false;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // ジャンプコード
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_flagJumpKey = false;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 踏みつけコード
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_flagTrampleKey = false;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // スタートコード
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_flagStartKey = false;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // コード生成
    //*|***|***|***|***|***|***|***|***|***|***|***|
    void Update()
    {
        ResetCommond();
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 攻撃コード
        //*|***|***|***|***|***|***|***|***|***|***|***|
        if (Input.GetKeyDown(KeyCode.X) || Input.GetKeyDown(KeyCode.K))
        {
            m_flagAttackKey = true;
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // ジャンプコード
        //*|***|***|***|***|***|***|***|***|***|***|***|
        if (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.J))
        {
            m_flagJumpKey = true;
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 踏みつけコード
        //*|***|***|***|***|***|***|***|***|***|***|***|
        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            m_flagTrampleKey = true;
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // スタートコード
        //*|***|***|***|***|***|***|***|***|***|***|***|
        if (Input.GetKeyDown(KeyCode.Space))
        {
            m_flagStartKey = true;
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 移動スティック
        //*|***|***|***|***|***|***|***|***|***|***|***|
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            m_stickPower.x += 1;
        }
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            m_stickPower.x += -1;
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // スティック情報登録
        //*|***|***|***|***|***|***|***|***|***|***|***|
        MakeStick();
    }


    //*|***|***|***|***|***|***|***|***|***|***|***|
    // スティック情報登録
    //*|***|***|***|***|***|***|***|***|***|***|***|
    private void MakeStick()
    {
        if (MyCalculator.LongVector2(m_stickPower) != 0.0f)
        {
            m_stickMove = true;
            m_stickAngle = ChangeData.Vector2ToAngleDeg(m_stickPower);
        }
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 攻撃コード確認
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public bool ChackAttack()
    {
        return m_flagAttackKey;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // ジャンプコード確認
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public bool ChackJump()
    {
        return m_flagJumpKey;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 踏みつけコード確認
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public bool ChackTrample()
    {
        return m_flagTrampleKey;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // スタートコード確認
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public bool ChackStart()
    {
        return m_flagStartKey;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // スティック情報確認
    // 動いたか？
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public bool ChackStickMove()
    {
        return m_stickMove;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // スティック情報確認
    // 動きの量
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public Vector2 ChackStickPower()
    {
        return m_stickPower;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // スティック情報確認
    // 動きの方向
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public float ChackStickAngle()
    {
        return m_stickAngle;
    }
}
