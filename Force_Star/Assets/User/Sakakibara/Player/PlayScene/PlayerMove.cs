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

    private DebugPlayerParts m_partsLegFalse2D;
    private GameObject m_rigidOnlyFalseLeg;
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

    private GameObject m_catchArmObject_L;
    private GameObject m_catchArmObject_R;
    private PlayerPartsCatch m_catchArmParts_L;
    private PlayerPartsCatch m_catchArmParts_R;
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 当たりの判定
    //*|***|***|***|***|***|***|***|***|***|***|***|
    private bool m_hitFlagArm;

    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 当たりの判定体
    //*|***|***|***|***|***|***|***|***|***|***|***|
    private GameObject m_hitFlagBodyObject;
    private PlayerPartsPain m_hitFlagBodyParts;

    private GameObject m_catchBodyObject;
    private PlayerPartsCatch m_catchBodyParts;
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 当たりの判定
    //*|***|***|***|***|***|***|***|***|***|***|***|
    private bool m_hitFlagBody;

    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 当たりの判定頭
    //*|***|***|***|***|***|***|***|***|***|***|***|
    private GameObject m_hitFlagHeadObject;
    private PlayerPartsPain m_hitFlagHeadParts;

    private GameObject m_catchHeadObject;
    private PlayerPartsCatch m_catchHeadParts;
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

    private GameObject m_catchLegObject_L;
    private GameObject m_catchLegObject_R;
    private PlayerPartsCatch m_catchLegParts_L;
    private PlayerPartsCatch m_catchLegParts_R;
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 当たりの判定
    //*|***|***|***|***|***|***|***|***|***|***|***|
    private bool m_hitFlagLeg;
    private float m_scale;


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
    [SerializeField]
    private float m_movePowerY;
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
    // データベース設定
    //*|***|***|***|***|***|***|***|***|***|***|***|

    ////*|***|***|***|***|***|***|***|***|***|***|***|
    //// 移動力
    ////*|***|***|***|***|***|***|***|***|***|***|***|
    //private float m_moveNormalPower;
    //private float m_moveMaxNormalPower;
    //private float m_moveStrongPower;
    //private float m_moveMaxStrongPower;
    ////*|***|***|***|***|***|***|***|***|***|***|***|
    //// ジャンプ力
    ////*|***|***|***|***|***|***|***|***|***|***|***|
    //private float m_jumpNormalPower;
    //private float m_jumpMaxNormalPower;
    //private float m_jumpStrongPower;
    //private float m_jumpMaxStrongPower;

    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 関係：コマンド全般
    //*|***|***|***|***|***|***|***|***|***|***|***|
    //private Vector2 m_addForce;
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 関係：生死判定
    //*|***|***|***|***|***|***|***|***|***|***|***|
    private bool m_arive;
    private bool m_stop;
    private bool m_death;

    //*|***|***|***|***|***|***|***|***|***|***|***|
    // データマリオベース設定
    //*|***|***|***|***|***|***|***|***|***|***|***|
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 移動データマリオ
    //*|***|***|***|***|***|***|***|***|***|***|***|
    private Vector2 m_rigid_Vec;
    private Vector2 m_addForce;
    private bool m_jumpingFlag;
    private float m_jumpingTime;
    private float m_jumpingGravity;
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // ジャンプ力マリオ
    //*|***|***|***|***|***|***|***|***|***|***|***|
    private float m_jumpPower;
    private float m_jumpPowerTime;
    private float m_jumpPower_S;
    private float m_jumpPowerTime_S;
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 移動力マリオ
    //*|***|***|***|***|***|***|***|***|***|***|***|
    private float m_movePower;
    private float m_movePowerTime;
    private float m_movePower_S;
    private float m_movePowerTime_S;
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 移動力限界マリオ
    //*|***|***|***|***|***|***|***|***|***|***|***|
    private float m_jumpPowerMax;
    private float m_jumpPowerMax_S;
    private float m_jumpPowerMaxUnder;
    private float m_movePowerMax;
    private float m_movePowerMax_S;
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 外からの力
    //*|***|***|***|***|***|***|***|***|***|***|***|
    private Vector2 m_addForceOut;


    //*|***|***|***|***|***|***|***|***|***|***|***|
    // これが出来たときに
    //*|***|***|***|***|***|***|***|***|***|***|***|
    void Awake()
    {
        gameObject.layer = 8;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // データベース設定
        //*|***|***|***|***|***|***|***|***|***|***|***|
        SetDataBase();
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // データマリオベース設定
        //*|***|***|***|***|***|***|***|***|***|***|***|
        SetDataBaseMario();
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // ボディデータ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        this.m_rigid2D = gameObject.AddComponent<Rigidbody2D>();
        this.m_box2D = gameObject.AddComponent<BoxCollider2D>();
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 移動データ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        this.m_rigid2D.gravityScale = 0.0f;
        this.m_rigid2D.angularDrag = 0.0f;
        this.m_rigid2D.drag = 0.0f;
        this.m_rigid2D.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        this.m_rigid2D.sleepMode = RigidbodySleepMode2D.NeverSleep;
        this.m_rigid2D.constraints = RigidbodyConstraints2D.FreezeRotation;
        ////*|***|***|***|***|***|***|***|***|***|***|***|
        //// 親たちのデータ
        ////*|***|***|***|***|***|***|***|***|***|***|***|
        //PlayerData_Number_List partsListNum;
        //string partsName;
        //GameObject parent = null;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 足データ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_rigidOnlyLeg = new GameObject("rigidLeg");
        m_rigidOnlyLeg.transform.parent = gameObject.transform;
        this.m_partsLeg2D = m_rigidOnlyLeg.AddComponent<DebugPlayerParts>();

        m_rigidOnlyFalseLeg = new GameObject("rigidLeg");
        m_rigidOnlyFalseLeg.transform.parent = gameObject.transform;
        this.m_partsLegFalse2D = m_rigidOnlyFalseLeg.AddComponent<DebugPlayerParts>();
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
        m_movePowerY = 0.0f;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 関係：生死判定
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_arive = true;
        m_death = false;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 大きさ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_scale = 2.0f;
        gameObject.transform.localScale = new Vector3(m_scale, m_scale, m_scale);
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // データベース設定
    //*|***|***|***|***|***|***|***|***|***|***|***|
    void SetDataBase()
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 移動データマリオ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_rigid_Vec = Vector2.zero;
        m_addForce = Vector2.zero;
        m_jumpingFlag = false;
        m_jumpingTime = 0;
        m_jumpingGravity = 0.0098f;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // ジャンプ力マリオ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_jumpPower = 0.3f;
        m_jumpPowerTime = 15.0f;
        m_jumpPower_S = 0.6f;
        m_jumpPowerTime_S = 30.0f;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 移動力マリオ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_movePower = 0.025f;
        m_movePowerTime = 20.0f;
        m_movePower_S = 0.05f;
        m_movePowerTime_S = 30.0f;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 移動力限界マリオ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_jumpPowerMax = 2.0f;
        m_jumpPowerMax_S = 2.0f;
        m_jumpPowerMaxUnder = 1.0f;
        m_movePowerMax = 0.70f;
        m_movePowerMax_S = 1.20f;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 強化フラグ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_armStrong = false;
        m_bodyStrong = false;
        m_headStrong = false;
        m_legStrong = false;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // データマリオベース設定
    //*|***|***|***|***|***|***|***|***|***|***|***|
    void SetDataBaseMario()
    {


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
        // 当たり判定腕のタグ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 左
        //*|***|***|***|***|***|***|***|***|***|***|***|
        partsListNum = PlayerData_Number_List.LEFTHAND;
        partsName = partsListNum.ToString();
        parent = GameDataPublic.SearchChildAllHierarchy(gameObject, partsName);
        m_hitFlagArmObject_L = new GameObject("hitFlagArm_L");
        m_hitFlagArmObject_L.transform.parent = parent.transform;

        m_catchArmObject_L = new GameObject("catchArm_L");
        m_catchArmObject_L.transform.parent = parent.transform;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 右
        //*|***|***|***|***|***|***|***|***|***|***|***|
        partsListNum = PlayerData_Number_List.RIGHTHAND;
        partsName = partsListNum.ToString();
        parent = GameDataPublic.SearchChildAllHierarchy(gameObject, partsName);
        m_hitFlagArmObject_R = new GameObject("hitFlagArm_R");
        m_hitFlagArmObject_R.transform.parent = parent.transform;

        m_catchArmObject_R = new GameObject("catchArm_R");
        m_catchArmObject_R.transform.parent = parent.transform;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 当たり判定腕
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 左
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_hitFlagArmObject_L.tag = WarehousePlayer.GetTag_PlayerHitArmParts();
        this.m_hitFlagArmParts_L = m_hitFlagArmObject_L.AddComponent<PlayerPartsPain>();

        m_catchArmObject_L.tag = WarehousePlayer.GetTag_PlayerHitArmParts();
        this.m_catchArmParts_L = m_catchArmObject_L.AddComponent<PlayerPartsCatch>();
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 右
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_hitFlagArmObject_R.tag = WarehousePlayer.GetTag_PlayerHitArmParts();
        this.m_hitFlagArmParts_R = m_hitFlagArmObject_R.AddComponent<PlayerPartsPain>();

        m_catchArmObject_R.tag = WarehousePlayer.GetTag_PlayerHitArmParts();
        this.m_catchArmParts_R = m_catchArmObject_R.AddComponent<PlayerPartsCatch>();


        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 当たり判定体のタグ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        partsListNum = PlayerData_Number_List.BODYTOP;
        partsName = partsListNum.ToString();
        parent = GameDataPublic.SearchChildAllHierarchy(gameObject, partsName);
        m_hitFlagBodyObject = new GameObject("hitFlagBody");
        m_hitFlagBodyObject.transform.parent = parent.transform;

        m_catchBodyObject = new GameObject("catchBody");
        m_catchBodyObject.transform.parent = parent.transform;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 当たり判定頭
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_hitFlagBodyObject.tag = WarehousePlayer.GetTag_PlayerHitBodyParts();
        this.m_hitFlagBodyParts = m_hitFlagBodyObject.AddComponent<PlayerPartsPain>();

        m_catchBodyObject.tag = WarehousePlayer.GetTag_PlayerHitBodyParts();
        this.m_catchBodyParts = m_catchBodyObject.AddComponent<PlayerPartsCatch>();



        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 当たり判定頭のタグ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        partsListNum = PlayerData_Number_List.PLAYERHEAD;
        partsName = partsListNum.ToString();
        parent = GameDataPublic.SearchChildAllHierarchy(gameObject, partsName);
        m_hitFlagHeadObject = new GameObject("hitFlagHead");
        m_hitFlagHeadObject.transform.parent = parent.transform;

        m_catchHeadObject = new GameObject("catchHead");
        m_catchHeadObject.transform.parent = parent.transform;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 当たり判定頭
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_hitFlagHeadObject.tag = WarehousePlayer.GetTag_PlayerHitHeadParts();
        this.m_hitFlagHeadParts = m_hitFlagHeadObject.AddComponent<PlayerPartsPain>();

        m_catchHeadObject.tag = WarehousePlayer.GetTag_PlayerHitHeadParts();
        this.m_catchHeadParts = m_catchHeadObject.AddComponent<PlayerPartsCatch>();

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

        m_catchLegObject_L = new GameObject("catchLeg_L");
        m_catchLegObject_L.transform.parent = parent.transform;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 右
        //*|***|***|***|***|***|***|***|***|***|***|***|
        partsListNum = PlayerData_Number_List.RIGHTLEG;
        partsName = partsListNum.ToString();
        parent = GameDataPublic.SearchChildAllHierarchy(gameObject, partsName);
        m_hitFlagLegObject_R = new GameObject("hitFlagLeg_R");
        m_hitFlagLegObject_R.transform.parent = parent.transform;

        m_catchLegObject_R = new GameObject("catchLeg_R");
        m_catchLegObject_R.transform.parent = parent.transform;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 当たり判定脚
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 左
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_hitFlagLegObject_L.tag = WarehousePlayer.GetTag_PlayerHitLegParts();
        this.m_hitFlagLegParts_L = m_hitFlagLegObject_L.AddComponent<PlayerPartsPain>();

        m_catchLegObject_L.tag = WarehousePlayer.GetTag_PlayerHitLegParts();
        this.m_catchLegParts_L = m_catchLegObject_L.AddComponent<PlayerPartsCatch>();
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 右
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_hitFlagLegObject_R.tag = WarehousePlayer.GetTag_PlayerHitLegParts();
        this.m_hitFlagLegParts_R = m_hitFlagLegObject_R.AddComponent<PlayerPartsPain>();

        m_catchLegObject_R.tag = WarehousePlayer.GetTag_PlayerHitLegParts();
        this.m_catchLegParts_R = m_catchLegObject_R.AddComponent<PlayerPartsCatch>();
    }


    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 起動：ジャンプ
    //*|***|***|***|***|***|***|***|***|***|***|***|
    void AwakwJumpCommond()
    {


    }
    void Start()
    {
        Vector2 sizeLeg = new Vector2(0.8f, 0.2f);
        Vector2 pointLeg = new Vector2(0.0f, -0.5f);
        m_partsLeg2D.SetPointSize(pointLeg, sizeLeg);

        sizeLeg = new Vector2(0.8f, 0.4f);
        pointLeg = new Vector2(0.0f, 0.0f);
        m_partsLegFalse2D.SetPointSize(pointLeg, sizeLeg);
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

        m_catchArmParts_L.SetPointSize(pos, size);
        m_catchArmParts_R.SetPointSize(pos, size);
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 当たり判定フラグ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_hitFlagArm = false;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 当たり判定頭
        //*|***|***|***|***|***|***|***|***|***|***|***|
        pos = new Vector2(0.0f, 0.0f);
        size = new Vector2(0.8f, 0.8f);
        m_hitFlagBodyParts.SetPointSize(pos, size);

        m_catchBodyParts.SetPointSize(pos, size);
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

        m_catchHeadParts.SetPointSize(pos, size);
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

        m_catchLegParts_L.SetPointSize(pos, size);
        m_catchLegParts_R.SetPointSize(pos, size);
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
        // 外からの力
        //*|***|***|***|***|***|***|***|***|***|***|***|
        UpdateForseOut();
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 当たりの判定
        //*|***|***|***|***|***|***|***|***|***|***|***|
        UpdateAttackParts();
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 生死判定
        //*|***|***|***|***|***|***|***|***|***|***|***|
        UpdateArive();


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
        // 腕パワーアップ中なら
        // 腕ダメージなし。
        //*|***|***|***|***|***|***|***|***|***|***|***|
        if (m_armStrong && m_attackingFlag)
        {
            m_hitFlagArmParts_L.SetStopHit();
            m_hitFlagArmParts_R.SetStopHit();
        }
        else
        {
            m_hitFlagArmParts_L.SetPlayHit();
            m_hitFlagArmParts_R.SetPlayHit();
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 攻撃ボールデータ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_partsAttackPos = new Vector2(0.0f, 0.0f);
        m_partsAttackSize = new Vector2(0.5f, 0.5f);
        m_partsAttack2D.SetPointSize(m_partsAttackPos, m_partsAttackSize);
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 起動：横移動
    //*|***|***|***|***|***|***|***|***|***|***|***|
    void UpdateMoveCommond()
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 初期化
        //*|***|***|***|***|***|***|***|***|***|***|***|
        Vector2 force = m_controllerData.ChackStickPower();
        float forceDead = 0.15f;
        force.y = 0;
        m_addPower = false;

        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 十字キーの力をつぎ込む
        //*|***|***|***|***|***|***|***|***|***|***|***|
        if (forceDead < MyCalculator.LongVector2(force))
        {
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 体強化状態
            //*|***|***|***|***|***|***|***|***|***|***|***|
            if (m_bodyStrong)
            {
                m_addForce.x = force.x * m_movePower_S;
                m_rigid_Vec.x += m_addForce.x;
            }
            else
            {
                m_addForce.x = force.x * m_movePower;
                m_rigid_Vec.x += m_addForce.x;
            }
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 力を加えられた
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_addPower = true;
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 移動ベクトル取得
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_movePowerX = m_rigid_Vec.x;
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
    // 起動マリオ：ジャンプ
    //*|***|***|***|***|***|***|***|***|***|***|***|
    void UpdateJumpCommond()
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // ジャンプの瞬間
        //*|***|***|***|***|***|***|***|***|***|***|***|
        if (m_controllerData.ChackJumpTrigger() && m_groundFlagFlame)
        {
            m_jumpingFlag = true;
            if (m_rigid_Vec.y < 0)
            {
                m_rigid_Vec.y = 0;
            }
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // ジャンプ中
        //*|***|***|***|***|***|***|***|***|***|***|***|
        if (m_controllerData.ChackJump() && m_jumpingFlag)
        {
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // ジャンプ時間
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_jumpingTime += 1.0f;
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 脚強化状態
            //*|***|***|***|***|***|***|***|***|***|***|***|
            if (m_legStrong)
            {
                if (m_jumpingTime >= m_jumpPowerTime_S)
                {
                    m_jumpingFlag = false;
                }
            }
            else
            {
                if (m_jumpingTime >= m_jumpPowerTime)
                {
                    m_jumpingFlag = false;
                }
            }
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // ジャンプするなら
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_rigid_Vec.y = 0;
            
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 脚強化状態
            //*|***|***|***|***|***|***|***|***|***|***|***|
            if (m_legStrong)
            {
                m_addForce.y = m_jumpPower_S;
                m_rigid_Vec.y = m_addForce.y;
            }
            else
            {
                m_addForce.y = m_jumpPower;
                m_rigid_Vec.y = m_addForce.y;
            }
        }
        else
        {
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // ジャンプフラグ
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_jumpingFlag = false;
            m_jumpingTime = 0;
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 空中
            //*|***|***|***|***|***|***|***|***|***|***|***|
            if (!m_groundFlagFlame)
            {
                float resistance = 0.75f;
                //*|***|***|***|***|***|***|***|***|***|***|***|
                // 推進力低下
                //*|***|***|***|***|***|***|***|***|***|***|***|
                if (m_rigid_Vec.y > 0)
                {
                    m_rigid_Vec.y = m_rigid_Vec.y * resistance;
                }
                //*|***|***|***|***|***|***|***|***|***|***|***|
                // 重力の影響
                //*|***|***|***|***|***|***|***|***|***|***|***|
                m_addForce.y = m_jumpingGravity * -1;
                m_rigid_Vec.y += m_addForce.y;
            }
            else
            {
                //*|***|***|***|***|***|***|***|***|***|***|***|
                // 重力の影響
                //*|***|***|***|***|***|***|***|***|***|***|***|
                m_addForce.y = 0;
                m_rigid_Vec.y = m_addForce.y;
            }
        }
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 起動マリオ：抵抗＆重力
    //*|***|***|***|***|***|***|***|***|***|***|***|
    void UpdateResistance()
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 移動ベクトル取得
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_movePowerX = m_rigid_Vec.x;
        m_movePowerY = m_rigid_Vec.y;
        float absVelX = Mathf.Abs(m_movePowerX);
        float absVelY = Mathf.Abs(m_movePowerY);
        float resistance = 0.0f;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 移動ベクトルが大きいと制限される
        //*|***|***|***|***|***|***|***|***|***|***|***|
        float maxPowerX = 20.0f;
        float maxPowerY = m_jumpPowerMaxUnder;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 体強化状態で最大速度が変わる
        //*|***|***|***|***|***|***|***|***|***|***|***|
        if (m_bodyStrong)
        {
            maxPowerX = m_movePowerMax_S;
        }
        else
        {
            maxPowerX = m_movePowerMax;
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 速度オーバー
        //*|***|***|***|***|***|***|***|***|***|***|***|
        if (absVelX > maxPowerX)
        {
            float speedMax = MyCalculator.Multiplication(maxPowerX, 2);
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
            resistance = 0.75f;
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
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 上昇か落下か
        //*|***|***|***|***|***|***|***|***|***|***|***|
        if (m_movePowerY > 0)
        {
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 脚強化状態
            //*|***|***|***|***|***|***|***|***|***|***|***|
            if (m_legStrong)
            {
                maxPowerY = m_jumpPowerMax_S;
            }
            else
            {
                maxPowerY = m_jumpPowerMax;
            }
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 上昇速度オーバー
            //*|***|***|***|***|***|***|***|***|***|***|***|
            if (absVelY > maxPowerY)
            {
                float speedMax = MyCalculator.Multiplication(maxPowerY, 2);
                float speedNow = MyCalculator.Multiplication(absVelY, 2);
                float speedOver = MyCalculator.LeapReverseCalculation(0, speedNow, speedMax);
                //*|***|***|***|***|***|***|***|***|***|***|***|
                // 抵抗度
                //*|***|***|***|***|***|***|***|***|***|***|***|
                float resistancePar = MyCalculator.InversionOfProportion(speedOver);
                float resistanceParX = MyCalculator.Multiplication(resistancePar, 2);
                resistance = MyCalculator.InversionOfProportion(resistanceParX);

                m_movePowerY *= resistance;

            }
        }
        else
        {
            maxPowerY = m_jumpPowerMaxUnder;
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 落下速度オーバー
            //*|***|***|***|***|***|***|***|***|***|***|***|
            if (absVelY > maxPowerY)
            {
                float speedMax = MyCalculator.Multiplication(maxPowerY, 2);
                float speedNow = MyCalculator.Multiplication(absVelY, 2);
                float speedOver = MyCalculator.LeapReverseCalculation(0, speedNow, speedMax);
                //*|***|***|***|***|***|***|***|***|***|***|***|
                // 抵抗度
                //*|***|***|***|***|***|***|***|***|***|***|***|
                float resistancePar = MyCalculator.InversionOfProportion(speedOver);
                float resistanceParX = MyCalculator.Multiplication(resistancePar, 2);
                resistance = MyCalculator.InversionOfProportion(resistanceParX);

                m_movePowerY *= resistance;
            }
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 移動力計算
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_rigid_Vec.x = m_movePowerX;
        m_rigid_Vec.y = m_movePowerY;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 移動結果
        //*|***|***|***|***|***|***|***|***|***|***|***|
        Vector3 position = gameObject.transform.position;
        position += ChangeData.GetVector3(m_rigid_Vec);
        gameObject.transform.position = position;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 外からの力
    //*|***|***|***|***|***|***|***|***|***|***|***|
    void UpdateForseOut()
    {
        float resistance = 0.75f;
        float addPowerOutX;
        float addPowerOutY;
        {
            float absVelX;
            float absVelY;
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // X
            //*|***|***|***|***|***|***|***|***|***|***|***|
            addPowerOutX = m_addForceOut.x * resistance;
            absVelX = Mathf.Abs(addPowerOutX);
            if (absVelX < 0.01f)
            {
                addPowerOutX = 0;
            }
            m_addForceOut.x = addPowerOutX;
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // Y
            //*|***|***|***|***|***|***|***|***|***|***|***|
            addPowerOutY = m_addForceOut.y * resistance;
            absVelY = Mathf.Abs(addPowerOutY);
            if (absVelY < 0.01f)
            {
                addPowerOutY = 0;
            }
            m_addForceOut.y = addPowerOutY;
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 外からの終焉
        //*|***|***|***|***|***|***|***|***|***|***|***|
        if (MyCalculator.LongVector2(m_addForceOut) == 0.0f)
        {
            m_rigid2D.velocity = Vector2.zero;
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 移動結果
        //*|***|***|***|***|***|***|***|***|***|***|***|
        Vector3 position = gameObject.transform.position;
        position += ChangeData.GetVector3(m_addForceOut);
        gameObject.transform.position = position;
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
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 偽当たり判定にあたっている状態での遷移禁止。
            //*|***|***|***|***|***|***|***|***|***|***|***|
            if (!m_groundFlagFlame && m_partsLegFalse2D.GetHitFlag())
            {
                m_groundFlagFlame = false;
            }
            else
            {
                m_groundFlagFlame = true;
            }
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
    // 生死判定
    //*|***|***|***|***|***|***|***|***|***|***|***|
    void UpdateArive()
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 停止
        //*|***|***|***|***|***|***|***|***|***|***|***|
        if (m_stop)
        {
            UpdateStopCommond();
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 生死
        //*|***|***|***|***|***|***|***|***|***|***|***|
        if (m_arive == false)
        {
            if (m_death == false)
            {
                UpdateDeathCommond();
            }
        }
        else
        {
            if (m_death == true)
            {
                UpdateRegenerationCommond();
            }
        }
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 起動：復活
    //*|***|***|***|***|***|***|***|***|***|***|***|
    void UpdateRegenerationCommond()
    {
        //m_rigid2D.isKinematic = false;
        //m_rigid2D.velocity = Vector2.zero;
        SetAllPlayHit();
        m_death = false;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 起動：死
    //*|***|***|***|***|***|***|***|***|***|***|***|
    void UpdateDeathCommond()
    {
        //m_rigid2D.isKinematic = true;
        //m_rigid2D.velocity = Vector2.zero;
        SetAllStopHit();
        m_death = true;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 起動：停止
    //*|***|***|***|***|***|***|***|***|***|***|***|
    void UpdateStopCommond()
    {
        m_rigid2D.isKinematic = true;
        m_rigid2D.velocity = Vector2.zero;

        m_rigid_Vec = Vector2.zero;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 当たり判定あり
    //*|***|***|***|***|***|***|***|***|***|***|***|
    void SetAllPlayHit()
    {
        m_box2D.enabled = true;
        m_hitFlagArmParts_L.SetPlayHit();
        m_hitFlagArmParts_R.SetPlayHit();
        m_hitFlagBodyParts.SetPlayHit();
        m_hitFlagHeadParts.SetPlayHit();
        m_hitFlagLegParts_L.SetPlayHit();
        m_hitFlagLegParts_R.SetPlayHit();

        m_catchArmParts_L.SetPlayHit();
        m_catchArmParts_R.SetPlayHit();
        m_catchBodyParts.SetPlayHit();
        m_catchHeadParts.SetPlayHit();
        m_catchLegParts_L.SetPlayHit();
        m_catchLegParts_R.SetPlayHit();
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 当たり判定消失
    //*|***|***|***|***|***|***|***|***|***|***|***|
    void SetAllStopHit()
    {
        m_box2D.enabled = false;
        m_hitFlagArmParts_L.SetStopHit();
        m_hitFlagArmParts_R.SetStopHit();
        m_hitFlagBodyParts.SetStopHit();
        m_hitFlagHeadParts.SetStopHit();
        m_hitFlagLegParts_L.SetStopHit();
        m_hitFlagLegParts_R.SetStopHit();

        m_catchArmParts_L.SetStopHit();
        m_catchArmParts_R.SetStopHit();
        m_catchBodyParts.SetStopHit();
        m_catchHeadParts.SetStopHit();
        m_catchLegParts_L.SetStopHit();
        m_catchLegParts_R.SetStopHit();
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // コントローラー取得
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public void LinkController(PlayerControllerData getController)
    {
        m_controllerData = getController;
    }
    public void AddForsePower(Vector2 addForce)
    {
        m_addForceOut += addForce;
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
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // プレイヤーパーツ位置情報取得
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public Vector3 GetPlayerArmRightPositon()
    {
        return m_catchArmObject_R.transform.position;
    }
    public Vector3 GetPlayerArmLeftPositon()
    {
        return m_catchArmObject_L.transform.position;
    }
    public Vector3 GetPlayerBodyPositon()
    {
        return m_catchBodyObject.transform.position;
    }
    public Vector3 GetPlayerHeadPositon()
    {
        return m_catchHeadObject.transform.position;
    }
    public Vector3 GetPlayerLegRightPositon()
    {
        return m_catchLegObject_R.transform.position;
    }
    public Vector3 GetPlayerLegLeftPositon()
    {
        return m_catchLegObject_L.transform.position;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 強化状態共有
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public void SetPowerUp(bool arm, bool body, bool head, bool leg)
    {
        m_armStrong = arm;
        m_bodyStrong = body;
        m_headStrong = head;
        m_legStrong = leg;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 生死判定
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public void SetArive(bool data)
    {
        m_arive = data;
    }
    public void SetStop(bool data)
    {
        m_stop = data;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // プレイヤー情報取得
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public float GetScale()
    {
        return m_scale;
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

////*|***|***|***|***|***|***|***|***|***|***|***|
//// 起動：横移動
////*|***|***|***|***|***|***|***|***|***|***|***|
//void UpdateMoveCommond()
//{
//    //*|***|***|***|***|***|***|***|***|***|***|***|
//    // 初期化
//    //*|***|***|***|***|***|***|***|***|***|***|***|
//    Vector2 force = m_controllerData.ChackStickPower();
//    float forceDead = 0.05f;
//    force.y = 0;
//    m_addPower = false;
//    //*|***|***|***|***|***|***|***|***|***|***|***|
//    // 十字キーの力をつぎ込む
//    //*|***|***|***|***|***|***|***|***|***|***|***|
//    if (forceDead < MyCalculator.LongVector2(force))
//    {
//        //*|***|***|***|***|***|***|***|***|***|***|***|
//        // 脚強化状態
//        //*|***|***|***|***|***|***|***|***|***|***|***|
//        if (m_legStrong)
//        {
//            m_addForce = (force * m_moveStrongPower);
//        }
//        else
//        {
//            m_addForce = (force * m_moveNormalPower);
//        }
//        //*|***|***|***|***|***|***|***|***|***|***|***|
//        // 力を加える
//        //*|***|***|***|***|***|***|***|***|***|***|***|
//        this.m_rigid2D.AddForce(m_addForce);
//        //*|***|***|***|***|***|***|***|***|***|***|***|
//        // 力を加えられた
//        //*|***|***|***|***|***|***|***|***|***|***|***|
//        m_addPower = true;
//    }
//    //*|***|***|***|***|***|***|***|***|***|***|***|
//    // 移動ベクトル取得
//    //*|***|***|***|***|***|***|***|***|***|***|***|
//    Vector2 velocityData = m_rigid2D.velocity;
//    m_movePowerX = velocityData.x;
//    float absVelX = Mathf.Abs(m_movePowerX);
//    //*|***|***|***|***|***|***|***|***|***|***|***|
//    // 移動ベクトルが小さいと認知しない
//    //*|***|***|***|***|***|***|***|***|***|***|***|
//    if (absVelX < 0.01f)
//    {
//        m_movePowerX = 0;
//    }
//}
////*|***|***|***|***|***|***|***|***|***|***|***|
//// 起動：抵抗＆重力
////*|***|***|***|***|***|***|***|***|***|***|***|
//void UpdateResistance()
//{
//    //*|***|***|***|***|***|***|***|***|***|***|***|
//    // 移動ベクトル取得
//    //*|***|***|***|***|***|***|***|***|***|***|***|
//    Vector2 velocityData = m_rigid2D.velocity;
//    m_movePowerX = velocityData.x;
//    float absVelX = Mathf.Abs(m_movePowerX);
//    float resistance = 0.0f;
//    //*|***|***|***|***|***|***|***|***|***|***|***|
//    // 移動ベクトルが大きいと制限される
//    //*|***|***|***|***|***|***|***|***|***|***|***|
//    float maxPower = 20.0f;
//    //*|***|***|***|***|***|***|***|***|***|***|***|
//    // 脚強化状態で最大速度が変わる
//    //*|***|***|***|***|***|***|***|***|***|***|***|
//    if (m_legStrong)
//    {
//        maxPower = m_moveMaxStrongPower;
//    }
//    else
//    {
//        maxPower = m_moveMaxNormalPower;
//    }
//    //*|***|***|***|***|***|***|***|***|***|***|***|
//    // 速度オーバー
//    //*|***|***|***|***|***|***|***|***|***|***|***|
//    if (absVelX > maxPower)
//    {
//        float speedMax = MyCalculator.Multiplication(maxPower, 2);
//        float speedNow = MyCalculator.Multiplication(absVelX, 2);
//        float speedOver = MyCalculator.LeapReverseCalculation(0, speedNow, speedMax);
//        //*|***|***|***|***|***|***|***|***|***|***|***|
//        // 抵抗度
//        //*|***|***|***|***|***|***|***|***|***|***|***|
//        float resistancePar = MyCalculator.InversionOfProportion(speedOver);
//        float resistanceParX = MyCalculator.Multiplication(resistancePar, 2);
//        resistance = MyCalculator.InversionOfProportion(resistanceParX);

//        m_movePowerX *= resistance;

//    }
//    //*|***|***|***|***|***|***|***|***|***|***|***|
//    // 前方に進んでいないと制限される
//    //*|***|***|***|***|***|***|***|***|***|***|***|
//    if (m_reverseArrow || !m_addPower)
//    {
//        resistance = 0.98f;
//        m_movePowerX *= resistance;
//        absVelX = Mathf.Abs(m_movePowerX);
//        if (absVelX < 0.01f)
//        {
//            m_movePowerX = 0;
//            if (m_addPower)
//            {
//                m_moveingPower = true;
//            }
//            else
//            {
//                m_moveingPower = false;
//            }
//        }
//    }

//    velocityData.x = m_movePowerX;
//    this.m_rigid2D.velocity = velocityData;
//}
////*|***|***|***|***|***|***|***|***|***|***|***|
//// 起動：ジャンプ
////*|***|***|***|***|***|***|***|***|***|***|***|
//void UpdateJumpCommond()
//{
//    Vector2 velocityData = m_rigid2D.velocity;
//    Vector2 jumpForce;
//    if (m_controllerData.ChackJumpTrigger() && m_groundFlagFlame)
//    {
//        m_rigid2D.velocity = new Vector2(velocityData.x, 0);
//        //*|***|***|***|***|***|***|***|***|***|***|***|
//        // 脚強化状態
//        //*|***|***|***|***|***|***|***|***|***|***|***|
//        if (m_legStrong)
//        {
//            jumpForce = (transform.up * m_jumpStrongPower);
//        }
//        else
//        {
//            jumpForce = (transform.up * m_jumpNormalPower);
//        }
//        //*|***|***|***|***|***|***|***|***|***|***|***|
//        // ジャンプパワー
//        //*|***|***|***|***|***|***|***|***|***|***|***|
//        this.m_rigid2D.AddForce(jumpForce);
//        //*|***|***|***|***|***|***|***|***|***|***|***|
//        // ジャンプの音~~~~
//        //*|***|***|***|***|***|***|***|***|***|***|***|
//        //soundJump.PlayOneShot(soundJump.clip);
//    }
//}
