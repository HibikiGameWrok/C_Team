using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//*|***|***|***|***|***|***|***|***|***|***|***|
// プレイヤーナンバー言い換え
//*|***|***|***|***|***|***|***|***|***|***|***|
using WarehousePlayer = WarehouseData.PlayerData.WarehousePlayer;
using PlayerDataNum = WarehouseData.PlayerData.WarehousePlayer.PlayerData_Number;
using PlayerData_Number_List = WarehouseData.PlayerData.WarehousePlayer.PlayerData_Number_List;

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
    // パーツの状態
    //*|***|***|***|***|***|***|***|***|***|***|***|
    enum PartsState
    {
        NORMAL,
        DAMAGED,
        STRONG,
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // ボディデータ
    //*|***|***|***|***|***|***|***|***|***|***|***|
    private Rigidbody2D m_player2D;
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 昇天
    //*|***|***|***|***|***|***|***|***|***|***|***|
    [Serializable]
    struct Ascension
    {
        public bool start;
        public float time;
        public bool end;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 昇天データ
    //*|***|***|***|***|***|***|***|***|***|***|***|
    [SerializeField]
    private Ascension m_ascension;
    private float m_deathTime;
    private float m_deathBombTime;
    private float m_ascensionTime;
    private float m_fadeOutTimeRate;
    private bool m_fadeOutFlag;
    private GameObject m_ascensionObject;
    private DeathExplosion m_ascensionEffect;

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
        //m_player2D.constraints = RigidbodyConstraints2D.FreezeRotation;
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
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 昇天データ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_ascension = new Ascension();
        m_ascension.start = false;
        m_ascension.time = 0.0f;
        m_ascension.end = false;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 昇天データ絵
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_ascensionObject = new GameObject("AscensionEffect");
        m_ascensionEffect = m_ascensionObject.AddComponent<DeathExplosion>();
        m_deathTime = 40.0f;
        m_deathBombTime = 20.0f;
        m_ascensionTime = 180.0f;
        m_fadeOutFlag = false;
        m_fadeOutTimeRate = MyCalculator.Division(90.0f, m_ascensionTime);
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // ゲーム共通ディレクターに自らが狙われると名乗り出る
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_directorIndex.SetObjectTargetCamera(m_playerCenter);
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
        // プレイヤーの状態確認
        //*|***|***|***|***|***|***|***|***|***|***|***|
        if (!m_ascension.start)
        {
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // プレイヤーを生かすも
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_playerMove.SetArive(true);
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // プレイヤーのパーツイメージを変える
            //*|***|***|***|***|***|***|***|***|***|***|***|
            PlayerImageUpdate();

        }
        else
        {
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // プレイヤーを殺すも
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_playerMove.SetArive(false);
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // プレイヤーを殺す
            //*|***|***|***|***|***|***|***|***|***|***|***|
            UpdateGameOver();
        }
        ////*|***|***|***|***|***|***|***|***|***|***|***|
        //// 操作更新
        ////*|***|***|***|***|***|***|***|***|***|***|***|
        //UpdateMove();
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // プレイヤー情報
    //*|***|***|***|***|***|***|***|***|***|***|***|
    void PlayerImageUpdate()
    {
        float hp = 1;
        bool strong = false;
        PartsState state = PartsState.NORMAL;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 腕のテクスチャ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        hp = m_dataBace.GetArmDurableParsent();
        strong = m_dataBace.GetArmStrong();
        state = GetPartsState(strong, hp);
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 分岐
        //*|***|***|***|***|***|***|***|***|***|***|***|
        switch (state)
        {
            case PartsState.NORMAL:
                {
                    //*|***|***|***|***|***|***|***|***|***|***|***|
                    // 腕の先
                    //*|***|***|***|***|***|***|***|***|***|***|***|
                    m_listAnime[(int)PlayerData_Number_List.RIGHTHAND].spriteData.m_dataNum = (int)PlayerDataNum.ATTACKBOAL;
                    m_listAnime[(int)PlayerData_Number_List.LEFTHAND].spriteData.m_dataNum = (int)PlayerDataNum.ATTACKBOAL;
                    //*|***|***|***|***|***|***|***|***|***|***|***|
                    // 腕の関節
                    //*|***|***|***|***|***|***|***|***|***|***|***|
                    m_listAnime[(int)PlayerData_Number_List.RARMJOINT].spriteData.m_dataNum = (int)PlayerDataNum.JOINTBOAL;
                    m_listAnime[(int)PlayerData_Number_List.LARMJOINT].spriteData.m_dataNum = (int)PlayerDataNum.JOINTBOAL;
                    break;
                }
            case PartsState.DAMAGED:
                {
                    //*|***|***|***|***|***|***|***|***|***|***|***|
                    // 腕の先
                    //*|***|***|***|***|***|***|***|***|***|***|***|
                    m_listAnime[(int)PlayerData_Number_List.RIGHTHAND].spriteData.m_dataNum = (int)PlayerDataNum.ATTACKBOAL_DAMAGE;
                    m_listAnime[(int)PlayerData_Number_List.LEFTHAND].spriteData.m_dataNum = (int)PlayerDataNum.ATTACKBOAL_DAMAGE;
                    //*|***|***|***|***|***|***|***|***|***|***|***|
                    // 腕の関節
                    //*|***|***|***|***|***|***|***|***|***|***|***|
                    m_listAnime[(int)PlayerData_Number_List.RARMJOINT].spriteData.m_dataNum = (int)PlayerDataNum.JOINTBOAL_DAMAGE;
                    m_listAnime[(int)PlayerData_Number_List.LARMJOINT].spriteData.m_dataNum = (int)PlayerDataNum.JOINTBOAL_DAMAGE;
                    break;
                }
            case PartsState.STRONG:
                {
                    //*|***|***|***|***|***|***|***|***|***|***|***|
                    // 腕の先
                    //*|***|***|***|***|***|***|***|***|***|***|***|
                    m_listAnime[(int)PlayerData_Number_List.RIGHTHAND].spriteData.m_dataNum = (int)PlayerDataNum.ATTACKBOAL_STRONG;
                    m_listAnime[(int)PlayerData_Number_List.LEFTHAND].spriteData.m_dataNum = (int)PlayerDataNum.ATTACKBOAL_STRONG;
                    //*|***|***|***|***|***|***|***|***|***|***|***|
                    // 腕の関節
                    //*|***|***|***|***|***|***|***|***|***|***|***|
                    m_listAnime[(int)PlayerData_Number_List.RARMJOINT].spriteData.m_dataNum = (int)PlayerDataNum.JOINTBOAL_STRONG;
                    m_listAnime[(int)PlayerData_Number_List.LARMJOINT].spriteData.m_dataNum = (int)PlayerDataNum.JOINTBOAL_STRONG;
                    break;
                }
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 体のテクスチャ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        hp = m_dataBace.GetBodyDurableParsent();
        strong = m_dataBace.GetBodyStrong();
        state = GetPartsState(strong, hp);
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 分岐
        //*|***|***|***|***|***|***|***|***|***|***|***|
        switch (state)
        {
            case PartsState.NORMAL:
                {
                    m_listAnime[(int)PlayerData_Number_List.BODYTOP].spriteData.m_dataNum = (int)PlayerDataNum.BODYTOP;
                    m_listAnime[(int)PlayerData_Number_List.BODYBOTTOM].spriteData.m_dataNum = (int)PlayerDataNum.BODYBOTTOM;
                    break;
                }
            case PartsState.DAMAGED:
                {
                    m_listAnime[(int)PlayerData_Number_List.BODYTOP].spriteData.m_dataNum = (int)PlayerDataNum.BODYTOP_DAMAGE;
                    m_listAnime[(int)PlayerData_Number_List.BODYBOTTOM].spriteData.m_dataNum = (int)PlayerDataNum.BODYBOTTOM_DAMAGE;
                    break;
                }
            case PartsState.STRONG:
                {
                    m_listAnime[(int)PlayerData_Number_List.BODYTOP].spriteData.m_dataNum = (int)PlayerDataNum.BODYTOP_STRONG;
                    m_listAnime[(int)PlayerData_Number_List.BODYBOTTOM].spriteData.m_dataNum = (int)PlayerDataNum.BODYBOTTOM_STRONG;
                    break;
                }
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 頭のテクスチャ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        hp = m_dataBace.GetHeadDurableParsent();
        strong = m_dataBace.GetHeadStrong();
        state = GetPartsState(strong, hp);
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 分岐
        //*|***|***|***|***|***|***|***|***|***|***|***|
        switch (state)
        {
            case PartsState.NORMAL:
                {
                    m_listAnime[(int)PlayerData_Number_List.PLAYERHEAD].spriteData.m_dataNum = (int)PlayerDataNum.PLAYERBOAL;
                    break;
                }
            case PartsState.DAMAGED:
                {
                    m_listAnime[(int)PlayerData_Number_List.PLAYERHEAD].spriteData.m_dataNum = (int)PlayerDataNum.PLAYERBOAL_DAMAGE;
                    break;
                }
            case PartsState.STRONG:
                {
                    m_listAnime[(int)PlayerData_Number_List.PLAYERHEAD].spriteData.m_dataNum = (int)PlayerDataNum.PLAYERBOAL_STRONG;
                    break;
                }
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 脚のテクスチャ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        hp = m_dataBace.GetLegDurableParsent();
        strong = m_dataBace.GetLegStrong();
        state = GetPartsState(strong, hp);
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 分岐
        //*|***|***|***|***|***|***|***|***|***|***|***|
        switch (state)
        {
            case PartsState.NORMAL:
                {
                    //*|***|***|***|***|***|***|***|***|***|***|***|
                    // 脚の先
                    //*|***|***|***|***|***|***|***|***|***|***|***|
                    m_listAnime[(int)PlayerData_Number_List.RIGHTLEG].spriteData.m_dataNum = (int)PlayerDataNum.RIGHTLEG;
                    m_listAnime[(int)PlayerData_Number_List.LEFTLEG].spriteData.m_dataNum = (int)PlayerDataNum.LEFTLEG;
                    //*|***|***|***|***|***|***|***|***|***|***|***|
                    // 脚の関節
                    //*|***|***|***|***|***|***|***|***|***|***|***|
                    m_listAnime[(int)PlayerData_Number_List.RLEGJOINT].spriteData.m_dataNum = (int)PlayerDataNum.JOINTBOAL;
                    m_listAnime[(int)PlayerData_Number_List.LLEGJOINT].spriteData.m_dataNum = (int)PlayerDataNum.JOINTBOAL;
                    break;
                }
            case PartsState.DAMAGED:
                {
                    //*|***|***|***|***|***|***|***|***|***|***|***|
                    // 脚の先
                    //*|***|***|***|***|***|***|***|***|***|***|***|
                    m_listAnime[(int)PlayerData_Number_List.RIGHTLEG].spriteData.m_dataNum = (int)PlayerDataNum.RIGHTLEG_DAMAGE;
                    m_listAnime[(int)PlayerData_Number_List.LEFTLEG].spriteData.m_dataNum = (int)PlayerDataNum.LEFTLEG_DAMAGE;
                    //*|***|***|***|***|***|***|***|***|***|***|***|
                    // 脚の関節
                    //*|***|***|***|***|***|***|***|***|***|***|***|
                    m_listAnime[(int)PlayerData_Number_List.RLEGJOINT].spriteData.m_dataNum = (int)PlayerDataNum.JOINTBOAL_DAMAGE;
                    m_listAnime[(int)PlayerData_Number_List.LLEGJOINT].spriteData.m_dataNum = (int)PlayerDataNum.JOINTBOAL_DAMAGE;
                    break;
                }
            case PartsState.STRONG:
                {
                    //*|***|***|***|***|***|***|***|***|***|***|***|
                    // 脚の先
                    //*|***|***|***|***|***|***|***|***|***|***|***|
                    m_listAnime[(int)PlayerData_Number_List.RIGHTLEG].spriteData.m_dataNum = (int)PlayerDataNum.RIGHTLEG_STRONG;
                    m_listAnime[(int)PlayerData_Number_List.LEFTLEG].spriteData.m_dataNum = (int)PlayerDataNum.LEFTLEG_STRONG;
                    //*|***|***|***|***|***|***|***|***|***|***|***|
                    // 脚の関節
                    //*|***|***|***|***|***|***|***|***|***|***|***|
                    m_listAnime[(int)PlayerData_Number_List.RLEGJOINT].spriteData.m_dataNum = (int)PlayerDataNum.JOINTBOAL_STRONG;
                    m_listAnime[(int)PlayerData_Number_List.LLEGJOINT].spriteData.m_dataNum = (int)PlayerDataNum.JOINTBOAL_STRONG;
                    break;
                }
        }
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // パーツダメージ
    //*|***|***|***|***|***|***|***|***|***|***|***|
    private PartsState GetPartsState(bool strong, float hp)
    {
        float damageLine = 0.5f;
        PartsState state;
        if (strong)
        {
            state = PartsState.STRONG;
        }
        else if (hp < damageLine)
        {
            state = PartsState.DAMAGED;
        }
        else
        {
            state = PartsState.NORMAL;
        }
        return state;
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
            power *= 1.0f;
            if (rightPower)
            {
                power.x = power.x * -1;
            }
            m_playerMove.AddForsePower(power);
            //m_player2D.AddForce(power);
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
        // 爆発後
        //*|***|***|***|***|***|***|***|***|***|***|***|
        if (m_ascension.end)
        {
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 爆発後は操作不可
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_controllerData.ResetCommond();
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
        // 死んでいるなら
        //*|***|***|***|***|***|***|***|***|***|***|***|
        if (m_ascension.start)
        {
            m_myAnime.SetInteger("MoveEnum", 5);
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
    // 志望しました。
    //*|***|***|***|***|***|***|***|***|***|***|***|
    void UpdateGameOver()
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 爆発モーション
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_ascension.time = m_ascension.time + 1;


        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 腕パーツ耐久チェック
        //*|***|***|***|***|***|***|***|***|***|***|***|
        if (m_dataBace.GetArmDurable() == 0.0f)
        {
            UpdateWhiteArm();
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 体パーツ耐久チェック
        //*|***|***|***|***|***|***|***|***|***|***|***|
        if (m_dataBace.GetBodyDurable() == 0.0f)
        {
            UpdateWhiteBody();
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 頭パーツ耐久チェック
        //*|***|***|***|***|***|***|***|***|***|***|***|
        if (m_dataBace.GetHeadDurable() == 0.0f)
        {
            UpdateWhiteHead();
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 脚パーツ耐久チェック
        //*|***|***|***|***|***|***|***|***|***|***|***|
        if (m_dataBace.GetLegDurable() == 0.0f)
        {
            UpdateWhiteLeg();
        }


        //*|***|***|***|***|***|***|***|***|***|***|***|
        // フェードアウト更新
        //*|***|***|***|***|***|***|***|***|***|***|***|
        if (!m_fadeOutFlag)
        {
            float parsent = MyCalculator.Division(m_ascension.time, m_ascensionTime);
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // フェードアウトトリガー
            //*|***|***|***|***|***|***|***|***|***|***|***|
            if (m_ascension.end && m_fadeOutTimeRate <= parsent)
            {
                float timeN = m_ascensionTime - m_ascension.time;
                //*|***|***|***|***|***|***|***|***|***|***|***|
                // フェードアウト！
                //*|***|***|***|***|***|***|***|***|***|***|***|
                m_directorRocketIndex.SetFadeAlpha(0.0f);
                m_directorRocketIndex.StartFadeOut(MyCalculator.Division(1.0f, timeN));
                m_fadeOutFlag = true;
            }
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // BGMストッパー
        //*|***|***|***|***|***|***|***|***|***|***|***|
        if (m_ascension.start && m_ascension.end)
        {
            m_directorRocketIndex.SetBGMFlow(false);
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 爆発！
        //*|***|***|***|***|***|***|***|***|***|***|***|
        if (!m_ascension.end && m_ascension.time >= m_deathTime)
        {
            m_ascension.end = true;
            m_ascension.time = 0;
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 吹き飛べ！
            //*|***|***|***|***|***|***|***|***|***|***|***|
            UpdateWhite();
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // プレイヤーを生かすも
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_playerMove.SetStop(true);
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 風前
        //*|***|***|***|***|***|***|***|***|***|***|***|
        if (m_ascension.start && m_ascension.end && m_ascension.time >= m_ascensionTime)
        {
            m_ascension.time = m_ascensionTime;
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 志望完了
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_directorIndex.SetGameOverFlag();
        }
        else
        {
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 爆発の
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_ascensionEffect.SetPoint(GetPlayerPositon());
        }
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 志望しました。
    //*|***|***|***|***|***|***|***|***|***|***|***|
    void GameOverPlayer()
    {
        bool rightPower = m_playerMove.GetRightPowerFlag();
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 飛び上がる
        //*|***|***|***|***|***|***|***|***|***|***|***|
        if (!m_ascension.start)
        {
            m_ascension.start = true;
            m_ascension.time = 0;
            m_ascension.end = false;
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 幕引き準備
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_fadeOutFlag = false;
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 志望する
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_directorIndex.SetGameOverAnimation();
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 爆発の
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_ascensionEffect.AwakeON();
            m_ascensionEffect.SetMaxTime(m_deathTime);
            m_ascensionEffect.SetMaxSecondTime(m_deathBombTime);
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 吹き飛べ！
            //*|***|***|***|***|***|***|***|***|***|***|***|
            Vector2 power = ChangeData.AngleDegToVector2(80.0f);
            power = ChangeData.AngleDegToVector2(10.0f);
            power = ChangeData.AngleDegToVector2(45.0f);
            power *= 10.0f;
            if (rightPower)
            {
                power.x = power.x * -1;
            }
            m_playerMove.AddForsePower(power);
            //m_player2D.AddForce(power);
        }
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // プレイヤー情報取得
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public Vector3 GetPlayerPositon()
    {
        return m_playerMove.GetPosition();
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // プレイヤーパーツ位置情報取得
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public Vector3 GetPlayerArmRightPositon()
    {
        return m_playerMove.GetPlayerArmRightPositon();
    }
    public Vector3 GetPlayerArmLeftPositon()
    {
        return m_playerMove.GetPlayerArmLeftPositon();
    }
    public Vector3 GetPlayerBodyPositon()
    {
        return m_playerMove.GetPlayerBodyPositon();
    }
    public Vector3 GetPlayerHeadPositon()
    {
        return m_playerMove.GetPlayerHeadPositon();
    }
    public Vector3 GetPlayerLegRightPositon()
    {
        return m_playerMove.GetPlayerLegRightPositon();
    }
    public Vector3 GetPlayerLegLeftPositon()
    {
        return m_playerMove.GetPlayerLegLeftPositon();
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // プレイヤー情報取得
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public float GetScale()
    {
        return m_playerMove.GetScale();
    }
    public bool GetGroundFlag()
    {
        return m_playerMove.GetGroundFlag();
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
