using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

using WarehouseData;
//*|***|***|***|***|***|***|***|***|***|***|***|
// 言い換え
//*|***|***|***|***|***|***|***|***|***|***|***|
using TexImageData = GameDataPublic.TexImageData;
using RenderImageData = GameDataPublic.RenderImageData;
using TexImageHidden = GameDataPublic.TexImageHidden;
//*|***|***|***|***|***|***|***|***|***|***|***|
// 番号データ共通
//*|***|***|***|***|***|***|***|***|***|***|***|
using WarehouseObject = WarehouseData.WarehouseObject;
using CommonImageNum = WarehouseData.WarehouseStaticData.Object2D_Numbers_Common;
using AppImageNum = WarehouseData.WarehouseStaticData.Object2D_Numbers_App;
using Symbol_ENUM = WarehouseData.WarehouseStaticData.Symbol_ENUM;

using PlayerAnotherImageNum = WarehouseData.PlayerData.WarehousePlayer.PlayerData_Another_Number_List;
using PlayerImageNum = WarehouseData.PlayerData.WarehousePlayer.PlayerData_Number_List;

public class PlayerRecoveryUI : GameCanvas
{
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // プレイヤー倉庫
    //*|***|***|***|***|***|***|***|***|***|***|***|
    private WarehouseData.PlayerData.WarehousePlayer m_warehousePlayer;
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 回復コマンドパーツ
    //*|***|***|***|***|***|***|***|***|***|***|***|
    private class RecoveryPartsUI
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // コマンドの背景
        //*|***|***|***|***|***|***|***|***|***|***|***|
        public OriginUIGroup background;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // コマンドの本体
        //*|***|***|***|***|***|***|***|***|***|***|***|
        public OriginUIGroup action;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 腕回復
    //*|***|***|***|***|***|***|***|***|***|***|***|
    private RecoveryPartsUI m_healArm;
    private static string HealArmName = "HealArm";
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 体回復
    //*|***|***|***|***|***|***|***|***|***|***|***|
    private RecoveryPartsUI m_healBody;
    private static string HealBodyName = "HealBody";
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 頭回復
    //*|***|***|***|***|***|***|***|***|***|***|***|
    private RecoveryPartsUI m_healHead;
    private static string HealHeadName = "HealHead";
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 脚回復
    //*|***|***|***|***|***|***|***|***|***|***|***|
    private RecoveryPartsUI m_healLeg;
    private static string HealLegName = "HealLeg";
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 値
    //*|***|***|***|***|***|***|***|***|***|***|***|
    private int m_feeNumberDigit;

    private List<OriginUIGroup> m_armFeeNumber;
    private OriginUIGroup m_armFeeStar;
    private OriginUIGroup m_armFeeCross;

    private List<OriginUIGroup> m_bodyFeeNumber;
    private OriginUIGroup m_bodyFeeStar;
    private OriginUIGroup m_bodyFeeCross;

    private List<OriginUIGroup> m_headFeeNumber;
    private OriginUIGroup m_headFeeStar;
    private OriginUIGroup m_headFeeCross;

    private List<OriginUIGroup> m_legFeeNumber;
    private OriginUIGroup m_legFeeStar;
    private OriginUIGroup m_legFeeCross;


    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 全体パーツ
    //*|***|***|***|***|***|***|***|***|***|***|***|
    private OriginUIGroup m_partsAll;
    private static string PartsAllName = "PartsAll";
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 腕パーツ
    //*|***|***|***|***|***|***|***|***|***|***|***|
    private OriginUIGroup m_partsArm;
    private static string PartsArmName = "PartsArm";
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 体パーツ
    //*|***|***|***|***|***|***|***|***|***|***|***|
    private OriginUIGroup m_partsBody;
    private static string PartsBodyName = "PartsBody";
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 頭パーツ
    //*|***|***|***|***|***|***|***|***|***|***|***|
    private OriginUIGroup m_partsHead;
    private static string PartsHeadName = "PartsHead";
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 脚パーツ
    //*|***|***|***|***|***|***|***|***|***|***|***|
    private OriginUIGroup m_partsLeg;
    private static string PartsLegName = "PartsLeg";
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 腕輝パーツ
    //*|***|***|***|***|***|***|***|***|***|***|***|
    private OriginUIGroup m_partsArmShine;
    private static string PartsArmShineName = "PartsArmShine";
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 体輝パーツ
    //*|***|***|***|***|***|***|***|***|***|***|***|
    private OriginUIGroup m_partsBodyShine;
    private static string PartsBodyShineName = "PartsBodyShine";
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 頭輝パーツ
    //*|***|***|***|***|***|***|***|***|***|***|***|
    private OriginUIGroup m_partsHeadShine;
    private static string PartsHeadShineName = "PartsHeadShine";
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 脚輝パーツ
    //*|***|***|***|***|***|***|***|***|***|***|***|
    private OriginUIGroup m_partsLegShine;
    private static string PartsLegShineName = "PartsLegShine";

    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 背景の影
    //*|***|***|***|***|***|***|***|***|***|***|***|
    private OriginUIGroup m_backBlack;
    private static string BackBlackName = "BackBlack";
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 戻る
    //*|***|***|***|***|***|***|***|***|***|***|***|
    private OriginUIGroup m_return;
    private static string ReturnName = "Return";




    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 進行ゲージ
    //*|***|***|***|***|***|***|***|***|***|***|***|
    [SerializeField]
    private float m_progressParsent;
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 進行ゲージによる移動量
    //*|***|***|***|***|***|***|***|***|***|***|***|
    private Vector2 m_progressMovePlayer;
    private Vector2 m_progressMoveAction;
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 進行ゲージによる透明度
    //*|***|***|***|***|***|***|***|***|***|***|***|
    private float m_progressAlphaPlayer;
    private float m_progressAlphaAction;
    private float m_progressAlphaAnoter;
    private float m_progressAlphaRetrun;
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // ダメージ表示
    //*|***|***|***|***|***|***|***|***|***|***|***|
    [SerializeField]
    private float m_armDurableParsent;
    [SerializeField]
    private float m_bodyDurableParsent;
    [SerializeField]
    private float m_headDurableParsent;
    [SerializeField]
    private float m_legDurableParsent;
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 回復費
    //*|***|***|***|***|***|***|***|***|***|***|***|
    [SerializeField]
    private int m_armRecoveryFee;
    [SerializeField]
    private int m_bodyRecoveryFee;
    [SerializeField]
    private int m_headRecoveryFee;
    [SerializeField]
    private int m_legRecoveryFee;
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 強化表示
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
    // 強化費
    //*|***|***|***|***|***|***|***|***|***|***|***|
    [SerializeField]
    private int m_armStrongFee;
    [SerializeField]
    private int m_bodyStrongFee;
    [SerializeField]
    private int m_headStrongFee;
    [SerializeField]
    private int m_legStrongFee;
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 全体進行
    //*|***|***|***|***|***|***|***|***|***|***|***|
    [SerializeField]
    private float m_progressParsentAnimate;
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 強化か？
    //*|***|***|***|***|***|***|***|***|***|***|***|
    [SerializeField]
    private bool m_armStrongMode;
    [SerializeField]
    private bool m_bodyStrongMode;
    [SerializeField]
    private bool m_headStrongMode;
    [SerializeField]
    private bool m_legStrongMode;
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 使用する深度
    //*|***|***|***|***|***|***|***|***|***|***|***|
    private enum DepthAttach
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // その他・背景
        //*|***|***|***|***|***|***|***|***|***|***|***|
        TOTALLY_BACKBLACK,
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // その他・戻る
        //*|***|***|***|***|***|***|***|***|***|***|***|
        TOTALLY_RETURN,
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // プレイヤー全面
        //*|***|***|***|***|***|***|***|***|***|***|***|
        TOTALLY_PLAYER_ALL,
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // プレイヤー各パーツ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        TOTALLY_PLAYER_PARTS,
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // プレイヤー各輝パーツ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        TOTALLY_PLAYER_PARTS_SHINE,
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // コマンドの背景
        //*|***|***|***|***|***|***|***|***|***|***|***|
        COMMOND_BACKGROUND,
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // コマンドの本体
        //*|***|***|***|***|***|***|***|***|***|***|***|
        COMMOND_ACTION,
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // コマンドの費
        //*|***|***|***|***|***|***|***|***|***|***|***|
        COMMOND_FEE,
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 数量
        //*|***|***|***|***|***|***|***|***|***|***|***|
        NUM,
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 使用する最大深度
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public static int DepthMax = (int)DepthAttach.NUM;
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 獲得する深度
    //*|***|***|***|***|***|***|***|***|***|***|***|
    private static int GetDepth(DepthAttach data)
    {
        int dataNum = (int)data;
        return dataNum;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 進行ゲージ
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public void SetProgressParsent(float number)
    {
        m_progressParsent = number;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // ダメージ表示
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public void SetArmDurableParsent(float number)
    {
        m_armDurableParsent = number;
    }
    public void SetBodyDurableParsent(float number)
    {
        m_bodyDurableParsent = number;
    }
    public void SetHeadDurableParsent(float number)
    {
        m_headDurableParsent = number;
    }
    public void SetLegDurableParsent(float number)
    {
        m_legDurableParsent = number;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 回復費
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public void SetArmRecoveryFee(int number)
    {
        m_armRecoveryFee = number;
    }
    public void SetBodyRecoveryFee(int number)
    {
        m_bodyRecoveryFee = number;
    }
    public void SetHeadRecoveryFee(int number)
    {
        m_headRecoveryFee = number;
    }
    public void SetLegRecoveryFee(int number)
    {
        m_legRecoveryFee = number;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 強化表示
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public void SetArmStrong(bool number)
    {
        m_armStrong = number;
    }
    public void SetBodyStrong(bool number)
    {
        m_bodyStrong = number;
    }
    public void SetHeadStrong(bool number)
    {
        m_headStrong = number;
    }
    public void SetLegStrong(bool number)
    {
        m_legStrong = number;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 強化費
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public void SetArmStrongFee(int number)
    {
        m_armStrongFee = number;
    }
    public void SetBodyStrongFee(int number)
    {
        m_bodyStrongFee = number;
    }
    public void SetHeadStrongFee(int number)
    {
        m_headStrongFee = number;
    }
    public void SetLegStrongFee(int number)
    {
        m_legStrongFee = number;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 全体進行
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public void SetProgressParsentAnimate(float number)
    {
        m_progressParsentAnimate = number;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 強化か？
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public void SetArmStrongMode(bool number)
    {
        m_armStrongMode = number;
    }
    public void SetBodyStrongMode(bool number)
    {
        m_bodyStrongMode = number;
    }
    public void SetHeadStrongMode(bool number)
    {
        m_headStrongMode = number;
    }
    public void SetLegStrongMode(bool number)
    {
        m_legStrongMode = number;
    }


    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 起動したとき
    //*|***|***|***|***|***|***|***|***|***|***|***|
    protected override void AwakeImage()
    {

        //*|***|***|***|***|***|***|***|***|***|***|***|
        // プレイヤー倉庫
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_warehousePlayer = WarehouseData.PlayerData.WarehousePlayer.GetInstance();



        m_canvasObject = new GameObject("PlayerUICanvas");
        m_haveCanvas = m_canvasObject.AddComponent<Canvas>();
        m_canvasObject.AddComponent<GraphicRaycaster>();
        m_haveCanvas.renderMode = RenderMode.ScreenSpaceCamera;

        //*|***|***|***|***|***|***|***|***|***|***|***|
        // パーツ起動
        //*|***|***|***|***|***|***|***|***|***|***|***|
        AwakeParts();
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 腕回復起動
        //*|***|***|***|***|***|***|***|***|***|***|***|
        AwakeHealArm();
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 体回復起動
        //*|***|***|***|***|***|***|***|***|***|***|***|
        AwakeHealBody();
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 頭回復起動
        //*|***|***|***|***|***|***|***|***|***|***|***|
        AwakeHealHead();
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 脚回復起動
        //*|***|***|***|***|***|***|***|***|***|***|***|
        AwakeHealLeg();
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 値起動
        //*|***|***|***|***|***|***|***|***|***|***|***|
        AwakeFee();
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // その他起動
        //*|***|***|***|***|***|***|***|***|***|***|***|
        AwakeAnoters();
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 進行ゲージ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_progressParsent = 0;
        m_progressMovePlayer = Vector2.zero;
        m_progressMoveAction = Vector2.zero;
        m_progressAlphaPlayer = 0;
        m_progressAlphaAction = 0;
        m_progressAlphaAnoter = 0;
        m_progressAlphaRetrun = 0;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // パーツ起動
    //*|***|***|***|***|***|***|***|***|***|***|***|
    void AwakeParts()
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 実体獲得
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_partsAll = new OriginUIGroup();
        m_partsArm = new OriginUIGroup();
        m_partsBody = new OriginUIGroup();
        m_partsHead = new OriginUIGroup();
        m_partsLeg = new OriginUIGroup();
        m_partsArmShine = new OriginUIGroup();
        m_partsBodyShine = new OriginUIGroup();
        m_partsHeadShine = new OriginUIGroup();
        m_partsLegShine = new OriginUIGroup();
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // TexImageData
        //*|***|***|***|***|***|***|***|***|***|***|***|
        TexImageData tex = null;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 全体パーツ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        {
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // イメージのイラスト
            //*|***|***|***|***|***|***|***|***|***|***|***|
            tex = new TexImageData();
            tex.Reset();
            tex.image = m_warehousePlayer.GetAnotherTexture2D(PlayerAnotherImageNum.RECOVERYUI_ALLBODY);
            tex.rextParsent = MyCalculator.RectSizeReverse_Y(0, 1, 1);
            tex.size = new Vector2(1, 1);
            m_partsAll.gameObjectUI = CreateMenber(tex, PartsAllName);
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // イメージの情報
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_partsAll.gameObjectUI.SetDepth(GetDepth(DepthAttach.TOTALLY_PLAYER_ALL));

            m_partsAll.gameObjectUI.gameObject.transform.SetParent(m_canvasObject.gameObject.transform);
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 腕パーツ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        {
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // イメージのイラスト
            //*|***|***|***|***|***|***|***|***|***|***|***|
            tex = new TexImageData();
            tex.Reset();
            tex.image = m_warehousePlayer.GetAnotherTexture2D(PlayerAnotherImageNum.RECOVERYUI_REDARM);
            tex.rextParsent = MyCalculator.RectSizeReverse_Y(0, 1, 1);
            tex.size = new Vector2(1, 1);
            m_partsArm.gameObjectUI = CreateMenber(tex, PartsArmName);
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // イメージの情報
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_partsArm.gameObjectUI.SetDepth(GetDepth(DepthAttach.TOTALLY_PLAYER_PARTS));

            m_partsArm.gameObjectUI.gameObject.transform.SetParent(m_canvasObject.gameObject.transform);
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 体パーツ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        {
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // イメージのイラスト
            //*|***|***|***|***|***|***|***|***|***|***|***|
            tex = new TexImageData();
            tex.Reset();
            tex.image = m_warehousePlayer.GetAnotherTexture2D(PlayerAnotherImageNum.RECOVERYUI_REDBODY);
            tex.rextParsent = MyCalculator.RectSizeReverse_Y(0, 1, 1);
            tex.size = new Vector2(1, 1);
            m_partsBody.gameObjectUI = CreateMenber(tex, PartsBodyName);
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // イメージの情報
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_partsBody.gameObjectUI.SetDepth(GetDepth(DepthAttach.TOTALLY_PLAYER_PARTS));

            m_partsBody.gameObjectUI.gameObject.transform.SetParent(m_canvasObject.gameObject.transform);
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 頭パーツ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        {
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // イメージのイラスト
            //*|***|***|***|***|***|***|***|***|***|***|***|
            tex = new TexImageData();
            tex.Reset();
            tex.image = m_warehousePlayer.GetAnotherTexture2D(PlayerAnotherImageNum.RECOVERYUI_REDHEAD);
            tex.rextParsent = MyCalculator.RectSizeReverse_Y(0, 1, 1);
            tex.size = new Vector2(1, 1);
            m_partsHead.gameObjectUI = CreateMenber(tex, PartsHeadName);
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // イメージの情報
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_partsHead.gameObjectUI.SetDepth(GetDepth(DepthAttach.TOTALLY_PLAYER_PARTS));


            m_partsHead.gameObjectUI.gameObject.transform.SetParent(m_canvasObject.gameObject.transform);
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 脚パーツ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        {
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // イメージのイラスト
            //*|***|***|***|***|***|***|***|***|***|***|***|
            tex = new TexImageData();
            tex.Reset();
            tex.image = m_warehousePlayer.GetAnotherTexture2D(PlayerAnotherImageNum.RECOVERYUI_REDLEG);
            tex.rextParsent = MyCalculator.RectSizeReverse_Y(0, 1, 1);
            tex.size = new Vector2(1, 1);
            m_partsLeg.gameObjectUI = CreateMenber(tex, PartsLegName);
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // イメージの情報
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_partsLeg.gameObjectUI.SetDepth(GetDepth(DepthAttach.TOTALLY_PLAYER_PARTS));

            m_partsLeg.gameObjectUI.gameObject.transform.SetParent(m_canvasObject.gameObject.transform);
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 腕輝パーツ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        {
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // イメージのイラスト
            //*|***|***|***|***|***|***|***|***|***|***|***|
            tex = new TexImageData();
            tex.Reset();
            tex.image = m_warehousePlayer.GetAnotherTexture2D(PlayerAnotherImageNum.RECOVERYUI_STRONGARM);
            tex.rextParsent = MyCalculator.RectSizeReverse_Y(0, 1, 1);
            tex.size = new Vector2(1, 1);
            m_partsArmShine.gameObjectUI = CreateMenber(tex, PartsArmShineName);
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // イメージの情報
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_partsArmShine.gameObjectUI.SetDepth(GetDepth(DepthAttach.TOTALLY_PLAYER_PARTS_SHINE));

            m_partsArmShine.gameObjectUI.gameObject.transform.SetParent(m_canvasObject.gameObject.transform);
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 体輝パーツ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        {
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // イメージのイラスト
            //*|***|***|***|***|***|***|***|***|***|***|***|
            tex = new TexImageData();
            tex.Reset();
            tex.image = m_warehousePlayer.GetAnotherTexture2D(PlayerAnotherImageNum.RECOVERYUI_STRONGBODY);
            tex.rextParsent = MyCalculator.RectSizeReverse_Y(0, 1, 1);
            tex.size = new Vector2(1, 1);
            m_partsBodyShine.gameObjectUI = CreateMenber(tex, PartsBodyShineName);
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // イメージの情報
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_partsBodyShine.gameObjectUI.SetDepth(GetDepth(DepthAttach.TOTALLY_PLAYER_PARTS_SHINE));

            m_partsBodyShine.gameObjectUI.gameObject.transform.SetParent(m_canvasObject.gameObject.transform);
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 頭輝パーツ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        {
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // イメージのイラスト
            //*|***|***|***|***|***|***|***|***|***|***|***|
            tex = new TexImageData();
            tex.Reset();
            tex.image = m_warehousePlayer.GetAnotherTexture2D(PlayerAnotherImageNum.RECOVERYUI_STRONGHEAD);
            tex.rextParsent = MyCalculator.RectSizeReverse_Y(0, 1, 1);
            tex.size = new Vector2(1, 1);
            m_partsHeadShine.gameObjectUI = CreateMenber(tex, PartsHeadShineName);
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // イメージの情報
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_partsHeadShine.gameObjectUI.SetDepth(GetDepth(DepthAttach.TOTALLY_PLAYER_PARTS_SHINE));


            m_partsHeadShine.gameObjectUI.gameObject.transform.SetParent(m_canvasObject.gameObject.transform);
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 脚輝パーツ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        {
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // イメージのイラスト
            //*|***|***|***|***|***|***|***|***|***|***|***|
            tex = new TexImageData();
            tex.Reset();
            tex.image = m_warehousePlayer.GetAnotherTexture2D(PlayerAnotherImageNum.RECOVERYUI_STRONGLEG);
            tex.rextParsent = MyCalculator.RectSizeReverse_Y(0, 1, 1);
            tex.size = new Vector2(1, 1);
            m_partsLegShine.gameObjectUI = CreateMenber(tex, PartsLegShineName);
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // イメージの情報
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_partsLegShine.gameObjectUI.SetDepth(GetDepth(DepthAttach.TOTALLY_PLAYER_PARTS_SHINE));

            m_partsLegShine.gameObjectUI.gameObject.transform.SetParent(m_canvasObject.gameObject.transform);
        }
}
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 腕回復起動
    //*|***|***|***|***|***|***|***|***|***|***|***|
    void AwakeHealArm()
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 実体獲得
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_healArm = new RecoveryPartsUI();
        m_healArm.action = new OriginUIGroup();
        m_healArm.background = new OriginUIGroup();
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // TexImageData
        //*|***|***|***|***|***|***|***|***|***|***|***|
        TexImageData tex = null;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // コマンドの背景
        //*|***|***|***|***|***|***|***|***|***|***|***|
        {
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // イメージのイラスト
            //*|***|***|***|***|***|***|***|***|***|***|***|
            tex = new TexImageData();
            tex.Reset();
            tex.image = m_warehousePlayer.GetAnotherTexture2D(PlayerAnotherImageNum.RECOVERYUI_HEALBACK_GREEN);
            tex.rextParsent = MyCalculator.RectSizeReverse_Y(0, 1, 1);
            tex.size = new Vector2(1, 1);
            m_healArm.background.gameObjectUI = CreateMenber(tex, HealArmName + "BACKGROUND");
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // イメージの情報
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_healArm.background.gameObjectUI.SetDepth(GetDepth(DepthAttach.COMMOND_BACKGROUND));

            m_healArm.background.gameObjectUI.gameObject.transform.SetParent(m_canvasObject.gameObject.transform);
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // コマンドの本体
        //*|***|***|***|***|***|***|***|***|***|***|***|
        {
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // イメージのイラスト
            //*|***|***|***|***|***|***|***|***|***|***|***|
            tex = new TexImageData();
            tex.Reset();
            tex.image = m_warehousePlayer.GetAnotherTexture2D(PlayerAnotherImageNum.RECOVERYUI_HEALARM);
            tex.rextParsent = MyCalculator.RectSizeReverse_Y(0, 1, 1);
            tex.size = new Vector2(1, 1);
            m_healArm.action.gameObjectUI = CreateMenber(tex, HealArmName + "ACTION");
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // イメージの情報
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_healArm.action.gameObjectUI.SetDepth(GetDepth(DepthAttach.COMMOND_ACTION));

            m_healArm.action.gameObjectUI.gameObject.transform.SetParent(m_canvasObject.gameObject.transform);
        }
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 体回復起動
    //*|***|***|***|***|***|***|***|***|***|***|***|
    void AwakeHealBody()
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 実体獲得
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_healBody = new RecoveryPartsUI();
        m_healBody.action = new OriginUIGroup();
        m_healBody.background = new OriginUIGroup();
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // TexImageData
        //*|***|***|***|***|***|***|***|***|***|***|***|
        TexImageData tex = null;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // コマンドの背景
        //*|***|***|***|***|***|***|***|***|***|***|***|
        {
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // イメージのイラスト
            //*|***|***|***|***|***|***|***|***|***|***|***|
            tex = new TexImageData();
            tex.Reset();
            tex.image = m_warehousePlayer.GetAnotherTexture2D(PlayerAnotherImageNum.RECOVERYUI_HEALBACK_GREEN);
            tex.rextParsent = MyCalculator.RectSizeReverse_Y(0, 1, 1);
            tex.size = new Vector2(1, 1);
            m_healBody.background.gameObjectUI = CreateMenber(tex, HealBodyName + "BACKGROUND");
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // イメージの情報
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_healBody.background.gameObjectUI.SetDepth(GetDepth(DepthAttach.COMMOND_BACKGROUND));

            m_healBody.background.gameObjectUI.gameObject.transform.SetParent(m_canvasObject.gameObject.transform);
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // コマンドの本体
        //*|***|***|***|***|***|***|***|***|***|***|***|
        {
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // イメージのイラスト
            //*|***|***|***|***|***|***|***|***|***|***|***|
            tex = new TexImageData();
            tex.Reset();
            tex.image = m_warehousePlayer.GetAnotherTexture2D(PlayerAnotherImageNum.RECOVERYUI_HEALBODY);
            tex.rextParsent = MyCalculator.RectSizeReverse_Y(0, 1, 1);
            tex.size = new Vector2(1, 1);
            m_healBody.action.gameObjectUI = CreateMenber(tex, HealBodyName + "ACTION");
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // イメージの情報
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_healBody.action.gameObjectUI.SetDepth(GetDepth(DepthAttach.COMMOND_ACTION));

            m_healBody.action.gameObjectUI.gameObject.transform.SetParent(m_canvasObject.gameObject.transform);
        }
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 頭回復起動
    //*|***|***|***|***|***|***|***|***|***|***|***|
    void AwakeHealHead()
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 実体獲得
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_healHead = new RecoveryPartsUI();
        m_healHead.action = new OriginUIGroup();
        m_healHead.background = new OriginUIGroup();
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // TexImageData
        //*|***|***|***|***|***|***|***|***|***|***|***|
        TexImageData tex = null;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // コマンドの背景
        //*|***|***|***|***|***|***|***|***|***|***|***|
        {
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // イメージのイラスト
            //*|***|***|***|***|***|***|***|***|***|***|***|
            tex = new TexImageData();
            tex.Reset();
            tex.image = m_warehousePlayer.GetAnotherTexture2D(PlayerAnotherImageNum.RECOVERYUI_HEALBACK_GREEN);
            tex.rextParsent = MyCalculator.RectSizeReverse_Y(0, 1, 1);
            tex.size = new Vector2(1, 1);
            m_healHead.background.gameObjectUI = CreateMenber(tex, HealHeadName + "BACKGROUND");
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // イメージの情報
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_healHead.background.gameObjectUI.SetDepth(GetDepth(DepthAttach.COMMOND_BACKGROUND));

            m_healHead.background.gameObjectUI.gameObject.transform.SetParent(m_canvasObject.gameObject.transform);
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // コマンドの本体
        //*|***|***|***|***|***|***|***|***|***|***|***|
        {
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // イメージのイラスト
            //*|***|***|***|***|***|***|***|***|***|***|***|
            tex = new TexImageData();
            tex.Reset();
            tex.image = m_warehousePlayer.GetAnotherTexture2D(PlayerAnotherImageNum.RECOVERYUI_HEALHEAD);
            tex.rextParsent = MyCalculator.RectSizeReverse_Y(0, 1, 1);
            tex.size = new Vector2(1, 1);
            m_healHead.action.gameObjectUI = CreateMenber(tex, HealHeadName + "ACTION");
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // イメージの情報
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_healHead.action.gameObjectUI.SetDepth(GetDepth(DepthAttach.COMMOND_ACTION));

            m_healHead.action.gameObjectUI.gameObject.transform.SetParent(m_canvasObject.gameObject.transform);
        }
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 脚回復起動
    //*|***|***|***|***|***|***|***|***|***|***|***|
    void AwakeHealLeg()
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 実体獲得
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_healLeg = new RecoveryPartsUI();
        m_healLeg.action = new OriginUIGroup();
        m_healLeg.background = new OriginUIGroup();
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // TexImageData
        //*|***|***|***|***|***|***|***|***|***|***|***|
        TexImageData tex = null;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // コマンドの背景
        //*|***|***|***|***|***|***|***|***|***|***|***|
        {
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // イメージのイラスト
            //*|***|***|***|***|***|***|***|***|***|***|***|
            tex = new TexImageData();
            tex.Reset();
            tex.image = m_warehousePlayer.GetAnotherTexture2D(PlayerAnotherImageNum.RECOVERYUI_HEALBACK_GREEN);
            tex.rextParsent = MyCalculator.RectSizeReverse_Y(0, 1, 1);
            tex.size = new Vector2(1, 1);
            m_healLeg.background.gameObjectUI = CreateMenber(tex, HealLegName + "BACKGROUND");
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // イメージの情報
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_healLeg.background.gameObjectUI.SetDepth(GetDepth(DepthAttach.COMMOND_BACKGROUND));

            m_healLeg.background.gameObjectUI.gameObject.transform.SetParent(m_canvasObject.gameObject.transform);
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // コマンドの本体
        //*|***|***|***|***|***|***|***|***|***|***|***|
        {
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // イメージのイラスト
            //*|***|***|***|***|***|***|***|***|***|***|***|
            tex = new TexImageData();
            tex.Reset();
            tex.image = m_warehousePlayer.GetAnotherTexture2D(PlayerAnotherImageNum.RECOVERYUI_HEALLEG);
            tex.rextParsent = MyCalculator.RectSizeReverse_Y(0, 1, 1);
            tex.size = new Vector2(1, 1);
            m_healLeg.action.gameObjectUI = CreateMenber(tex, HealLegName + "ACTION");
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // イメージの情報
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_healLeg.action.gameObjectUI.SetDepth(GetDepth(DepthAttach.COMMOND_ACTION));

            m_healLeg.action.gameObjectUI.gameObject.transform.SetParent(m_canvasObject.gameObject.transform);
        }
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 値起動
    //*|***|***|***|***|***|***|***|***|***|***|***|
    void AwakeFee()
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // TexImageData
        //*|***|***|***|***|***|***|***|***|***|***|***|
        TexImageData tex = null;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 値
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_feeNumberDigit = 2;
        m_armFeeNumber = new List<OriginUIGroup>();
        m_bodyFeeNumber = new List<OriginUIGroup>();
        m_headFeeNumber = new List<OriginUIGroup>();
        m_legFeeNumber = new List<OriginUIGroup>();
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 値数字
        //*|***|***|***|***|***|***|***|***|***|***|***|
        for (int index = 0; index < m_feeNumberDigit; index++)
        {
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // イメージ作成
            //*|***|***|***|***|***|***|***|***|***|***|***|
            tex = new TexImageData();
            tex.Reset();
            tex.image = m_warehouseObject.GetTexture2DApp(AppImageNum.NUMBERS_DATA16_N1);
            tex.rextParsent = MyCalculator.RectSizeReverse_Y(0, 4, 4);
            tex.size = new Vector2(1, 1);
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 桁作成
            //*|***|***|***|***|***|***|***|***|***|***|***|
            OriginUIGroup digitDataArm = new OriginUIGroup();
            OriginUIGroup digitDataBody = new OriginUIGroup();
            OriginUIGroup digitDataHead = new OriginUIGroup();
            OriginUIGroup digitDataLeg = new OriginUIGroup();
            digitDataArm.gameObjectUI = CreateMenber(tex, "ArmFeeNumber" + index.ToString());
            digitDataBody.gameObjectUI = CreateMenber(tex, "BodyFeeNumber" + index.ToString());
            digitDataHead.gameObjectUI = CreateMenber(tex, "HeadFeeNumber" + index.ToString());
            digitDataLeg.gameObjectUI = CreateMenber(tex, "LegFeeNumber" + index.ToString());
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // イメージの情報
            //*|***|***|***|***|***|***|***|***|***|***|***|
            digitDataArm.gameObjectUI.SetDepth(GetDepth(DepthAttach.COMMOND_FEE));
            digitDataBody.gameObjectUI.SetDepth(GetDepth(DepthAttach.COMMOND_FEE));
            digitDataHead.gameObjectUI.SetDepth(GetDepth(DepthAttach.COMMOND_FEE));
            digitDataLeg.gameObjectUI.SetDepth(GetDepth(DepthAttach.COMMOND_FEE));

            digitDataArm.gameObjectUI.gameObject.transform.SetParent(m_canvasObject.gameObject.transform);
            digitDataBody.gameObjectUI.gameObject.transform.SetParent(m_canvasObject.gameObject.transform);
            digitDataHead.gameObjectUI.gameObject.transform.SetParent(m_canvasObject.gameObject.transform);
            digitDataLeg.gameObjectUI.gameObject.transform.SetParent(m_canvasObject.gameObject.transform);
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 桁挿入
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_armFeeNumber.Add(digitDataArm);
            m_bodyFeeNumber.Add(digitDataBody);
            m_headFeeNumber.Add(digitDataHead);
            m_legFeeNumber.Add(digitDataLeg);
        }
        m_armRecoveryFee = 0;
        m_armStrongFee = 0;
        m_bodyRecoveryFee = 0;
        m_bodyStrongFee = 0;
        m_headRecoveryFee = 0;
        m_headStrongFee = 0;
        m_legRecoveryFee = 0;
        m_legStrongFee = 0;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 値その他
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_armFeeCross = new OriginUIGroup();
        m_armFeeStar = new OriginUIGroup();
        m_bodyFeeCross = new OriginUIGroup();
        m_bodyFeeStar = new OriginUIGroup();
        m_headFeeCross = new OriginUIGroup();
        m_headFeeStar = new OriginUIGroup();
        m_legFeeCross = new OriginUIGroup();
        m_legFeeStar = new OriginUIGroup();
        {
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // イメージ作成
            //*|***|***|***|***|***|***|***|***|***|***|***|
            tex = new TexImageData();
            tex.Reset();
            tex.image = m_warehouseObject.GetTexture2DApp(AppImageNum.SYMBOL_N1);
            tex.rextParsent = MyCalculator.RectSizeReverse_Y((int)Symbol_ENUM.MULTIPLICATION, 4, 4);
            tex.size = new Vector2(1, 1);
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 桁作成
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_armFeeCross.gameObjectUI = CreateMenber(tex, "ArmFeeCross");
            m_bodyFeeCross.gameObjectUI = CreateMenber(tex, "BodyFeeCross");
            m_headFeeCross.gameObjectUI = CreateMenber(tex, "HeadFeeCross");
            m_legFeeCross.gameObjectUI = CreateMenber(tex, "LegFeeCross");
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // イメージの情報
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_armFeeCross.gameObjectUI.SetDepth(GetDepth(DepthAttach.COMMOND_FEE));
            m_bodyFeeCross.gameObjectUI.SetDepth(GetDepth(DepthAttach.COMMOND_FEE));
            m_headFeeCross.gameObjectUI.SetDepth(GetDepth(DepthAttach.COMMOND_FEE));
            m_legFeeCross.gameObjectUI.SetDepth(GetDepth(DepthAttach.COMMOND_FEE));

            m_armFeeCross.gameObjectUI.gameObject.transform.SetParent(m_canvasObject.gameObject.transform);
            m_bodyFeeCross.gameObjectUI.gameObject.transform.SetParent(m_canvasObject.gameObject.transform);
            m_headFeeCross.gameObjectUI.gameObject.transform.SetParent(m_canvasObject.gameObject.transform);
            m_legFeeCross.gameObjectUI.gameObject.transform.SetParent(m_canvasObject.gameObject.transform);
        }
        {
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // イメージ作成
            //*|***|***|***|***|***|***|***|***|***|***|***|
            tex = new TexImageData();
            tex.Reset();
            tex.image = m_warehouseObject.GetTexture2DApp(AppImageNum.SYMBOL_N1);
            tex.rextParsent = MyCalculator.RectSizeReverse_Y((int)Symbol_ENUM.STAR_IMAGE, 4, 4);
            tex.size = new Vector2(1, 1);
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 桁作成
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_armFeeStar.gameObjectUI = CreateMenber(tex, "ArmFeeStar");
            m_bodyFeeStar.gameObjectUI = CreateMenber(tex, "BodyFeeStar");
            m_headFeeStar.gameObjectUI = CreateMenber(tex, "HeadFeeStar");
            m_legFeeStar.gameObjectUI = CreateMenber(tex, "LegFeeStar");
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // イメージの情報
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_armFeeStar.gameObjectUI.SetDepth(GetDepth(DepthAttach.COMMOND_FEE));
            m_bodyFeeStar.gameObjectUI.SetDepth(GetDepth(DepthAttach.COMMOND_FEE));
            m_headFeeStar.gameObjectUI.SetDepth(GetDepth(DepthAttach.COMMOND_FEE));
            m_legFeeStar.gameObjectUI.SetDepth(GetDepth(DepthAttach.COMMOND_FEE));

            m_armFeeStar.gameObjectUI.gameObject.transform.SetParent(m_canvasObject.gameObject.transform);
            m_bodyFeeStar.gameObjectUI.gameObject.transform.SetParent(m_canvasObject.gameObject.transform);
            m_headFeeStar.gameObjectUI.gameObject.transform.SetParent(m_canvasObject.gameObject.transform);
            m_legFeeStar.gameObjectUI.gameObject.transform.SetParent(m_canvasObject.gameObject.transform);
        }
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // その他起動
    //*|***|***|***|***|***|***|***|***|***|***|***|
    void AwakeAnoters()
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 実体獲得
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_return = new OriginUIGroup();
        m_backBlack = new OriginUIGroup();
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // TexImageData
        //*|***|***|***|***|***|***|***|***|***|***|***|
        TexImageData tex = null;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 背景の影
        //*|***|***|***|***|***|***|***|***|***|***|***|
        {
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // イメージのイラスト
            //*|***|***|***|***|***|***|***|***|***|***|***|
            tex = new TexImageData();
            tex.Reset();
            tex.image = m_warehousePlayer.GetAnotherTexture2D(PlayerAnotherImageNum.RECOVERYUI_BACKBLACK);
            tex.rextParsent = MyCalculator.RectSizeReverse_Y(0, 1, 1);
            tex.size = new Vector2(1, 1);
            m_return.gameObjectUI = CreateMenber(tex, BackBlackName);
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // イメージの情報
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_return.gameObjectUI.SetDepth(GetDepth(DepthAttach.TOTALLY_BACKBLACK));

            m_return.gameObjectUI.gameObject.transform.SetParent(m_canvasObject.gameObject.transform);
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 戻る
        //*|***|***|***|***|***|***|***|***|***|***|***|
        {
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // イメージのイラスト
            //*|***|***|***|***|***|***|***|***|***|***|***|
            tex = new TexImageData();
            tex.Reset();
            tex.image = m_warehousePlayer.GetAnotherTexture2D(PlayerAnotherImageNum.RECOVERYUI_RETURN);
            tex.rextParsent = MyCalculator.RectSizeReverse_Y(0, 1, 1);
            tex.size = new Vector2(1, 1);
            m_backBlack.gameObjectUI = CreateMenber(tex, ReturnName);
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // イメージの情報
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_backBlack.gameObjectUI.SetDepth(GetDepth(DepthAttach.TOTALLY_RETURN));

            m_backBlack.gameObjectUI.gameObject.transform.SetParent(m_canvasObject.gameObject.transform);
        }
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 起動したとき
    //*|***|***|***|***|***|***|***|***|***|***|***|
    protected override void AwakeUI()
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // パーツ起動
        //*|***|***|***|***|***|***|***|***|***|***|***|
        AwakePartsUI();
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 腕回復起動
        //*|***|***|***|***|***|***|***|***|***|***|***|
        AwakeHealArmUI();
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 体回復起動
        //*|***|***|***|***|***|***|***|***|***|***|***|
        AwakeHealBodyUI();
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 頭回復起動
        //*|***|***|***|***|***|***|***|***|***|***|***|
        AwakeHealHeadUI();
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 脚回復起動
        //*|***|***|***|***|***|***|***|***|***|***|***|
        AwakeHealLegUI();
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 値起動
        //*|***|***|***|***|***|***|***|***|***|***|***|
        AwakeFeeUI();
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // その他起動
        //*|***|***|***|***|***|***|***|***|***|***|***|
        AwakeAnotersUI();
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // パーツ起動
    //*|***|***|***|***|***|***|***|***|***|***|***|
    void AwakePartsUI()
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 腕パーツ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_partsAll.imageUIData = new ImageUIData();
        m_partsAll.imageUIData.Init();
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 腕パーツ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_partsArm.imageUIData = new ImageUIData();
        m_partsArm.imageUIData.Init();
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 体パーツ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_partsBody.imageUIData = new ImageUIData();
        m_partsBody.imageUIData.Init();
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 頭パーツ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_partsHead.imageUIData = new ImageUIData();
        m_partsHead.imageUIData.Init();
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 脚パーツ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_partsLeg.imageUIData = new ImageUIData();
        m_partsLeg.imageUIData.Init();
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 腕輝パーツ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_partsArmShine.imageUIData = new ImageUIData();
        m_partsArmShine.imageUIData.Init();
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 体輝パーツ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_partsBodyShine.imageUIData = new ImageUIData();
        m_partsBodyShine.imageUIData.Init();
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 頭輝パーツ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_partsHeadShine.imageUIData = new ImageUIData();
        m_partsHeadShine.imageUIData.Init();
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 脚輝パーツ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_partsLegShine.imageUIData = new ImageUIData();
        m_partsLegShine.imageUIData.Init();
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 腕回復起動
    //*|***|***|***|***|***|***|***|***|***|***|***|
    void AwakeHealArmUI()
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // コマンドの背景
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_healArm.background.imageUIData = new ImageUIData();
        m_healArm.background.imageUIData.Init();
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // コマンドの本体
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_healArm.action.imageUIData = new ImageUIData();
        m_healArm.action.imageUIData.Init();
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 腕回復起動
    //*|***|***|***|***|***|***|***|***|***|***|***|
    void AwakeHealBodyUI()
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // コマンドの背景
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_healBody.background.imageUIData = new ImageUIData();
        m_healBody.background.imageUIData.Init();
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // コマンドの本体
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_healBody.action.imageUIData = new ImageUIData();
        m_healBody.action.imageUIData.Init();
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 頭回復起動
    //*|***|***|***|***|***|***|***|***|***|***|***|
    void AwakeHealHeadUI()
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // コマンドの背景
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_healHead.background.imageUIData = new ImageUIData();
        m_healHead.background.imageUIData.Init();
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // コマンドの本体
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_healHead.action.imageUIData = new ImageUIData();
        m_healHead.action.imageUIData.Init();
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 脚回復起動
    //*|***|***|***|***|***|***|***|***|***|***|***|
    void AwakeHealLegUI()
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // コマンドの背景
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_healLeg.background.imageUIData = new ImageUIData();
        m_healLeg.background.imageUIData.Init();
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // コマンドの本体
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_healLeg.action.imageUIData = new ImageUIData();
        m_healLeg.action.imageUIData.Init();
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 値起動
    //*|***|***|***|***|***|***|***|***|***|***|***|
    void AwakeFeeUI()
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 値数字
        //*|***|***|***|***|***|***|***|***|***|***|***|
        for (int index = 0; index < m_feeNumberDigit; index++)
        {
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 桁挿入
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_armFeeNumber[index].imageUIData = new ImageUIData();
            m_armFeeNumber[index].imageUIData.Init();
            m_bodyFeeNumber[index].imageUIData = new ImageUIData();
            m_bodyFeeNumber[index].imageUIData.Init();
            m_headFeeNumber[index].imageUIData = new ImageUIData();
            m_headFeeNumber[index].imageUIData.Init();
            m_legFeeNumber[index].imageUIData = new ImageUIData();
            m_legFeeNumber[index].imageUIData.Init();
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 値その他
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_armFeeCross.imageUIData = new ImageUIData();
        m_armFeeCross.imageUIData.Init();
        m_armFeeStar.imageUIData = new ImageUIData();
        m_armFeeStar.imageUIData.Init();
        m_bodyFeeCross.imageUIData = new ImageUIData();
        m_bodyFeeCross.imageUIData.Init();
        m_bodyFeeStar.imageUIData = new ImageUIData();
        m_bodyFeeStar.imageUIData.Init();
        m_headFeeCross.imageUIData = new ImageUIData();
        m_headFeeCross.imageUIData.Init();
        m_headFeeStar.imageUIData = new ImageUIData();
        m_headFeeStar.imageUIData.Init();
        m_legFeeCross.imageUIData = new ImageUIData();
        m_legFeeCross.imageUIData.Init();
        m_legFeeStar.imageUIData = new ImageUIData();
        m_legFeeStar.imageUIData.Init();
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // その他起動
    //*|***|***|***|***|***|***|***|***|***|***|***|
    void AwakeAnotersUI()
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 背景の影
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_return.imageUIData = new ImageUIData();
        m_return.imageUIData.Init();
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 戻る
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_backBlack.imageUIData = new ImageUIData();
        m_backBlack.imageUIData.Init();
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 更新したとき
    //*|***|***|***|***|***|***|***|***|***|***|***|
    protected override void UpdateWork()
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // パーツ更新
        //*|***|***|***|***|***|***|***|***|***|***|***|
        UpdatePartsUI();
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 腕回復更新
        //*|***|***|***|***|***|***|***|***|***|***|***|
        UpdateHealArm();
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 体回復更新
        //*|***|***|***|***|***|***|***|***|***|***|***|
        UpdateHealBody();
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 頭回復更新
        //*|***|***|***|***|***|***|***|***|***|***|***|
        UpdateHealHead();
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 脚回復更新
        //*|***|***|***|***|***|***|***|***|***|***|***|
        UpdateHealLeg();
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 脚回復更新
        //*|***|***|***|***|***|***|***|***|***|***|***|
        UpdateHealLeg();
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 値更新
        //*|***|***|***|***|***|***|***|***|***|***|***|
        UpdateFee();
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // その他更新
        //*|***|***|***|***|***|***|***|***|***|***|***|
        UpdateAnoters();
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // パーツ更新
    //*|***|***|***|***|***|***|***|***|***|***|***|
    void UpdatePartsUI()
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 進行ゲージ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_progressParsent = ChangeData.Among(m_progressParsent, 0.0f, 1.0f);
        float parsentAir = m_progressParsent;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 進行ゲージによる移動量
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_progressMovePlayer.x = MyCalculator.Leap(-1.0f, 1.0f, parsentAir);
        m_progressMovePlayer.x = MyCalculator.Leap(-0.02f, 0.02f, parsentAir);
        m_progressMovePlayer.y = 0.0f;
        m_progressMoveAction.x = 0.0f;
        m_progressMoveAction.y = MyCalculator.Leap(-1.0f, 1.0f, parsentAir);
        m_progressMoveAction.y = MyCalculator.Leap(-0.02f, 0.02f, parsentAir);
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 透明度
        //*|***|***|***|***|***|***|***|***|***|***|***|
        float parsentImage = Mathf.Abs(m_progressParsent - 0.5f) * MyCalculator.Division(1.0f, 0.5f);
        parsentImage = MyCalculator.InversionOfProportion(parsentImage);
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 進行ゲージによる透明度
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_progressAlphaPlayer = parsentImage;
        m_progressAlphaAction = parsentImage;
        m_progressAlphaAnoter = parsentImage;
        int ROUND = (int)parsentImage;
        m_progressAlphaRetrun = (float)ROUND;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // データ集計
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_armDurableParsent = ChangeData.Among(m_armDurableParsent, 0.0f, 1.0f);
        m_bodyDurableParsent = ChangeData.Among(m_bodyDurableParsent, 0.0f, 1.0f);
        m_headDurableParsent = ChangeData.Among(m_headDurableParsent, 0.0f, 1.0f);
        m_legDurableParsent = ChangeData.Among(m_legDurableParsent, 0.0f, 1.0f);
        float parsentArm = MyCalculator.InversionOfProportion(m_armDurableParsent);
        float parsentBody = MyCalculator.InversionOfProportion(m_bodyDurableParsent);
        float parsentHead = MyCalculator.InversionOfProportion(m_headDurableParsent);
        float parsentLeg = MyCalculator.InversionOfProportion(m_legDurableParsent);

        float strongArm = 0;
        if (m_armStrong)
        {
            strongArm = 1;
        }
        float strongBody = 0;
        if (m_bodyStrong)
        {
            strongBody = 1;
        }
        float strongHead = 0;
        if (m_headStrong)
        {
            strongHead = 1;
        }
        float strongLeg = 0;
        if (m_legStrong)
        {
            strongLeg = 1;
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // データ集計
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_progressParsentAnimate = ChangeData.Among(m_progressParsentAnimate, 0.0f, 1.0f);
        float shineAnimate = (m_progressParsentAnimate + 0.5f) / 2.0f;
        float redAnimate = (m_progressParsentAnimate * 2.0f) - 1.0f;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 画像データ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        //Rect imageRect = MyCalculator.RectSizeReverse_Y(0, 1, 1);
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 調整用データ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        float pointX = 0;
        float pointY = 0;
        float sizeX = 0;
        float sizeY = 0;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 調整用データ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        Vector2 pos = new Vector2(0.0f, 0.0f);
        Vector2 scale = new Vector2(0.0f, 0.0f);
        Vector2 persent_AssistS = new Vector2(0.5f, 0.5f);
        Vector2 persent_AssistP = new Vector2(0.5f, 0.5f);
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 調整済みデータ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        Vector2 posScleen = new Vector2(0.0f, 0.0f);
        Vector2 posReverseY = new Vector2(0.0f, 0.0f);
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 全ての位置
        //*|***|***|***|***|***|***|***|***|***|***|***|
        {
            persent_AssistS.x = MyCalculator.Division(1.0f, 1);
            persent_AssistS.y = MyCalculator.Division(1.0f, 1);
            persent_AssistP.x = MyCalculator.Division(persent_AssistS.x, 2);
            persent_AssistP.y = MyCalculator.Division(persent_AssistS.y, 2);
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 計算用初期化、データ確保
            //*|***|***|***|***|***|***|***|***|***|***|***|
            pos.x = persent_AssistP.x + m_progressMovePlayer.x;
            pos.y = persent_AssistP.y + m_progressMovePlayer.y;
            scale = persent_AssistS;
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 調整済みデータ
            //*|***|***|***|***|***|***|***|***|***|***|***|
            posScleen = MouseToScreenPos(pos);
            posReverseY = MyCalculator.EachTimes(posScleen, new Vector2(1.0f, -1.0f));
            pos = posReverseY;
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 以下も同じ場所に現れる
        //*|***|***|***|***|***|***|***|***|***|***|***|
        pointX = pos.x;
        pointY = pos.y;
        sizeX = scale.x;
        sizeY = scale.y;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 腕パーツ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        {
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // そのまま
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_partsAll.imageUIData.imagePos.x = pointX;
            m_partsAll.imageUIData.imagePos.y = pointY;
            m_partsAll.imageUIData.imageScale.x = sizeX;
            m_partsAll.imageUIData.imageScale.y = sizeY;
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 透明度
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_partsAll.gameObjectUI.SetAlpha(m_progressAlphaPlayer);
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 腕パーツ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        {
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // そのまま
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_partsArm.imageUIData.imagePos.x = pointX;
            m_partsArm.imageUIData.imagePos.y = pointY;
            m_partsArm.imageUIData.imageScale.x = sizeX;
            m_partsArm.imageUIData.imageScale.y = sizeY;
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 透明度
            //*|***|***|***|***|***|***|***|***|***|***|***|
            float alphaParsent = m_progressAlphaPlayer * redAnimate * parsentArm;
            m_partsArm.gameObjectUI.SetAlpha(alphaParsent);
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 体パーツ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        {
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // そのまま
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_partsBody.imageUIData.imagePos.x = pointX;
            m_partsBody.imageUIData.imagePos.y = pointY;
            m_partsBody.imageUIData.imageScale.x = sizeX;
            m_partsBody.imageUIData.imageScale.y = sizeY;
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 透明度
            //*|***|***|***|***|***|***|***|***|***|***|***|
            float alphaParsent = m_progressAlphaPlayer * redAnimate * parsentBody;
            m_partsBody.gameObjectUI.SetAlpha(alphaParsent);
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 頭パーツ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        {
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // そのまま
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_partsHead.imageUIData.imagePos.x = pointX;
            m_partsHead.imageUIData.imagePos.y = pointY;
            m_partsHead.imageUIData.imageScale.x = sizeX;
            m_partsHead.imageUIData.imageScale.y = sizeY;
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 透明度
            //*|***|***|***|***|***|***|***|***|***|***|***|
            float alphaParsent = m_progressAlphaPlayer * redAnimate * parsentHead;
            m_partsHead.gameObjectUI.SetAlpha(alphaParsent);
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 脚パーツ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        {
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // そのまま
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_partsLeg.imageUIData.imagePos.x = pointX;
            m_partsLeg.imageUIData.imagePos.y = pointY;
            m_partsLeg.imageUIData.imageScale.x = sizeX;
            m_partsLeg.imageUIData.imageScale.y = sizeY;
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 透明度
            //*|***|***|***|***|***|***|***|***|***|***|***|
            float alphaParsent = m_progressAlphaPlayer * redAnimate * parsentLeg;
            m_partsLeg.gameObjectUI.SetAlpha(alphaParsent);
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 腕輝パーツ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        {
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // そのまま
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_partsArmShine.imageUIData.imagePos.x = pointX;
            m_partsArmShine.imageUIData.imagePos.y = pointY;
            m_partsArmShine.imageUIData.imageScale.x = sizeX;
            m_partsArmShine.imageUIData.imageScale.y = sizeY;
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 透明度
            //*|***|***|***|***|***|***|***|***|***|***|***|
            float alphaParsent = m_progressAlphaPlayer * shineAnimate * strongArm;
            m_partsArmShine.gameObjectUI.SetAlpha(alphaParsent);
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 体輝パーツ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        {
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // そのまま
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_partsBodyShine.imageUIData.imagePos.x = pointX;
            m_partsBodyShine.imageUIData.imagePos.y = pointY;
            m_partsBodyShine.imageUIData.imageScale.x = sizeX;
            m_partsBodyShine.imageUIData.imageScale.y = sizeY;
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 透明度
            //*|***|***|***|***|***|***|***|***|***|***|***|
            float alphaParsent = m_progressAlphaPlayer * shineAnimate * strongBody;
            m_partsBodyShine.gameObjectUI.SetAlpha(alphaParsent);
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 頭輝パーツ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        {
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // そのまま
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_partsHeadShine.imageUIData.imagePos.x = pointX;
            m_partsHeadShine.imageUIData.imagePos.y = pointY;
            m_partsHeadShine.imageUIData.imageScale.x = sizeX;
            m_partsHeadShine.imageUIData.imageScale.y = sizeY;
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 透明度
            //*|***|***|***|***|***|***|***|***|***|***|***|
            float alphaParsent = m_progressAlphaPlayer * shineAnimate * strongHead;
            m_partsHeadShine.gameObjectUI.SetAlpha(alphaParsent);
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 脚輝パーツ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        {
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // そのまま
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_partsLegShine.imageUIData.imagePos.x = pointX;
            m_partsLegShine.imageUIData.imagePos.y = pointY;
            m_partsLegShine.imageUIData.imageScale.x = sizeX;
            m_partsLegShine.imageUIData.imageScale.y = sizeY;
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 透明度
            //*|***|***|***|***|***|***|***|***|***|***|***|
            float alphaParsent = m_progressAlphaPlayer * shineAnimate * strongLeg;
            m_partsLegShine.gameObjectUI.SetAlpha(alphaParsent);
        }
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 腕回復更新
    //*|***|***|***|***|***|***|***|***|***|***|***|
    void UpdateHealArm()
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // データ集計
        //*|***|***|***|***|***|***|***|***|***|***|***|
        float parsentArm = MyCalculator.InversionOfProportion(m_armDurableParsent);
        float redAnimate = (m_progressParsentAnimate * 2.0f) - 1.0f;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 調整用データ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        float pointX = 0;
        float pointY = 0;
        float sizeX = 0;
        float sizeY = 0;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 調整用データ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        Vector2 pos = new Vector2(0.0f, 0.0f);
        Vector2 scale = new Vector2(0.0f, 0.0f);
        Vector2 persent_AssistS = new Vector2(0.5f, 0.5f);
        Vector2 persent_AssistP = new Vector2(0.5f, 0.5f);
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 調整済みデータ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        Vector2 posScleen = new Vector2(0.0f, 0.0f);
        Vector2 posReverseY = new Vector2(0.0f, 0.0f);
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // コマンドのイラスト
        //*|***|***|***|***|***|***|***|***|***|***|***|
        if (parsentArm > 0.9f && redAnimate > 0)
        {
            m_healArm.background.gameObjectUI.SetTexture(m_warehousePlayer.GetAnotherTexture2D(PlayerAnotherImageNum.RECOVERYUI_HEALBACK_RED));
        }
        else if (parsentArm == 0)
        {
            m_healArm.background.gameObjectUI.SetTexture(m_warehousePlayer.GetAnotherTexture2D(PlayerAnotherImageNum.RECOVERYUI_HEALBACK_GOLD));
        }
        else
        {
            m_healArm.background.gameObjectUI.SetTexture(m_warehousePlayer.GetAnotherTexture2D(PlayerAnotherImageNum.RECOVERYUI_HEALBACK_GREEN));
        }




        //*|***|***|***|***|***|***|***|***|***|***|***|
        // コマンドの背景
        //*|***|***|***|***|***|***|***|***|***|***|***|
        {
            persent_AssistS.x = MyCalculator.Division(0.2f, 1);
            persent_AssistS.y = MyCalculator.Division(0.2f, 1);
            persent_AssistP.x = 0.75f;
            persent_AssistP.y = 0.5f;
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 計算用初期化、データ確保
            //*|***|***|***|***|***|***|***|***|***|***|***|
            pos.x = persent_AssistP.x + m_progressMoveAction.x;
            pos.y = persent_AssistP.y + m_progressMoveAction.y;
            scale = persent_AssistS;
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 調整済みデータ
            //*|***|***|***|***|***|***|***|***|***|***|***|
            posScleen = MouseToScreenPos(pos);
            posReverseY = MyCalculator.EachTimes(posScleen, new Vector2(1.0f, -1.0f));
            pos = posReverseY;
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 桁挿入
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_healArm.background.imageUIData.imagePos = new Vector2(pos.x, pos.y);
            m_healArm.background.imageUIData.imageScale = new Vector2(scale.x, scale.y);
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 以下も同じ場所に現れる
        //*|***|***|***|***|***|***|***|***|***|***|***|
        pointX = m_healArm.background.imageUIData.imagePos.x;
        pointY = m_healArm.background.imageUIData.imagePos.y;
        sizeX = m_healArm.background.imageUIData.imageScale.x;
        sizeY = m_healArm.background.imageUIData.imageScale.y;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // コマンドの本体
        //*|***|***|***|***|***|***|***|***|***|***|***|
        {
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // そのまま
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_healArm.action.imageUIData.imagePos.x = pointX;
            m_healArm.action.imageUIData.imagePos.y = pointY;
            m_healArm.action.imageUIData.imageScale.x = sizeX;
            m_healArm.action.imageUIData.imageScale.y = sizeY;
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 透明度
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_healArm.action.gameObjectUI.SetAlpha(m_progressAlphaAction);
            m_healArm.background.gameObjectUI.SetAlpha(m_progressAlphaAction);
        }
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 体回復更新
    //*|***|***|***|***|***|***|***|***|***|***|***|
    void UpdateHealBody()
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // データ集計
        //*|***|***|***|***|***|***|***|***|***|***|***|
        float parsentBody = MyCalculator.InversionOfProportion(m_bodyDurableParsent);
        float redAnimate = (m_progressParsentAnimate * 2.0f) - 1.0f;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 調整用データ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        float pointX = 0;
        float pointY = 0;
        float sizeX = 0;
        float sizeY = 0;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 調整用データ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        Vector2 pos = new Vector2(0.0f, 0.0f);
        Vector2 scale = new Vector2(0.0f, 0.0f);
        Vector2 persent_AssistS = new Vector2(0.5f, 0.5f);
        Vector2 persent_AssistP = new Vector2(0.5f, 0.5f);
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 調整済みデータ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        Vector2 posScleen = new Vector2(0.0f, 0.0f);
        Vector2 posReverseY = new Vector2(0.0f, 0.0f);
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // コマンドのイラスト
        //*|***|***|***|***|***|***|***|***|***|***|***|
        if (parsentBody > 0.9f && redAnimate > 0)
        {
            m_healBody.background.gameObjectUI.SetTexture(m_warehousePlayer.GetAnotherTexture2D(PlayerAnotherImageNum.RECOVERYUI_HEALBACK_RED));
        }
        else if (parsentBody == 0)
        {
            m_healBody.background.gameObjectUI.SetTexture(m_warehousePlayer.GetAnotherTexture2D(PlayerAnotherImageNum.RECOVERYUI_HEALBACK_GOLD));
        }
        else
        {
            m_healBody.background.gameObjectUI.SetTexture(m_warehousePlayer.GetAnotherTexture2D(PlayerAnotherImageNum.RECOVERYUI_HEALBACK_GREEN));
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // コマンドの背景
        //*|***|***|***|***|***|***|***|***|***|***|***|
        {
            persent_AssistS.x = MyCalculator.Division(0.2f, 1);
            persent_AssistS.y = MyCalculator.Division(0.2f, 1);
            persent_AssistP.x = 0.25f;
            persent_AssistP.y = 0.5f;
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 計算用初期化、データ確保
            //*|***|***|***|***|***|***|***|***|***|***|***|
            pos.x = persent_AssistP.x + m_progressMoveAction.x;
            pos.y = persent_AssistP.y + m_progressMoveAction.y;
            scale = persent_AssistS;
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 調整済みデータ
            //*|***|***|***|***|***|***|***|***|***|***|***|
            posScleen = MouseToScreenPos(pos);
            posReverseY = MyCalculator.EachTimes(posScleen, new Vector2(1.0f, -1.0f));
            pos = posReverseY;
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 桁挿入
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_healBody.background.imageUIData.imagePos = new Vector2(pos.x, pos.y);
            m_healBody.background.imageUIData.imageScale = new Vector2(scale.x, scale.y);
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 以下も同じ場所に現れる
        //*|***|***|***|***|***|***|***|***|***|***|***|
        pointX = m_healBody.background.imageUIData.imagePos.x;
        pointY = m_healBody.background.imageUIData.imagePos.y;
        sizeX = m_healBody.background.imageUIData.imageScale.x;
        sizeY = m_healBody.background.imageUIData.imageScale.y;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // コマンドの本体
        //*|***|***|***|***|***|***|***|***|***|***|***|
        {
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // そのまま
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_healBody.action.imageUIData.imagePos.x = pointX;
            m_healBody.action.imageUIData.imagePos.y = pointY;
            m_healBody.action.imageUIData.imageScale.x = sizeX;
            m_healBody.action.imageUIData.imageScale.y = sizeY;
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 透明度
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_healBody.action.gameObjectUI.SetAlpha(m_progressAlphaAction);
            m_healBody.background.gameObjectUI.SetAlpha(m_progressAlphaAction);
        }
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 頭回復更新
    //*|***|***|***|***|***|***|***|***|***|***|***|
    void UpdateHealHead()
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // データ集計
        //*|***|***|***|***|***|***|***|***|***|***|***|
        float parsentHead = MyCalculator.InversionOfProportion(m_headDurableParsent);
        float redAnimate = (m_progressParsentAnimate * 2.0f) - 1.0f;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 調整用データ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        float pointX = 0;
        float pointY = 0;
        float sizeX = 0;
        float sizeY = 0;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 調整用データ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        Vector2 pos = new Vector2(0.0f, 0.0f);
        Vector2 scale = new Vector2(0.0f, 0.0f);
        Vector2 persent_AssistS = new Vector2(0.5f, 0.5f);
        Vector2 persent_AssistP = new Vector2(0.5f, 0.5f);
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 調整済みデータ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        Vector2 posScleen = new Vector2(0.0f, 0.0f);
        Vector2 posReverseY = new Vector2(0.0f, 0.0f);
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // コマンドのイラスト
        //*|***|***|***|***|***|***|***|***|***|***|***|
        if (parsentHead > 0.9f && redAnimate > 0)
        {
            m_healHead.background.gameObjectUI.SetTexture(m_warehousePlayer.GetAnotherTexture2D(PlayerAnotherImageNum.RECOVERYUI_HEALBACK_RED));
        }
        else if (parsentHead == 0)
        {
            m_healHead.background.gameObjectUI.SetTexture(m_warehousePlayer.GetAnotherTexture2D(PlayerAnotherImageNum.RECOVERYUI_HEALBACK_GOLD));
        }
        else
        {
            m_healHead.background.gameObjectUI.SetTexture(m_warehousePlayer.GetAnotherTexture2D(PlayerAnotherImageNum.RECOVERYUI_HEALBACK_GREEN));
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // コマンドの背景
        //*|***|***|***|***|***|***|***|***|***|***|***|
        {
            persent_AssistS.x = MyCalculator.Division(0.2f, 1);
            persent_AssistS.y = MyCalculator.Division(0.2f, 1);
            persent_AssistP.x = 0.5f;
            persent_AssistP.y = 0.25f;
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 計算用初期化、データ確保
            //*|***|***|***|***|***|***|***|***|***|***|***|
            pos.x = persent_AssistP.x + m_progressMoveAction.x;
            pos.y = persent_AssistP.y + m_progressMoveAction.y;
            scale = persent_AssistS;
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 調整済みデータ
            //*|***|***|***|***|***|***|***|***|***|***|***|
            posScleen = MouseToScreenPos(pos);
            posReverseY = MyCalculator.EachTimes(posScleen, new Vector2(1.0f, -1.0f));
            pos = posReverseY;
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 桁挿入
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_healHead.background.imageUIData.imagePos = new Vector2(pos.x, pos.y);
            m_healHead.background.imageUIData.imageScale = new Vector2(scale.x, scale.y);
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 以下も同じ場所に現れる
        //*|***|***|***|***|***|***|***|***|***|***|***|
        pointX = m_healHead.background.imageUIData.imagePos.x;
        pointY = m_healHead.background.imageUIData.imagePos.y;
        sizeX = m_healHead.background.imageUIData.imageScale.x;
        sizeY = m_healHead.background.imageUIData.imageScale.y;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // コマンドの本体
        //*|***|***|***|***|***|***|***|***|***|***|***|
        {
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // そのまま
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_healHead.action.imageUIData.imagePos.x = pointX;
            m_healHead.action.imageUIData.imagePos.y = pointY;
            m_healHead.action.imageUIData.imageScale.x = sizeX;
            m_healHead.action.imageUIData.imageScale.y = sizeY;
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 透明度
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_healHead.action.gameObjectUI.SetAlpha(m_progressAlphaAction);
            m_healHead.background.gameObjectUI.SetAlpha(m_progressAlphaAction);
        }
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 脚回復更新
    //*|***|***|***|***|***|***|***|***|***|***|***|
    void UpdateHealLeg()
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // データ集計
        //*|***|***|***|***|***|***|***|***|***|***|***|
        float parsentLeg = MyCalculator.InversionOfProportion(m_legDurableParsent);
        float redAnimate = (m_progressParsentAnimate * 2.0f) - 1.0f;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 調整用データ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        float pointX = 0;
        float pointY = 0;
        float sizeX = 0;
        float sizeY = 0;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 調整用データ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        Vector2 pos = new Vector2(0.0f, 0.0f);
        Vector2 scale = new Vector2(0.0f, 0.0f);
        Vector2 persent_AssistS = new Vector2(0.5f, 0.5f);
        Vector2 persent_AssistP = new Vector2(0.5f, 0.5f);
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 調整済みデータ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        Vector2 posScleen = new Vector2(0.0f, 0.0f);
        Vector2 posReverseY = new Vector2(0.0f, 0.0f);
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // コマンドのイラスト
        //*|***|***|***|***|***|***|***|***|***|***|***|
        if (parsentLeg > 0.9f && redAnimate > 0)
        {
            m_healLeg.background.gameObjectUI.SetTexture(m_warehousePlayer.GetAnotherTexture2D(PlayerAnotherImageNum.RECOVERYUI_HEALBACK_RED));
        }
        else if (parsentLeg == 0)
        {
            m_healLeg.background.gameObjectUI.SetTexture(m_warehousePlayer.GetAnotherTexture2D(PlayerAnotherImageNum.RECOVERYUI_HEALBACK_GOLD));
        }
        else
        {
            m_healLeg.background.gameObjectUI.SetTexture(m_warehousePlayer.GetAnotherTexture2D(PlayerAnotherImageNum.RECOVERYUI_HEALBACK_GREEN));
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // コマンドの背景
        //*|***|***|***|***|***|***|***|***|***|***|***|
        {
            persent_AssistS.x = MyCalculator.Division(0.2f, 1);
            persent_AssistS.y = MyCalculator.Division(0.2f, 1);
            persent_AssistP.x = 0.5f;
            persent_AssistP.y = 0.75f;
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 計算用初期化、データ確保
            //*|***|***|***|***|***|***|***|***|***|***|***|
            pos.x = persent_AssistP.x + m_progressMoveAction.x;
            pos.y = persent_AssistP.y + m_progressMoveAction.y;
            scale = persent_AssistS;
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 調整済みデータ
            //*|***|***|***|***|***|***|***|***|***|***|***|
            posScleen = MouseToScreenPos(pos);
            posReverseY = MyCalculator.EachTimes(posScleen, new Vector2(1.0f, -1.0f));
            pos = posReverseY;
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 桁挿入
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_healLeg.background.imageUIData.imagePos = new Vector2(pos.x, pos.y);
            m_healLeg.background.imageUIData.imageScale = new Vector2(scale.x, scale.y);
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 以下も同じ場所に現れる
        //*|***|***|***|***|***|***|***|***|***|***|***|
        pointX = m_healLeg.background.imageUIData.imagePos.x;
        pointY = m_healLeg.background.imageUIData.imagePos.y;
        sizeX = m_healLeg.background.imageUIData.imageScale.x;
        sizeY = m_healLeg.background.imageUIData.imageScale.y;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // コマンドの本体
        //*|***|***|***|***|***|***|***|***|***|***|***|
        {
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // そのまま
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_healLeg.action.imageUIData.imagePos.x = pointX;
            m_healLeg.action.imageUIData.imagePos.y = pointY;
            m_healLeg.action.imageUIData.imageScale.x = sizeX;
            m_healLeg.action.imageUIData.imageScale.y = sizeY;
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 透明度
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_healLeg.action.gameObjectUI.SetAlpha(m_progressAlphaAction);
            m_healLeg.background.gameObjectUI.SetAlpha(m_progressAlphaAction);
        }
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 値更新
    //*|***|***|***|***|***|***|***|***|***|***|***|
    void UpdateFee()
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 画像データ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        Rect imageRect = MyCalculator.RectSizeReverse_Y(0, 1, 1);
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // データ集計
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_armRecoveryFee = ChangeData.Among(m_armRecoveryFee, 0, 99);
        m_bodyRecoveryFee = ChangeData.Among(m_bodyRecoveryFee, 0, 99);
        m_headRecoveryFee = ChangeData.Among(m_headRecoveryFee, 0, 99);
        m_legRecoveryFee = ChangeData.Among(m_legRecoveryFee, 0, 99);
        m_armStrongFee = ChangeData.Among(m_armStrongFee, 0, 99);
        m_bodyStrongFee = ChangeData.Among(m_bodyStrongFee, 0, 99);
        m_headStrongFee = ChangeData.Among(m_headStrongFee, 0, 99);
        m_legStrongFee = ChangeData.Among(m_legStrongFee, 0, 99);
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 現れる数値
        //*|***|***|***|***|***|***|***|***|***|***|***|
        int digitNum = 0;
        for (int digit = 0; digit < m_feeNumberDigit; digit++)
        {
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 腕
            //*|***|***|***|***|***|***|***|***|***|***|***|
            if (m_armStrongMode)
            {
                digitNum = MyCalculator.Get10Digit(m_armStrongFee, digit);
            }
            else
            {
                digitNum = MyCalculator.Get10Digit(m_armRecoveryFee, digit);
            }
            imageRect = MyCalculator.RectSizeReverse_Y(digitNum, 4, 4);
            m_armFeeNumber[digit].gameObjectUI.SetRect(imageRect);
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 体
            //*|***|***|***|***|***|***|***|***|***|***|***|
            if (m_bodyStrongMode)
            {
                digitNum = MyCalculator.Get10Digit(m_bodyStrongFee, digit);
            }
            else
            {
                digitNum = MyCalculator.Get10Digit(m_bodyRecoveryFee, digit);
            }
            imageRect = MyCalculator.RectSizeReverse_Y(digitNum, 4, 4);
            m_bodyFeeNumber[digit].gameObjectUI.SetRect(imageRect);
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 頭
            //*|***|***|***|***|***|***|***|***|***|***|***|
            if (m_headStrongMode)
            {
                digitNum = MyCalculator.Get10Digit(m_headStrongFee, digit);
            }
            else
            {
                digitNum = MyCalculator.Get10Digit(m_headRecoveryFee, digit);
            }
            imageRect = MyCalculator.RectSizeReverse_Y(digitNum, 4, 4);
            m_headFeeNumber[digit].gameObjectUI.SetRect(imageRect);
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 脚
            //*|***|***|***|***|***|***|***|***|***|***|***|
            if (m_legStrongMode)
            {
                digitNum = MyCalculator.Get10Digit(m_legStrongFee, digit);
            }
            else
            {
                digitNum = MyCalculator.Get10Digit(m_legRecoveryFee, digit);
            }
            imageRect = MyCalculator.RectSizeReverse_Y(digitNum, 4, 4);
            m_legFeeNumber[digit].gameObjectUI.SetRect(imageRect);
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 調整用データ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        Vector2 posSave = new Vector2(0.0f, 0.0f);
        Vector2 scaleSave = new Vector2(0.0f, 0.0f);
        Vector2 posSaveX = new Vector2(0.0f, 0.0f);
        int indexReverse = 0;
        float indexPoint = 0;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 調整用データ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        Vector2 pos = new Vector2(0.0f, 0.0f);
        Vector2 scale = new Vector2(0.0f, 0.0f);
        Vector2 persent_AssistS = new Vector2(0.5f, 0.5f);
        Vector2 persent_AssistP = new Vector2(0.5f, 0.5f);
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 調整済みデータ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        Vector2 posScleen = new Vector2(0.0f, 0.0f);
        Vector2 posReverseY = new Vector2(0.0f, 0.0f);
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 本当の
        //*|***|***|***|***|***|***|***|***|***|***|***|
        int trueFeeNumberDigit = m_feeNumberDigit + 2;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 腕
        //*|***|***|***|***|***|***|***|***|***|***|***|
        {
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 計算用初期化、データ確保
            //*|***|***|***|***|***|***|***|***|***|***|***|
            pos = new Vector2(0.0f, -0.05f);
            scale = new Vector2(0.1f, 0.05f);
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 計算用初期化、データ確保
            //*|***|***|***|***|***|***|***|***|***|***|***|
            persent_AssistP = pos;
            persent_AssistS = scale;
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 調整
            //*|***|***|***|***|***|***|***|***|***|***|***|
            pos = persent_AssistP;
            scale = persent_AssistS;
            persent_AssistS.x = MyCalculator.Division(persent_AssistS.x, trueFeeNumberDigit);
            scale.x = persent_AssistS.x;
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 以下も同じ場所に現れる
        //*|***|***|***|***|***|***|***|***|***|***|***|
        posSave = pos;
        scaleSave = scale;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 腕
        //*|***|***|***|***|***|***|***|***|***|***|***|
        {
            posSaveX = m_healArm.background.imageUIData.imagePos;
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // クロス
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 計算用初期化、データ確保
            //*|***|***|***|***|***|***|***|***|***|***|***|
            indexReverse = MyCalculator.InversionOfIndex(trueFeeNumberDigit - 2, trueFeeNumberDigit);
            indexPoint = MyCalculator.IndexCenterPos(indexReverse, trueFeeNumberDigit);
            pos.x = posSave.x + (persent_AssistS.x * indexPoint);
            pos.y = posSave.y;
            scale = scaleSave;
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 補正
            //*|***|***|***|***|***|***|***|***|***|***|***|
            pos = pos + posSaveX;
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 星
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_armFeeCross.imageUIData.imagePos = new Vector2(pos.x, pos.y);
            m_armFeeCross.imageUIData.imageScale = new Vector2(scale.x, scale.y);
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 透明度
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_armFeeCross.gameObjectUI.SetAlpha(m_progressAlphaAction);
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 計算用初期化、データ確保
            //*|***|***|***|***|***|***|***|***|***|***|***|
            indexReverse = MyCalculator.InversionOfIndex(trueFeeNumberDigit - 1, trueFeeNumberDigit);
            indexPoint = MyCalculator.IndexCenterPos(indexReverse, trueFeeNumberDigit);
            pos.x = posSave.x + (persent_AssistS.x * indexPoint);
            pos.y = posSave.y;
            scale = scaleSave;
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 補正
            //*|***|***|***|***|***|***|***|***|***|***|***|
            pos = pos + posSaveX;
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 桁挿入
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_armFeeStar.imageUIData.imagePos = new Vector2(pos.x, pos.y);
            m_armFeeStar.imageUIData.imageScale = new Vector2(scale.x, scale.y);
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 透明度
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_armFeeStar.gameObjectUI.SetAlpha(m_progressAlphaAction);
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 数字
            //*|***|***|***|***|***|***|***|***|***|***|***|
            for (int index = 0; index < m_armFeeNumber.Count; index++)
            {
                //*|***|***|***|***|***|***|***|***|***|***|***|
                // 計算用初期化、データ確保
                //*|***|***|***|***|***|***|***|***|***|***|***|
                indexReverse = MyCalculator.InversionOfIndex(index, trueFeeNumberDigit);
                indexPoint = MyCalculator.IndexCenterPos(indexReverse, trueFeeNumberDigit);
                pos.x = posSave.x + (persent_AssistS.x * indexPoint);
                pos.y = posSave.y;
                scale = scaleSave;
                //*|***|***|***|***|***|***|***|***|***|***|***|
                // 補正
                //*|***|***|***|***|***|***|***|***|***|***|***|
                pos = pos + posSaveX;
                //*|***|***|***|***|***|***|***|***|***|***|***|
                // 桁挿入
                //*|***|***|***|***|***|***|***|***|***|***|***|
                m_armFeeNumber[index].imageUIData.imagePos = new Vector2(pos.x, pos.y);
                m_armFeeNumber[index].imageUIData.imageScale = new Vector2(scale.x, scale.y);
                //*|***|***|***|***|***|***|***|***|***|***|***|
                // 透明度
                //*|***|***|***|***|***|***|***|***|***|***|***|
                m_armFeeNumber[index].gameObjectUI.SetAlpha(m_progressAlphaAction);
            }
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 体
        //*|***|***|***|***|***|***|***|***|***|***|***|
        {
            posSaveX = m_healBody.background.imageUIData.imagePos;
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // クロス
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 計算用初期化、データ確保
            //*|***|***|***|***|***|***|***|***|***|***|***|
            indexReverse = MyCalculator.InversionOfIndex(trueFeeNumberDigit - 2, trueFeeNumberDigit);
            indexPoint = MyCalculator.IndexCenterPos(indexReverse, trueFeeNumberDigit);
            pos.x = posSave.x + (persent_AssistS.x * indexPoint);
            pos.y = posSave.y;
            scale = scaleSave;
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 補正
            //*|***|***|***|***|***|***|***|***|***|***|***|
            pos = pos + posSaveX;
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 星
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_bodyFeeCross.imageUIData.imagePos = new Vector2(pos.x, pos.y);
            m_bodyFeeCross.imageUIData.imageScale = new Vector2(scale.x, scale.y);
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 透明度
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_bodyFeeCross.gameObjectUI.SetAlpha(m_progressAlphaAction);
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 計算用初期化、データ確保
            //*|***|***|***|***|***|***|***|***|***|***|***|
            indexReverse = MyCalculator.InversionOfIndex(trueFeeNumberDigit - 1, trueFeeNumberDigit);
            indexPoint = MyCalculator.IndexCenterPos(indexReverse, trueFeeNumberDigit);
            pos.x = posSave.x + (persent_AssistS.x * indexPoint);
            pos.y = posSave.y;
            scale = scaleSave;
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 補正
            //*|***|***|***|***|***|***|***|***|***|***|***|
            pos = pos + posSaveX;
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 桁挿入
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_bodyFeeStar.imageUIData.imagePos = new Vector2(pos.x, pos.y);
            m_bodyFeeStar.imageUIData.imageScale = new Vector2(scale.x, scale.y);
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 透明度
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_bodyFeeStar.gameObjectUI.SetAlpha(m_progressAlphaAction);
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 数字
            //*|***|***|***|***|***|***|***|***|***|***|***|
            for (int index = 0; index < m_bodyFeeNumber.Count; index++)
            {
                //*|***|***|***|***|***|***|***|***|***|***|***|
                // 計算用初期化、データ確保
                //*|***|***|***|***|***|***|***|***|***|***|***|
                indexReverse = MyCalculator.InversionOfIndex(index, trueFeeNumberDigit);
                indexPoint = MyCalculator.IndexCenterPos(indexReverse, trueFeeNumberDigit);
                pos.x = posSave.x + (persent_AssistS.x * indexPoint);
                pos.y = posSave.y;
                scale = scaleSave;
                //*|***|***|***|***|***|***|***|***|***|***|***|
                // 補正
                //*|***|***|***|***|***|***|***|***|***|***|***|
                pos = pos + posSaveX;
                //*|***|***|***|***|***|***|***|***|***|***|***|
                // 桁挿入
                //*|***|***|***|***|***|***|***|***|***|***|***|
                m_bodyFeeNumber[index].imageUIData.imagePos = new Vector2(pos.x, pos.y);
                m_bodyFeeNumber[index].imageUIData.imageScale = new Vector2(scale.x, scale.y);
                //*|***|***|***|***|***|***|***|***|***|***|***|
                // 透明度
                //*|***|***|***|***|***|***|***|***|***|***|***|
                m_bodyFeeNumber[index].gameObjectUI.SetAlpha(m_progressAlphaAction);
            }
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 頭
        //*|***|***|***|***|***|***|***|***|***|***|***|
        {
            posSaveX = m_healHead.background.imageUIData.imagePos;
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // クロス
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 計算用初期化、データ確保
            //*|***|***|***|***|***|***|***|***|***|***|***|
            indexReverse = MyCalculator.InversionOfIndex(trueFeeNumberDigit - 2, trueFeeNumberDigit);
            indexPoint = MyCalculator.IndexCenterPos(indexReverse, trueFeeNumberDigit);
            pos.x = posSave.x + (persent_AssistS.x * indexPoint);
            pos.y = posSave.y;
            scale = scaleSave;
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 補正
            //*|***|***|***|***|***|***|***|***|***|***|***|
            pos = pos + posSaveX;
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 星
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_headFeeCross.imageUIData.imagePos = new Vector2(pos.x, pos.y);
            m_headFeeCross.imageUIData.imageScale = new Vector2(scale.x, scale.y);
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 透明度
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_headFeeCross.gameObjectUI.SetAlpha(m_progressAlphaAction);
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 計算用初期化、データ確保
            //*|***|***|***|***|***|***|***|***|***|***|***|
            indexReverse = MyCalculator.InversionOfIndex(trueFeeNumberDigit - 1, trueFeeNumberDigit);
            indexPoint = MyCalculator.IndexCenterPos(indexReverse, trueFeeNumberDigit);
            pos.x = posSave.x + (persent_AssistS.x * indexPoint);
            pos.y = posSave.y;
            scale = scaleSave;
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 補正
            //*|***|***|***|***|***|***|***|***|***|***|***|
            pos = pos + posSaveX;
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 桁挿入
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_headFeeStar.imageUIData.imagePos = new Vector2(pos.x, pos.y);
            m_headFeeStar.imageUIData.imageScale = new Vector2(scale.x, scale.y);
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 透明度
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_headFeeStar.gameObjectUI.SetAlpha(m_progressAlphaAction);
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 数字
            //*|***|***|***|***|***|***|***|***|***|***|***|
            for (int index = 0; index < m_headFeeNumber.Count; index++)
            {
                //*|***|***|***|***|***|***|***|***|***|***|***|
                // 計算用初期化、データ確保
                //*|***|***|***|***|***|***|***|***|***|***|***|
                indexReverse = MyCalculator.InversionOfIndex(index, trueFeeNumberDigit);
                indexPoint = MyCalculator.IndexCenterPos(indexReverse, trueFeeNumberDigit);
                pos.x = posSave.x + (persent_AssistS.x * indexPoint);
                pos.y = posSave.y;
                scale = scaleSave;
                //*|***|***|***|***|***|***|***|***|***|***|***|
                // 補正
                //*|***|***|***|***|***|***|***|***|***|***|***|
                pos = pos + posSaveX;
                //*|***|***|***|***|***|***|***|***|***|***|***|
                // 桁挿入
                //*|***|***|***|***|***|***|***|***|***|***|***|
                m_headFeeNumber[index].imageUIData.imagePos = new Vector2(pos.x, pos.y);
                m_headFeeNumber[index].imageUIData.imageScale = new Vector2(scale.x, scale.y);
                //*|***|***|***|***|***|***|***|***|***|***|***|
                // 透明度
                //*|***|***|***|***|***|***|***|***|***|***|***|
                m_headFeeNumber[index].gameObjectUI.SetAlpha(m_progressAlphaAction);
            }
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 脚
        //*|***|***|***|***|***|***|***|***|***|***|***|
        {
            posSaveX = m_healLeg.background.imageUIData.imagePos;
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // クロス
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 計算用初期化、データ確保
            //*|***|***|***|***|***|***|***|***|***|***|***|
            indexReverse = MyCalculator.InversionOfIndex(trueFeeNumberDigit - 2, trueFeeNumberDigit);
            indexPoint = MyCalculator.IndexCenterPos(indexReverse, trueFeeNumberDigit);
            pos.x = posSave.x + (persent_AssistS.x * indexPoint);
            pos.y = posSave.y;
            scale = scaleSave;
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 補正
            //*|***|***|***|***|***|***|***|***|***|***|***|
            pos = pos + posSaveX;
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 星
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_legFeeCross.imageUIData.imagePos = new Vector2(pos.x, pos.y);
            m_legFeeCross.imageUIData.imageScale = new Vector2(scale.x, scale.y);
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 透明度
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_legFeeCross.gameObjectUI.SetAlpha(m_progressAlphaAction);
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 計算用初期化、データ確保
            //*|***|***|***|***|***|***|***|***|***|***|***|
            indexReverse = MyCalculator.InversionOfIndex(trueFeeNumberDigit - 1, trueFeeNumberDigit);
            indexPoint = MyCalculator.IndexCenterPos(indexReverse, trueFeeNumberDigit);
            pos.x = posSave.x + (persent_AssistS.x * indexPoint);
            pos.y = posSave.y;
            scale = scaleSave;
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 補正
            //*|***|***|***|***|***|***|***|***|***|***|***|
            pos = pos + posSaveX;
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 桁挿入
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_legFeeStar.imageUIData.imagePos = new Vector2(pos.x, pos.y);
            m_legFeeStar.imageUIData.imageScale = new Vector2(scale.x, scale.y);
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 透明度
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_legFeeStar.gameObjectUI.SetAlpha(m_progressAlphaAction);
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 数字
            //*|***|***|***|***|***|***|***|***|***|***|***|
            for (int index = 0; index < m_legFeeNumber.Count; index++)
            {
                //*|***|***|***|***|***|***|***|***|***|***|***|
                // 計算用初期化、データ確保
                //*|***|***|***|***|***|***|***|***|***|***|***|
                indexReverse = MyCalculator.InversionOfIndex(index, trueFeeNumberDigit);
                indexPoint = MyCalculator.IndexCenterPos(indexReverse, trueFeeNumberDigit);
                pos.x = posSave.x + (persent_AssistS.x * indexPoint);
                pos.y = posSave.y;
                scale = scaleSave;
                //*|***|***|***|***|***|***|***|***|***|***|***|
                // 補正
                //*|***|***|***|***|***|***|***|***|***|***|***|
                pos = pos + posSaveX;
                //*|***|***|***|***|***|***|***|***|***|***|***|
                // 桁挿入
                //*|***|***|***|***|***|***|***|***|***|***|***|
                m_legFeeNumber[index].imageUIData.imagePos = new Vector2(pos.x, pos.y);
                m_legFeeNumber[index].imageUIData.imageScale = new Vector2(scale.x, scale.y);
                //*|***|***|***|***|***|***|***|***|***|***|***|
                // 透明度
                //*|***|***|***|***|***|***|***|***|***|***|***|
                m_legFeeNumber[index].gameObjectUI.SetAlpha(m_progressAlphaAction);
            }
        }
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // その他更新
    //*|***|***|***|***|***|***|***|***|***|***|***|
    void UpdateAnoters()
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 画像データ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        //Rect imageRect = MyCalculator.RectSizeReverse_Y(0, 1, 1);
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 調整用データ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        float pointX = 0;
        float pointY = 0;
        float sizeX = 0;
        float sizeY = 0;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 調整用データ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        Vector2 pos = new Vector2(0.0f, 0.0f);
        Vector2 scale = new Vector2(0.0f, 0.0f);
        Vector2 persent_AssistS = new Vector2(0.5f, 0.5f);
        Vector2 persent_AssistP = new Vector2(0.5f, 0.5f);
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 調整済みデータ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        Vector2 posScleen = new Vector2(0.0f, 0.0f);
        Vector2 posReverseY = new Vector2(0.0f, 0.0f);
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 全ての位置
        //*|***|***|***|***|***|***|***|***|***|***|***|
        {
            persent_AssistS.x = MyCalculator.Division(1.0f, 1);
            persent_AssistS.y = MyCalculator.Division(1.0f, 1);
            persent_AssistP.x = MyCalculator.Division(persent_AssistS.x, 2);
            persent_AssistP.y = MyCalculator.Division(persent_AssistS.y, 2);
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 計算用初期化、データ確保
            //*|***|***|***|***|***|***|***|***|***|***|***|
            pos.x = persent_AssistP.x;
            pos.y = persent_AssistP.y;
            scale = persent_AssistS;
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 調整済みデータ
            //*|***|***|***|***|***|***|***|***|***|***|***|
            posScleen = MouseToScreenPos(pos);
            posReverseY = MyCalculator.EachTimes(posScleen, new Vector2(1.0f, -1.0f));
            pos = posReverseY;
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 以下も同じ場所に現れる
        //*|***|***|***|***|***|***|***|***|***|***|***|
        pointX = pos.x;
        pointY = pos.y;
        sizeX = scale.x;
        sizeY = scale.y;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 背景の影
        //*|***|***|***|***|***|***|***|***|***|***|***|
        {
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // そのまま
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_return.imageUIData.imagePos.x = pointX;
            m_return.imageUIData.imagePos.y = pointY;
            m_return.imageUIData.imageScale.x = sizeX;
            m_return.imageUIData.imageScale.y = sizeY;
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 透明度
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_return.gameObjectUI.SetAlpha(m_progressAlphaAnoter);
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 戻る
        //*|***|***|***|***|***|***|***|***|***|***|***|
        {
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // そのまま
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_backBlack.imageUIData.imagePos.x = pointX;
            m_backBlack.imageUIData.imagePos.y = pointY;
            m_backBlack.imageUIData.imageScale.x = sizeX;
            m_backBlack.imageUIData.imageScale.y = sizeY;
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 透明度
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_backBlack.gameObjectUI.SetAlpha(m_progressAlphaRetrun);
        }
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 更新したとき
    //*|***|***|***|***|***|***|***|***|***|***|***|
    protected override void UpdateImage()
    {

    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 更新したとき
    //*|***|***|***|***|***|***|***|***|***|***|***|
    protected override void UpdateUI()
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // パーツ描画
        //*|***|***|***|***|***|***|***|***|***|***|***|
        RenderPartsUI();
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 腕回復描画
        //*|***|***|***|***|***|***|***|***|***|***|***|
        RenderHealArm();
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 体回復描画
        //*|***|***|***|***|***|***|***|***|***|***|***|
        RenderHealBody();
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 頭回復描画
        //*|***|***|***|***|***|***|***|***|***|***|***|
        RenderHealHead();
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 脚回復描画
        //*|***|***|***|***|***|***|***|***|***|***|***|
        RenderHealLeg();
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 値描画
        //*|***|***|***|***|***|***|***|***|***|***|***|
        RenderFee();
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // その他描画
        //*|***|***|***|***|***|***|***|***|***|***|***|
        RenderAnoters();
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // パーツ描画
    //*|***|***|***|***|***|***|***|***|***|***|***|
    void RenderPartsUI()
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 全体パーツ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        {
            AssetSet(ref m_partsAll, m_screenSize, true);
        }  
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 腕パーツ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        {
            AssetSet(ref m_partsArm, m_screenSize, true);
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 体パーツ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        {
            AssetSet(ref m_partsBody, m_screenSize, true);
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 頭パーツ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        {
            AssetSet(ref m_partsHead, m_screenSize, true);
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 脚パーツ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        {
            AssetSet(ref m_partsLeg, m_screenSize, true);
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 腕輝パーツ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        {
            AssetSet(ref m_partsArmShine, m_screenSize, true);
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 体輝パーツ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        {
            AssetSet(ref m_partsBodyShine, m_screenSize, true);
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 頭輝パーツ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        {
            AssetSet(ref m_partsHeadShine, m_screenSize, true);
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 脚輝パーツ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        {
            AssetSet(ref m_partsLegShine, m_screenSize, true);
        }
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 腕回復描画
    //*|***|***|***|***|***|***|***|***|***|***|***|
    void RenderHealArm()
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // コマンドの背景
        //*|***|***|***|***|***|***|***|***|***|***|***|
        {
            AssetSet(ref m_healArm.background, m_screenSize, true);
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // コマンドの本体
        //*|***|***|***|***|***|***|***|***|***|***|***|
        {
            AssetSet(ref m_healArm.action, m_screenSize, true);
        }
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 体回復描画
    //*|***|***|***|***|***|***|***|***|***|***|***|
    void RenderHealBody()
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // コマンドの背景
        //*|***|***|***|***|***|***|***|***|***|***|***|
        {
            AssetSet(ref m_healBody.background, m_screenSize, true);
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // コマンドの本体
        //*|***|***|***|***|***|***|***|***|***|***|***|
        {
            AssetSet(ref m_healBody.action, m_screenSize, true);
        }
    }

    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 頭回復描画
    //*|***|***|***|***|***|***|***|***|***|***|***|
    void RenderHealHead()
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // コマンドの背景
        //*|***|***|***|***|***|***|***|***|***|***|***|
        {
            AssetSet(ref m_healHead.background, m_screenSize, true);
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // コマンドの本体
        //*|***|***|***|***|***|***|***|***|***|***|***|
        {
            AssetSet(ref m_healHead.action, m_screenSize, true);
        }
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 脚回復描画
    //*|***|***|***|***|***|***|***|***|***|***|***|
    void RenderHealLeg()
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // コマンドの背景
        //*|***|***|***|***|***|***|***|***|***|***|***|
        {
            AssetSet(ref m_healLeg.background, m_screenSize, true);
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // コマンドの本体
        //*|***|***|***|***|***|***|***|***|***|***|***|
        {
            AssetSet(ref m_healLeg.action, m_screenSize, true);
        }
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 値描画
    //*|***|***|***|***|***|***|***|***|***|***|***|
    void RenderFee()
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 腕
        //*|***|***|***|***|***|***|***|***|***|***|***|
        {
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // クロス
            //*|***|***|***|***|***|***|***|***|***|***|***|
            {
                AssetSet(ref m_armFeeCross, m_screenSize, true);
            }
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 星
            //*|***|***|***|***|***|***|***|***|***|***|***|
            {
                AssetSet(ref m_armFeeStar, m_screenSize, true);
            }
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 数字
            //*|***|***|***|***|***|***|***|***|***|***|***|
            for (int index = 0; index < m_armFeeNumber.Count; index++)
            {
                //*|***|***|***|***|***|***|***|***|***|***|***|
                // 桁挿入
                //*|***|***|***|***|***|***|***|***|***|***|***|
                OriginUIGroup migawari = m_armFeeNumber[index];
                AssetSet(ref migawari, m_screenSize, true);
                m_armFeeNumber[index] = migawari;
            }
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 体
        //*|***|***|***|***|***|***|***|***|***|***|***|
        {
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // クロス
            //*|***|***|***|***|***|***|***|***|***|***|***|
            {
                AssetSet(ref m_bodyFeeCross, m_screenSize, true);
            }
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 星
            //*|***|***|***|***|***|***|***|***|***|***|***|
            {
                AssetSet(ref m_bodyFeeStar, m_screenSize, true);
            }
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 数字
            //*|***|***|***|***|***|***|***|***|***|***|***|
            for (int index = 0; index < m_bodyFeeNumber.Count; index++)
            {
                //*|***|***|***|***|***|***|***|***|***|***|***|
                // 桁挿入
                //*|***|***|***|***|***|***|***|***|***|***|***|
                OriginUIGroup migawari = m_bodyFeeNumber[index];
                AssetSet(ref migawari, m_screenSize, true);
                m_bodyFeeNumber[index] = migawari;
            }
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 頭
        //*|***|***|***|***|***|***|***|***|***|***|***|
        {
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // クロス
            //*|***|***|***|***|***|***|***|***|***|***|***|
            {
                AssetSet(ref m_headFeeCross, m_screenSize, true);
            }
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 星
            //*|***|***|***|***|***|***|***|***|***|***|***|
            {
                AssetSet(ref m_headFeeStar, m_screenSize, true);
            }
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 数字
            //*|***|***|***|***|***|***|***|***|***|***|***|
            for (int index = 0; index < m_headFeeNumber.Count; index++)
            {
                //*|***|***|***|***|***|***|***|***|***|***|***|
                // 桁挿入
                //*|***|***|***|***|***|***|***|***|***|***|***|
                OriginUIGroup migawari = m_headFeeNumber[index];
                AssetSet(ref migawari, m_screenSize, true);
                m_headFeeNumber[index] = migawari;
            }
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 脚
        //*|***|***|***|***|***|***|***|***|***|***|***|
        {
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // クロス
            //*|***|***|***|***|***|***|***|***|***|***|***|
            {
                AssetSet(ref m_legFeeCross, m_screenSize, true);
            }
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 星
            //*|***|***|***|***|***|***|***|***|***|***|***|
            {
                AssetSet(ref m_legFeeStar, m_screenSize, true);
            }
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 数字
            //*|***|***|***|***|***|***|***|***|***|***|***|
            for (int index = 0; index < m_legFeeNumber.Count; index++)
            {
                //*|***|***|***|***|***|***|***|***|***|***|***|
                // 桁挿入
                //*|***|***|***|***|***|***|***|***|***|***|***|
                OriginUIGroup migawari = m_legFeeNumber[index];
                AssetSet(ref migawari, m_screenSize, true);
                m_legFeeNumber[index] = migawari;
            }
        }
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // その他描画
    //*|***|***|***|***|***|***|***|***|***|***|***|
    void RenderAnoters()
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 背景の影
        //*|***|***|***|***|***|***|***|***|***|***|***|
        {
            AssetSet(ref m_return, m_screenSize, true);
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 戻る
        //*|***|***|***|***|***|***|***|***|***|***|***|
        {
            AssetSet(ref m_backBlack, m_screenSize, true);
        }
    }
}
