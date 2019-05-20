﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//*|***|***|***|***|***|***|***|***|***|***|***|
// パーツ言い換え
//*|***|***|***|***|***|***|***|***|***|***|***|
using PartsID = PlayStaticData.PartsID;

public partial class PlayerDirector : MonoBehaviour
{
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // データベース
    //*|***|***|***|***|***|***|***|***|***|***|***|
    [SerializeField]
    private PlayerDataBace m_dataBace;
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // データベースUI
    //*|***|***|***|***|***|***|***|***|***|***|***|
    private GameObject m_dataUIObject;
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 基本のUI
    //*|***|***|***|***|***|***|***|***|***|***|***|
    private DebugPlayerUI m_dataUI;
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 回復のUI
    //*|***|***|***|***|***|***|***|***|***|***|***|
    private DebugPlayerRecoveryUI m_dataRecoveryUI;
    private float m_progressParsent;
    private float m_progressParsentAnimate;
    private float m_progressParsentAnimateTime;
    private bool m_dataRecoveryUIAwake;
    private bool m_pushRecoveryKey;


    //*|***|***|***|***|***|***|***|***|***|***|***|
    // ダメージ更新
    //*|***|***|***|***|***|***|***|***|***|***|***|
    private struct PlayerDamageReservation
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // ダメージを与える
        // IgnoreInvincibility 無敵を無視する。
        //*|***|***|***|***|***|***|***|***|***|***|***|
        public bool IgnoreInvincibility;
        public float damage;
    }
    private List<PlayerDamageReservation> m_armDamage;
    private List<PlayerDamageReservation> m_bodyDamage;
    private List<PlayerDamageReservation> m_headDamage;
    private List<PlayerDamageReservation> m_legDamage;
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // ダメージ無敵
    //*|***|***|***|***|***|***|***|***|***|***|***|
    [SerializeField]
    private bool m_invincibility;
    [SerializeField]
    private float m_invincibilityTime;
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // ダメージ受けたか
    //*|***|***|***|***|***|***|***|***|***|***|***|
    [SerializeField]
    private bool m_nowDamage;
    [SerializeField]
    private float m_damageTime;
    [SerializeField]
    private float m_damageInvincibilityTime;
    private bool m_exceptionDamage;
    private bool m_damageTrigger;
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // パワーアップ中
    //*|***|***|***|***|***|***|***|***|***|***|***|
    [SerializeField]
    private bool m_armStrong;
    [SerializeField]
    private bool m_bodyStrong;
    [SerializeField]
    private bool m_headStrong;
    [SerializeField]
    private bool m_legStrong;
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 回復の入力データ
    //*|***|***|***|***|***|***|***|***|***|***|***|
    private float m_recoveryAngle;
    private bool m_recoveryMove;
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // データベース最大値
    //*|***|***|***|***|***|***|***|***|***|***|***|
    private float m_maxDurable;
    private float m_maxStrong;
    private float m_maxAir;
    private int m_maxStarMokuhyou;
    enum TriggerAngle
    {
        UP,
        DOWN,
        RIGHT,
        LEFT,
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 星更新
    //*|***|***|***|***|***|***|***|***|***|***|***|
    private struct PlayerGetStarReservation
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 星の数
        //*|***|***|***|***|***|***|***|***|***|***|***|
        public int starNum;
    }
    private List<PlayerGetStarReservation> m_getStar;
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 所持パーツ状況
    //*|***|***|***|***|***|***|***|***|***|***|***|
    private bool m_haveAllPartsFlag;

    //*|***|***|***|***|***|***|***|***|***|***|***|
    // プレイヤー情報
    //*|***|***|***|***|***|***|***|***|***|***|***|
    void AwakePlayerUI()
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // データベース設定
        //*|***|***|***|***|***|***|***|***|***|***|***|
        SetDataBase();
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // データベース
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_dataBace = new PlayerDataBace();
        m_dataBace.ResetAll(m_maxAir);
        m_dataBace.ResetPartsDurable(m_maxDurable);
        m_dataBace.ResetPartsStrong(m_maxStrong);
        m_dataBace.ResetStars(0, m_maxStarMokuhyou);
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // パワーアップ中
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_armStrong = false;
        m_bodyStrong = false;
        m_headStrong = false;
        m_legStrong = false;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // データベースUI
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_dataUIObject = new GameObject("PLayerUI");
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 基本のUI
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_dataUI = m_dataUIObject.AddComponent<DebugPlayerUI>();
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 回復のUI
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_dataRecoveryUI = m_dataUIObject.AddComponent<DebugPlayerRecoveryUI>();
        m_progressParsent = 0.0f;
        m_dataRecoveryUIAwake = false;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // ダメージ更新
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_armDamage = new List<PlayerDamageReservation>();
        m_bodyDamage = new List<PlayerDamageReservation>();
        m_headDamage = new List<PlayerDamageReservation>();
        m_legDamage = new List<PlayerDamageReservation>();
        m_invincibility = false;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 星更新
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_getStar = new List<PlayerGetStarReservation>();
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // ダメージ受けたか
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_nowDamage = false;
        m_damageTime = 0.5f;
        m_damageInvincibilityTime = 2.0f;
        m_damageTrigger = false;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 回復の入力データ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_recoveryAngle = 0.0f;
        m_recoveryMove = false;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 所持パーツ状況
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_haveAllPartsFlag = false;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // データベース設定
    //*|***|***|***|***|***|***|***|***|***|***|***|
    void SetDataBase()
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // データベース最大値
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_maxDurable = 2000.0f;
        m_maxStrong = 1000.0f;
        m_maxAir = 7200.0f;
        m_maxStarMokuhyou = 2000;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // プレイヤー情報
    //*|***|***|***|***|***|***|***|***|***|***|***|
    void UpdatePlayerUI()
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // データベースを更新
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 時間経過
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_dataBace.CatchTimers(-1);
        float time = m_dataBace.GetTimeParsent();

        if (Input.GetKey(KeyCode.L))
        {
            m_dataBace.CatchStars(10);
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // パワーアップ中
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_dataBace.TimePartsStrong(1.0f);
        m_armStrong = m_dataBace.GetArmStrong();
        m_bodyStrong = m_dataBace.GetBodyStrong();
        m_headStrong = m_dataBace.GetHeadStrong();
        m_legStrong = m_dataBace.GetLegStrong();
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // ステートチェック
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_dataBace.ChackUpdate();
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // プレイヤーはどこにいますの？
        //*|***|***|***|***|***|***|***|***|***|***|***|
        Vector3 playerPos = m_playerCenter.gameObject.transform.position;
        Vector3 screenPos = m_directorIndex.GetScreenPos(playerPos);





        //*|***|***|***|***|***|***|***|***|***|***|***|
        // データベースから取得
        //*|***|***|***|***|***|***|***|***|***|***|***|
        int haveStar = m_dataBace.GetStarsNum();
        int needStar = m_dataBace.GetNeedStarsNum();
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 計算
        //*|***|***|***|***|***|***|***|***|***|***|***|
        float starParsent = MyCalculator.Division((float)haveStar, (float)needStar);
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // パーツ所持状況
        //*|***|***|***|***|***|***|***|***|***|***|***|
        uint partsCollection = m_dataBace.GetHavePartsId();
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // データベースUIに反映
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_dataUI.SetAirGaugeNumber(time);
        m_dataUI.SetStarGaugeNumber(starParsent);
        m_dataUI.SetHaveStarNumber(haveStar);
        m_dataUI.SetNeedStarNumber(needStar);
        m_dataUI.SetPartsCollectionNumber(partsCollection);
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 強化をデータベースUIに反映
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_dataUI.SetArmStrongTime(m_dataBace.GetArmStrongParsent());
        m_dataUI.SetBodyStrongTime(m_dataBace.GetBodyStrongParsent());
        m_dataUI.SetHeadStrongTime(m_dataBace.GetHeadStrongParsent());
        m_dataUI.SetLegStrongTime(m_dataBace.GetLegStrongParsent());

        //*|***|***|***|***|***|***|***|***|***|***|***|
        // ダメージ受けたか
        //*|***|***|***|***|***|***|***|***|***|***|***|
        if (m_invincibilityTime > m_damageInvincibilityTime - m_damageTime)
        {
            m_nowDamage = true;
        }
        else
        {
            m_nowDamage = false;
        }


        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 星更新
        //*|***|***|***|***|***|***|***|***|***|***|***|
        UpdateGetStar();
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // ダメージ回復
        //*|***|***|***|***|***|***|***|***|***|***|***|
        UpdateRecovery();
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 強化適用
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_playerMove.SetPowerUp(m_armStrong, m_bodyStrong, m_headStrong, m_legStrong);

    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // プレイヤー情報
    //*|***|***|***|***|***|***|***|***|***|***|***|
    void FixedUpdatePlayerUI()
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // ダメージ更新
        //*|***|***|***|***|***|***|***|***|***|***|***|
        UpdateHitFlag();
        //UpdateHitFlagEX();
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 所持パーツ状況
        //*|***|***|***|***|***|***|***|***|***|***|***|
        UpdatePartsId();
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 所持パーツ状況
    //*|***|***|***|***|***|***|***|***|***|***|***|
    void UpdatePartsId()
    {
        uint havePartsID = m_dataBace.GetHavePartsId();
        bool haveAllParts = true;
        bool haveParts = false;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // ダメージ報告脚
        //*|***|***|***|***|***|***|***|***|***|***|***|
        for (int partsNum = 0; partsNum < (int)PartsID.NUM; partsNum++)
        {
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // パーツ持っている？
            //*|***|***|***|***|***|***|***|***|***|***|***|
            haveParts = MyCalculator.DigitBoolean(havePartsID, (uint)partsNum);
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 全部持っているか？
            //*|***|***|***|***|***|***|***|***|***|***|***|
            if (!haveParts)
            {
                haveAllParts = false;
            }
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 全部持っている情報更新
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_haveAllPartsFlag = haveAllParts;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // ダメージ回復
    //*|***|***|***|***|***|***|***|***|***|***|***|
    void UpdateRecovery()
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 操作コードから取得
        //*|***|***|***|***|***|***|***|***|***|***|***|
        float center = 0.5f;
        float end = 1.0f;
        float goal = 0.0f;
        m_pushRecoveryKey = m_controller.ChackRecovery();
        uint trigger = TriggerCompass();
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 押されているか？
        //*|***|***|***|***|***|***|***|***|***|***|***|
        if (m_dataRecoveryUIAwake == false)
        {
            if (m_pushRecoveryKey)
            {
                m_dataRecoveryUIAwake = true;
            }
        }
        else
        {
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 目標地点設定
            //*|***|***|***|***|***|***|***|***|***|***|***|
            goal = end;
            if (m_pushRecoveryKey)
            {
                goal = center;
            }
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 小さいなら
            //*|***|***|***|***|***|***|***|***|***|***|***|
            float speed = 5.0f;
            if (m_progressParsent < goal)
            {
                m_progressParsent += Time.deltaTime * speed;
                if (m_progressParsent > goal)
                {
                    m_progressParsent = goal;
                    if (!m_pushRecoveryKey)
                    {
                        m_dataRecoveryUIAwake = false;
                        m_progressParsent = 0.0f;
                    }
                }
            }
            else
            {
                m_progressParsent -= Time.deltaTime * speed;
                if (m_progressParsent < goal)
                {
                    m_progressParsent = goal;
                    if (!m_pushRecoveryKey)
                    {
                        m_dataRecoveryUIAwake = false;
                        m_progressParsent = 0.0f;
                    }
                }
            }
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // アニメーション進行度を記録
        //*|***|***|***|***|***|***|***|***|***|***|***|
        if (m_dataRecoveryUIAwake)
        {
            m_progressParsentAnimateTime += 0.25f;
            m_progressParsentAnimate = Mathf.Cos(m_progressParsentAnimateTime);
            m_progressParsentAnimate = (m_progressParsentAnimate + 1.0f) / 2.0f;
        }
        else
        {
            m_progressParsentAnimateTime = 0;
            m_progressParsentAnimate = 0;
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 進行度を記録
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_dataRecoveryUI.SetProgressParsent(m_progressParsent);
        m_dataRecoveryUI.SetProgressParsentAnimate(m_progressParsentAnimate);
        m_dataRecoveryUI.SetArmDurableParsent(m_dataBace.GetArmDurableParsent());
        m_dataRecoveryUI.SetBodyDurableParsent(m_dataBace.GetBodyDurableParsent());
        m_dataRecoveryUI.SetHeadDurableParsent(m_dataBace.GetHeadDurableParsent());
        m_dataRecoveryUI.SetLegDurableParsent(m_dataBace.GetLegDurableParsent());



        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 回復判定
        //*|***|***|***|***|***|***|***|***|***|***|***|
        if (m_pushRecoveryKey)
        {
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 金。いや星。
            //*|***|***|***|***|***|***|***|***|***|***|***|
            float partsParsent = 0.0f;
            float partsDamageParsent = 0.0f;
            float starsFeeFloat = 0.0f;
            int starsNum = 0;
            int starsFee = 0;
            int starsFeeR = 0;
            int starsFeeS = 0;
            int starsFeeBasisDamage = 10;
            int starsFeeBasisStrong = 10;
            bool strongCommond = false;
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 回復。
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 頭
            //*|***|***|***|***|***|***|***|***|***|***|***|
            {
                partsParsent = m_dataBace.GetHeadDurableParsent();
                partsDamageParsent = MyCalculator.InversionOfProportion(partsParsent);
                //*|***|***|***|***|***|***|***|***|***|***|***|
                // 料金設定
                //*|***|***|***|***|***|***|***|***|***|***|***|
                starsFeeS = starsFeeBasisStrong;
                starsFeeFloat = partsDamageParsent * starsFeeBasisDamage;
                starsFeeR = (int)Mathf.Ceil(starsFeeFloat);
                //*|***|***|***|***|***|***|***|***|***|***|***|
                // モード設定
                //*|***|***|***|***|***|***|***|***|***|***|***|
                if (partsParsent == 1)
                {
                    strongCommond = true;
                    starsFee = starsFeeS;
                }
                else
                {
                    strongCommond = false;
                    starsFee = starsFeeR;
                }
                //*|***|***|***|***|***|***|***|***|***|***|***|
                // コマンド
                //*|***|***|***|***|***|***|***|***|***|***|***|
                if (MyCalculator.DigitBoolean(trigger, (uint)TriggerAngle.UP))
                {

                    //*|***|***|***|***|***|***|***|***|***|***|***|
                    // 料金支払って実行
                    //*|***|***|***|***|***|***|***|***|***|***|***|
                    starsNum = m_dataBace.GetStarsNum();
                    if (starsNum >= starsFee)
                    {
                        m_dataBace.CatchStars(starsFee * -1);
                        //*|***|***|***|***|***|***|***|***|***|***|***|
                        // 強化か否か
                        //*|***|***|***|***|***|***|***|***|***|***|***|
                        if (strongCommond)
                        {
                            //*|***|***|***|***|***|***|***|***|***|***|***|
                            // 強化だ
                            //*|***|***|***|***|***|***|***|***|***|***|***|
                            m_dataBace.StartHeadStrong();
                        }
                        else
                        {
                            //*|***|***|***|***|***|***|***|***|***|***|***|
                            // 回復だ
                            //*|***|***|***|***|***|***|***|***|***|***|***|
                            m_dataBace.RecoveryHeadDurable(m_maxDurable);
                        }
                    }
                }
                //*|***|***|***|***|***|***|***|***|***|***|***|
                // 設定だ
                //*|***|***|***|***|***|***|***|***|***|***|***|
                m_dataRecoveryUI.SetHeadStrongFee(starsFeeS);
                m_dataRecoveryUI.SetHeadRecoveryFee(starsFeeR);
                m_dataRecoveryUI.SetHeadStrongMode(strongCommond);
            }
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 脚
            //*|***|***|***|***|***|***|***|***|***|***|***|
            {
                partsParsent = m_dataBace.GetLegDurableParsent();
                partsDamageParsent = MyCalculator.InversionOfProportion(partsParsent);
                //*|***|***|***|***|***|***|***|***|***|***|***|
                // 料金設定
                //*|***|***|***|***|***|***|***|***|***|***|***|
                starsFeeS = starsFeeBasisStrong;
                starsFeeFloat = partsDamageParsent * starsFeeBasisDamage;
                starsFeeR = (int)Mathf.Ceil(starsFeeFloat);
                //*|***|***|***|***|***|***|***|***|***|***|***|
                // モード設定
                //*|***|***|***|***|***|***|***|***|***|***|***|
                if (partsParsent == 1)
                {
                    strongCommond = true;
                    starsFee = starsFeeS;
                }
                else
                {
                    strongCommond = false;
                    starsFee = starsFeeR;
                }
                //*|***|***|***|***|***|***|***|***|***|***|***|
                // コマンド
                //*|***|***|***|***|***|***|***|***|***|***|***|
                if (MyCalculator.DigitBoolean(trigger, (uint)TriggerAngle.DOWN))
                {
                    //*|***|***|***|***|***|***|***|***|***|***|***|
                    // 料金支払って実行
                    //*|***|***|***|***|***|***|***|***|***|***|***|
                    starsNum = m_dataBace.GetStarsNum();
                    if (starsNum >= starsFee)
                    {
                        m_dataBace.CatchStars(starsFee * -1);
                        //*|***|***|***|***|***|***|***|***|***|***|***|
                        // 強化か否か
                        //*|***|***|***|***|***|***|***|***|***|***|***|
                        if (strongCommond)
                        {
                            //*|***|***|***|***|***|***|***|***|***|***|***|
                            // 強化だ
                            //*|***|***|***|***|***|***|***|***|***|***|***|
                            m_dataBace.StartLegStrong();
                        }
                        else
                        {
                            //*|***|***|***|***|***|***|***|***|***|***|***|
                            // 回復だ
                            //*|***|***|***|***|***|***|***|***|***|***|***|
                            m_dataBace.RecoveryLegDurable(m_maxDurable);
                        }
                    }
                }
                //*|***|***|***|***|***|***|***|***|***|***|***|
                // 設定だ
                //*|***|***|***|***|***|***|***|***|***|***|***|
                m_dataRecoveryUI.SetLegStrongFee(starsFeeS);
                m_dataRecoveryUI.SetLegRecoveryFee(starsFeeR);
                m_dataRecoveryUI.SetLegStrongMode(strongCommond);
            }
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 腕
            //*|***|***|***|***|***|***|***|***|***|***|***|
            {
                partsParsent = m_dataBace.GetArmDurableParsent();
                partsDamageParsent = MyCalculator.InversionOfProportion(partsParsent);
                //*|***|***|***|***|***|***|***|***|***|***|***|
                // 料金設定
                //*|***|***|***|***|***|***|***|***|***|***|***|
                starsFeeS = starsFeeBasisStrong;
                starsFeeFloat = partsDamageParsent * starsFeeBasisDamage;
                starsFeeR = (int)Mathf.Ceil(starsFeeFloat);
                //*|***|***|***|***|***|***|***|***|***|***|***|
                // モード設定
                //*|***|***|***|***|***|***|***|***|***|***|***|
                if (partsParsent == 1)
                {
                    strongCommond = true;
                    starsFee = starsFeeS;
                }
                else
                {
                    strongCommond = false;
                    starsFee = starsFeeR;
                }
                //*|***|***|***|***|***|***|***|***|***|***|***|
                // コマンド
                //*|***|***|***|***|***|***|***|***|***|***|***|
                if (MyCalculator.DigitBoolean(trigger, (uint)TriggerAngle.RIGHT))
                {
                    //*|***|***|***|***|***|***|***|***|***|***|***|
                    // 料金支払って実行
                    //*|***|***|***|***|***|***|***|***|***|***|***|
                    starsNum = m_dataBace.GetStarsNum();
                    if (starsNum >= starsFee)
                    {
                        m_dataBace.CatchStars(starsFee * -1);
                        //*|***|***|***|***|***|***|***|***|***|***|***|
                        // 強化か否か
                        //*|***|***|***|***|***|***|***|***|***|***|***|
                        if (strongCommond)
                        {
                            //*|***|***|***|***|***|***|***|***|***|***|***|
                            // 強化だ
                            //*|***|***|***|***|***|***|***|***|***|***|***|
                            m_dataBace.StartArmStrong();
                        }
                        else
                        {
                            //*|***|***|***|***|***|***|***|***|***|***|***|
                            // 回復だ
                            //*|***|***|***|***|***|***|***|***|***|***|***|
                            m_dataBace.RecoveryArmDurable(m_maxDurable);
                        }
                    }
                }
                //*|***|***|***|***|***|***|***|***|***|***|***|
                // 設定だ
                //*|***|***|***|***|***|***|***|***|***|***|***|
                m_dataRecoveryUI.SetArmStrongFee(starsFeeS);
                m_dataRecoveryUI.SetArmRecoveryFee(starsFeeR);
                m_dataRecoveryUI.SetArmStrongMode(strongCommond);
            }
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 体
            //*|***|***|***|***|***|***|***|***|***|***|***|
            {
                partsParsent = m_dataBace.GetBodyDurableParsent();
                partsDamageParsent = MyCalculator.InversionOfProportion(partsParsent);
                //*|***|***|***|***|***|***|***|***|***|***|***|
                // 料金設定
                //*|***|***|***|***|***|***|***|***|***|***|***|
                starsFeeS = starsFeeBasisStrong;
                starsFeeFloat = partsDamageParsent * starsFeeBasisDamage;
                starsFeeR = (int)Mathf.Ceil(starsFeeFloat);
                //*|***|***|***|***|***|***|***|***|***|***|***|
                // モード設定
                //*|***|***|***|***|***|***|***|***|***|***|***|
                if (partsParsent == 1)
                {
                    strongCommond = true;
                    starsFee = starsFeeS;
                }
                else
                {
                    strongCommond = false;
                    starsFee = starsFeeR;
                }
                //*|***|***|***|***|***|***|***|***|***|***|***|
                // コマンド
                //*|***|***|***|***|***|***|***|***|***|***|***|
                if (MyCalculator.DigitBoolean(trigger, (uint)TriggerAngle.LEFT))
                {

                    //*|***|***|***|***|***|***|***|***|***|***|***|
                    // 料金支払って実行
                    //*|***|***|***|***|***|***|***|***|***|***|***|
                    starsNum = m_dataBace.GetStarsNum();
                    if (starsNum >= starsFee)
                    {
                        m_dataBace.CatchStars(starsFee * -1);
                        //*|***|***|***|***|***|***|***|***|***|***|***|
                        // 強化か否か
                        //*|***|***|***|***|***|***|***|***|***|***|***|
                        if (strongCommond)
                        {
                            //*|***|***|***|***|***|***|***|***|***|***|***|
                            // 強化だ
                            //*|***|***|***|***|***|***|***|***|***|***|***|
                            m_dataBace.StartBodyStrong();
                        }
                        else
                        {
                            //*|***|***|***|***|***|***|***|***|***|***|***|
                            // 回復だ
                            //*|***|***|***|***|***|***|***|***|***|***|***|
                            m_dataBace.RecoveryBodyDurable(m_maxDurable);
                        }
                    }
                }
                //*|***|***|***|***|***|***|***|***|***|***|***|
                // 設定だ
                //*|***|***|***|***|***|***|***|***|***|***|***|
                m_dataRecoveryUI.SetBodyStrongFee(starsFeeS);
                m_dataRecoveryUI.SetBodyRecoveryFee(starsFeeR);
                m_dataRecoveryUI.SetBodyStrongMode(strongCommond);
            }
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // ステートチェック
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_dataBace.ChackUpdate();
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 進行度を記録
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_dataRecoveryUI.SetArmStrong(m_dataBace.GetArmStrong());
        m_dataRecoveryUI.SetBodyStrong(m_dataBace.GetBodyStrong());
        m_dataRecoveryUI.SetHeadStrong(m_dataBace.GetHeadStrong());
        m_dataRecoveryUI.SetLegStrong(m_dataBace.GetLegStrong());

    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // ダメージ更新
    //*|***|***|***|***|***|***|***|***|***|***|***|
    void UpdateHitFlag()
    {

        PlayerDamageReservation data;
        bool damageFlag = false;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // ダメージ報告腕
        //*|***|***|***|***|***|***|***|***|***|***|***|
        for (int index = 0; index < m_armDamage.Count; index++)
        {
            data = m_armDamage[index];
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 無敵を貫通するか？
            //*|***|***|***|***|***|***|***|***|***|***|***|
            if (data.IgnoreInvincibility)
            {
                m_dataBace.DamageArmDurable(data.damage);
            }
            else
            {
                //*|***|***|***|***|***|***|***|***|***|***|***|
                // 無敵状態か？
                //*|***|***|***|***|***|***|***|***|***|***|***|
                if (!m_invincibility)
                {
                    //*|***|***|***|***|***|***|***|***|***|***|***|
                    // カウントする
                    //*|***|***|***|***|***|***|***|***|***|***|***|
                    damageFlag = true;
                    m_dataBace.DamageArmDurable(data.damage);
                }
            }
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // ダメージ報告体
        //*|***|***|***|***|***|***|***|***|***|***|***|
        for (int index = 0; index < m_bodyDamage.Count; index++)
        {
            data = m_bodyDamage[index];
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 無敵を貫通するか？
            //*|***|***|***|***|***|***|***|***|***|***|***|
            if (data.IgnoreInvincibility)
            {
                m_dataBace.DamageBodyDurable(data.damage);
            }
            else
            {
                //*|***|***|***|***|***|***|***|***|***|***|***|
                // 無敵状態か？
                //*|***|***|***|***|***|***|***|***|***|***|***|
                if (!m_invincibility)
                {
                    //*|***|***|***|***|***|***|***|***|***|***|***|
                    // カウントする
                    //*|***|***|***|***|***|***|***|***|***|***|***|
                    damageFlag = true;
                    m_dataBace.DamageBodyDurable(data.damage);
                }
            }
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // ダメージ報告頭
        //*|***|***|***|***|***|***|***|***|***|***|***|
        for (int index = 0; index < m_headDamage.Count; index++)
        {
            data = m_headDamage[index];
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 無敵を貫通するか？
            //*|***|***|***|***|***|***|***|***|***|***|***|
            if (data.IgnoreInvincibility)
            {
                m_dataBace.DamageHeadDurable(data.damage);
            }
            else
            {
                //*|***|***|***|***|***|***|***|***|***|***|***|
                // 無敵状態か？
                //*|***|***|***|***|***|***|***|***|***|***|***|
                if (!m_invincibility)
                {
                    //*|***|***|***|***|***|***|***|***|***|***|***|
                    // カウントする
                    //*|***|***|***|***|***|***|***|***|***|***|***|
                    damageFlag = true;
                    m_dataBace.DamageHeadDurable(data.damage);
                }
            }
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // ダメージ報告脚
        //*|***|***|***|***|***|***|***|***|***|***|***|
        for (int index = 0; index < m_legDamage.Count; index++)
        {
            data = m_legDamage[index];
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 無敵を貫通するか？
            //*|***|***|***|***|***|***|***|***|***|***|***|
            if (data.IgnoreInvincibility)
            {
                m_dataBace.DamageLegDurable(data.damage);
            }
            else
            {
                //*|***|***|***|***|***|***|***|***|***|***|***|
                // 無敵状態か？
                //*|***|***|***|***|***|***|***|***|***|***|***|
                if (!m_invincibility)
                {
                    //*|***|***|***|***|***|***|***|***|***|***|***|
                    // カウントする
                    //*|***|***|***|***|***|***|***|***|***|***|***|
                    damageFlag = true;
                    m_dataBace.DamageLegDurable(data.damage);
                }
            }
        }


        //*|***|***|***|***|***|***|***|***|***|***|***|
        // ダメージ無敵更新
        //*|***|***|***|***|***|***|***|***|***|***|***|

        if(m_invincibility)
        {
            m_invincibilityTime -= Time.deltaTime;
            if (m_invincibilityTime < 0)
            {
                m_invincibilityTime = 0;
                m_invincibility = false;
            }
        }
        else if (damageFlag)
        {
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // ダメージ受けた！
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_invincibility = true;
            m_invincibilityTime = m_damageInvincibilityTime;

            m_damageTrigger = true;


            m_directorIndex.PlaySoundEffect(SEManager.SoundID.DAMAGE_01);
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // ステートチェック
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_dataBace.ChackUpdate();

        //*|***|***|***|***|***|***|***|***|***|***|***|
        // ダメージ更新
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_armDamage.Clear();
        m_bodyDamage.Clear();
        m_headDamage.Clear();
        m_legDamage.Clear();
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // ダメージ更新
    //*|***|***|***|***|***|***|***|***|***|***|***|
    void UpdateHitFlagEX()
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // ステートチェック
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_dataBace.ChackUpdate();

        //*|***|***|***|***|***|***|***|***|***|***|***|
        // ダメージ更新
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_armDamage.Clear();
        m_bodyDamage.Clear();
        m_headDamage.Clear();
        m_legDamage.Clear();
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 星更新
    //*|***|***|***|***|***|***|***|***|***|***|***|
    void UpdateGetStar()
    { 
        PlayerGetStarReservation data;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 獲得報告
        //*|***|***|***|***|***|***|***|***|***|***|***|
        for (int index = 0; index < m_getStar.Count; index++)
        {
            data = m_getStar[index];
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // カウントする
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_dataBace.CatchStars(data.starNum);
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 星更新
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_getStar.Clear();
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // ダメージ報告
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public void DamageArm(float damage, bool IgnoreInvincibility)
    {
        PlayerDamageReservation data = new PlayerDamageReservation();
        data.damage = damage;
        data.IgnoreInvincibility = IgnoreInvincibility;
        m_armDamage.Add(data);
    }
    public void DamageBody(float damage, bool IgnoreInvincibility)
    {
        PlayerDamageReservation data = new PlayerDamageReservation();
        data.damage = damage;
        data.IgnoreInvincibility = IgnoreInvincibility;
        m_bodyDamage.Add(data);
    }
    public void DamageHead(float damage, bool IgnoreInvincibility)
    {
        PlayerDamageReservation data = new PlayerDamageReservation();
        data.damage = damage;
        data.IgnoreInvincibility = IgnoreInvincibility;
        m_headDamage.Add(data);
    }
    public void DamageLeg(float damage, bool IgnoreInvincibility)
    {
        PlayerDamageReservation data = new PlayerDamageReservation();
        data.damage = damage;
        data.IgnoreInvincibility = IgnoreInvincibility;
        m_legDamage.Add(data);
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 星報告
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public void GetStar(int starNum)
    {
        PlayerGetStarReservation data = new PlayerGetStarReservation();
        data.starNum = starNum;
        m_getStar.Add(data);
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // ステートチェック
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_dataBace.ChackUpdate();
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // パーツ報告
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public void GetParts(PartsID partsID)
    {
        m_dataBace.CatchPartID(partsID);
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // ステートチェック
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_dataBace.ChackUpdate();
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 星報告
    //*|***|***|***|***|***|***|***|***|***|***|***|
    private uint TriggerCompass()
    {
        uint trigger = 0;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 回復の入力データ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        float nowRecoveryAngle = m_controller.ChackCompassAngle();
        bool nowRecoveryMove = m_controller.ChackCompassMove();
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // コンパスデータ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        float dif = 15.0f;
        float upA = 90.0f;
        float downA = 270.0f;
        float rightA = 0.0f;
        float leftA = 180.0f;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 現在のコンパス
        //*|***|***|***|***|***|***|***|***|***|***|***|
        bool Up = false;
        bool Down = false;
        bool Right = false;
        bool Left = false;
        if (nowRecoveryMove)
        {
            if (MyCalculator.AngleWheelDeg(nowRecoveryAngle, upA, dif))
            {
                Up = true;
            }
            if (MyCalculator.AngleWheelDeg(nowRecoveryAngle, downA, dif))
            {
                Down = true;
            }
            if (MyCalculator.AngleWheelDeg(nowRecoveryAngle, rightA, dif))
            {
                Right = true;
            }
            if (MyCalculator.AngleWheelDeg(nowRecoveryAngle, leftA, dif))
            {
                Left = true;
            }
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 前のコンパス
        //*|***|***|***|***|***|***|***|***|***|***|***|
        bool UpJust = false;
        bool DownJust = false;
        bool RightJust = false;
        bool LeftJust = false;
        if (m_recoveryMove)
        {
            if (MyCalculator.AngleWheelDeg(m_recoveryAngle, upA, dif))
            {
                UpJust = true;
            }
            if (MyCalculator.AngleWheelDeg(m_recoveryAngle, downA, dif))
            {
                DownJust = true;
            }
            if (MyCalculator.AngleWheelDeg(m_recoveryAngle, rightA, dif))
            {
                RightJust = true;
            }
            if (MyCalculator.AngleWheelDeg(m_recoveryAngle, leftA, dif))
            {
                LeftJust = true;
            }
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // トリガーの競合
        //*|***|***|***|***|***|***|***|***|***|***|***|
        if (Up && !UpJust)
        {
            trigger += MyCalculator.MultiplicationBinary((uint)TriggerAngle.UP);
        }
        if (Down && !DownJust)
        {
            trigger += MyCalculator.MultiplicationBinary((uint)TriggerAngle.DOWN);
        }
        if (Right && !RightJust)
        {
            trigger += MyCalculator.MultiplicationBinary((uint)TriggerAngle.RIGHT);
        }
        if (Left && !LeftJust)
        {
            trigger += MyCalculator.MultiplicationBinary((uint)TriggerAngle.LEFT);
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 行動を記録
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_recoveryAngle = nowRecoveryAngle;
        m_recoveryMove = nowRecoveryMove;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 結果
        //*|***|***|***|***|***|***|***|***|***|***|***|
        return trigger;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // プレイヤー情報取得
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public float GetAirParsent()
    {
        return m_dataBace.GetTimeParsent();
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // プレイヤー情報取得
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public float GetHaveStarParsent()
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // データベースから取得
        //*|***|***|***|***|***|***|***|***|***|***|***|
        int haveStar = m_dataBace.GetStarsNum();
        int needStar = m_dataBace.GetNeedStarsNum();
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 計算
        //*|***|***|***|***|***|***|***|***|***|***|***|
        float starParsent = MyCalculator.Division((float)haveStar, (float)needStar);
        return starParsent;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 所持パーツ情報取得
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public bool GetHaveAllPartsFlag()
    {
        return m_haveAllPartsFlag;
    }
}
//bool Up = m_controller.ChackStartTrigger();
//bool Down = m_controller.ChackTrampleTrigger();
//bool Right = m_controller.ChackJumpTrigger();
//bool Left = m_controller.ChackAttackTrigger();
//            if (Up)
//            {
//                m_dataBace.RecoveryHeadDurable(m_maxDurable);
//            }
//            if (Down) 
//            {
//                m_dataBace.RecoveryLegDurable(m_maxDurable);
//            }
//            if (Right)
//            {
//                m_dataBace.RecoveryArmDurable(m_maxDurable);
//            }
//            if (Left)
//            {
//                m_dataBace.RecoveryBodyDurable(m_maxDurable);
//            }
//            m_dataBace.ChackUpdate();