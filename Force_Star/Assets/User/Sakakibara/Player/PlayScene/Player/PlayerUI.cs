﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

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
//*|***|***|***|***|***|***|***|***|***|***|***|
// パーツ言い換え
//*|***|***|***|***|***|***|***|***|***|***|***|
using PartsID = PlayStaticData.PartsID;



public class PlayerUI : GameCanvas
{

    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 酸素ゲージ
    // HIGH 画像の下
    // LOW  画像の上
    //*|***|***|***|***|***|***|***|***|***|***|***|
    static float AIR_HIGH = MyCalculator.Division(53.0f, 384.0f);
    static float AIR_LOW = MyCalculator.InversionOfProportion(MyCalculator.Division(34.0f, 384.0f));
    static float AIR_ROW = MyCalculator.Division(0.8f, 1.0f);
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 星ゲージ
    // HIGH 画像の下
    // LOW  画像の上
    //*|***|***|***|***|***|***|***|***|***|***|***|
    static float STAR_HIGH = MyCalculator.Division(12.0f, 227.0f);
    static float STAR_LOW = MyCalculator.InversionOfProportion(MyCalculator.Division(4.0f, 227.0f));
    static float STAR_ROW = 1.0f;


    //*|***|***|***|***|***|***|***|***|***|***|***|
    // プレイヤー倉庫
    //*|***|***|***|***|***|***|***|***|***|***|***|
    private WarehouseData.PlayerData.WarehousePlayer m_warehousePlayer;
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 酸素ゲージ
    //*|***|***|***|***|***|***|***|***|***|***|***|

    //private int m_airRateDigit;
    //private List<GameObjectUI> m_airRate;
    //private List<ImageUIData> m_airRateUI;

    private OriginUIGroup m_airGaugeMain;
    private OriginUIGroup m_airGaugeFrame;
    private OriginUIGroup m_airGaugeShadow;
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // ゲージのサイズと場所
    //*|***|***|***|***|***|***|***|***|***|***|***|
    private Vector2 m_airGaugeSize;
    private Vector2 m_airGaugeMaxSize;
    private Vector2 m_airGaugePos;

    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 星ゲージ
    //*|***|***|***|***|***|***|***|***|***|***|***|
    private int m_starRateDigit;
    private List<OriginUIGroup> m_starRate;

    private OriginUIGroup m_starRateParsent;


    private OriginUIGroup m_starGaugeMain;
    private OriginUIGroup m_starGaugeFrame;
    private OriginUIGroup m_starGaugeShadow;
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // ゲージのサイズと場所
    //*|***|***|***|***|***|***|***|***|***|***|***|
    private Vector2 m_starGaugeSize;
    private Vector2 m_starGaugeMaxSize;
    private Vector2 m_starGaugePos;

    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 所持数星量
    //*|***|***|***|***|***|***|***|***|***|***|***|
    private int m_haveStarDigit;
    private List<OriginUIGroup> m_haveStar;

    private OriginUIGroup m_haveStarKO;
    private OriginUIGroup m_haveStarStar;
    private OriginUIGroup m_haveStarCross;
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 所持数星量文字
    //*|***|***|***|***|***|***|***|***|***|***|***|
    private OriginUIGroup m_haveStarMozi;
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 目標数星量
    //*|***|***|***|***|***|***|***|***|***|***|***|
    private int m_needStarDigit;
    private List<OriginUIGroup> m_needStar;
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 目標数星量その他
    //*|***|***|***|***|***|***|***|***|***|***|***|
    private OriginUIGroup m_needStarKO;
    private OriginUIGroup m_needStarStar;
    private OriginUIGroup m_needStarCross;
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 目標数星量文字
    //*|***|***|***|***|***|***|***|***|***|***|***|
    private OriginUIGroup m_needStarMozi;
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 所持パーツ
    //*|***|***|***|***|***|***|***|***|***|***|***|
    private int m_partsCollectionDigit;
    private List<OriginUIGroup> m_partsCollection;
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // パワーアップタイム
    //*|***|***|***|***|***|***|***|***|***|***|***|
    private class PowerUpIcon
    {
        public OriginUIGroup m_icon;
        public OriginUIGroup m_time;
    }
    private PowerUpIcon m_armPowerUp;
    private PowerUpIcon m_bodyPowerUp;
    private PowerUpIcon m_headPowerUp;
    private PowerUpIcon m_legPowerUp;
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 警報
    //*|***|***|***|***|***|***|***|***|***|***|***|
    private OriginUIGroup m_alarm;
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // ムービー
    //*|***|***|***|***|***|***|***|***|***|***|***|
    private OriginUIGroup m_movie;
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 酸素ゲージ
    //*|***|***|***|***|***|***|***|***|***|***|***|
    [SerializeField]
    private float m_airParsent;
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 星ゲージ
    //*|***|***|***|***|***|***|***|***|***|***|***|
    [SerializeField]
    private float m_starParsent;
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 所持数星量
    //*|***|***|***|***|***|***|***|***|***|***|***|
    [SerializeField]
    private int m_haveStarNumber;
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 目標数星量
    //*|***|***|***|***|***|***|***|***|***|***|***|
    [SerializeField]
    private int m_needStarNumber;
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 所持パーツのデータ
    //*|***|***|***|***|***|***|***|***|***|***|***|
    [SerializeField]
    private uint m_partsCollectionNumber;
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // パワーアップタイム
    //*|***|***|***|***|***|***|***|***|***|***|***|
    [SerializeField]
    private float m_armStrongTime;
    [SerializeField]
    private float m_bodyStrongTime;
    [SerializeField]
    private float m_headStrongTime;
    [SerializeField]
    private float m_legStrongTime;
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 警報の濃度
    //*|***|***|***|***|***|***|***|***|***|***|***|
    [SerializeField]
    private float m_alarmStrong;
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 終了
    //*|***|***|***|***|***|***|***|***|***|***|***|
    [SerializeField]
    private float m_gameEnd;
    [SerializeField]
    private float m_gameMovie;
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 使用する深度
    //*|***|***|***|***|***|***|***|***|***|***|***|
    private enum DepthAttach
    { 
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 警報
        //*|***|***|***|***|***|***|***|***|***|***|***|
        ALARM,
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 酸素ゲージ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        AIRGAUGE_SHADOW,
        AIRGAUGE_MAIN,
        AIRGAUGE_FRAME,
        AIRGAUGE_NUMBER,
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 星ゲージ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        STARGAUGE_SHADOW,
        STARGAUGE_MAIN,
        STARGAUGE_FRAME,
        STARGAUGE_NUMBER,
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 所持数星量
        //*|***|***|***|***|***|***|***|***|***|***|***|
        HAVESTAR_NUMBER,
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 目標数星量
        //*|***|***|***|***|***|***|***|***|***|***|***|
        NEEDSTAR_NUMBER,
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // パワーアップ状態
        //*|***|***|***|***|***|***|***|***|***|***|***|
        PLAYER_STATE_TIME,
        PLAYER_STATE,
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // ムービー
        //*|***|***|***|***|***|***|***|***|***|***|***|
        MOVIE,
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
        dataNum += PlayerRecoveryUI.DepthMax;
        return dataNum;
    }


    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 酸素ゲージ
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public void SetAirGaugeNumber(float number)
    {
        m_airParsent = number;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 星ゲージ
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public void SetStarGaugeNumber(float number)
    {
        m_starParsent = number;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 所持数星量
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public void SetHaveStarNumber(int number)
    {
        m_haveStarNumber = number;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 目標数星量
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public void SetNeedStarNumber(int number)
    {
        m_needStarNumber = number;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 所持パーツのデータ
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public void SetPartsCollectionNumber(uint number)
    {
        m_partsCollectionNumber = number;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // パワーアップ
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public void SetArmStrongTime(float number)
    {
        m_armStrongTime = number;
    }
    public void SetBodyStrongTime(float number)
    {
        m_bodyStrongTime = number;
    }
    public void SetHeadStrongTime(float number)
    {
        m_headStrongTime = number;
    }
    public void SetLegStrongTime(float number)
    {
        m_legStrongTime = number;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 警報の濃度
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public void SetAlarmStrong(float number)
    {
        m_alarmStrong = number;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 終了
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public void SetGameEnd(float number)
    {
        m_gameEnd = number;
    }
    public void SetGameMovie(float number)
    {
        m_gameMovie = number;
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
        // 酸素ゲージ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        AwakeAirData();
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 星ゲージ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        AwakeStarData();
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 所持数星量
        //*|***|***|***|***|***|***|***|***|***|***|***|
        AwakeHaveStarData();
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 目標数星量
        //*|***|***|***|***|***|***|***|***|***|***|***|
        AwakeNeedStarData();
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 所持パーツ起動
        //*|***|***|***|***|***|***|***|***|***|***|***|
        AwakePartsCollection();
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 警報起動
        //*|***|***|***|***|***|***|***|***|***|***|***|
        AwakeAlarm();
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // パワーアップ起動
        //*|***|***|***|***|***|***|***|***|***|***|***|
        AwakePowerUp();
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 終了
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_gameEnd = 1.0f;
        m_gameMovie = 0.0f;
    }


    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 酸素ゲージ起動
    //*|***|***|***|***|***|***|***|***|***|***|***|
    void AwakeAirData()
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // TexImageData
        //*|***|***|***|***|***|***|***|***|***|***|***|
        TexImageData tex = null;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 酸素ゲージ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_airGaugeFrame = new OriginUIGroup();
        m_airGaugeMain = new OriginUIGroup();
        m_airGaugeShadow = new OriginUIGroup();
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 酸素ゲージフレーム
        //*|***|***|***|***|***|***|***|***|***|***|***|
        {
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // イメージのイラスト
            //*|***|***|***|***|***|***|***|***|***|***|***|
            tex = new TexImageData();
            tex.Reset();
            tex.image = m_warehousePlayer.GetAnotherTexture2D(PlayerAnotherImageNum.AIR_GAUGE_FRAME);
            tex.rextParsent = MyCalculator.RectSizeReverse_Y(0, 1, 1);
            tex.size = new Vector2(1, 1);
            m_airGaugeFrame.gameObjectUI = CreateMenber(tex, "AirGaugeFrame");
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // イメージの情報
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_airGaugeFrame.gameObjectUI.SetDepth(GetDepth(DepthAttach.AIRGAUGE_FRAME));

            m_airGaugeFrame.gameObjectUI.gameObject.transform.SetParent(m_canvasObject.gameObject.transform);
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 酸素ゲージ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        {
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // イメージのイラスト
            //*|***|***|***|***|***|***|***|***|***|***|***|
            tex = new TexImageData();
            tex.Reset();
            tex.image = m_warehousePlayer.GetAnotherTexture2D(PlayerAnotherImageNum.AIR_GAUGE_MAIN);
            tex.rextParsent = MyCalculator.RectSizeReverse_Y(0, 1, 1);
            tex.size = new Vector2(1, 1);
            m_airGaugeMain.gameObjectUI = CreateMenber(tex, "AirGaugeMain");
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // イメージの情報
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_airGaugeMain.gameObjectUI.SetPibot(new Vector2(0.5f, 0.0f));
            m_airGaugeMain.gameObjectUI.SetDepth(GetDepth(DepthAttach.AIRGAUGE_MAIN));

            m_airGaugeMain.gameObjectUI.gameObject.transform.SetParent(m_canvasObject.gameObject.transform);
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 酸素ゲージ影
        //*|***|***|***|***|***|***|***|***|***|***|***|
        {
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // イメージのイラスト
            //*|***|***|***|***|***|***|***|***|***|***|***|
            tex = new TexImageData();
            tex.Reset();
            tex.image = m_warehousePlayer.GetAnotherTexture2D(PlayerAnotherImageNum.AIR_GAUGE_SHADOW);
            tex.rextParsent = MyCalculator.RectSizeReverse_Y(0, 1, 1);
            tex.size = new Vector2(1, 1);
            m_airGaugeShadow.gameObjectUI = CreateMenber(tex, "AirGaugeShadow");
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // イメージの情報
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_airGaugeShadow.gameObjectUI.SetPibot(new Vector2(0.5f, 0.0f));
            m_airGaugeShadow.gameObjectUI.SetDepth(GetDepth(DepthAttach.AIRGAUGE_SHADOW));

            m_airGaugeShadow.gameObjectUI.gameObject.transform.SetParent(m_canvasObject.gameObject.transform);
        }
        m_airGaugeSize = new Vector2();
        m_airGaugeMaxSize = new Vector2();
        m_airGaugePos = new Vector2();
        m_airParsent = 1;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 星ゲージ起動
    //*|***|***|***|***|***|***|***|***|***|***|***|
    void AwakeStarData()
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // TexImageData
        //*|***|***|***|***|***|***|***|***|***|***|***|
        TexImageData tex = null;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 星ゲージ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_starRateDigit = 3;
        m_starRate = new List<OriginUIGroup>();
        m_starRateParsent = new OriginUIGroup();
        m_starGaugeFrame = new OriginUIGroup();
        m_starGaugeMain = new OriginUIGroup();
        m_starGaugeShadow = new OriginUIGroup();
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 星ゲージパーセント
        //*|***|***|***|***|***|***|***|***|***|***|***|
        for (int index = 0; index < m_starRateDigit; index++)
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
            OriginUIGroup digitData = new OriginUIGroup();
            digitData.gameObjectUI = CreateMenber(tex, "StarRate" + index.ToString());
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // イメージの情報
            //*|***|***|***|***|***|***|***|***|***|***|***|
            digitData.gameObjectUI.SetDepth(GetDepth(DepthAttach.STARGAUGE_NUMBER));
            digitData.gameObjectUI.gameObject.transform.SetParent(m_canvasObject.gameObject.transform);
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 桁挿入
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_starRate.Add(digitData);
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 星ゲージパーセント
        //*|***|***|***|***|***|***|***|***|***|***|***|
        {
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // イメージ作成
            //*|***|***|***|***|***|***|***|***|***|***|***|
            tex = new TexImageData();
            tex.Reset();
            tex.image = m_warehouseObject.GetTexture2DApp(AppImageNum.SYMBOL_N1);
            tex.rextParsent = MyCalculator.RectSizeReverse_Y((int)Symbol_ENUM.PERCENT, 4, 4);
            tex.size = new Vector2(1, 1);
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 桁作成
            //*|***|***|***|***|***|***|***|***|***|***|***|
            OriginUIGroup digitData = new OriginUIGroup();
            m_starRateParsent.gameObjectUI = CreateMenber(tex, "StarRateParsent");
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // イメージの情報
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_starRateParsent.gameObjectUI.SetDepth(GetDepth(DepthAttach.STARGAUGE_NUMBER));
            m_starRateParsent.gameObjectUI.gameObject.transform.SetParent(m_canvasObject.gameObject.transform);
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 星ゲージフレーム
        //*|***|***|***|***|***|***|***|***|***|***|***|
        {
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // イメージのイラスト
            //*|***|***|***|***|***|***|***|***|***|***|***|
            tex = new TexImageData();
            tex.Reset();
            tex.image = m_warehousePlayer.GetAnotherTexture2D(PlayerAnotherImageNum.STAR_GAUGE_FRAME);
            tex.rextParsent = MyCalculator.RectSizeReverse_Y(0, 1, 1);
            tex.size = new Vector2(1024, 1024);
            m_starGaugeFrame.gameObjectUI = CreateMenber(tex, "StarGaugeFrame");
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // イメージの情報
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_starGaugeFrame.gameObjectUI.SetDepth(GetDepth(DepthAttach.STARGAUGE_FRAME));

            m_starGaugeFrame.gameObjectUI.gameObject.transform.SetParent(m_canvasObject.gameObject.transform);
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 星ゲージ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        {
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // イメージのイラスト
            //*|***|***|***|***|***|***|***|***|***|***|***|
            tex = new TexImageData();
            tex.Reset();
            tex.image = m_warehousePlayer.GetAnotherTexture2D(PlayerAnotherImageNum.STAR_GAUGE_MAIN);
            tex.rextParsent = MyCalculator.RectSizeReverse_Y(0, 1, 1);
            tex.size = new Vector2(1, 1);
            m_starGaugeMain.gameObjectUI = CreateMenber(tex, "StarGaugeMain");
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // イメージの情報
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_starGaugeMain.gameObjectUI.SetPibot(new Vector2(0.5f, 0.0f));
            m_starGaugeMain.gameObjectUI.SetDepth(GetDepth(DepthAttach.STARGAUGE_MAIN));

            m_starGaugeMain.gameObjectUI.gameObject.transform.SetParent(m_canvasObject.gameObject.transform);
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 星ゲージ影
        //*|***|***|***|***|***|***|***|***|***|***|***|
        {
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // イメージのイラスト
            //*|***|***|***|***|***|***|***|***|***|***|***|
            tex = new TexImageData();
            tex.Reset();
            tex.image = m_warehousePlayer.GetAnotherTexture2D(PlayerAnotherImageNum.STAR_GAUGE_SHADOW);
            tex.rextParsent = MyCalculator.RectSizeReverse_Y(0, 1, 1);
            tex.size = new Vector2(1, 1);
            m_starGaugeShadow.gameObjectUI = CreateMenber(tex, "StarGaugeShadow");
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // イメージの情報
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_starGaugeShadow.gameObjectUI.SetPibot(new Vector2(0.5f, 0.0f));
            m_starGaugeShadow.gameObjectUI.SetDepth(GetDepth(DepthAttach.STARGAUGE_SHADOW));

            m_starGaugeShadow.gameObjectUI.gameObject.transform.SetParent(m_canvasObject.gameObject.transform);
        }
        m_starGaugeSize = new Vector2();
        m_starGaugeMaxSize = new Vector2();
        m_starGaugePos = new Vector2();
        m_starParsent = 0;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 所持数星量起動
    //*|***|***|***|***|***|***|***|***|***|***|***|
    void AwakeHaveStarData()
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // TexImageData
        //*|***|***|***|***|***|***|***|***|***|***|***|
        TexImageData tex = null;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 所持数星量
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_haveStarDigit = 5;
        m_haveStar = new List<OriginUIGroup>();
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 所持数星量数字
        //*|***|***|***|***|***|***|***|***|***|***|***|
        for (int index = 0; index < m_haveStarDigit; index++)
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
            OriginUIGroup digitData = new OriginUIGroup();
            digitData.gameObjectUI = CreateMenber(tex, "HaveStar" + index.ToString());
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // イメージの情報
            //*|***|***|***|***|***|***|***|***|***|***|***|
            digitData.gameObjectUI.SetDepth(GetDepth(DepthAttach.HAVESTAR_NUMBER));
            digitData.gameObjectUI.gameObject.transform.SetParent(m_canvasObject.gameObject.transform);
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 桁挿入
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_haveStar.Add(digitData);
        }
        m_haveStarNumber = 0;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 目標数星量その他
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_haveStarKO = new OriginUIGroup();
        m_haveStarStar = new OriginUIGroup();
        m_haveStarCross = new OriginUIGroup();
        {
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // イメージ作成
            //*|***|***|***|***|***|***|***|***|***|***|***|
            tex = new TexImageData();
            tex.Reset();
            tex.image = m_warehouseObject.GetTexture2DApp(AppImageNum.SYMBOL_N1);
            tex.rextParsent = MyCalculator.RectSizeReverse_Y((int)Symbol_ENUM.KO, 4, 4);
            tex.size = new Vector2(1, 1);
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 桁作成
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_haveStarKO.gameObjectUI = CreateMenber(tex, "HaveStarKO");
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // イメージの情報
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_haveStarKO.gameObjectUI.SetDepth(GetDepth(DepthAttach.HAVESTAR_NUMBER));
            m_haveStarKO.gameObjectUI.gameObject.transform.SetParent(m_canvasObject.gameObject.transform);
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
            m_haveStarStar.gameObjectUI = CreateMenber(tex, "HaveStarStar");
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // イメージの情報
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_haveStarStar.gameObjectUI.SetDepth(GetDepth(DepthAttach.HAVESTAR_NUMBER));
            m_haveStarStar.gameObjectUI.gameObject.transform.SetParent(m_canvasObject.gameObject.transform);
        }
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
            m_haveStarCross.gameObjectUI = CreateMenber(tex, "HaveStarCross");
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // イメージの情報
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_haveStarCross.gameObjectUI.SetDepth(GetDepth(DepthAttach.HAVESTAR_NUMBER));
            m_haveStarCross.gameObjectUI.gameObject.transform.SetParent(m_canvasObject.gameObject.transform);
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 目標数星量文字
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_haveStarMozi = new OriginUIGroup();
        {
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // イメージ作成
            //*|***|***|***|***|***|***|***|***|***|***|***|
            tex = new TexImageData();
            tex.Reset();
            tex.image = m_warehousePlayer.GetAnotherTexture2D(PlayerAnotherImageNum.SHOJIRYO);
            tex.rextParsent = MyCalculator.RectSizeReverse_Y(0, 1, 1);
            tex.size = new Vector2(1, 1);
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 桁作成
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_haveStarMozi.gameObjectUI = CreateMenber(tex, "HaveStarMozi");
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // イメージの情報
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_haveStarMozi.gameObjectUI.SetDepth(GetDepth(DepthAttach.HAVESTAR_NUMBER));
            m_haveStarMozi.gameObjectUI.gameObject.transform.SetParent(m_canvasObject.gameObject.transform);
        }
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 目標数星量起動
    //*|***|***|***|***|***|***|***|***|***|***|***|
    void AwakeNeedStarData()
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // TexImageData
        //*|***|***|***|***|***|***|***|***|***|***|***|
        TexImageData tex = null;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 目標数星量
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_needStarDigit = 5;
        m_needStar = new List<OriginUIGroup>();
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 目標数星量数字
        //*|***|***|***|***|***|***|***|***|***|***|***|
        for (int index = 0; index < m_needStarDigit; index++)
        {
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // イメージ作成
            //*|***|***|***|***|***|***|***|***|***|***|***|
            tex = new TexImageData();
            tex.Reset();
            tex.image = m_warehouseObject.GetTexture2DApp(AppImageNum.NUMBERS_DATA16_N2);
            tex.rextParsent = MyCalculator.RectSizeReverse_Y(0, 4, 4);
            tex.size = new Vector2(1, 1);
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 桁作成
            //*|***|***|***|***|***|***|***|***|***|***|***|
            OriginUIGroup digitData = new OriginUIGroup();
            digitData.gameObjectUI = CreateMenber(tex, "NeedStar" + index.ToString());
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // イメージの情報
            //*|***|***|***|***|***|***|***|***|***|***|***|
            digitData.gameObjectUI.SetDepth(GetDepth(DepthAttach.HAVESTAR_NUMBER));
            digitData.gameObjectUI.gameObject.transform.SetParent(m_canvasObject.gameObject.transform);
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 桁挿入
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_needStar.Add(digitData);
        }
        m_needStarNumber = 0;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 目標数星量その他
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_needStarKO = new OriginUIGroup();
        m_needStarStar = new OriginUIGroup();
        m_needStarCross = new OriginUIGroup();
        {
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // イメージ作成
            //*|***|***|***|***|***|***|***|***|***|***|***|
            tex = new TexImageData();
            tex.Reset();
            tex.image = m_warehouseObject.GetTexture2DApp(AppImageNum.SYMBOL_N1);
            tex.rextParsent = MyCalculator.RectSizeReverse_Y((int)Symbol_ENUM.KO, 4, 4);
            tex.size = new Vector2(1, 1);
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 桁作成
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_needStarKO.gameObjectUI = CreateMenber(tex, "NeedStarKO");
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // イメージの情報
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_needStarKO.gameObjectUI.SetDepth(GetDepth(DepthAttach.HAVESTAR_NUMBER));
            m_needStarKO.gameObjectUI.gameObject.transform.SetParent(m_canvasObject.gameObject.transform);
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
            m_needStarStar.gameObjectUI = CreateMenber(tex, "NeedStarStar");
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // イメージの情報
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_needStarStar.gameObjectUI.SetDepth(GetDepth(DepthAttach.HAVESTAR_NUMBER));
            m_needStarStar.gameObjectUI.gameObject.transform.SetParent(m_canvasObject.gameObject.transform);
        }
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
            m_needStarCross.gameObjectUI = CreateMenber(tex, "NeedStarCross");
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // イメージの情報
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_needStarCross.gameObjectUI.SetDepth(GetDepth(DepthAttach.HAVESTAR_NUMBER));
            m_needStarCross.gameObjectUI.gameObject.transform.SetParent(m_canvasObject.gameObject.transform);
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 目標数星量文字
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_needStarMozi = new OriginUIGroup();
        {
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // イメージ作成
            //*|***|***|***|***|***|***|***|***|***|***|***|
            tex = new TexImageData();
            tex.Reset();
            tex.image = m_warehousePlayer.GetAnotherTexture2D(PlayerAnotherImageNum.MOKUHYO);
            tex.rextParsent = MyCalculator.RectSizeReverse_Y(0, 1, 1);
            tex.size = new Vector2(1, 1);
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 桁作成
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_needStarMozi.gameObjectUI = CreateMenber(tex, "NeedStarMozi");
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // イメージの情報
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_needStarMozi.gameObjectUI.SetDepth(GetDepth(DepthAttach.HAVESTAR_NUMBER));
            m_needStarMozi.gameObjectUI.gameObject.transform.SetParent(m_canvasObject.gameObject.transform);
        }
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 所持パーツ起動
    //*|***|***|***|***|***|***|***|***|***|***|***|
    void AwakePartsCollection()
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // TexImageData
        //*|***|***|***|***|***|***|***|***|***|***|***|
        TexImageData tex = null;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 目標数星量
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_partsCollectionDigit = (int)PartsID.NUM;
        m_partsCollection = new List<OriginUIGroup>();
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 目標数星量数字
        //*|***|***|***|***|***|***|***|***|***|***|***|
        for (int index = 0; index < m_partsCollectionDigit; index++)
        {
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // イメージ作成
            //*|***|***|***|***|***|***|***|***|***|***|***|
            tex = new TexImageData();
            tex.Reset();
            tex.image = m_warehouseObject.GetTexture2DApp(AppImageNum.ROCKETPARTS);
            tex.rextParsent = MyCalculator.RectSizeReverse_Y(index, 3, 1);
            tex.size = new Vector2(1, 1);
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 桁作成
            //*|***|***|***|***|***|***|***|***|***|***|***|
            OriginUIGroup digitData = new OriginUIGroup();
            digitData.gameObjectUI = CreateMenber(tex, "PartCollection" + index.ToString());
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // イメージの情報
            //*|***|***|***|***|***|***|***|***|***|***|***|
            digitData.gameObjectUI.SetDepth(GetDepth(DepthAttach.HAVESTAR_NUMBER));
            digitData.gameObjectUI.gameObject.transform.SetParent(m_canvasObject.gameObject.transform);
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 桁挿入
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_partsCollection.Add(digitData);
        }
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 警報起動
    //*|***|***|***|***|***|***|***|***|***|***|***|
    void AwakeAlarm()
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // TexImageData
        //*|***|***|***|***|***|***|***|***|***|***|***|
        TexImageData tex = null; 
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 警報
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_alarm = new OriginUIGroup();
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 警報
        //*|***|***|***|***|***|***|***|***|***|***|***|
        {
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // イメージ作成
            //*|***|***|***|***|***|***|***|***|***|***|***|
            tex = new TexImageData();
            tex.Reset();
            tex.image = m_warehouseObject.GetTexture2DApp(AppImageNum.ALARMRED);
            tex.rextParsent = MyCalculator.RectSizeReverse_Y(0, 1, 1);
            tex.size = new Vector2(1, 1);
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 桁作成
            //*|***|***|***|***|***|***|***|***|***|***|***|
            OriginUIGroup digitData = new OriginUIGroup();
            m_alarm.gameObjectUI = CreateMenber(tex, "Alarm");
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // イメージの情報
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_alarm.gameObjectUI.SetDepth(GetDepth(DepthAttach.ALARM));
            m_alarm.gameObjectUI.gameObject.transform.SetParent(m_canvasObject.gameObject.transform);
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // ムービー
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_movie = new OriginUIGroup();
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // ムービー
        //*|***|***|***|***|***|***|***|***|***|***|***|
        {
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // イメージ作成
            //*|***|***|***|***|***|***|***|***|***|***|***|
            tex = new TexImageData();
            tex.Reset();
            tex.image = m_warehouseObject.GetTexture2DApp(AppImageNum.MOVIEFRAME);
            tex.rextParsent = MyCalculator.RectSizeReverse_Y(0, 1, 1);
            tex.size = new Vector2(1, 1);
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 桁作成
            //*|***|***|***|***|***|***|***|***|***|***|***|
            OriginUIGroup digitData = new OriginUIGroup();
            m_movie.gameObjectUI = CreateMenber(tex, "Movie");
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // イメージの情報
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_movie.gameObjectUI.SetDepth(GetDepth(DepthAttach.MOVIE));
            m_movie.gameObjectUI.gameObject.transform.SetParent(m_canvasObject.gameObject.transform);
        }
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // パワーアップ起動
    //*|***|***|***|***|***|***|***|***|***|***|***|
    void AwakePowerUp()
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // TexImageData
        //*|***|***|***|***|***|***|***|***|***|***|***|
        TexImageData texTime = null;
        TexImageData texIcon = null;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // パワーアップたち
        // 実体獲得
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_armPowerUp = new PowerUpIcon();
        m_armPowerUp.m_icon = new OriginUIGroup();
        m_armPowerUp.m_time = new OriginUIGroup();
        m_bodyPowerUp = new PowerUpIcon();
        m_bodyPowerUp.m_icon = new OriginUIGroup();
        m_bodyPowerUp.m_time = new OriginUIGroup();
        m_headPowerUp = new PowerUpIcon();
        m_headPowerUp.m_icon = new OriginUIGroup();
        m_headPowerUp.m_time = new OriginUIGroup();
        m_legPowerUp = new PowerUpIcon();
        m_legPowerUp.m_icon = new OriginUIGroup();
        m_legPowerUp.m_time = new OriginUIGroup();
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // パワーアップたち
        //*|***|***|***|***|***|***|***|***|***|***|***|
        {
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // イメージ作成
            //*|***|***|***|***|***|***|***|***|***|***|***|
            texTime = new TexImageData();
            texTime.Reset();
            texTime.image = m_warehouseObject.GetTexture2DApp(AppImageNum.POWERUP_TIME);
            texTime.rextParsent = MyCalculator.RectSizeReverse_Y(0, 1, 1);
            texTime.size = new Vector2(1, 1);

            texIcon = new TexImageData();
            texIcon.Reset();
            texIcon.image = m_warehouseObject.GetTexture2DApp(AppImageNum.POWERUP_ARM);
            texIcon.rextParsent = MyCalculator.RectSizeReverse_Y(0, 1, 1);
            texIcon.size = new Vector2(1, 1);
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 桁作成
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_armPowerUp.m_icon.gameObjectUI = CreateMenber(texIcon, "ArmIcon");
            m_armPowerUp.m_time.gameObjectUI = CreateMenber(texTime, "ArmTime");
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // イメージの情報
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_armPowerUp.m_icon.gameObjectUI.SetDepth(GetDepth(DepthAttach.PLAYER_STATE));
            m_armPowerUp.m_icon.gameObjectUI.gameObject.transform.SetParent(m_canvasObject.gameObject.transform);
            m_armPowerUp.m_time.gameObjectUI.SetDepth(GetDepth(DepthAttach.PLAYER_STATE_TIME));
            m_armPowerUp.m_time.gameObjectUI.gameObject.transform.SetParent(m_canvasObject.gameObject.transform);
        }
        m_armStrongTime = 0.0f;
        {
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // イメージ作成
            //*|***|***|***|***|***|***|***|***|***|***|***|
            texTime = new TexImageData();
            texTime.Reset();
            texTime.image = m_warehouseObject.GetTexture2DApp(AppImageNum.POWERUP_TIME);
            texTime.rextParsent = MyCalculator.RectSizeReverse_Y(0, 1, 1);
            texTime.size = new Vector2(1, 1);

            texIcon = new TexImageData();
            texIcon.Reset();
            texIcon.image = m_warehouseObject.GetTexture2DApp(AppImageNum.POWERUP_BODY);
            texIcon.rextParsent = MyCalculator.RectSizeReverse_Y(0, 1, 1);
            texIcon.size = new Vector2(1, 1);
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 桁作成
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_bodyPowerUp.m_icon.gameObjectUI = CreateMenber(texIcon, "ArmIcon");
            m_bodyPowerUp.m_time.gameObjectUI = CreateMenber(texTime, "ArmTime");
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // イメージの情報
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_bodyPowerUp.m_icon.gameObjectUI.SetDepth(GetDepth(DepthAttach.PLAYER_STATE));
            m_bodyPowerUp.m_icon.gameObjectUI.gameObject.transform.SetParent(m_canvasObject.gameObject.transform);
            m_bodyPowerUp.m_time.gameObjectUI.SetDepth(GetDepth(DepthAttach.PLAYER_STATE_TIME));
            m_bodyPowerUp.m_time.gameObjectUI.gameObject.transform.SetParent(m_canvasObject.gameObject.transform);
        }
        m_bodyStrongTime = 0.0f;
        {
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // イメージ作成
            //*|***|***|***|***|***|***|***|***|***|***|***|
            texTime = new TexImageData();
            texTime.Reset();
            texTime.image = m_warehouseObject.GetTexture2DApp(AppImageNum.POWERUP_TIME);
            texTime.rextParsent = MyCalculator.RectSizeReverse_Y(0, 1, 1);
            texTime.size = new Vector2(1, 1);

            texIcon = new TexImageData();
            texIcon.Reset();
            texIcon.image = m_warehouseObject.GetTexture2DApp(AppImageNum.POWERUP_HEAD);
            texIcon.rextParsent = MyCalculator.RectSizeReverse_Y(0, 1, 1);
            texIcon.size = new Vector2(1, 1);
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 桁作成
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_headPowerUp.m_icon.gameObjectUI = CreateMenber(texIcon, "ArmIcon");
            m_headPowerUp.m_time.gameObjectUI = CreateMenber(texTime, "ArmTime");
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // イメージの情報
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_headPowerUp.m_icon.gameObjectUI.SetDepth(GetDepth(DepthAttach.PLAYER_STATE));
            m_headPowerUp.m_icon.gameObjectUI.gameObject.transform.SetParent(m_canvasObject.gameObject.transform);
            m_headPowerUp.m_time.gameObjectUI.SetDepth(GetDepth(DepthAttach.PLAYER_STATE_TIME));
            m_headPowerUp.m_time.gameObjectUI.gameObject.transform.SetParent(m_canvasObject.gameObject.transform);
        }
        m_headStrongTime = 0.0f;
        {
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // イメージ作成
            //*|***|***|***|***|***|***|***|***|***|***|***|
            texTime = new TexImageData();
            texTime.Reset();
            texTime.image = m_warehouseObject.GetTexture2DApp(AppImageNum.POWERUP_TIME);
            texTime.rextParsent = MyCalculator.RectSizeReverse_Y(0, 1, 1);
            texTime.size = new Vector2(1, 1);

            texIcon = new TexImageData();
            texIcon.Reset();
            texIcon.image = m_warehouseObject.GetTexture2DApp(AppImageNum.POWERUP_LEG);
            texIcon.rextParsent = MyCalculator.RectSizeReverse_Y(0, 1, 1);
            texIcon.size = new Vector2(1, 1);
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 桁作成
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_legPowerUp.m_icon.gameObjectUI = CreateMenber(texIcon, "ArmIcon");
            m_legPowerUp.m_time.gameObjectUI = CreateMenber(texTime, "ArmTime");
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // イメージの情報
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_legPowerUp.m_icon.gameObjectUI.SetDepth(GetDepth(DepthAttach.PLAYER_STATE));
            m_legPowerUp.m_icon.gameObjectUI.gameObject.transform.SetParent(m_canvasObject.gameObject.transform);
            m_legPowerUp.m_time.gameObjectUI.SetDepth(GetDepth(DepthAttach.PLAYER_STATE_TIME));
            m_legPowerUp.m_time.gameObjectUI.gameObject.transform.SetParent(m_canvasObject.gameObject.transform);
        }
        m_legStrongTime = 0.0f;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 起動したとき
    //*|***|***|***|***|***|***|***|***|***|***|***|
    protected override void AwakeUI()
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 酸素ゲージ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        AwakeAirDataUI();
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 星ゲージ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        AwakeStarDataUI();
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 所持数星量
        //*|***|***|***|***|***|***|***|***|***|***|***|
        AwakeHaveStarDataUI();
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 目標数星量
        //*|***|***|***|***|***|***|***|***|***|***|***|
        AwakeNeedStarDataUI();
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 所持パーツ起動
        //*|***|***|***|***|***|***|***|***|***|***|***|
        AwakePartsCollectionUI();
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 警報起動
        //*|***|***|***|***|***|***|***|***|***|***|***|
        AwakeAlarmUI();
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // パワーアップ起動
        //*|***|***|***|***|***|***|***|***|***|***|***|
        AwakePowerUpUI();

    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 酸素ゲージ起動
    //*|***|***|***|***|***|***|***|***|***|***|***|
    void AwakeAirDataUI()
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 酸素ゲージ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        {
            m_airGaugeMain.imageUIData = new ImageUIData();
            m_airGaugeMain.imageUIData.Init();

            m_airGaugeFrame.imageUIData = new ImageUIData();
            m_airGaugeFrame.imageUIData.Init();

            m_airGaugeShadow.imageUIData = new ImageUIData();
            m_airGaugeShadow.imageUIData.Init();
        }
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 星ゲージ
    //*|***|***|***|***|***|***|***|***|***|***|***|
    void AwakeStarDataUI()
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 星ゲージパーセント
        //*|***|***|***|***|***|***|***|***|***|***|***|
        for (int index = 0; index < m_starRateDigit; index++)
        {
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 桁挿入
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_starRate[index].imageUIData = new ImageUIData();
            m_starRate[index].imageUIData.Init();
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 星ゲージパーセント
        //*|***|***|***|***|***|***|***|***|***|***|***|
        {
            m_starRateParsent.imageUIData = new ImageUIData();
            m_starRateParsent.imageUIData.Init();
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 星ゲージ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        {
            m_starGaugeMain.imageUIData = new ImageUIData();
            m_starGaugeMain.imageUIData.Init();

            m_starGaugeFrame.imageUIData = new ImageUIData();
            m_starGaugeFrame.imageUIData.Init();

            m_starGaugeShadow.imageUIData = new ImageUIData();
            m_starGaugeShadow.imageUIData.Init();
        }
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 所持数星量
    //*|***|***|***|***|***|***|***|***|***|***|***|
    void AwakeHaveStarDataUI()
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 所持数星量数字
        //*|***|***|***|***|***|***|***|***|***|***|***|
        for (int index = 0; index < m_haveStarDigit; index++)
        {
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 桁挿入
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_haveStar[index].imageUIData = new ImageUIData();
            m_haveStar[index].imageUIData.Init();
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 所持数星量その他
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_haveStarKO.imageUIData = new ImageUIData();
        m_haveStarKO.imageUIData.Init();
        m_haveStarStar.imageUIData = new ImageUIData();
        m_haveStarStar.imageUIData.Init();
        m_haveStarCross.imageUIData = new ImageUIData();
        m_haveStarCross.imageUIData.Init();
        m_haveStarMozi.imageUIData = new ImageUIData();
        m_haveStarMozi.imageUIData.Init();
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 目標数星量
    //*|***|***|***|***|***|***|***|***|***|***|***|
    void AwakeNeedStarDataUI()
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 目標数星量数字
        //*|***|***|***|***|***|***|***|***|***|***|***|
        for (int index = 0; index < m_needStarDigit; index++)
        {
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 桁挿入
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_needStar[index].imageUIData = new ImageUIData();
            m_needStar[index].imageUIData.Init();
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 目標数星量その他
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_needStarKO.imageUIData = new ImageUIData();
        m_needStarKO.imageUIData.Init();
        m_needStarStar.imageUIData = new ImageUIData();
        m_needStarStar.imageUIData.Init();
        m_needStarCross.imageUIData = new ImageUIData();
        m_needStarCross.imageUIData.Init();
        m_needStarMozi.imageUIData = new ImageUIData();
        m_needStarMozi.imageUIData.Init();
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 所持パーツ起動
    //*|***|***|***|***|***|***|***|***|***|***|***|
    void AwakePartsCollectionUI()
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 所持パーツ数字
        //*|***|***|***|***|***|***|***|***|***|***|***|
        for (int index = 0; index < m_partsCollectionDigit; index++)
        {
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 桁挿入
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_partsCollection[index].imageUIData = new ImageUIData();
            m_partsCollection[index].imageUIData.Init();
        }
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 警報起動
    //*|***|***|***|***|***|***|***|***|***|***|***|
    void AwakeAlarmUI()
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 警報
        //*|***|***|***|***|***|***|***|***|***|***|***|
        {
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 桁挿入
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_alarm.imageUIData = new ImageUIData();
            m_alarm.imageUIData.Init();
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // ムービー
        //*|***|***|***|***|***|***|***|***|***|***|***|
        {
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 桁挿入
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_movie.imageUIData = new ImageUIData();
            m_movie.imageUIData.Init();
        }
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // パワーアップ起動
    //*|***|***|***|***|***|***|***|***|***|***|***|
    void AwakePowerUpUI()
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // パワーアップ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        {
            m_armPowerUp.m_icon.imageUIData = new ImageUIData();
            m_armPowerUp.m_icon.imageUIData.Init();

            m_armPowerUp.m_time.imageUIData = new ImageUIData();
            m_armPowerUp.m_time.imageUIData.Init();
        }
        {
            m_bodyPowerUp.m_icon.imageUIData = new ImageUIData();
            m_bodyPowerUp.m_icon.imageUIData.Init();

            m_bodyPowerUp.m_time.imageUIData = new ImageUIData();
            m_bodyPowerUp.m_time.imageUIData.Init();
        }
        {
            m_headPowerUp.m_icon.imageUIData = new ImageUIData();
            m_headPowerUp.m_icon.imageUIData.Init();

            m_headPowerUp.m_time.imageUIData = new ImageUIData();
            m_headPowerUp.m_time.imageUIData.Init();
        }
        {
            m_legPowerUp.m_icon.imageUIData = new ImageUIData();
            m_legPowerUp.m_icon.imageUIData.Init();

            m_legPowerUp.m_time.imageUIData = new ImageUIData();
            m_legPowerUp.m_time.imageUIData.Init();
        }
    }

    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 更新したとき
    //*|***|***|***|***|***|***|***|***|***|***|***|
    protected override void UpdateWork()
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 終了
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_gameEnd = ChangeData.Among(m_gameEnd, 0.0f, 1.0f);
        m_gameMovie = ChangeData.Among(m_gameMovie, 0.0f, 1.0f);    
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 酸素ゲージ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        UpdateAirData();
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 星ゲージ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        UpdateStarData();
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 所持数星量更新
        //*|***|***|***|***|***|***|***|***|***|***|***|
        UpdateHaveStarData();
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 目標数星量更新
        //*|***|***|***|***|***|***|***|***|***|***|***|
        UpdateNeedStarData();
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 所持パーツ更新
        //*|***|***|***|***|***|***|***|***|***|***|***|
        UpdatePartsCollection();
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 警報更新
        //*|***|***|***|***|***|***|***|***|***|***|***|
        UpdateAlarm();
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // パワーアップ更新
        //*|***|***|***|***|***|***|***|***|***|***|***|
        UpdatePowerUp();
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 酸素ゲージ更新
    //*|***|***|***|***|***|***|***|***|***|***|***|
    void UpdateAirData()
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 画像データ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        //Rect imageRect = MyCalculator.RectSizeReverse_Y(0, 1, 1);
        float alphaParsent = 1.0f * m_gameEnd;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // データ集計
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_airParsent = ChangeData.Among(m_airParsent, 0.0f, 1.0f);
        float parsentAir = m_airParsent;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 調整用データ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        float pointX = 0;
        float pointY = 0;
        float sizeX = 0;
        float sizeY = 0;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // ゲージの大きさ測定
        //*|***|***|***|***|***|***|***|***|***|***|***|
        float dif = 0;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // ゲージの100%大きさ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        float allSizeX = 0;
        float allSizeY = 0;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // ゲージの今の大きさ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        float trueSizeX = 0;
        float trueSizeY = 0;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // ゲージの下
        //*|***|***|***|***|***|***|***|***|***|***|***|
        float row = 0;
        float column = 0;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // ゲージの中心
        //*|***|***|***|***|***|***|***|***|***|***|***|
        float centerX = 0;
        float centerY = 0;
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
        // 酸素ゲージフレーム
        //*|***|***|***|***|***|***|***|***|***|***|***|
        {
            persent_AssistS.x = MyCalculator.Division(0.1f, 1);
            persent_AssistS.y = 0.2f;
            persent_AssistP.x = 0.75f;
            persent_AssistP.y = MyCalculator.Division(persent_AssistS.y, 2.0f);
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
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 透明度
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_airGaugeFrame.gameObjectUI.SetAlpha(alphaParsent);
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 桁挿入
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_airGaugeFrame.imageUIData.imagePos = new Vector2(pos.x, pos.y);
            m_airGaugeFrame.imageUIData.imageScale = new Vector2(scale.x, scale.y);
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 場所を継承
        //*|***|***|***|***|***|***|***|***|***|***|***|
        pointX = m_airGaugeFrame.imageUIData.imagePos.x;
        pointY = m_airGaugeFrame.imageUIData.imagePos.y;
        sizeX = m_airGaugeFrame.imageUIData.imageScale.x;
        sizeY = m_airGaugeFrame.imageUIData.imageScale.y;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // ゲージの大きさ測定
        //*|***|***|***|***|***|***|***|***|***|***|***|
        dif = AIR_LOW - AIR_HIGH;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // ゲージの100%大きさ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        allSizeX = AIR_ROW * sizeX;
        allSizeY = dif * sizeY;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // ゲージの今の大きさ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        trueSizeX = allSizeX;
        trueSizeY = allSizeY * parsentAir;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // ゲージの下
        //*|***|***|***|***|***|***|***|***|***|***|***|
        column = MyCalculator.Division(sizeX, 2.0f) - MyCalculator.Division(allSizeX, 2.0f);
        row = MyCalculator.Division(sizeY, 2.0f) - (AIR_LOW * sizeY);
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // ゲージの中心
        //*|***|***|***|***|***|***|***|***|***|***|***|
        centerX = pointX - column;
        centerY = pointY + row;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 適応
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_airGaugeSize.x = trueSizeX;
        m_airGaugeSize.y = trueSizeY;
        m_airGaugeMaxSize.x = allSizeX;
        m_airGaugeMaxSize.y = allSizeY;
        m_airGaugePos.x = centerX;
        m_airGaugePos.y = centerY;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 透明度
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_airGaugeMain.gameObjectUI.SetAlpha(alphaParsent);
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 酸素ゲージ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_airGaugeMain.imageUIData.imagePos = m_airGaugePos;
        m_airGaugeMain.imageUIData.imageScale = m_airGaugeSize;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 透明度
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_airGaugeShadow.gameObjectUI.SetAlpha(alphaParsent);
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 酸素ゲージ影
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_airGaugeShadow.imageUIData.imagePos = m_airGaugePos;
        m_airGaugeShadow.imageUIData.imageScale = m_airGaugeMaxSize;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 星ゲージ更新
    //*|***|***|***|***|***|***|***|***|***|***|***|
    void UpdateStarData()
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 画像データ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        Rect imageRect = MyCalculator.RectSizeReverse_Y(0, 1, 1);
        float alphaParsent = 1.0f * m_gameEnd;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // データ集計
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_starParsent = ChangeData.Among(m_starParsent, 0.0f, 1.0f);
        float parsentStar = m_starParsent;
        imageRect = MyCalculator.RectSizeReverse_Y(0, 1, 1);
        float parsentStar100 = m_starParsent * 100.0f;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 現れる数値
        //*|***|***|***|***|***|***|***|***|***|***|***|
        int jugNumber100 = (int)parsentStar100;
        int digitNum = 0;

        for (int digit = 0; digit < m_starRateDigit; digit++)
        {
            digitNum = MyCalculator.Get10Digit(jugNumber100, digit);
            imageRect = MyCalculator.RectSizeReverse_Y(digitNum, 4, 4);
            m_starRate[digit].gameObjectUI.SetRect(imageRect);
        }
        
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // Yの限界地点
        //*|***|***|***|***|***|***|***|***|***|***|***|
        float parsentPoint = MyCalculator.Leap(STAR_HIGH, STAR_LOW, parsentStar);
        imageRect = MyCalculator.RectSizeReverse_Y(0, 1, 1);
        imageRect.yMax = parsentPoint;
        m_starGaugeMain.gameObjectUI.SetRect(imageRect);
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 調整用データ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        float pointX = 0;
        float pointY = 0;
        float sizeX = 0;
        float sizeY = 0;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // ゲージの大きさ測定
        //*|***|***|***|***|***|***|***|***|***|***|***|
        float dif = 0;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // ゲージの100%大きさ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        float allSizeX = 0;
        float allSizeY = 0;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // ゲージの今の大きさ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        float trueSizeX = 0;
        float trueSizeY = 0;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // ゲージの下
        //*|***|***|***|***|***|***|***|***|***|***|***|
        float row = 0;
        float column = 0;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // ゲージの中心
        //*|***|***|***|***|***|***|***|***|***|***|***|
        float centerX = 0;
        float centerY = 0;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 調整用データ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        Vector2 posSave = new Vector2(0.0f, 0.0f);
        Vector2 scaleSave = new Vector2(0.0f, 0.0f);
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
        int trueStarRateDigit = m_starRateDigit + 1;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 星ゲージフレーム
        //*|***|***|***|***|***|***|***|***|***|***|***|
        {
            persent_AssistS.x = MyCalculator.Division(0.2f, 1);
            persent_AssistS.y = 0.2f;
            persent_AssistP.x = 0.9f;
            persent_AssistP.y = MyCalculator.Division(persent_AssistS.y, 2.0f);
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
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 透明度
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_starGaugeFrame.gameObjectUI.SetAlpha(alphaParsent);
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 桁挿入
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_starGaugeFrame.imageUIData.imagePos = new Vector2(pos.x, pos.y);
            m_starGaugeFrame.imageUIData.imageScale = new Vector2(scale.x, scale.y);
        }


        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 場所を継承
        //*|***|***|***|***|***|***|***|***|***|***|***|
        pointX = m_starGaugeFrame.imageUIData.imagePos.x;
        pointY = m_starGaugeFrame.imageUIData.imagePos.y;
        sizeX = m_starGaugeFrame.imageUIData.imageScale.x;
        sizeY = m_starGaugeFrame.imageUIData.imageScale.y;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // ゲージの大きさ測定
        //*|***|***|***|***|***|***|***|***|***|***|***|
        dif = STAR_LOW - STAR_HIGH;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // ゲージの100%大きさ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        allSizeX = STAR_ROW * sizeX;
        allSizeY = sizeY;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // ゲージの今の大きさ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        trueSizeX = allSizeX;
        trueSizeY = allSizeY * (parsentPoint);
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // ゲージの下
        //*|***|***|***|***|***|***|***|***|***|***|***|
        column = MyCalculator.Division(sizeX, 2.0f) - MyCalculator.Division(allSizeX, 2.0f);
        row = MyCalculator.Division(sizeY, 2.0f);
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // ゲージの中心
        //*|***|***|***|***|***|***|***|***|***|***|***|
        centerX = pointX + column;
        centerY = pointY - row;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 適応
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_starGaugeSize.x = trueSizeX;
        m_starGaugeSize.y = trueSizeY;
        m_starGaugeMaxSize.x = allSizeX;
        m_starGaugeMaxSize.y = allSizeY;
        m_starGaugePos.x = centerX;
        m_starGaugePos.y = centerY;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 所持数星量その他
        //*|***|***|***|***|***|***|***|***|***|***|***|
        {
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 計算用初期化、データ確保
            //*|***|***|***|***|***|***|***|***|***|***|***|
            pos = m_starGaugeFrame.imageUIData.imagePos;
            scale = m_starGaugeFrame.imageUIData.imageScale;
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 計算用初期化、データ確保
            //*|***|***|***|***|***|***|***|***|***|***|***|
            persent_AssistP = pos;
            persent_AssistS = scale;
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 調整
            //*|***|***|***|***|***|***|***|***|***|***|***|
            persent_AssistS.x = MyCalculator.Division(persent_AssistS.x, trueStarRateDigit);
            scale.x = persent_AssistS.x;
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 以下も同じ場所に現れる
        //*|***|***|***|***|***|***|***|***|***|***|***|
        posSave = pos;
        scaleSave = scale;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 星ゲージパーセント
        //*|***|***|***|***|***|***|***|***|***|***|***|
        for (int index = 0; index < m_starRate.Count; index++)
        {
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 計算用初期化、データ確保
            //*|***|***|***|***|***|***|***|***|***|***|***|
            int indexReverse = MyCalculator.InversionOfIndex(index + 1, trueStarRateDigit);
            float indexPoint = MyCalculator.IndexCenterPos(indexReverse, trueStarRateDigit);
            pos.x = posSave.x + (persent_AssistS.x * indexPoint);
            pos.y = posSave.y;
            scale = scaleSave;
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 透明度
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_starRate[index].gameObjectUI.SetAlpha(alphaParsent);
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 桁挿入
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_starRate[index].imageUIData.imagePos = new Vector2(pos.x, pos.y);
            m_starRate[index].imageUIData.imageScale = new Vector2(scale.x, scale.y);
        }
        {
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 計算用初期化、データ確保
            //*|***|***|***|***|***|***|***|***|***|***|***|
            int indexReverse = MyCalculator.InversionOfIndex(0, trueStarRateDigit);
            float indexPoint = MyCalculator.IndexCenterPos(indexReverse, trueStarRateDigit);
            pos.x = posSave.x + (persent_AssistS.x * indexPoint);
            pos.y = posSave.y;
            scale = scaleSave;
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 透明度
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_starRateParsent.gameObjectUI.SetAlpha(alphaParsent);
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 桁挿入
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_starRateParsent.imageUIData.imagePos = new Vector2(pos.x, pos.y);
            m_starRateParsent.imageUIData.imageScale = new Vector2(scale.x, scale.y);
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 透明度
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_starGaugeMain.gameObjectUI.SetAlpha(alphaParsent);
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 星ゲージ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_starGaugeMain.imageUIData.imagePos = m_starGaugePos;
        m_starGaugeMain.imageUIData.imageScale = m_starGaugeSize;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 透明度
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_starGaugeShadow.gameObjectUI.SetAlpha(alphaParsent);
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 星ゲージ影
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_starGaugeShadow.imageUIData.imagePos = m_starGaugePos;
        m_starGaugeShadow.imageUIData.imageScale = m_starGaugeMaxSize;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 所持数星量更新
    //*|***|***|***|***|***|***|***|***|***|***|***|
    void UpdateHaveStarData()
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 画像データ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        Rect imageRect = MyCalculator.RectSizeReverse_Y(0, 1, 1);
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // データ集計
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_haveStarNumber = ChangeData.Among(m_haveStarNumber, 0, 99999);
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 現れる数値
        //*|***|***|***|***|***|***|***|***|***|***|***|
        int digitNum = 0;
        for (int digit = 0; digit < m_haveStarDigit; digit++)
        {
            digitNum = MyCalculator.Get10Digit(m_haveStarNumber, digit);
            imageRect = MyCalculator.RectSizeReverse_Y(digitNum, 4, 4);
            m_haveStar[digit].gameObjectUI.SetRect(imageRect);
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 調整用データ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        Vector2 posSave = new Vector2(0.0f, 0.0f);
        Vector2 scaleSave = new Vector2(0.0f, 0.0f);
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
        int trueHaveStarDigit = m_haveStarDigit + 3;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 所持数星量その他
        //*|***|***|***|***|***|***|***|***|***|***|***|
        {
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 計算用初期化、データ確保
            //*|***|***|***|***|***|***|***|***|***|***|***|
            pos = new Vector2(0.1f, 0.05f + MyCalculator.Division(0.05f, 2));
            scale = new Vector2(0.2f, 0.05f);
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 調整済みデータ
            //*|***|***|***|***|***|***|***|***|***|***|***|
            posScleen = MouseToScreenPos(pos);
            posReverseY = MyCalculator.EachTimes(posScleen, new Vector2(1.0f, -1.0f));
            pos = posReverseY;
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 計算用初期化、データ確保
            //*|***|***|***|***|***|***|***|***|***|***|***|
            persent_AssistP = pos;
            persent_AssistS = scale;
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 調整
            //*|***|***|***|***|***|***|***|***|***|***|***|
            persent_AssistS.x = MyCalculator.Division(persent_AssistS.x, trueHaveStarDigit);
            scale.x = persent_AssistS.x;
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 以下も同じ場所に現れる
        //*|***|***|***|***|***|***|***|***|***|***|***|
        posSave = pos;
        scaleSave = scale;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 所持数星量その他
        //*|***|***|***|***|***|***|***|***|***|***|***|
        {
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 計算用初期化、データ確保
            //*|***|***|***|***|***|***|***|***|***|***|***|
            int indexReverse = MyCalculator.InversionOfIndex(0, trueHaveStarDigit);
            float indexPoint = MyCalculator.IndexCenterPos(indexReverse, trueHaveStarDigit);
            pos.x = posSave.x + (persent_AssistS.x * indexPoint);
            pos.y = posSave.y;
            scale = scaleSave;
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 透明度
            //*|***|***|***|***|***|***|***|***|***|***|***|
            float alphaParsent = 1.0f * m_gameEnd;
            m_haveStarKO.gameObjectUI.SetAlpha(alphaParsent);
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 桁挿入
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_haveStarKO.imageUIData.imagePos = new Vector2(pos.x, pos.y);
            m_haveStarKO.imageUIData.imageScale = new Vector2(scale.x, scale.y);
        }
        {
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 計算用初期化、データ確保
            //*|***|***|***|***|***|***|***|***|***|***|***|
            int indexReverse = MyCalculator.InversionOfIndex(7, trueHaveStarDigit);
            float indexPoint = MyCalculator.IndexCenterPos(indexReverse, trueHaveStarDigit);
            pos.x = posSave.x + (persent_AssistS.x * indexPoint);
            pos.y = posSave.y;
            scale = scaleSave;
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 透明度
            //*|***|***|***|***|***|***|***|***|***|***|***|
            float alphaParsent = 1.0f * m_gameEnd;
            m_haveStarStar.gameObjectUI.SetAlpha(alphaParsent);
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 桁挿入
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_haveStarStar.imageUIData.imagePos = new Vector2(pos.x, pos.y);
            m_haveStarStar.imageUIData.imageScale = new Vector2(scale.x, scale.y);
        }
        {
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 計算用初期化、データ確保
            //*|***|***|***|***|***|***|***|***|***|***|***|
            int indexReverse = MyCalculator.InversionOfIndex(6, trueHaveStarDigit);
            float indexPoint = MyCalculator.IndexCenterPos(indexReverse, trueHaveStarDigit);
            pos.x = posSave.x + (persent_AssistS.x * indexPoint);
            pos.y = posSave.y;
            scale = scaleSave;
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 透明度
            //*|***|***|***|***|***|***|***|***|***|***|***|
            float alphaParsent = 1.0f * m_gameEnd;
            m_haveStarCross.gameObjectUI.SetAlpha(alphaParsent);
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 桁挿入
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_haveStarCross.imageUIData.imagePos = new Vector2(pos.x, pos.y);
            m_haveStarCross.imageUIData.imageScale = new Vector2(scale.x, scale.y);
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 所持数星量数字
        //*|***|***|***|***|***|***|***|***|***|***|***|
        for (int index = 0; index < m_haveStar.Count; index++)
        {
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 計算用初期化、データ確保
            //*|***|***|***|***|***|***|***|***|***|***|***|
            int indexReverse = MyCalculator.InversionOfIndex(index + 1, trueHaveStarDigit);
            float indexPoint = MyCalculator.IndexCenterPos(indexReverse, trueHaveStarDigit);
            pos.x = posSave.x + (persent_AssistS.x * indexPoint);
            pos.y = posSave.y;
            scale = scaleSave;
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 透明度
            //*|***|***|***|***|***|***|***|***|***|***|***|
            float alphaParsent = 1.0f * m_gameEnd;
            m_haveStar[index].gameObjectUI.SetAlpha(alphaParsent);
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 桁挿入
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_haveStar[index].imageUIData.imagePos = new Vector2(pos.x, pos.y);
            m_haveStar[index].imageUIData.imageScale = new Vector2(scale.x, scale.y);
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 所持数星量文字
        //*|***|***|***|***|***|***|***|***|***|***|***|
        {
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 計算用初期化、データ確保
            //*|***|***|***|***|***|***|***|***|***|***|***|
            pos = new Vector2(0.1f, 0.00f + MyCalculator.Division(0.05f, 2));
            scale = new Vector2(0.2f, 0.05f);
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 調整済みデータ
            //*|***|***|***|***|***|***|***|***|***|***|***|
            posScleen = MouseToScreenPos(pos);
            posReverseY = MyCalculator.EachTimes(posScleen, new Vector2(1.0f, -1.0f));
            pos = posReverseY;
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 計算用初期化、データ確保
            //*|***|***|***|***|***|***|***|***|***|***|***|
            persent_AssistP = pos;
            persent_AssistS = scale;
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 調整
            //*|***|***|***|***|***|***|***|***|***|***|***|

        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 以下も同じ場所に現れる
        //*|***|***|***|***|***|***|***|***|***|***|***|
        posSave = pos;
        scaleSave = scale;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 所持数星量文字
        //*|***|***|***|***|***|***|***|***|***|***|***|
        {
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 計算用初期化、データ確保
            //*|***|***|***|***|***|***|***|***|***|***|***|
            pos.x = posSave.x;
            pos.y = posSave.y;
            scale = scaleSave;
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 透明度
            //*|***|***|***|***|***|***|***|***|***|***|***|
            float alphaParsent = 1.0f * m_gameEnd;
            m_haveStarMozi.gameObjectUI.SetAlpha(alphaParsent);
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 桁挿入
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_haveStarMozi.imageUIData.imagePos = new Vector2(pos.x, pos.y);
            m_haveStarMozi.imageUIData.imageScale = new Vector2(scale.x, scale.y);
        }
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 目標数星量更新
    //*|***|***|***|***|***|***|***|***|***|***|***|
    void UpdateNeedStarData()
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 画像データ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        Rect imageRect = MyCalculator.RectSizeReverse_Y(0, 1, 1);
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // データ集計
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_needStarNumber = ChangeData.Among(m_needStarNumber, 0, 99999);
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 現れる数値
        //*|***|***|***|***|***|***|***|***|***|***|***|
        int digitNum = 0;
        for (int digit = 0; digit < m_needStarDigit; digit++)
        {
            digitNum = MyCalculator.Get10Digit(m_needStarNumber, digit);
            imageRect = MyCalculator.RectSizeReverse_Y(digitNum, 4, 4);
            m_needStar[digit].gameObjectUI.SetRect(imageRect);
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 調整用データ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        Vector2 posSave = new Vector2(0.0f, 0.0f);
        Vector2 scaleSave = new Vector2(0.0f, 0.0f);
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
        int trueNeedStarDigit = m_needStarDigit + 3;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 所持数星量その他
        //*|***|***|***|***|***|***|***|***|***|***|***|
        {
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 計算用初期化、データ確保
            //*|***|***|***|***|***|***|***|***|***|***|***|
            pos = new Vector2(0.1f, 0.15f + MyCalculator.Division(0.05f, 2));
            scale = new Vector2(0.2f, 0.05f);
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 調整済みデータ
            //*|***|***|***|***|***|***|***|***|***|***|***|
            posScleen = MouseToScreenPos(pos);
            posReverseY = MyCalculator.EachTimes(posScleen, new Vector2(1.0f, -1.0f));
            pos = posReverseY;
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 計算用初期化、データ確保
            //*|***|***|***|***|***|***|***|***|***|***|***|
            persent_AssistP = pos;
            persent_AssistS = scale;
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 調整
            //*|***|***|***|***|***|***|***|***|***|***|***|
            persent_AssistS.x = MyCalculator.Division(persent_AssistS.x, trueNeedStarDigit);
            scale.x = persent_AssistS.x;
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 以下も同じ場所に現れる
        //*|***|***|***|***|***|***|***|***|***|***|***|
        posSave = pos;
        scaleSave = scale;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 目標数星量その他
        //*|***|***|***|***|***|***|***|***|***|***|***|
        {
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 計算用初期化、データ確保
            //*|***|***|***|***|***|***|***|***|***|***|***|
            int indexReverse = MyCalculator.InversionOfIndex(0, trueNeedStarDigit);
            float indexPoint = MyCalculator.IndexCenterPos(indexReverse, trueNeedStarDigit);
            pos.x = posSave.x + (persent_AssistS.x * indexPoint);
            pos.y = posSave.y;
            scale = scaleSave;
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 透明度
            //*|***|***|***|***|***|***|***|***|***|***|***|
            float alphaParsent = 1.0f * m_gameEnd;
            m_needStarKO.gameObjectUI.SetAlpha(alphaParsent);
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 桁挿入
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_needStarKO.imageUIData.imagePos = new Vector2(pos.x, pos.y);
            m_needStarKO.imageUIData.imageScale = new Vector2(scale.x, scale.y);
        }
        {
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 計算用初期化、データ確保
            //*|***|***|***|***|***|***|***|***|***|***|***|
            int indexReverse = MyCalculator.InversionOfIndex(7, trueNeedStarDigit);
            float indexPoint = MyCalculator.IndexCenterPos(indexReverse, trueNeedStarDigit);
            pos.x = posSave.x + (persent_AssistS.x * indexPoint);
            pos.y = posSave.y;
            scale = scaleSave;
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 透明度
            //*|***|***|***|***|***|***|***|***|***|***|***|
            float alphaParsent = 1.0f * m_gameEnd;
            m_needStarStar.gameObjectUI.SetAlpha(alphaParsent);
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 桁挿入
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_needStarStar.imageUIData.imagePos = new Vector2(pos.x, pos.y);
            m_needStarStar.imageUIData.imageScale = new Vector2(scale.x, scale.y);
        }
        {
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 計算用初期化、データ確保
            //*|***|***|***|***|***|***|***|***|***|***|***|
            int indexReverse = MyCalculator.InversionOfIndex(6, trueNeedStarDigit);
            float indexPoint = MyCalculator.IndexCenterPos(indexReverse, trueNeedStarDigit);
            pos.x = posSave.x + (persent_AssistS.x * indexPoint);
            pos.y = posSave.y;
            scale = scaleSave;
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 透明度
            //*|***|***|***|***|***|***|***|***|***|***|***|
            float alphaParsent = 1.0f * m_gameEnd;
            m_needStarCross.gameObjectUI.SetAlpha(alphaParsent);
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 桁挿入
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_needStarCross.imageUIData.imagePos = new Vector2(pos.x, pos.y);
            m_needStarCross.imageUIData.imageScale = new Vector2(scale.x, scale.y);
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 目標数星量数字
        //*|***|***|***|***|***|***|***|***|***|***|***|
        for (int index = 0; index < m_needStarDigit; index++)
        {
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 計算用初期化、データ確保
            //*|***|***|***|***|***|***|***|***|***|***|***|
            int indexReverse = MyCalculator.InversionOfIndex(index + 1, trueNeedStarDigit);
            float indexPoint = MyCalculator.IndexCenterPos(indexReverse, trueNeedStarDigit);
            pos.x = persent_AssistP.x + (persent_AssistS.x * indexPoint);
            pos.y = persent_AssistP.y;
            scale = persent_AssistS;
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 透明度
            //*|***|***|***|***|***|***|***|***|***|***|***|
            float alphaParsent = 1.0f * m_gameEnd;
            m_needStar[index].gameObjectUI.SetAlpha(alphaParsent);
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 桁挿入
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_needStar[index].imageUIData.imagePos = new Vector2(pos.x, pos.y);
            m_needStar[index].imageUIData.imageScale = new Vector2(scale.x, scale.y);
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 目標数星量文字
        //*|***|***|***|***|***|***|***|***|***|***|***|
        {
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 計算用初期化、データ確保
            //*|***|***|***|***|***|***|***|***|***|***|***|
            pos = new Vector2(0.1f, 0.10f + MyCalculator.Division(0.05f, 2));
            scale = new Vector2(0.2f, 0.05f);
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 調整済みデータ
            //*|***|***|***|***|***|***|***|***|***|***|***|
            posScleen = MouseToScreenPos(pos);
            posReverseY = MyCalculator.EachTimes(posScleen, new Vector2(1.0f, -1.0f));
            pos = posReverseY;
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 計算用初期化、データ確保
            //*|***|***|***|***|***|***|***|***|***|***|***|
            persent_AssistP = pos;
            persent_AssistS = scale;
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 調整
            //*|***|***|***|***|***|***|***|***|***|***|***|

        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 以下も同じ場所に現れる
        //*|***|***|***|***|***|***|***|***|***|***|***|
        posSave = pos;
        scaleSave = scale;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 目標数星量文字
        //*|***|***|***|***|***|***|***|***|***|***|***|
        {
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 計算用初期化、データ確保
            //*|***|***|***|***|***|***|***|***|***|***|***|
            pos.x = posSave.x;
            pos.y = posSave.y;
            scale = scaleSave;
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 透明度
            //*|***|***|***|***|***|***|***|***|***|***|***|
            float alphaParsent = 1.0f * m_gameEnd;
            m_needStarMozi.gameObjectUI.SetAlpha(alphaParsent);
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 桁挿入
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_needStarMozi.imageUIData.imagePos = new Vector2(pos.x, pos.y);
            m_needStarMozi.imageUIData.imageScale = new Vector2(scale.x, scale.y);
        }
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 所持パーツ更新
    //*|***|***|***|***|***|***|***|***|***|***|***|
    void UpdatePartsCollection()
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 画像データ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        Rect imageRect = MyCalculator.RectSizeReverse_Y(0, 1, 1);
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // データ集計
        //*|***|***|***|***|***|***|***|***|***|***|***|
        List<bool> dataBool = new List<bool>();
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 現れる数値
        //*|***|***|***|***|***|***|***|***|***|***|***|
        bool digitNum = false;
        for (int digit = 0; digit < m_partsCollectionDigit; digit++)
        {
            digitNum = MyCalculator.DigitBoolean(m_partsCollectionNumber, (uint)digit);
            dataBool.Add(digitNum);
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 調整用データ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        Vector2 posSave = new Vector2(0.0f, 0.0f);
        Vector2 scaleSave = new Vector2(0.0f, 0.0f);
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
        int truePartsCollectionDigit = m_partsCollectionDigit + 0;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 所持数星量その他
        //*|***|***|***|***|***|***|***|***|***|***|***|
        {
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 計算用初期化、データ確保
            //*|***|***|***|***|***|***|***|***|***|***|***|

            scale.x = MyCalculator.Division(0.2f, 1);
            scale.y = 0.1f;
            pos.x = 1.0f - MyCalculator.Division(scale.x, 2);
            pos.y = 0.2f + MyCalculator.Division(scale.y, 2.0f);
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 調整済みデータ
            //*|***|***|***|***|***|***|***|***|***|***|***|
            posScleen = MouseToScreenPos(pos);
            posReverseY = MyCalculator.EachTimes(posScleen, new Vector2(1.0f, -1.0f));
            pos = posReverseY;
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 計算用初期化、データ確保
            //*|***|***|***|***|***|***|***|***|***|***|***|
            persent_AssistP = pos;
            persent_AssistS = scale;
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 調整
            //*|***|***|***|***|***|***|***|***|***|***|***|
            persent_AssistS.x = MyCalculator.Division(persent_AssistS.x, truePartsCollectionDigit);
            scale.x = persent_AssistS.x;
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 以下も同じ場所に現れる
        //*|***|***|***|***|***|***|***|***|***|***|***|
        posSave = pos;
        scaleSave = scale;
        Texture2D parts = m_warehouseObject.GetTexture2DApp(AppImageNum.ROCKETPARTS);
        Texture2D partsShadow = m_warehouseObject.GetTexture2DApp(AppImageNum.ROCKETPARTSSHADOW);
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 所持パーツ数字
        //*|***|***|***|***|***|***|***|***|***|***|***|
        for (int index = 0; index < m_partsCollectionDigit; index++)
        {
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 計算用初期化、データ確保
            //*|***|***|***|***|***|***|***|***|***|***|***|
            int indexReverse = MyCalculator.InversionOfIndex(index, truePartsCollectionDigit);
            float indexPoint = MyCalculator.IndexCenterPos(indexReverse, truePartsCollectionDigit);
            pos.x = posSave.x + (persent_AssistS.x * indexPoint);
            pos.y = posSave.y;
            scale = scaleSave;
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 透明度
            //*|***|***|***|***|***|***|***|***|***|***|***|
            float alphaParsent = 1.0f * m_gameEnd;
            m_partsCollection[index].gameObjectUI.SetAlpha(alphaParsent);
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 桁挿入
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_partsCollection[index].imageUIData.imagePos = new Vector2(pos.x, pos.y);
            m_partsCollection[index].imageUIData.imageScale = new Vector2(scale.x, scale.y);
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 持っているか？
            //*|***|***|***|***|***|***|***|***|***|***|***|
            digitNum = dataBool[index];
            if(digitNum)
            {
                m_partsCollection[index].gameObjectUI.SetTexture(parts);
            }
            else
            {
                m_partsCollection[index].gameObjectUI.SetTexture(partsShadow);
            }
        }
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 警報起動
    //*|***|***|***|***|***|***|***|***|***|***|***|
    void UpdateAlarm()
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 画像データ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        Rect imageRect = MyCalculator.RectSizeReverse_Y(0, 1, 1);
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // データ集計
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_alarmStrong = ChangeData.Among(m_alarmStrong, 0.0f, 1.0f);
        float alarmParsent = m_alarmStrong;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 調整用データ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        Vector2 posSave = new Vector2(0.0f, 0.0f);
        Vector2 scaleSave = new Vector2(0.0f, 0.0f);
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 調整用データ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        Vector2 pos = new Vector2(0.0f, 0.0f);
        Vector2 scale = new Vector2(0.0f, 0.0f);
        //Vector2 persent_AssistS = new Vector2(0.5f, 0.5f);
        //Vector2 persent_AssistP = new Vector2(0.5f, 0.5f);
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 調整済みデータ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        Vector2 posScleen = new Vector2(0.0f, 0.0f);
        Vector2 posReverseY = new Vector2(0.0f, 0.0f);
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 本当の
        //*|***|***|***|***|***|***|***|***|***|***|***|
        int truePartsCollectionDigit = m_partsCollectionDigit + 0;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 警報
        //*|***|***|***|***|***|***|***|***|***|***|***|
        {
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 計算用初期化、データ確保
            //*|***|***|***|***|***|***|***|***|***|***|***|

            scale.x = 1.0f;
            scale.y = 1.0f;
            pos.x = MyCalculator.Division(scale.x, 2.0f);
            pos.y = MyCalculator.Division(scale.y, 2.0f);
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
        posSave = pos;
        scaleSave = scale;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 警報
        //*|***|***|***|***|***|***|***|***|***|***|***|
        {
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 計算用初期化、データ確保
            //*|***|***|***|***|***|***|***|***|***|***|***|
            pos = posSave;
            scale = scaleSave;
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 桁挿入
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_alarm.imageUIData.imagePos = new Vector2(pos.x, pos.y);
            m_alarm.imageUIData.imageScale = new Vector2(scale.x, scale.y);
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 透明度
            //*|***|***|***|***|***|***|***|***|***|***|***|
            float alphaParsent = alarmParsent * m_gameEnd;
            m_alarm.gameObjectUI.SetAlpha(alphaParsent);
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // ムービー
        //*|***|***|***|***|***|***|***|***|***|***|***|
        {
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 計算用初期化、データ確保
            //*|***|***|***|***|***|***|***|***|***|***|***|
            pos = posSave;
            scale = scaleSave;
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 桁挿入
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_movie.imageUIData.imagePos = new Vector2(pos.x, pos.y);
            m_movie.imageUIData.imageScale = new Vector2(scale.x, scale.y);
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 透明度
            //*|***|***|***|***|***|***|***|***|***|***|***|
            float alphaParsent = m_gameMovie;
            m_movie.gameObjectUI.SetAlpha(alphaParsent);
        }
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // パワーアップ更新
    //*|***|***|***|***|***|***|***|***|***|***|***|
    void UpdatePowerUp()
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // データ調整
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_armStrongTime = ChangeData.Among(m_armStrongTime, 0.0f, 1.0f);
        m_bodyStrongTime = ChangeData.Among(m_bodyStrongTime, 0.0f, 1.0f);
        m_headStrongTime = ChangeData.Among(m_headStrongTime, 0.0f, 1.0f);
        m_legStrongTime = ChangeData.Among(m_legStrongTime, 0.0f, 1.0f);
        float armStrongFlag = 0;
        float bodyStrongFlag = 0;
        float headStrongFlag = 0;
        float legStrongFlag = 0;
        if (m_armStrongTime > 0)
        {
            armStrongFlag = 1;
        }
        if (m_bodyStrongTime > 0)
        {
            bodyStrongFlag = 1;
        }
        if (m_headStrongTime > 0)
        {
            headStrongFlag = 1;
        }
        if (m_legStrongTime > 0)
        {
            legStrongFlag = 1;
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 調整用データ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        Vector2 posSave = new Vector2(0.0f, 0.0f);
        Vector2 scaleSave = new Vector2(0.0f, 0.0f);
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

        int areaDigit = 4;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 所持数星量その他
        //*|***|***|***|***|***|***|***|***|***|***|***|
        {
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 計算用初期化、データ確保
            //*|***|***|***|***|***|***|***|***|***|***|***|
            pos = new Vector2(0.1f, 0.2f + MyCalculator.Division(0.05f, 2));
            scale = new Vector2(0.2f, 0.05f);
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 調整済みデータ
            //*|***|***|***|***|***|***|***|***|***|***|***|
            posScleen = MouseToScreenPos(pos);
            posReverseY = MyCalculator.EachTimes(posScleen, new Vector2(1.0f, -1.0f));
            pos = posReverseY;
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 計算用初期化、データ確保
            //*|***|***|***|***|***|***|***|***|***|***|***|
            persent_AssistP = pos;
            persent_AssistS = scale;
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 調整
            //*|***|***|***|***|***|***|***|***|***|***|***|
            persent_AssistS.x = MyCalculator.Division(persent_AssistS.x, areaDigit);
            scale.x = persent_AssistS.x;
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 以下も同じ場所に現れる
        //*|***|***|***|***|***|***|***|***|***|***|***|
        posSave = pos;
        scaleSave = scale;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // パワーアップ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        {
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 計算用初期化、データ確保
            //*|***|***|***|***|***|***|***|***|***|***|***|
            int indexReverse = MyCalculator.InversionOfIndex(0, areaDigit);
            float indexPoint = MyCalculator.IndexCenterPos(indexReverse, areaDigit);
            pos.x = posSave.x + (persent_AssistS.x * indexPoint);
            pos.y = posSave.y;
            scale = scaleSave;
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 桁挿入
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_armPowerUp.m_icon.imageUIData.imagePos = new Vector2(pos.x, pos.y);
            m_armPowerUp.m_icon.imageUIData.imageScale = new Vector2(scale.x, scale.y);
            m_armPowerUp.m_time.imageUIData.imagePos = new Vector2(pos.x, pos.y);
            m_armPowerUp.m_time.imageUIData.imageScale = new Vector2(scale.x, scale.y);
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 透明度
            //*|***|***|***|***|***|***|***|***|***|***|***|
            float alphaParsent = armStrongFlag * m_gameEnd;
            m_armPowerUp.m_icon.gameObjectUI.SetAlpha(alphaParsent);
            m_armPowerUp.m_time.gameObjectUI.SetAlpha(alphaParsent);
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 円形変化
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_armPowerUp.m_time.gameObjectUI.SetCircleMode(90, m_armStrongTime);
        }
        {
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 計算用初期化、データ確保
            //*|***|***|***|***|***|***|***|***|***|***|***|
            int indexReverse = MyCalculator.InversionOfIndex(1, areaDigit);
            float indexPoint = MyCalculator.IndexCenterPos(indexReverse, areaDigit);
            pos.x = posSave.x + (persent_AssistS.x * indexPoint);
            pos.y = posSave.y;
            scale = scaleSave;
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 桁挿入
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_bodyPowerUp.m_icon.imageUIData.imagePos = new Vector2(pos.x, pos.y);
            m_bodyPowerUp.m_icon.imageUIData.imageScale = new Vector2(scale.x, scale.y);
            m_bodyPowerUp.m_time.imageUIData.imagePos = new Vector2(pos.x, pos.y);
            m_bodyPowerUp.m_time.imageUIData.imageScale = new Vector2(scale.x, scale.y);
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 透明度
            //*|***|***|***|***|***|***|***|***|***|***|***|
            float alphaParsent = bodyStrongFlag * m_gameEnd;
            m_bodyPowerUp.m_icon.gameObjectUI.SetAlpha(alphaParsent);
            m_bodyPowerUp.m_time.gameObjectUI.SetAlpha(alphaParsent);
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 円形変化
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_bodyPowerUp.m_time.gameObjectUI.SetCircleMode(90, m_bodyStrongTime);
        }
        {
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 計算用初期化、データ確保
            //*|***|***|***|***|***|***|***|***|***|***|***|
            int indexReverse = MyCalculator.InversionOfIndex(2, areaDigit);
            float indexPoint = MyCalculator.IndexCenterPos(indexReverse, areaDigit);
            pos.x = posSave.x + (persent_AssistS.x * indexPoint);
            pos.y = posSave.y;
            scale = scaleSave;
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 桁挿入
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_headPowerUp.m_icon.imageUIData.imagePos = new Vector2(pos.x, pos.y);
            m_headPowerUp.m_icon.imageUIData.imageScale = new Vector2(scale.x, scale.y);
            m_headPowerUp.m_time.imageUIData.imagePos = new Vector2(pos.x, pos.y);
            m_headPowerUp.m_time.imageUIData.imageScale = new Vector2(scale.x, scale.y);
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 透明度
            //*|***|***|***|***|***|***|***|***|***|***|***|
            float alphaParsent = headStrongFlag * m_gameEnd;
            m_headPowerUp.m_icon.gameObjectUI.SetAlpha(alphaParsent);
            m_headPowerUp.m_time.gameObjectUI.SetAlpha(alphaParsent);
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 円形変化
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_headPowerUp.m_time.gameObjectUI.SetCircleMode(90, m_headStrongTime);
        }
        {
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 計算用初期化、データ確保
            //*|***|***|***|***|***|***|***|***|***|***|***|
            int indexReverse = MyCalculator.InversionOfIndex(3, areaDigit);
            float indexPoint = MyCalculator.IndexCenterPos(indexReverse, areaDigit);
            pos.x = posSave.x + (persent_AssistS.x * indexPoint);
            pos.y = posSave.y;
            scale = scaleSave;
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 桁挿入
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_legPowerUp.m_icon.imageUIData.imagePos = new Vector2(pos.x, pos.y);
            m_legPowerUp.m_icon.imageUIData.imageScale = new Vector2(scale.x, scale.y);
            m_legPowerUp.m_time.imageUIData.imagePos = new Vector2(pos.x, pos.y);
            m_legPowerUp.m_time.imageUIData.imageScale = new Vector2(scale.x, scale.y);
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 透明度
            //*|***|***|***|***|***|***|***|***|***|***|***|
            float alphaParsent = legStrongFlag * m_gameEnd;
            m_legPowerUp.m_icon.gameObjectUI.SetAlpha(alphaParsent);
            m_legPowerUp.m_time.gameObjectUI.SetAlpha(alphaParsent);
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 円形変化
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_legPowerUp.m_time.gameObjectUI.SetCircleMode(90, m_legStrongTime);
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
        // 酸素ゲージ更新
        //*|***|***|***|***|***|***|***|***|***|***|***|
        RenderAirData();
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 星ゲージ更新
        //*|***|***|***|***|***|***|***|***|***|***|***|
        RenderStarData();
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 所持数星量更新
        //*|***|***|***|***|***|***|***|***|***|***|***|
        RenderHaveStarData();
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 目標数星量更新
        //*|***|***|***|***|***|***|***|***|***|***|***|
        RenderNeedStarData();
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 所持パーツ更新
        //*|***|***|***|***|***|***|***|***|***|***|***|
        RenderPartsCollection();
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 警報更新
        //*|***|***|***|***|***|***|***|***|***|***|***|
        RenderAlarm();
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // パワーアップ更新
        //*|***|***|***|***|***|***|***|***|***|***|***|
        RenderPowerUp();
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 酸素ゲージ更新
    //*|***|***|***|***|***|***|***|***|***|***|***|
    void RenderAirData()
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 酸素ゲージフレーム
        //*|***|***|***|***|***|***|***|***|***|***|***|
        {
            AssetSet(ref m_airGaugeFrame, m_screenSize, true);
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 酸素ゲージ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        {
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 挿入
            //*|***|***|***|***|***|***|***|***|***|***|***|
            AssetSet(ref m_airGaugeMain, m_screenSize, true);
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 酸素ゲージ影
        //*|***|***|***|***|***|***|***|***|***|***|***|
        {
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 挿入
            //*|***|***|***|***|***|***|***|***|***|***|***|
            AssetSet(ref m_airGaugeShadow, m_screenSize, true);
        }
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 星ゲージ更新
    //*|***|***|***|***|***|***|***|***|***|***|***|
    void RenderStarData()
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 星ゲージパーセント
        //*|***|***|***|***|***|***|***|***|***|***|***|
        for (int index = 0; index < m_starRate.Count; index++)
        {
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 桁挿入
            //*|***|***|***|***|***|***|***|***|***|***|***|
            OriginUIGroup migawari = m_starRate[index];
            AssetSet(ref migawari, m_screenSize, true);
            m_starRate[index] = migawari;
        }
        {
            AssetSet(ref m_starRateParsent, m_screenSize, true);
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 星ゲージフレーム
        //*|***|***|***|***|***|***|***|***|***|***|***|
        {
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 挿入
            //*|***|***|***|***|***|***|***|***|***|***|***|
            AssetSet(ref m_starGaugeFrame, m_screenSize, true);
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 星ゲージ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        {
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 挿入
            //*|***|***|***|***|***|***|***|***|***|***|***|
            AssetSet(ref m_starGaugeMain, m_screenSize, true);
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 星ゲージ影
        //*|***|***|***|***|***|***|***|***|***|***|***|
        {
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 挿入
            //*|***|***|***|***|***|***|***|***|***|***|***|
            AssetSet(ref m_starGaugeShadow, m_screenSize, true);
        }
    }

    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 所持数星量更新
    //*|***|***|***|***|***|***|***|***|***|***|***|
    void RenderHaveStarData()
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 所持数星量数字
        //*|***|***|***|***|***|***|***|***|***|***|***|
        for (int index = 0; index < m_haveStar.Count; index++)
        {
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 桁挿入
            //*|***|***|***|***|***|***|***|***|***|***|***|
            OriginUIGroup migawari = m_haveStar[index];
            AssetSet(ref migawari, m_screenSize, true);
            m_haveStar[index] = migawari;
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 所持数星量その他
        //*|***|***|***|***|***|***|***|***|***|***|***|
        AssetSet(ref m_haveStarKO, m_screenSize, true);
        AssetSet(ref m_haveStarStar, m_screenSize, true);
        AssetSet(ref m_haveStarCross, m_screenSize, true);
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 所持数星量文字
        //*|***|***|***|***|***|***|***|***|***|***|***|
        AssetSet(ref m_haveStarMozi, m_screenSize, true);
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 目標数星量更新
    //*|***|***|***|***|***|***|***|***|***|***|***|
    void RenderNeedStarData()
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 目標数星量数字
        //*|***|***|***|***|***|***|***|***|***|***|***|
        for (int index = 0; index < m_needStar.Count; index++)
        {
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 桁挿入
            //*|***|***|***|***|***|***|***|***|***|***|***|
            OriginUIGroup migawari = m_needStar[index];
            AssetSet(ref migawari, m_screenSize, true);
            m_needStar[index] = migawari;
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 目標数星量その他
        //*|***|***|***|***|***|***|***|***|***|***|***|
        AssetSet(ref m_needStarKO, m_screenSize, true);
        AssetSet(ref m_needStarStar, m_screenSize, true);
        AssetSet(ref m_needStarCross, m_screenSize, true);
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 目標数星量文字
        //*|***|***|***|***|***|***|***|***|***|***|***|
        AssetSet(ref m_needStarMozi, m_screenSize, true);
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 所持パーツ更新
    //*|***|***|***|***|***|***|***|***|***|***|***|
    void RenderPartsCollection()
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 所持パーツ数字
        //*|***|***|***|***|***|***|***|***|***|***|***|
        for (int index = 0; index < m_partsCollection.Count; index++)
        {
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 桁挿入
            //*|***|***|***|***|***|***|***|***|***|***|***|
            OriginUIGroup migawari = m_partsCollection[index];
            AssetSet(ref migawari, m_screenSize, true);
            m_partsCollection[index] = migawari;
        }
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 警報更新
    //*|***|***|***|***|***|***|***|***|***|***|***|
    void RenderAlarm()
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 警報
        //*|***|***|***|***|***|***|***|***|***|***|***|
        {
            AssetSet(ref m_alarm, m_screenSize, true);
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // ムービー
        //*|***|***|***|***|***|***|***|***|***|***|***|
        {
            AssetSet(ref m_movie, m_screenSize, true);
        }
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // パワーアップ更新
    //*|***|***|***|***|***|***|***|***|***|***|***|
    void RenderPowerUp()
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // パワーアップ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        AssetSet(ref m_armPowerUp.m_time, m_screenSize, true);
        AssetSet(ref m_armPowerUp.m_icon, m_screenSize, true);

        AssetSet(ref m_bodyPowerUp.m_time, m_screenSize, true);
        AssetSet(ref m_bodyPowerUp.m_icon, m_screenSize, true);

        AssetSet(ref m_headPowerUp.m_time, m_screenSize, true);
        AssetSet(ref m_headPowerUp.m_icon, m_screenSize, true);

        AssetSet(ref m_legPowerUp.m_time, m_screenSize, true);
        AssetSet(ref m_legPowerUp.m_icon, m_screenSize, true);

    }
}
