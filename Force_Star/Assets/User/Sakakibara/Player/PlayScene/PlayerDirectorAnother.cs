﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    // プレイヤー情報
    //*|***|***|***|***|***|***|***|***|***|***|***|
    void AwakePlayerUI()
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // データベース
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_dataBace = new PlayerDataBace();
        m_dataBace.ResetAll(7200);
        m_dataBace.ResetPartsDurable(2000.0f);
        m_dataBace.ResetStars(0, 2000);
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
        // データベースUIに反映
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_dataUI.SetAirGaugeNumber(time);
        m_dataUI.SetStarGaugeNumber(starParsent);
        m_dataUI.SetHaveStarNumber(haveStar);
        m_dataUI.SetNeedStarNumber(needStar);



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
        // 進行度を記録
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_dataRecoveryUI.SetProgressParsentNumber(m_progressParsent);



        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 回復判定
        //*|***|***|***|***|***|***|***|***|***|***|***|
        if (m_pushRecoveryKey)
        {
            //bool move = m_controller.ChackCompassMove();
            //float angle = m_controller.ChackCompassAngle();
            //float dif = 15.0f;

            ////*|***|***|***|***|***|***|***|***|***|***|***|
            //// コンパス上
            ////*|***|***|***|***|***|***|***|***|***|***|***|
            //float upA = 90.0f;
            //float upStart = upA - dif;
            //float upEnd = upA + dif;
            //bool Up = false;
            //if(UpR)
            //if (ChangeData.Between(angle, upA - dif, upA + dif))
            //{
            //    Up = true;
            //}

            //float downA = 180.0f;
            //float rightA = 0.0f;
            //float leftA = 270.0f;

            //float downStart = downA - dif;
            //float rightStart = rightA - dif;
            //float leftStart = leftA - dif;
            //float downEnd = downA + dif;
            //float rightEnd = rightA + dif;
            //float leftEnd = leftA + dif;

            //bool DownR = false;
            //bool RightR = false;
            //bool LeftR = false;


            //bool Down = false;
            //bool Right = false;
            //bool Left = false;
            ////bool Up = m_controller.ChackStartTrigger();
            ////bool Down = m_controller.ChackTrampleTrigger();
            ////bool Right = m_controller.ChackJumpTrigger();
            ////bool Left = m_controller.ChackAttackTrigger();

            //if (ChangeData.Between(angle, upA - dif, upA + dif))
            //{
            //    Up = true;
            //}
            //if (ChangeData.Between(angle, downA - dif, downA + dif))
            //{
            //    Down = true;
            //}
            //if (ChangeData.SetDeg360 angle, rightA - dif, rightA + dif))
            //{
            //    Right = true;
            //}
            //if (ChangeData.Between(angle, leftA - dif, leftA + dif))
            //{
            //    Left = true;
            //}

            bool Up = m_controller.ChackStartTrigger();
            bool Down = m_controller.ChackTrampleTrigger();
            bool Right = m_controller.ChackJumpTrigger();
            bool Left = m_controller.ChackAttackTrigger();

            if (Up)
            {
                m_dataBace.RecoveryHeadDurable(2000.0f);
            }
            if (Down) 
            {
                m_dataBace.RecoveryLegDurable(2000.0f);
            }
            if (Right)
            {
                m_dataBace.RecoveryArmDurable(2000.0f);
            }
            if (Left)
            {
                m_dataBace.RecoveryBodyDurable(2000.0f);
            }
            m_dataBace.ChackUpdate();
        }


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
        }


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
    }
}
