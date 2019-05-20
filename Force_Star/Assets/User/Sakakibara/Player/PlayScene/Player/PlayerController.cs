using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//*|***|***|***|***|***|***|***|***|***|***|***|
// コントローラー情報を転換せよ
//*|***|***|***|***|***|***|***|***|***|***|***|
public class PlayerController
{
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // コード情報
    //*|***|***|***|***|***|***|***|***|***|***|***|
    PlayerControllerStatic.PlayerControllerStruct m_controllerData;

    //*|***|***|***|***|***|***|***|***|***|***|***|
    // コンストラクタ
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public PlayerController()
    {
        ResetCommond();
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // コマンドリセット
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public void ResetCommond()
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // コード情報リセット
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_controllerData.ResetCommond();
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // コード生成
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public void Update()
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // コード情報リセット
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_controllerData.UpdateCommondReStart();
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // キーボード用
        //*|***|***|***|***|***|***|***|***|***|***|***|
        UpdateKeyPad();
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // ジョイコン用
        //*|***|***|***|***|***|***|***|***|***|***|***|
        //UpdateJoyPad();
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // コード情報登録
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_controllerData.UpdateCommondTrigger();
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // コード生成
    //*|***|***|***|***|***|***|***|***|***|***|***|
    private void UpdateKeyPad()
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 攻撃コード
        //*|***|***|***|***|***|***|***|***|***|***|***|
        if (Input.GetKey(KeyCode.X) || Input.GetKey(KeyCode.K))
        {
            m_controllerData.m_flagAttackKey = true;
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // ジャンプコード
        //*|***|***|***|***|***|***|***|***|***|***|***|
        if (Input.GetKey(KeyCode.Z) || Input.GetKey(KeyCode.J))
        {
            m_controllerData.m_flagJumpKey = true;
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 踏みつけコード
        //*|***|***|***|***|***|***|***|***|***|***|***|
        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            m_controllerData.m_flagTrampleKey = true;
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // スタートコード
        //*|***|***|***|***|***|***|***|***|***|***|***|
        if (Input.GetKey(KeyCode.Space))
        {
            m_controllerData.m_flagStartKey = true;
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 回復コード
        //*|***|***|***|***|***|***|***|***|***|***|***|
        if (Input.GetKey(KeyCode.C))
        {
            m_controllerData.m_flagRecoveryKey = true;
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 移動スティック
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 上下
        //*|***|***|***|***|***|***|***|***|***|***|***|
        if (Input.GetKey(KeyCode.UpArrow))
        {
            m_controllerData.m_stickPower.y += 1;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            m_controllerData.m_stickPower.y += -1;
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 上下
        //*|***|***|***|***|***|***|***|***|***|***|***|
        if (Input.GetKey(KeyCode.RightArrow))
        {
            m_controllerData.m_stickPower.x += 1;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            m_controllerData.m_stickPower.x += -1;
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // コンパススティック
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 上下
        //*|***|***|***|***|***|***|***|***|***|***|***|
        if (Input.GetKey(KeyCode.W))
        {
            m_controllerData.m_compassPower.y += 1;
        }
        if (Input.GetKey(KeyCode.S))
        {
            m_controllerData.m_compassPower.y += -1;
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 上下
        //*|***|***|***|***|***|***|***|***|***|***|***|
        if (Input.GetKey(KeyCode.D))
        {
            m_controllerData.m_compassPower.x += 1;
        }
        if (Input.GetKey(KeyCode.A))
        {
            m_controllerData.m_compassPower.x += -1;
        }
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // コード生成
    //*|***|***|***|***|***|***|***|***|***|***|***|
    private void UpdateJoyPad()
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // axis移動
        //*|***|***|***|***|***|***|***|***|***|***|***|
        float axisX = Input.GetAxis("MoveX");
        float axisY = Input.GetAxis("MoveY");
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // axis移動
        //*|***|***|***|***|***|***|***|***|***|***|***|
        float axisSelectX = Input.GetAxis("SelectX");
        float axisSelectY = Input.GetAxis("SelectY");
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // ボタンコード
        //*|***|***|***|***|***|***|***|***|***|***|***|
        float joyKeyAttack = Input.GetAxis("JoyKeyAttack");
        float joyKeyJump = Input.GetAxis("JoyKeyJump");
        float joyKeyStart = Input.GetAxis("JoyKeyStart");
        float joyKeyRecovery = Input.GetAxis("JoyKeyRecovery");
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 攻撃コード
        //*|***|***|***|***|***|***|***|***|***|***|***|
        if (joyKeyAttack != 0.0f)
        {
            m_controllerData.m_flagAttackKey = true;
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // ジャンプコード
        //*|***|***|***|***|***|***|***|***|***|***|***|
        if (joyKeyJump != 0.0f)
        {
            m_controllerData.m_flagJumpKey = true;
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 踏みつけコード
        //*|***|***|***|***|***|***|***|***|***|***|***|

        //if (joyKeyAttack != 0.0f)
        //{
        //    m_controllerData.m_flagTrampleKey = true;
        //}

        //*|***|***|***|***|***|***|***|***|***|***|***|
        // スタートコード
        //*|***|***|***|***|***|***|***|***|***|***|***|
        if (joyKeyStart != 0.0f)
        {
            m_controllerData.m_flagStartKey = true;
        }

        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 回復コード
        //*|***|***|***|***|***|***|***|***|***|***|***|
        if (joyKeyRecovery != 0.0f)
        {
            m_controllerData.m_flagRecoveryKey = true;
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 移動スティック
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_controllerData.m_stickPower.x = axisX;
        m_controllerData.m_stickPower.y = axisY;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // コンパススティック
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_controllerData.m_compassPower.x = axisSelectX;
        m_controllerData.m_compassPower.y = axisSelectY;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 攻撃コード確認
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public bool ChackAttackTrigger()
    {
        return m_controllerData.m_flagAttackKeyTrigger;
    }
    public bool ChackAttack()
    {
        return m_controllerData.m_flagAttackKey;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // ジャンプコード確認
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public bool ChackJumpTrigger()
    {
        return m_controllerData.m_flagJumpKeyTrigger;
    }
    public bool ChackJump()
    {
        return m_controllerData.m_flagJumpKey;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 踏みつけコード確認
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public bool ChackTrampleTrigger()
    {
        return m_controllerData.m_flagTrampleKeyTrigger;
    }
    public bool ChackTrample()
    {
        return m_controllerData.m_flagTrampleKey;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // スタートコード確認
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public bool ChackStartTrigger()
    {
        return m_controllerData.m_flagStartKeyTrigger;
    }
    public bool ChackStart()
    {
        return m_controllerData.m_flagStartKey;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 回復コード確認
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public bool ChackRecoveryTrigger()
    {
        return m_controllerData.m_flagRecoveryKeyTrigger;
    }
    public bool ChackRecovery()
    {
        return m_controllerData.m_flagRecoveryKey;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // コンパス情報
    // 動いたか？
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public bool ChackCompassMove()
    {
        return m_controllerData.m_compassMove;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // コンパス情報
    // 動きの量
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public Vector2 ChackCompassPower()
    {
        return m_controllerData.m_compassPower;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // コンパス情報
    // 動きの方向
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public float ChackCompassAngle()
    {
        return m_controllerData.m_compassAngle;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // スティック情報確認
    // 動いたか？
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public bool ChackStickMove()
    {
        return m_controllerData.m_stickMove;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // スティック情報確認
    // 動きの量
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public Vector2 ChackStickPower()
    {
        return m_controllerData.m_stickPower;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // スティック情報確認
    // 動きの方向
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public float ChackStickAngle()
    {
        return m_controllerData.m_stickAngle;
    }
}
