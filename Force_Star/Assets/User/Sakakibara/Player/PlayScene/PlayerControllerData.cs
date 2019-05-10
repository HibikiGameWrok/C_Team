using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerData
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
    bool m_flagAttackKeyTrigger;
    bool m_flagAttackKey;
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // ジャンプコード
    //*|***|***|***|***|***|***|***|***|***|***|***|
    bool m_flagJumpKeyTrigger;
    bool m_flagJumpKey;
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 踏みつけコード
    //*|***|***|***|***|***|***|***|***|***|***|***|
    bool m_flagTrampleKeyTrigger;
    bool m_flagTrampleKey;
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // スタートコード
    //*|***|***|***|***|***|***|***|***|***|***|***|
    bool m_flagStartKeyTrigger;
    bool m_flagStartKey;
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 回復コード
    //*|***|***|***|***|***|***|***|***|***|***|***|
    bool m_flagRecoveryKeyTrigger;
    bool m_flagRecoveryKey;

    //*|***|***|***|***|***|***|***|***|***|***|***|
    // コンストラクタ
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public PlayerControllerData()
    {
        ResetCommond();
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // コマンドリセット
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public void ResetCommond()
    {
        m_stickPower = Vector2.zero;
        m_stickAngle = 0.0f;
        m_stickMove = false;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 攻撃コード
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_flagAttackKeyTrigger = false;
        m_flagAttackKey = false;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // ジャンプコード
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_flagJumpKeyTrigger = false;
        m_flagJumpKey = false;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 踏みつけコード
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_flagTrampleKeyTrigger = false;
        m_flagTrampleKey = false;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // スタートコード
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_flagStartKeyTrigger = false;
        m_flagStartKey = false;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 回復コード
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_flagRecoveryKeyTrigger = false;
        m_flagRecoveryKey = false;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // コマンドリセット
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public void UpdateCommond(PlayerController data)
    {
        m_stickPower = data.ChackStickPower();
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // スティック情報登録
        //*|***|***|***|***|***|***|***|***|***|***|***|
        MakeStick();
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 攻撃コード
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_flagAttackKeyTrigger = data.ChackAttackTrigger();
        m_flagAttackKey = data.ChackAttack();
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // ジャンプコード
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_flagJumpKeyTrigger = data.ChackJumpTrigger();
        m_flagJumpKey = data.ChackJump();
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 踏みつけコード
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_flagTrampleKeyTrigger = data.ChackTrampleTrigger();
        m_flagTrampleKey = data.ChackTrample();
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // スタートコード
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_flagStartKeyTrigger = data.ChackStartTrigger();
        m_flagStartKey = data.ChackStart();
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 回復コード
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_flagRecoveryKeyTrigger = data.ChackRecoveryTrigger();
        m_flagRecoveryKey = data.ChackRecovery();
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // スティック情報登録
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public void EditStick(Vector2 stickPower)
    {
        m_stickPower = stickPower;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // スティック情報登録
        //*|***|***|***|***|***|***|***|***|***|***|***|
        MakeStick();
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 攻撃コード確認
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public void EditAttackTrigger(bool attackKeyTrigger)
    {
        m_flagAttackKeyTrigger = attackKeyTrigger;
    }
    public void EditAttack(bool attackKey)
    {
        m_flagAttackKey = attackKey;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // ジャンプコード確認
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public void EditJumpTrigger(bool jumpKeyTrigger)
    {
        m_flagJumpKeyTrigger = jumpKeyTrigger;
    }
    public void EditJump(bool jumpKey)
    {
        m_flagJumpKey = jumpKey;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 踏みつけコード確認
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public void EditTrampleTrigger(bool trampleKeyTrigger)
    {
        m_flagTrampleKeyTrigger = trampleKeyTrigger;
    }
    public void EditTrample(bool trampleKey)
    {
        m_flagTrampleKey = trampleKey;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // スタートコード確認
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public void EditStartTrigger(bool startKeyTrigger)
    {
        m_flagStartKeyTrigger = startKeyTrigger;
    }
    public void EditStart(bool startKey)
    {
        m_flagStartKey = startKey;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 回復コード確認
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public void EditRecoveryTrigger(bool recoveryKeyTrigger)
    {
        m_flagRecoveryKeyTrigger = recoveryKeyTrigger;
    }
    public void EditRecovery(bool recoveryKey)
    {
        m_flagRecoveryKey = recoveryKey;
    }






    //*|***|***|***|***|***|***|***|***|***|***|***|
    // スティック情報登録
    //*|***|***|***|***|***|***|***|***|***|***|***|
    private void MakeStick()
    {
        m_stickPower.x = ChangeData.Among(m_stickPower.x, -1.0f, 1.0f);
        m_stickPower.y = ChangeData.Among(m_stickPower.y, -1.0f, 1.0f);
        if (MyCalculator.LongVector2(m_stickPower) != 0.0f)
        {
            m_stickMove = true;
            m_stickAngle = ChangeData.Vector2ToAngleDeg(m_stickPower);
        }
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 攻撃コード確認
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public bool ChackAttackTrigger()
    {
        return m_flagAttackKeyTrigger;
    }
    public bool ChackAttack()
    {
        return m_flagAttackKey;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // ジャンプコード確認
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public bool ChackJumpTrigger()
    {
        return m_flagJumpKeyTrigger;
    }
    public bool ChackJump()
    {
        return m_flagJumpKey;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 踏みつけコード確認
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public bool ChackTrampleTrigger()
    {
        return m_flagTrampleKeyTrigger;
    }
    public bool ChackTrample()
    {
        return m_flagTrampleKey;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // スタートコード確認
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public bool ChackStartTrigger()
    {
        return m_flagStartKeyTrigger;
    }
    public bool ChackStart()
    {
        return m_flagStartKey;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 回復コード確認
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public bool ChackRecoveryTrigger()
    {
        return m_flagRecoveryKeyTrigger;
    }
    public bool ChackRecovery()
    {
        return m_flagRecoveryKey;
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
