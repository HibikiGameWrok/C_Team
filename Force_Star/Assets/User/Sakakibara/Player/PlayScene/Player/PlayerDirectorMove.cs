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
//*|***|***|***|***|***|***|***|***|***|***|***|
// 音言い換え
//*|***|***|***|***|***|***|***|***|***|***|***|
using PartsID = PlayStaticData.PartsID;
//*|***|***|***|***|***|***|***|***|***|***|***|
// 番号データ共通
//*|***|***|***|***|***|***|***|***|***|***|***|
using WarehouseObject = WarehouseData.WarehouseObject;
using CommonImageNum = WarehouseData.WarehouseStaticData.Object2D_Numbers_Common;
using AppImageNum = WarehouseData.WarehouseStaticData.Object2D_Numbers_App;
using Symbol_ENUM = WarehouseData.WarehouseStaticData.Symbol_ENUM;
//*|***|***|***|***|***|***|***|***|***|***|***|
// 音楽
//*|***|***|***|***|***|***|***|***|***|***|***|
using SoundID = SEManager.SoundID;


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
    private bool m_punchFlagSE = false;
    private bool m_punchHit = false;
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // パーツの状態
    //*|***|***|***|***|***|***|***|***|***|***|***|
    enum PartsState
    {
        NORMAL,
        DAMAGED,
        STRONG,
    }
    GameObject m_damageExprosionObject;
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 危険ライン
    //*|***|***|***|***|***|***|***|***|***|***|***|
    float m_damageLine;
    float m_damageFireLine;
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 危険ラインを超えるもの
    //*|***|***|***|***|***|***|***|***|***|***|***|
    bool m_damageArm;
    bool m_damageBody;
    bool m_damageHead;
    bool m_damageLeg;

    bool m_damageFireArm;
    bool m_damageFireBody;
    bool m_damageFireHead;
    bool m_damageFireLeg;
    ExprosionColor m_exprosionArm;
    ExprosionColor m_exprosionBody;
    ExprosionColor m_exprosionHead;
    ExprosionColor m_exprosionLeg;
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
    // 離脱
    //*|***|***|***|***|***|***|***|***|***|***|***|
    [Serializable]
    struct Fall
    {
        public bool awake;
        public bool start;
        public float height;
        public bool end;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 離脱データ
    //*|***|***|***|***|***|***|***|***|***|***|***|
    [SerializeField]
    private Fall m_fall;
    private Vector3 m_startPoint;
    private float m_heightMax;
    private float m_heightTime;
    private float m_bounseTime;
    private float m_bounseMax;
    private int m_fallAnimeBefore;
    private int m_fallAnimeAfter;


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
        // アニメーション用取得フラグ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_brakeTime = 0.0f;
        m_hashBackBrakeT = false;
        m_hashFlontBrakeT = false;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // パーツの状態
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_damageExprosionObject = new GameObject("ExprosionObject");
        m_damageLine = 0.5f;
        m_damageFireLine = 0.25f;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 危険ラインを超えるもの
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_damageArm = false;
        m_damageBody = false;
        m_damageHead = false;
        m_damageLeg = false;
        m_damageFireArm = false;
        m_damageFireBody = false;
        m_damageFireHead = false;
        m_damageFireLeg = false;

        m_exprosionArm = m_damageExprosionObject.AddComponent<ExprosionColor>();
        m_exprosionBody = m_damageExprosionObject.AddComponent<ExprosionColor>();
        m_exprosionHead = m_damageExprosionObject.AddComponent<ExprosionColor>();
        m_exprosionLeg = m_damageExprosionObject.AddComponent<ExprosionColor>();
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
        m_punchFlagSE = false;
        m_punchHit = false;
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
    // 落下するプレイヤー
    //*|***|***|***|***|***|***|***|***|***|***|***|
    void FallPlayerStart()
    {

        m_heightMax = 120.0f;
        m_heightTime = 120.0f;
        m_bounseTime = 3.0f;
        m_bounseMax = 3.0f;

        m_fallAnimeBefore = Animator.StringToHash("Base Layer.Basis.イントロ前");
        m_fallAnimeAfter = Animator.StringToHash("Base Layer.Basis.イントロ後");


        m_startPoint = new Vector3(0, -1.4f, 0);
        m_fall = new Fall();
        m_fall.awake = true;
        m_fall.start = false;
        m_fall.height = m_heightMax;
        m_fall.end = false;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // ぽ四ぽ四プレイヤー
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_playerMove.SetArive(false);
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // アニメ実行
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_myAnime.SetBool("FallAnime", true);
    }


    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 落下するプレイヤー
    //*|***|***|***|***|***|***|***|***|***|***|***|
    void FallUpdatePlayer()
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 落下
        //*|***|***|***|***|***|***|***|***|***|***|***|
        Vector3 truePoint = m_startPoint;
        truePoint.y = m_startPoint.y + m_fall.height;

        m_fall.height -= MyCalculator.Division(m_heightMax, m_heightTime);
        if(m_fall.height <= 0)
        {
            m_fall.height = 0;

            truePoint.y = m_startPoint.y + m_fall.height;

            if (!m_fall.start)
            {
                //*|***|***|***|***|***|***|***|***|***|***|***|
                // 星の出現
                //*|***|***|***|***|***|***|***|***|***|***|***|
                m_directorIndex.ApplyStarBounce(truePoint, 180.0f, 90.0f, 0.45f, 0.2f, 3000.0f, 1500.0f, 10, 100);
                m_directorIndex.ApplyStarBounce(truePoint, 0.0f, 90.0f, 0.45f, 0.2f, 3000.0f, 1500.0f, 10, 100);
                //*|***|***|***|***|***|***|***|***|***|***|***|
                // アニメ実行
                //*|***|***|***|***|***|***|***|***|***|***|***|
                m_myAnime.SetBool("FallAnime", false);

                m_bounseTime = m_bounseMax;
            }

            m_fall.start = true;
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 落下
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_playerCenter.transform.position = truePoint;
        if (m_fall.start && !m_fall.end)
        {
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // ぽ四ぽ四タイム
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_bounseTime -= Time.deltaTime;
            if (m_bounseTime <= 0)
            {
                m_bounseTime = 0;
                m_fall.end = true;
                m_fall.awake = false;
                //*|***|***|***|***|***|***|***|***|***|***|***|
                // ぽ四ぽ四プレイヤー
                //*|***|***|***|***|***|***|***|***|***|***|***|
                m_playerMove.SetArive(true);
            }
        }


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
        // ゲームクリアしている？
        //*|***|***|***|***|***|***|***|***|***|***|***|
        bool clearAnime = m_directorIndex.GetClearAnimation();
        if (clearAnime)
        {
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // プレイヤーを消す
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_playerMove.SetArive(false);
            UpdateWhite();
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 全てとおさらば。
            //*|***|***|***|***|***|***|***|***|***|***|***|
            ClearAllFlags();
            return;
        }
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

            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 謎
            //*|***|***|***|***|***|***|***|***|***|***|***|
            UpdateHushigi();
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // やったぜ。
            //*|***|***|***|***|***|***|***|***|***|***|***|
            UpdateYeah();
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
        // パーツの状態
        //*|***|***|***|***|***|***|***|***|***|***|***|
        if (hp < m_damageLine)
        {
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // パーツの状態
            //*|***|***|***|***|***|***|***|***|***|***|***|
            if (!m_damageArm)
            {
                m_exprosionArm.SetBomb(AppImageNum.EXPROSION_RED, 6.0f);
            }
            m_damageArm = true;
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 火が出る危険域
            //*|***|***|***|***|***|***|***|***|***|***|***|
            if (hp < m_damageFireLine)
            {
                if(!m_damageFireArm)
                {
                    m_exprosionArm.SetFire(AppImageNum.EXPROSION_RED, 6.0f);
                }
                m_damageFireArm = true;
            }
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 爆発位置
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_exprosionArm.SetPoint(GetPlayerPositon());
        }
        else
        {
            m_damageArm = false;
            m_damageFireArm = false;
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 爆発位置
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_exprosionArm.SetActive(false);
        }
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
        // パーツの状態
        //*|***|***|***|***|***|***|***|***|***|***|***|
        if (hp < m_damageLine)
        {
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // パーツの状態
            //*|***|***|***|***|***|***|***|***|***|***|***|
            if (!m_damageBody)
            {
                m_exprosionBody.SetBomb(AppImageNum.EXPROSION_YELLOW, 6.0f);
            }
            m_damageBody = true;
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 火が出る危険域
            //*|***|***|***|***|***|***|***|***|***|***|***|
            if (hp < m_damageFireLine)
            {
                if (!m_damageFireBody)
                {
                    m_exprosionBody.SetFire(AppImageNum.EXPROSION_YELLOW, 6.0f);
                }
                m_damageFireBody = true;
            }
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 爆発位置
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_exprosionBody.SetPoint(GetPlayerPositon());
        }
        else
        {
            m_damageBody = false;
            m_damageFireBody = false;
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 爆発位置
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_exprosionBody.SetActive(false);
        }
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
        // パーツの状態
        //*|***|***|***|***|***|***|***|***|***|***|***|
        if (hp < m_damageLine)
        {
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // パーツの状態
            //*|***|***|***|***|***|***|***|***|***|***|***|
            if (!m_damageHead)
            {
                m_exprosionHead.SetBomb(AppImageNum.EXPROSION_BLUE, 6.0f);
            }
            m_damageHead = true;
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 火が出る危険域
            //*|***|***|***|***|***|***|***|***|***|***|***|
            if (hp < m_damageFireLine)
            {
                if (!m_damageFireHead)
                {
                    m_exprosionHead.SetFire(AppImageNum.EXPROSION_BLUE, 6.0f);
                }
                m_damageFireHead = true;
            }
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 爆発位置
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_exprosionHead.SetPoint(GetPlayerPositon());
        }
        else
        {
            m_damageHead = false;
            m_damageFireHead = false;
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 爆発位置
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_exprosionHead.SetActive(false);
        }
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
        // パーツの状態
        //*|***|***|***|***|***|***|***|***|***|***|***|
        if (hp < m_damageLine)
        {
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // パーツの状態
            //*|***|***|***|***|***|***|***|***|***|***|***|
            if (!m_damageLeg)
            {
                m_exprosionLeg.SetBomb(AppImageNum.EXPROSION_GREEN, 6.0f);
            }
            m_damageLeg = true;
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 火が出る危険域
            //*|***|***|***|***|***|***|***|***|***|***|***|
            if (hp < m_damageFireLine)
            {
                if (!m_damageFireLeg)
                {
                    m_exprosionLeg.SetFire(AppImageNum.EXPROSION_GREEN, 6.0f);
                }
                m_damageFireLeg = true;
            }
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 爆発位置
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_exprosionLeg.SetPoint(GetPlayerPositon());
        }
        else
        {
            m_damageLeg = false;
            m_damageFireLeg = false;
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 爆発位置
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_exprosionLeg.SetActive(false);
        }
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
        PartsState state;
        if (strong)
        {
            state = PartsState.STRONG;
        }
        else if (hp < m_damageLine)
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
        // パンチは9割進むと終了可能
        //*|***|***|***|***|***|***|***|***|***|***|***|
        if (m_animePunch)
        {
            float time = state.normalizedTime;
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // ブーン
            //*|***|***|***|***|***|***|***|***|***|***|***|
            if (time > 0.25f)
            {
                //*|***|***|***|***|***|***|***|***|***|***|***|
                // アニメーション中音出す一回
                //*|***|***|***|***|***|***|***|***|***|***|***|
                if (!m_punchFlagSE)
                {
                    //*|***|***|***|***|***|***|***|***|***|***|***|
                    // 音
                    //*|***|***|***|***|***|***|***|***|***|***|***|
                    m_directorIndex.PlaySoundEffect(SoundID.PUNCHSWING);
                }
                m_punchFlagSE = true;
                m_punchHit = true;
            }

            if (time > 0.9f)
            {
                //*|***|***|***|***|***|***|***|***|***|***|***|
                // 終了可能
                //*|***|***|***|***|***|***|***|***|***|***|***|
                m_punchFlag = false;
                m_punchTrigger = false;
                m_punchHit = false;
            }
        }
        else
        {
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // アニメーション終了
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_punchFlagSE = false;
            m_punchHit = false;
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
                //*|***|***|***|***|***|***|***|***|***|***|***|
                // 右の方向か？
                //*|***|***|***|***|***|***|***|***|***|***|***|
                m_playerMove.SetRightFlag(rightPower);
            }
        }
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
        // ゲームクリアしている？
        //*|***|***|***|***|***|***|***|***|***|***|***|
        bool clearAnime = m_directorIndex.GetClearAnimation();
        if (clearAnime)
        {
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // ゲームクリア後は操作不可
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_controllerData.ResetCommond();
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
            m_playerMove.SetAttackFlag(m_punchHit);
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
        // 全てとおさらば。
        //*|***|***|***|***|***|***|***|***|***|***|***|
        ClearAllFlags();

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
