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
    PlayerController m_controller;
    PlayerControllerData m_controllerData;
    [SerializeField]
    public int m_animeNum = 0;
    private bool m_nextMirror = false;
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // アニメーション用取得フラグ
    //*|***|***|***|***|***|***|***|***|***|***|***|
    private float m_brakeTime = 0.0f;
    private bool m_hashBackBrakeT = false;
    private bool m_hashFlontBrakeT = false;
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // ブレーキ中
    //*|***|***|***|***|***|***|***|***|***|***|***|
    int m_hashBackBrake;
    int m_hashFlontBrake;
    private bool m_animeBackBrake = false;
    private bool m_animeFlontBrake = false;
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // パンチ中
    //*|***|***|***|***|***|***|***|***|***|***|***|
    int m_hashPunch;
    private bool m_animePunch = false;
    private bool m_punchTrigger = false;
    private bool m_punchFlag = false;


    //*|***|***|***|***|***|***|***|***|***|***|***|
    // ボディデータ
    //*|***|***|***|***|***|***|***|***|***|***|***|
    private Rigidbody2D m_player2D;
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // プレイヤー情報
    //*|***|***|***|***|***|***|***|***|***|***|***|
    void AwakePlayer()
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // プレイヤーコントローラーを作成せよ。
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_controller = new PlayerController();
        m_controller.ResetCommond();
        m_controllerData = new PlayerControllerData();
        m_controllerData.UpdateCommond(m_controller);
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // プレイヤーを飾り付けよ。
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_playerMove = m_playerCenter.AddComponent<PlayerMove>();
        m_playerMove.LinkController(m_controllerData);
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // ボディデータ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_player2D = m_playerCenter.GetComponent<Rigidbody2D>();
        m_player2D.constraints = RigidbodyConstraints2D.FreezeRotation;
        m_nextMirror = false;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // アニメーション用取得フラグ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_brakeTime = 0.0f;
        m_hashBackBrakeT = false;
        m_hashFlontBrakeT = false;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // ブレーキ中
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_hashBackBrake = Animator.StringToHash("Base Layer.Basis.ブレーキ.後方ブレーキ");
        m_hashFlontBrake = Animator.StringToHash("Base Layer.Basis.ブレーキ.停止ブレーキ");
        m_animeBackBrake = false;
        m_animeFlontBrake = false;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // パンチ中
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_hashPunch = Animator.StringToHash("Base Layer.Basis.Punch");
        m_animePunch = false;
        m_punchTrigger = false;
        m_punchFlag = false;
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
        ////*|***|***|***|***|***|***|***|***|***|***|***|
        //// アニメ更新
        ////*|***|***|***|***|***|***|***|***|***|***|***|
        //AnimeStudyFrild();
        ////*|***|***|***|***|***|***|***|***|***|***|***|
        //// 操作更新
        ////*|***|***|***|***|***|***|***|***|***|***|***|
        //UpdateMove();
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // プレイヤー情報
    //*|***|***|***|***|***|***|***|***|***|***|***|
    void LateUpdatePlayer()
    {

        //*|***|***|***|***|***|***|***|***|***|***|***|
        // アニメ更新
        //*|***|***|***|***|***|***|***|***|***|***|***|
        AnimeStudyFrild();
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // アニメ体更新
        //*|***|***|***|***|***|***|***|***|***|***|***|
        AnimeStudyBody();
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 操作更新
        //*|***|***|***|***|***|***|***|***|***|***|***|
        UpdateMove();
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
        bool rightPower = m_playerMove.GetRightPowerFlag();
        m_myAnime.speed = 1;

        //*|***|***|***|***|***|***|***|***|***|***|***|
        // パンチボタン押下
        //*|***|***|***|***|***|***|***|***|***|***|***|
        if (m_controller.ChackAttack())
        {
            m_punchTrigger = true;
        }
        else
        {
            m_punchTrigger = false;
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // パンチボタン押下なら
        //*|***|***|***|***|***|***|***|***|***|***|***|
        if (m_punchTrigger)
        {
            m_punchFlag = true;
            m_punchTrigger = false;
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // アニメーション取得
        //*|***|***|***|***|***|***|***|***|***|***|***|
        AnimatorStateInfo state = m_myAnime.GetCurrentAnimatorStateInfo(0);
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // ブレーキ中
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_animeBackBrake = (m_hashBackBrake == state.fullPathHash);
        m_animeFlontBrake = (m_hashFlontBrake == state.fullPathHash);
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // パンチ中
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_animePunch = (m_hashPunch == state.fullPathHash);
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // ブレーキは時間を共有する
        //*|***|***|***|***|***|***|***|***|***|***|***|
        if (m_animeBackBrake || m_animeFlontBrake)
        {
            m_brakeTime = state.normalizedTime;
            if (!addPowerFlag && !reverseArrowFlag)
            {
                if (!m_hashFlontBrakeT)
                {
                    m_myAnime.Play(m_hashFlontBrake, 0, m_brakeTime);
                }
                m_hashFlontBrakeT = true;
            }
            else
            {
                m_hashFlontBrakeT = false;
            }
            if (addPowerFlag && reverseArrowFlag)
            {
                //m_myAnime.Play(hashBackBrake, 0, m_brakeTime);
                if (!m_hashBackBrakeT)
                {
                    m_myAnime.Play(m_hashBackBrake, 0, m_brakeTime);
                }
                m_hashBackBrakeT = true;
            }
            else
            {
                m_hashBackBrakeT = false;
            }
        }
        else
        {
            m_brakeTime = 0.0f;
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // パンチは9割進むと***
        //*|***|***|***|***|***|***|***|***|***|***|***|
        if (m_animePunch)
        {
            float time = state.normalizedTime;
            if(time > 0.9f)
            {
                m_punchFlag = false;
                m_punchTrigger = false;
            }
        }
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // アニメ体の制御
    //*|***|***|***|***|***|***|***|***|***|***|***|
    void AnimeStudyBody()
    {
        bool groundFlag = m_playerMove.GetGroundFlag();
        bool addPowerFlag = m_playerMove.GetAddPowerFlag();
        bool moveingPowerFlag = m_playerMove.GetMoveingPowerFlag();
        bool reverseArrowFlag = m_playerMove.GetReverseArrowFlag();
        bool rightArrow = m_playerMove.GetRightArrowFlag();
        bool rightPower = m_playerMove.GetRightPowerFlag();
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 体の向きを反映
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // ブレーキアニメ、パンチアニメ中は禁止
        //*|***|***|***|***|***|***|***|***|***|***|***|
        {
            if (!m_animeBackBrake && !m_animeFlontBrake && !m_animePunch)
            {
                if (rightPower)
                {
                    m_playerCenter.transform.rotation = Quaternion.Euler(0, 180, 0);
                }
                else
                {
                    m_playerCenter.transform.rotation = Quaternion.Euler(0, 0, 0);
                }
            }
        }
        m_nextMirror = rightPower;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 操作更新
    //*|***|***|***|***|***|***|***|***|***|***|***|
    void UpdateMove()
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 現在の状態
        //*|***|***|***|***|***|***|***|***|***|***|***|
        bool groundFlag = m_playerMove.GetGroundFlag();
        bool addPowerFlag = m_playerMove.GetAddPowerFlag();
        bool moveingPowerFlag = m_playerMove.GetMoveingPowerFlag();
        bool reverseArrowFlag = m_playerMove.GetReverseArrowFlag();
        bool rightArrow = m_playerMove.GetRightArrowFlag();
        bool rightPower = m_playerMove.GetRightPowerFlag();
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 更新データ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_controllerData.UpdateCommond(m_controller);
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 回復判定
        //*|***|***|***|***|***|***|***|***|***|***|***|
        if (m_pushRecoveryKey)
        {
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 回復中は操作不可
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_controllerData.ResetCommond();
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // ノックバック
        //*|***|***|***|***|***|***|***|***|***|***|***|
        if (m_damageTrigger)
        {
            Vector2 power = new Vector2(1, 1);
            power *= 500.0f;
            if (rightPower)
            {
                power.x = power.x * -1;
            }
            m_player2D.AddForce(power);
            m_damageTrigger = false;
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // ノックバック判定
        //*|***|***|***|***|***|***|***|***|***|***|***|
        if (m_nowDamage)
        {
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // ノックバック中は操作不可？
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_controllerData.ResetCommond();
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // パンチ判定
        //*|***|***|***|***|***|***|***|***|***|***|***|
        if (m_animePunch)
        {
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // パンチ中は操作不可
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 移動禁止
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_controllerData.EditStick(Vector2.zero);

            //*|***|***|***|***|***|***|***|***|***|***|***|
            // ジャンプ禁止
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_controllerData.EditJumpTrigger(false);
            m_controllerData.EditJump(false);


        }

        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 攻撃中判定
        //*|***|***|***|***|***|***|***|***|***|***|***|
        bool noMoveFlag = true;
        if (m_controllerData.ChackStickMove())
        {
            m_myAnime.SetInteger("MoveEnum", 1);
            noMoveFlag = false;
        }
        if (m_punchFlag)
        {
            m_myAnime.SetInteger("MoveEnum", 2);
            noMoveFlag = false;
        }
        if (m_controllerData.ChackJump())
        {
            m_myAnime.SetInteger("MoveEnum", 3);
            noMoveFlag = false;
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // ダメージを受けているなら
        //*|***|***|***|***|***|***|***|***|***|***|***|
        if (m_nowDamage)
        {
            m_myAnime.SetInteger("MoveEnum", 5);
            noMoveFlag = false;
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 何もしてないなら
        //*|***|***|***|***|***|***|***|***|***|***|***|
        if (noMoveFlag)
        {
            m_myAnime.SetInteger("MoveEnum", 0);
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // フラグ更新なら
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_myAnime.SetBool("ClipLand", groundFlag);
        m_myAnime.SetBool("addPowerFlag", addPowerFlag);
        m_myAnime.SetBool("moveingPowerFlag", moveingPowerFlag);
        m_myAnime.SetBool("reverseArrowFlag", reverseArrowFlag);
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 攻撃中判定を反映
        //*|***|***|***|***|***|***|***|***|***|***|***|
        {
            m_playerMove.SetAttackFlag(m_punchFlag);
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
//if(state.fullPathHash == Animator.StringToHash("Base Layer.後方ブレーキ"))
//{
//    m_nextMirror = m_nextMirror;
//}
//if (state.fullPathHash == Animator.StringToHash("後方ブレーキ"))
//{
//    m_nextMirror = m_nextMirror;
//}
//if (Animator.StringToHash("Base Layer.Basis.Punch") == state.fullPathHash)
//{
//    m_nextMirror = m_nextMirror;
//}
//if (hashFlontBrakeF)
//{
//    if (!m_hashFlontBrakeT)
//    {
//        m_myAnime.Play(hashFlontBrake, 0, m_brakeTime);
//    }
//    m_hashFlontBrakeT = true;
//}
//else
//{
//    m_hashFlontBrakeT = false;
//}

//if (hashBackBrakeF)
//{
//    if (!m_hashBackBrakeT)
//    {
//        m_myAnime.Play(hashBackBrake, 0, m_brakeTime);
//    }
//    m_hashBackBrakeT = true;
//}
//else
//{
//    m_hashBackBrakeT = false;
//}
