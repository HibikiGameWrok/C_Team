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

public class PlayerMove : MonoBehaviour
{
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // ボディデータ
    //*|***|***|***|***|***|***|***|***|***|***|***|
    private Rigidbody2D m_rigid2D;
    private BoxCollider2D m_box2D;
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 足データ
    //*|***|***|***|***|***|***|***|***|***|***|***|
    private DebugPlayerParts m_partsLeg2D;
    private GameObject m_rigidOnlyLeg;
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 攻撃ボールデータ
    //*|***|***|***|***|***|***|***|***|***|***|***|
    private GameObject m_attackBoalObject;
    private Vector2 m_partsAttackPos;
    private Vector2 m_partsAttackSize;
    private PlayerPartsAttack m_partsAttack2D;
    private GameObject m_rigitOnlyAttack;

    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 当たりの判定腕
    //*|***|***|***|***|***|***|***|***|***|***|***|
    private GameObject m_hitFlagArmObject_L;
    private GameObject m_hitFlagArmObject_R;
    private PlayerPartsPain m_hitFlagArmParts_L;
    private PlayerPartsPain m_hitFlagArmParts_R;
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 当たりの判定
    //*|***|***|***|***|***|***|***|***|***|***|***|
    private bool m_hitFlagArm;

    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 当たりの判定体
    //*|***|***|***|***|***|***|***|***|***|***|***|
    private GameObject m_hitFlagBodyObject;
    private PlayerPartsPain m_hitFlagBodyParts;
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 当たりの判定
    //*|***|***|***|***|***|***|***|***|***|***|***|
    private bool m_hitFlagBody;

    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 当たりの判定頭
    //*|***|***|***|***|***|***|***|***|***|***|***|
    private GameObject m_hitFlagHeadObject;
    private PlayerPartsPain m_hitFlagHeadParts;
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 当たりの判定
    //*|***|***|***|***|***|***|***|***|***|***|***|
    private bool m_hitFlagHead;
    
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 当たりの判定脚
    //*|***|***|***|***|***|***|***|***|***|***|***|
    private GameObject m_hitFlagLegObject_L;
    private GameObject m_hitFlagLegObject_R;
    private PlayerPartsPain m_hitFlagLegParts_L;
    private PlayerPartsPain m_hitFlagLegParts_R;
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 当たりの判定
    //*|***|***|***|***|***|***|***|***|***|***|***|
    private bool m_hitFlagLeg;



    //*|***|***|***|***|***|***|***|***|***|***|***|
    // コントローラー情報を転換せよ
    //*|***|***|***|***|***|***|***|***|***|***|***|
    PlayerControllerData m_controllerData;
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 着地フラグ
    //*|***|***|***|***|***|***|***|***|***|***|***|
    [SerializeField]
    private bool m_groundFlagFlame;
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 動き、向きフラグ
    //*|***|***|***|***|***|***|***|***|***|***|***|
    [SerializeField]
    private bool m_addPower;
    [SerializeField]
    private bool m_moveingPower;
    [SerializeField]
    private bool m_rightArrow;
    [SerializeField]
    private bool m_rightPower;
    [SerializeField]
    private bool m_reverseArrow;
    [SerializeField]
    private float m_movePowerX;
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 動き、向きフラグ(マスク)
    //*|***|***|***|***|***|***|***|***|***|***|***|
    private bool m_nextReverseFlag;
    private bool m_nowReverseFlag;
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 攻撃中フラグ
    //*|***|***|***|***|***|***|***|***|***|***|***|
    private bool m_attackingFlag;


    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 関係：コマンド全般
    //*|***|***|***|***|***|***|***|***|***|***|***|
    private Vector2 m_addForce;
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // これが出来たときに
    //*|***|***|***|***|***|***|***|***|***|***|***|
    void Awake()
    {
        gameObject.layer = 8;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // ボディデータ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        this.m_rigid2D = gameObject.AddComponent<Rigidbody2D>();
        this.m_box2D = gameObject.AddComponent<BoxCollider2D>();

        this.m_rigid2D.sleepMode = RigidbodySleepMode2D.NeverSleep;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 親たちのデータ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        PlayerData_Number_List partsListNum;
        string partsName;
        GameObject parent = null;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 足データ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_rigidOnlyLeg = new GameObject("rigidLeg");
        m_rigidOnlyLeg.transform.parent = gameObject.transform;
        this.m_partsLeg2D = m_rigidOnlyLeg.AddComponent<DebugPlayerParts>();
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 当たりの判定
        //*|***|***|***|***|***|***|***|***|***|***|***|
        AwakeAttackParts();
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 当たりの判定
        //*|***|***|***|***|***|***|***|***|***|***|***|
        AwakeHitParts();
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 起動：ジャンプ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        AwakwJumpCommond();
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 向きフラグ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_addPower = false;
        m_moveingPower = false;
        m_rightArrow = false;
        m_rightPower = false;
        m_reverseArrow = false;
        m_movePowerX = 0.0f;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 当たりの判定
    //*|***|***|***|***|***|***|***|***|***|***|***|
    void AwakeAttackParts()
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 親たちのデータ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        PlayerData_Number_List partsListNum;
        string partsName;
        GameObject parent = null;

        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 攻撃ボールデータ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        partsListNum = PlayerData_Number_List.LEFTHAND;
        partsName = partsListNum.ToString();
        parent = GameDataPublic.SearchChildAllHierarchy(gameObject, partsName);
        m_rigitOnlyAttack = new GameObject("attackBoal");
        m_rigitOnlyAttack.transform.parent = parent.transform;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 攻撃ボールのタグ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        this.m_partsAttack2D = m_rigitOnlyAttack.AddComponent<PlayerPartsAttack>();
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 攻撃中フラグ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_attackingFlag = false;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 当たりの判定
    //*|***|***|***|***|***|***|***|***|***|***|***|
    void AwakeHitParts()
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 親たちのデータ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        PlayerData_Number_List partsListNum;
        string partsName;
        GameObject parent = null;

        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 当たり判定脚のタグ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 左
        //*|***|***|***|***|***|***|***|***|***|***|***|
        partsListNum = PlayerData_Number_List.LEFTHAND;
        partsName = partsListNum.ToString();
        parent = GameDataPublic.SearchChildAllHierarchy(gameObject, partsName);
        m_hitFlagArmObject_L = new GameObject("hitFlagArm_L");
        m_hitFlagArmObject_L.transform.parent = parent.transform;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 右
        //*|***|***|***|***|***|***|***|***|***|***|***|
        partsListNum = PlayerData_Number_List.RIGHTHAND;
        partsName = partsListNum.ToString();
        parent = GameDataPublic.SearchChildAllHierarchy(gameObject, partsName);
        m_hitFlagArmObject_R = new GameObject("hitFlagArm_R");
        m_hitFlagArmObject_R.transform.parent = parent.transform;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 当たり判定脚
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 左
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_hitFlagArmObject_L.tag = WarehousePlayer.GetTag_PlayerHitLegParts();
        this.m_hitFlagArmParts_L = m_hitFlagArmObject_L.AddComponent<PlayerPartsPain>();
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 右
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_hitFlagArmObject_R.tag = WarehousePlayer.GetTag_PlayerHitLegParts();
        this.m_hitFlagArmParts_R = m_hitFlagArmObject_R.AddComponent<PlayerPartsPain>();

        

        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 当たり判定体のタグ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        partsListNum = PlayerData_Number_List.BODYTOP;
        partsName = partsListNum.ToString();
        parent = GameDataPublic.SearchChildAllHierarchy(gameObject, partsName);
        m_hitFlagBodyObject = new GameObject("hitFlagBody");
        m_hitFlagBodyObject.transform.parent = parent.transform;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 当たり判定頭
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_hitFlagBodyObject.tag = WarehousePlayer.GetTag_PlayerHitBodyParts();
        this.m_hitFlagBodyParts = m_hitFlagBodyObject.AddComponent<PlayerPartsPain>();



        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 当たり判定頭のタグ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        partsListNum = PlayerData_Number_List.PLAYERHEAD;
        partsName = partsListNum.ToString();
        parent = GameDataPublic.SearchChildAllHierarchy(gameObject, partsName);
        m_hitFlagHeadObject = new GameObject("hitFlagHead");
        m_hitFlagHeadObject.transform.parent = parent.transform;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 当たり判定頭
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_hitFlagHeadObject.tag = WarehousePlayer.GetTag_PlayerHitHeadParts();
        this.m_hitFlagHeadParts = m_hitFlagHeadObject.AddComponent<PlayerPartsPain>();



        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 当たり判定脚のタグ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 左
        //*|***|***|***|***|***|***|***|***|***|***|***|
        partsListNum = PlayerData_Number_List.LEFTLEG;
        partsName = partsListNum.ToString();
        parent = GameDataPublic.SearchChildAllHierarchy(gameObject, partsName);
        m_hitFlagLegObject_L = new GameObject("hitFlagLeg_L");
        m_hitFlagLegObject_L.transform.parent = parent.transform;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 右
        //*|***|***|***|***|***|***|***|***|***|***|***|
        partsListNum = PlayerData_Number_List.RIGHTLEG;
        partsName = partsListNum.ToString();
        parent = GameDataPublic.SearchChildAllHierarchy(gameObject, partsName);
        m_hitFlagLegObject_R = new GameObject("hitFlagLeg_R");
        m_hitFlagLegObject_R.transform.parent = parent.transform;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 当たり判定脚
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 左
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_hitFlagLegObject_L.tag = WarehousePlayer.GetTag_PlayerHitLegParts();
        this.m_hitFlagLegParts_L = m_hitFlagLegObject_L.AddComponent<PlayerPartsPain>();
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 右
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_hitFlagLegObject_R.tag = WarehousePlayer.GetTag_PlayerHitLegParts();
        this.m_hitFlagLegParts_R = m_hitFlagLegObject_R.AddComponent<PlayerPartsPain>();
    }


    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 起動：ジャンプ
    //*|***|***|***|***|***|***|***|***|***|***|***|
    void AwakwJumpCommond()
    {


    }
    void Start()
    {
        Vector2 sizeLeg = new Vector2(1.0f, 0.2f);
        Vector2 pointLeg = new Vector2(0.0f, -0.5f);
        m_partsLeg2D.SetPointSize(pointLeg, sizeLeg);
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // プレイヤー大きさ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        gameObject.transform.localScale = Vector3.one * 2.0f;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 攻撃ボールデータ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        //gameObject.transform.childCount;
        //gameObject.transform.chi;

        //for (int index = 0; ; index++)
        //m_attackBoalObject = 

        m_partsAttackPos = new Vector2(0.0f, 0.0f);
        m_partsAttackSize = new Vector2(0.5f, 0.5f);
        m_partsAttack2D.SetPointSize(m_partsAttackPos, m_partsAttackSize);
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 当たりの判定
        //*|***|***|***|***|***|***|***|***|***|***|***|
        StartHitParts();
    }

    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 当たりの判定
    //*|***|***|***|***|***|***|***|***|***|***|***|
    void StartHitParts()
    {
        Vector2 pos = new Vector2(0.0f, 0.0f);
        Vector2 size = new Vector2(0.5f, 0.5f);
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 当たり判定腕
        //*|***|***|***|***|***|***|***|***|***|***|***|
        pos = new Vector2(0.0f, 0.0f);
        size = new Vector2(0.5f, 0.5f);
        m_hitFlagArmParts_L.SetPointSize(pos, size);
        m_hitFlagArmParts_R.SetPointSize(pos, size);
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 当たり判定フラグ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_hitFlagArm = false;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 当たり判定頭
        //*|***|***|***|***|***|***|***|***|***|***|***|
        pos = new Vector2(0.0f, 0.0f);
        size = new Vector2(1.0f, 1.0f);
        m_hitFlagBodyParts.SetPointSize(pos, size);
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 当たり判定フラグ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_hitFlagBody = false;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 当たり判定頭
        //*|***|***|***|***|***|***|***|***|***|***|***|
        pos = new Vector2(0.0f, 0.0f);
        size = new Vector2(1.0f, 1.0f);
        m_hitFlagHeadParts.SetPointSize(pos, size);
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 当たり判定フラグ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_hitFlagHead = false;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 当たり判定脚
        //*|***|***|***|***|***|***|***|***|***|***|***|
        pos = new Vector2(0.0f, 0.0f);
        size = new Vector2(0.5f, 0.5f);
        m_hitFlagLegParts_L.SetPointSize(pos, size);
        m_hitFlagLegParts_R.SetPointSize(pos, size);
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 当たり判定フラグ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_hitFlagLeg = false;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 更新データ
    //*|***|***|***|***|***|***|***|***|***|***|***|
    void Update()
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // すでにアップデートされているものとする
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_addForce = Vector2.zero;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 起動：横移動
        //*|***|***|***|***|***|***|***|***|***|***|***|
        UpdateMoveCommond();
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 起動：ジャンプ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        UpdateJumpCommond();
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 移動ベクトル取得
        //*|***|***|***|***|***|***|***|***|***|***|***|
        float addPowerX = m_addForce.x;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 方向指定
        //*|***|***|***|***|***|***|***|***|***|***|***|
        if (m_movePowerX != 0)
        {
            if (m_movePowerX < 0)
            {
                m_rightArrow = false;
            }
            if (m_movePowerX > 0)
            {
                m_rightArrow = true;
            }
            m_moveingPower = true;
        }
        else
        {
            if (m_addPower)
            {
                m_moveingPower = true;
            }
            else
            {
                m_moveingPower = false;
            }
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 力の方向指定
        //*|***|***|***|***|***|***|***|***|***|***|***|
        if (m_addPower)
        {
            if (addPowerX < 0)
            {
                m_rightPower = false;
            }
            if (addPowerX > 0)
            {
                m_rightPower = true;
            }

            if (m_rightPower ^ m_rightArrow)
            {
                m_reverseArrow = true;
            }
            else
            {
                m_reverseArrow = false;
            }
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 起動：抵抗＆重力
        //*|***|***|***|***|***|***|***|***|***|***|***|
        UpdateResistance();
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 当たりの判定
        //*|***|***|***|***|***|***|***|***|***|***|***|
        UpdateAttackParts();



    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 起動：横移動
    //*|***|***|***|***|***|***|***|***|***|***|***|
    void UpdateMoveCommond()
    {
        Vector2 force = m_controllerData.ChackStickPower();
        float forceDead = 1.0f;
        m_addForce = (force * 20.0f);
        m_addForce.y = 0;
        m_addPower = false;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 十字キーの力をつぎ込む
        //*|***|***|***|***|***|***|***|***|***|***|***|
        if (forceDead < MyCalculator.LongVector2(m_addForce))
        {
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 力を加える
            //*|***|***|***|***|***|***|***|***|***|***|***|
            this.m_rigid2D.AddForce(m_addForce);


            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 力を加えられた
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_addPower = true;
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 移動ベクトル取得
        //*|***|***|***|***|***|***|***|***|***|***|***|
        Vector2 velocityData = m_rigid2D.velocity;
        m_movePowerX = velocityData.x;
        float absVelX = Mathf.Abs(m_movePowerX);
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 移動ベクトルが小さいと認知しない
        //*|***|***|***|***|***|***|***|***|***|***|***|
        if (absVelX < 0.01f)
        {
            m_movePowerX = 0;
        }
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 起動：抵抗＆重力
    //*|***|***|***|***|***|***|***|***|***|***|***|
    void UpdateResistance()
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 移動ベクトル取得
        //*|***|***|***|***|***|***|***|***|***|***|***|
        Vector2 velocityData = m_rigid2D.velocity;
        m_movePowerX = velocityData.x;
        float absVelX = Mathf.Abs(m_movePowerX);
        float resistance = 0.0f;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 移動ベクトルが大きいと制限される
        //*|***|***|***|***|***|***|***|***|***|***|***|
        float maxPower = 20.0f;
        if (absVelX > maxPower)
        {
            float speedMax = MyCalculator.Multiplication(maxPower, 2);
            float speedNow = MyCalculator.Multiplication(absVelX, 2);
            float speedOver = MyCalculator.LeapReverseCalculation(0, speedNow, speedMax);
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 抵抗度
            //*|***|***|***|***|***|***|***|***|***|***|***|
            float resistancePar = MyCalculator.InversionOfProportion(speedOver);
            float resistanceParX = MyCalculator.Multiplication(resistancePar, 2);
            resistance = MyCalculator.InversionOfProportion(resistanceParX);

            m_movePowerX *= resistance;

        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 前方に進んでいないと制限される
        //*|***|***|***|***|***|***|***|***|***|***|***|
        if (m_reverseArrow || !m_addPower)
        {
            resistance = 0.98f;
            m_movePowerX *= resistance;
            absVelX = Mathf.Abs(m_movePowerX);
            if (absVelX < 0.01f)
            {
                m_movePowerX = 0;
                if (m_addPower)
                {
                    m_moveingPower = true;
                }
                else
                {
                    m_moveingPower = false;
                }
            }
        }

        velocityData.x = m_movePowerX;
        this.m_rigid2D.velocity = velocityData;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 起動：ジャンプ
    //*|***|***|***|***|***|***|***|***|***|***|***|
    void UpdateJumpCommond()
    {
        Vector2 velocityData = m_rigid2D.velocity;
        if (m_controllerData.ChackJumpTrigger() && m_groundFlagFlame)
        {
            m_rigid2D.velocity = new Vector2(velocityData.x, 0);
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // ジャンプパワー
            //*|***|***|***|***|***|***|***|***|***|***|***|
            this.m_rigid2D.AddForce(transform.up * 500);
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // ジャンプの音~~~~
            //*|***|***|***|***|***|***|***|***|***|***|***|
            //soundJump.PlayOneShot(soundJump.clip);
        }
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 当たりの判定
    //*|***|***|***|***|***|***|***|***|***|***|***|
    void UpdateAttackParts()
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 攻撃中フラグ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        if (m_attackingFlag)
        {
            m_partsAttack2D.SetPlayHit();
        }
        else
        {
            m_partsAttack2D.SetStopHit();
        }

        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 攻撃ボールデータ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_partsAttackPos = new Vector2(0.0f, 0.0f);
        m_partsAttackSize = new Vector2(0.5f, 0.5f);
        m_partsAttack2D.SetPointSize(m_partsAttackPos, m_partsAttackSize);
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 定期更新データ
    //*|***|***|***|***|***|***|***|***|***|***|***|
    void FixedUpdate()
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 当たり判定
        //*|***|***|***|***|***|***|***|***|***|***|***|
        if (m_partsLeg2D.GetHitFlag())
        {
            m_groundFlagFlame = true;
        }
        else
        {
            m_groundFlagFlame = false;
        }

        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 当たりの判定腕
        //*|***|***|***|***|***|***|***|***|***|***|***|
        if (m_hitFlagArmParts_L.GetHitFlag() || m_hitFlagArmParts_R.GetHitFlag())
        {
            m_hitFlagArm = true;
        }
        else
        {
            m_hitFlagArm = false;
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 当たりの判定体
        //*|***|***|***|***|***|***|***|***|***|***|***|
        if (m_hitFlagBodyParts.GetHitFlag())
        {
            m_hitFlagBody = true;
        }
        else
        {
            m_hitFlagBody = false;
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 当たりの判定頭
        //*|***|***|***|***|***|***|***|***|***|***|***|
        if (m_hitFlagHeadParts.GetHitFlag())
        {
            m_hitFlagHead = true;
        }
        else
        {
            m_hitFlagHead = false;
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 当たりの判定脚
        //*|***|***|***|***|***|***|***|***|***|***|***|
        if (m_hitFlagLegParts_L.GetHitFlag() || m_hitFlagLegParts_R.GetHitFlag())
        {
            m_hitFlagLeg = true;
        }
        else
        {
            m_hitFlagLeg = false;
        }








    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // コントローラー取得
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public void LinkController(PlayerControllerData getController)
    {
        m_controllerData = getController;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 向き判定取得
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public bool GetAddPowerFlag()
    {
        return m_addPower;
    }
    public bool GetMoveingPowerFlag()
    {
        return m_moveingPower;
    }
    public bool GetRightArrowFlag()
    {
        return m_rightArrow;
    }
    public bool GetRightPowerFlag()
    {
        return m_rightPower;
    }
    public bool GetReverseArrowFlag()
    {
        return m_reverseArrow;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 当たり判定取得
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public bool GetGroundFlag()
    {
        return m_groundFlagFlame;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 当たりの判定腕取得
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public bool GetHitFlagArm()
    {
        return m_hitFlagArm;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 当たりの判定体取得
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public bool GetHitFlagBody()
    {
        return m_hitFlagBody;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 当たりの判定頭取得
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public bool GetHitFlagHead()
    {
        return m_hitFlagHead;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 当たりの判定脚取得
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public bool GetHitFlagLeg()
    {
        return m_hitFlagLeg;
    }

    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 攻撃中判定取得
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public void SetAttackFlag(bool attackFlag)
    {
        m_attackingFlag = attackFlag;
    }

    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 攻撃中判定取得
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public Vector3 GetPosition()
    {
        return gameObject.transform.position;
    }
    ////*|***|***|***|***|***|***|***|***|***|***|***|
    //// 当たり判定取得
    ////*|***|***|***|***|***|***|***|***|***|***|***|
    //private void OnTriggerStay2D(Collision2D col)
    //{
    //    //地面に接していたらgroundFlagをtrueにする
    //    if (col.gameObject.tag == "Floor" || col.gameObject.tag == "Enemy" || col.gameObject.tag == "Shell")
    //    {
    //        //m_groundFlag = true;
    //    }
    //}
}
