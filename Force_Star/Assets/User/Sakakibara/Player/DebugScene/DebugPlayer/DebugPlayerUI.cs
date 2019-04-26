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


using CommonImageNum = WarehouseData.WarehouseStaticData.Object2D_Numbers_Common;

using PlayerAnotherImageNum = WarehouseData.PlayerData.WarehousePlayer.PlayerData_Another_Number_List;
using PlayerImageNum = WarehouseData.PlayerData.WarehousePlayer.PlayerData_Number_List;

public class DebugPlayerUI : DebugCanvas
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

    private GameObjectUI m_airGaugeMain;
    private GameObjectUI m_airGaugeFrame;
    private GameObjectUI m_airGaugeShadow;

    private ImageUIData m_airGaugeMainUI;
    private ImageUIData m_airGaugeFrameUI;
    private ImageUIData m_airGaugeShadowUI;
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
    private List<GameObjectUI> m_starRate;
    private List<ImageUIData> m_starRateUI;


    private GameObjectUI m_starGaugeMain;
    private GameObjectUI m_starGaugeFrame;
    private GameObjectUI m_starGaugeShadow;

    private ImageUIData m_starGaugeMainUI;
    private ImageUIData m_starGaugeFrameUI;
    private ImageUIData m_starGaugeShadowUI;
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
    private List<GameObjectUI> m_haveStar;
    private List<ImageUIData> m_haveStarUI;
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 目標数星量
    //*|***|***|***|***|***|***|***|***|***|***|***|
    private int m_needStarDigit;
    private List<GameObjectUI> m_needStar;
    private List<ImageUIData> m_needStarUI;


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


    enum DepthAttach
    {
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
            m_airGaugeFrame = CreateMenber(tex, "AirGaugeFrame");
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // イメージの情報
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_airGaugeFrame.SetDepth((int)DepthAttach.AIRGAUGE_FRAME);

            m_airGaugeFrame.gameObject.transform.SetParent(m_canvasObject.gameObject.transform);
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
            m_airGaugeMain = CreateMenber(tex, "AirGaugeMain");
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // イメージの情報
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_airGaugeMain.SetPibot(new Vector2(0.5f, 0.0f));
            m_airGaugeMain.SetDepth((int)DepthAttach.AIRGAUGE_MAIN);

            m_airGaugeMain.gameObject.transform.SetParent(m_canvasObject.gameObject.transform);
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
            m_airGaugeShadow = CreateMenber(tex, "AirGaugeShadow");
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // イメージの情報
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_airGaugeShadow.SetPibot(new Vector2(0.5f, 0.0f));
            m_airGaugeShadow.SetDepth((int)DepthAttach.AIRGAUGE_SHADOW);

            m_airGaugeShadow.gameObject.transform.SetParent(m_canvasObject.gameObject.transform);
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
        m_starRate = new List<GameObjectUI>();
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
            tex.image = m_warehouseObject.GetTexture2D(CommonImageNum.NUMBERS_GREEN);
            tex.rextParsent = MyCalculator.RectSizeReverse_Y(0, 4, 4);
            tex.size = new Vector2(1, 1);
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 桁作成
            //*|***|***|***|***|***|***|***|***|***|***|***|
            GameObjectUI digitData = null;
            digitData = CreateMenber(tex, "StarRate" + index.ToString());
            digitData.gameObject.transform.SetParent(m_canvasObject.gameObject.transform);
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // イメージの情報
            //*|***|***|***|***|***|***|***|***|***|***|***|
            digitData.SetDepth((int)DepthAttach.STARGAUGE_NUMBER);
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 桁挿入
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_starRate.Add(digitData);
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
            m_starGaugeFrame = CreateMenber(tex, "StarGaugeFrame");
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // イメージの情報
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_starGaugeFrame.SetDepth((int)DepthAttach.STARGAUGE_FRAME);

            m_starGaugeFrame.gameObject.transform.SetParent(m_canvasObject.gameObject.transform);
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
            m_starGaugeMain = CreateMenber(tex, "StarGaugeMain");
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // イメージの情報
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_starGaugeMain.SetPibot(new Vector2(0.5f, 0.0f));
            m_starGaugeMain.SetDepth((int)DepthAttach.STARGAUGE_MAIN);

            m_starGaugeMain.gameObject.transform.SetParent(m_canvasObject.gameObject.transform);
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
            m_starGaugeShadow = CreateMenber(tex, "StarGaugeShadow");
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // イメージの情報
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_starGaugeShadow.SetPibot(new Vector2(0.5f, 0.0f));
            m_starGaugeShadow.SetDepth((int)DepthAttach.STARGAUGE_SHADOW);

            m_starGaugeShadow.gameObject.transform.SetParent(m_canvasObject.gameObject.transform);
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
        m_haveStarDigit = 8;
        m_haveStar = new List<GameObjectUI>();
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
            tex.image = m_warehouseObject.GetTexture2D(CommonImageNum.NUMBERS_GREEN);
            tex.rextParsent = MyCalculator.RectSizeReverse_Y(0, 4, 4);
            tex.size = new Vector2(1, 1);
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 桁作成
            //*|***|***|***|***|***|***|***|***|***|***|***|
            GameObjectUI digitData = null;
            digitData = CreateMenber(tex, "HaveStar" + index.ToString());
            digitData.gameObject.transform.SetParent(m_canvasObject.gameObject.transform);
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // イメージの情報
            //*|***|***|***|***|***|***|***|***|***|***|***|
            digitData.SetDepth((int)DepthAttach.HAVESTAR_NUMBER);
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 桁挿入
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_haveStar.Add(digitData);
        }
        m_haveStarNumber = 0;
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
        m_needStarDigit = 8;
        m_needStar = new List<GameObjectUI>();
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
            tex.image = m_warehouseObject.GetTexture2D(CommonImageNum.NUMBERS_RED);
            tex.rextParsent = MyCalculator.RectSizeReverse_Y(0, 4, 4);
            tex.size = new Vector2(1, 1);
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 桁作成
            //*|***|***|***|***|***|***|***|***|***|***|***|
            GameObjectUI digitData = null;
            digitData = CreateMenber(tex, "NeedStar" + index.ToString());
            digitData.gameObject.transform.SetParent(m_canvasObject.gameObject.transform);
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // イメージの情報
            //*|***|***|***|***|***|***|***|***|***|***|***|
            digitData.SetDepth((int)DepthAttach.NEEDSTAR_NUMBER);
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 桁挿入
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_needStar.Add(digitData);
        }
        m_needStarNumber = 0;
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
            m_airGaugeMainUI = new ImageUIData();
            m_airGaugeMainUI.Init();

            m_airGaugeFrameUI = new ImageUIData();
            m_airGaugeFrameUI.Init();

            m_airGaugeShadowUI = new ImageUIData();
            m_airGaugeShadowUI.Init();
        }
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 星ゲージ
    //*|***|***|***|***|***|***|***|***|***|***|***|
    void AwakeStarDataUI()
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 星ゲージ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_starRateUI = new List<ImageUIData>();
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 星ゲージパーセント
        //*|***|***|***|***|***|***|***|***|***|***|***|
        for (int index = 0; index < m_starRateDigit; index++)
        {
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 桁作成
            //*|***|***|***|***|***|***|***|***|***|***|***|
            ImageUIData digitDataUI = null;
            digitDataUI = new ImageUIData();
            digitDataUI.Init();
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 桁挿入
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_starRateUI.Add(digitDataUI);
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 星ゲージ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        {
            m_starGaugeMainUI = new ImageUIData();
            m_starGaugeMainUI.Init();

            m_starGaugeFrameUI = new ImageUIData();
            m_starGaugeFrameUI.Init();

            m_starGaugeShadowUI = new ImageUIData();
            m_starGaugeShadowUI.Init();
        }
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 所持数星量
    //*|***|***|***|***|***|***|***|***|***|***|***|
    void AwakeHaveStarDataUI()
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 所持数星量
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_haveStarUI = new List<ImageUIData>();
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 所持数星量数字
        //*|***|***|***|***|***|***|***|***|***|***|***|
        for (int index = 0; index < m_haveStarDigit; index++)
        {
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 桁作成
            //*|***|***|***|***|***|***|***|***|***|***|***|
            ImageUIData digitDataUI = null;
            digitDataUI = new ImageUIData();
            digitDataUI.Init();
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 桁挿入
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_haveStarUI.Add(digitDataUI);
        }
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 目標数星量
    //*|***|***|***|***|***|***|***|***|***|***|***|
    void AwakeNeedStarDataUI()
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 目標数星量
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_needStarUI = new List<ImageUIData>();
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 目標数星量数字
        //*|***|***|***|***|***|***|***|***|***|***|***|
        for (int index = 0; index < m_needStarDigit; index++)
        {
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 桁作成
            //*|***|***|***|***|***|***|***|***|***|***|***|
            ImageUIData digitDataUI = null;
            digitDataUI = new ImageUIData();
            digitDataUI.Init();
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 桁挿入
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_needStarUI.Add(digitDataUI);
        }
    }


    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 更新したとき
    //*|***|***|***|***|***|***|***|***|***|***|***|
    protected override void UpdateWork()
    {
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
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 酸素ゲージ更新
    //*|***|***|***|***|***|***|***|***|***|***|***|
    void UpdateAirData()
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 画像データ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        Rect imageRect = MyCalculator.RectSizeReverse_Y(0, 1, 1);
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
            // 桁挿入
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_airGaugeFrameUI.imagePos = new Vector2(pos.x, pos.y);
            m_airGaugeFrameUI.imageScale = new Vector2(scale.x, scale.y);
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 場所を継承
        //*|***|***|***|***|***|***|***|***|***|***|***|
        pointX = m_airGaugeFrameUI.imagePos.x;
        pointY = m_airGaugeFrameUI.imagePos.y;
        sizeX = m_airGaugeFrameUI.imageScale.x;
        sizeY = m_airGaugeFrameUI.imageScale.y;
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
        // 酸素ゲージ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_airGaugeMainUI.imagePos = m_airGaugePos;
        m_airGaugeMainUI.imageScale = m_airGaugeSize;     
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 酸素ゲージ影
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_airGaugeShadowUI.imagePos = m_airGaugePos;
        m_airGaugeShadowUI.imageScale = m_airGaugeMaxSize;
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
            m_starRate[digit].SetRect(imageRect);
        }
        
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // Yの限界地点
        //*|***|***|***|***|***|***|***|***|***|***|***|
        float parsentPoint = MyCalculator.Leap(STAR_HIGH, STAR_LOW, parsentStar);
        imageRect = MyCalculator.RectSizeReverse_Y(0, 1, 1);
        imageRect.yMax = parsentPoint;
        m_starGaugeMain.SetRect(imageRect);
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
            // 桁挿入
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_starGaugeFrameUI.imagePos = new Vector2(pos.x, pos.y);
            m_starGaugeFrameUI.imageScale = new Vector2(scale.x, scale.y);
        }


        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 場所を継承
        //*|***|***|***|***|***|***|***|***|***|***|***|
        pointX = m_starGaugeFrameUI.imagePos.x;
        pointY = m_starGaugeFrameUI.imagePos.y;
        sizeX = m_starGaugeFrameUI.imageScale.x;
        sizeY = m_starGaugeFrameUI.imageScale.y;
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
        // 星ゲージパーセント
        //*|***|***|***|***|***|***|***|***|***|***|***|
        for (int index = 0; index < m_starRateUI.Count; index++)
        {
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 計算用初期化、データ確保
            //*|***|***|***|***|***|***|***|***|***|***|***|
            pos = m_starGaugeFrameUI.imagePos;
            scale = m_starGaugeFrameUI.imageScale;
            persent_AssistP = pos;
            persent_AssistS = scale;
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 調整
            //*|***|***|***|***|***|***|***|***|***|***|***|
            persent_AssistS.x = MyCalculator.Division(persent_AssistS.x, m_starRateDigit);
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 計算用初期化、データ確保
            //*|***|***|***|***|***|***|***|***|***|***|***|
            int indexReverse = MyCalculator.InversionOfIndex(index, m_starRateDigit);
            float indexPoint = MyCalculator.IndexCenterPos(indexReverse, m_starRateDigit);
            pos.x = persent_AssistP.x + (persent_AssistS.x * indexPoint);
            pos.y = persent_AssistP.y;
            scale = persent_AssistS;
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 桁挿入
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_starRateUI[index].imagePos = new Vector2(pos.x, pos.y);
            m_starRateUI[index].imageScale = new Vector2(scale.x, scale.y);
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 星ゲージ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_starGaugeMainUI.imagePos = m_starGaugePos;
        m_starGaugeMainUI.imageScale = m_starGaugeSize;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 星ゲージ影
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_starGaugeShadowUI.imagePos = m_starGaugePos;
        m_starGaugeShadowUI.imageScale = m_starGaugeMaxSize;
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
        m_haveStarNumber = ChangeData.Among(m_haveStarNumber, 0, 99999999);
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 現れる数値
        //*|***|***|***|***|***|***|***|***|***|***|***|
        int digitNum = 0;
        for (int digit = 0; digit < m_haveStarDigit; digit++)
        {
            digitNum = MyCalculator.Get10Digit(m_haveStarNumber, digit);
            imageRect = MyCalculator.RectSizeReverse_Y(digitNum, 4, 4);
            m_haveStar[digit].SetRect(imageRect);
        }

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
        // 所持数星量数字
        //*|***|***|***|***|***|***|***|***|***|***|***|
        for (int index = 0; index < m_haveStar.Count; index++)
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
            persent_AssistS.x = MyCalculator.Division(persent_AssistS.x, m_haveStarDigit);
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 計算用初期化、データ確保
            //*|***|***|***|***|***|***|***|***|***|***|***|
            int indexReverse = MyCalculator.InversionOfIndex(index, m_haveStarDigit);
            float indexPoint = MyCalculator.IndexCenterPos(indexReverse, m_haveStarDigit);
            pos.x = persent_AssistP.x + (persent_AssistS.x * indexPoint);
            pos.y = persent_AssistP.y;
            scale = persent_AssistS;
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 桁挿入
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_haveStarUI[index].imagePos = new Vector2(pos.x, pos.y);
            m_haveStarUI[index].imageScale = new Vector2(scale.x, scale.y);
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
        m_needStarNumber = ChangeData.Among(m_needStarNumber, 0, 99999999);
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 現れる数値
        //*|***|***|***|***|***|***|***|***|***|***|***|
        int digitNum = 0;
        for (int digit = 0; digit < m_needStarDigit; digit++)
        {
            digitNum = MyCalculator.Get10Digit(m_needStarNumber, digit);
            imageRect = MyCalculator.RectSizeReverse_Y(digitNum, 4, 4);
            m_needStar[digit].SetRect(imageRect);
        }
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
        // 目標数星量数字
        //*|***|***|***|***|***|***|***|***|***|***|***|
        for (int index = 0; index < m_needStarDigit; index++)
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
            persent_AssistS.x = MyCalculator.Division(persent_AssistS.x, m_needStarDigit);
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 計算用初期化、データ確保
            //*|***|***|***|***|***|***|***|***|***|***|***|
            int indexReverse = MyCalculator.InversionOfIndex(index, m_needStarDigit);
            float indexPoint = MyCalculator.IndexCenterPos(indexReverse, m_needStarDigit);
            pos.x = persent_AssistP.x + (persent_AssistS.x * indexPoint);
            pos.y = persent_AssistP.y;
            scale = persent_AssistS;
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 桁挿入
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_needStarUI[index].imagePos = new Vector2(pos.x, pos.y);
            m_needStarUI[index].imageScale = new Vector2(scale.x, scale.y);
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
            AssetSet(ref m_airGaugeFrame, m_screenSize, m_airGaugeFrameUI, true);
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 酸素ゲージ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        {
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 挿入
            //*|***|***|***|***|***|***|***|***|***|***|***|
            AssetSet(ref m_airGaugeMain, m_screenSize, m_airGaugeMainUI, true);
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 酸素ゲージ影
        //*|***|***|***|***|***|***|***|***|***|***|***|
        {
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 挿入
            //*|***|***|***|***|***|***|***|***|***|***|***|
            AssetSet(ref m_airGaugeShadow, m_screenSize, m_airGaugeShadowUI, true);
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
        for (int index = 0; index < m_starRateUI.Count; index++)
        {
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 桁挿入
            //*|***|***|***|***|***|***|***|***|***|***|***|
            GameObjectUI migawari = m_starRate[index];
            AssetSet(ref migawari, m_screenSize, m_starRateUI[index], true);
            m_starRate[index] = migawari;
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 星ゲージフレーム
        //*|***|***|***|***|***|***|***|***|***|***|***|
        {
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 挿入
            //*|***|***|***|***|***|***|***|***|***|***|***|
            AssetSet(ref m_starGaugeFrame, m_screenSize, m_starGaugeFrameUI, true);
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 星ゲージ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        {
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 挿入
            //*|***|***|***|***|***|***|***|***|***|***|***|
            AssetSet(ref m_starGaugeMain, m_screenSize, m_starGaugeMainUI, true);
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 星ゲージ影
        //*|***|***|***|***|***|***|***|***|***|***|***|
        {
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 挿入
            //*|***|***|***|***|***|***|***|***|***|***|***|
            AssetSet(ref m_starGaugeShadow, m_screenSize, m_starGaugeShadowUI, true);
        }
    }

    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 所持数星量更新
    //*|***|***|***|***|***|***|***|***|***|***|***|
    void RenderHaveStarData()
    {
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
        // 所持数星量数字
        //*|***|***|***|***|***|***|***|***|***|***|***|
        for (int index = 0; index < m_haveStar.Count; index++)
        {
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 桁挿入
            //*|***|***|***|***|***|***|***|***|***|***|***|
            GameObjectUI migawari = m_haveStar[index];
            AssetSet(ref migawari, m_screenSize, m_haveStarUI[index], true);
            m_haveStar[index] = migawari;
        }
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 目標数星量更新
    //*|***|***|***|***|***|***|***|***|***|***|***|
    void RenderNeedStarData()
    {
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
        // 目標数星量数字
        //*|***|***|***|***|***|***|***|***|***|***|***|
        for (int index = 0; index < m_needStar.Count; index++)
        {
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 桁挿入
            //*|***|***|***|***|***|***|***|***|***|***|***|
            GameObjectUI migawari = m_needStar[index];
            AssetSet(ref migawari, m_screenSize, m_needStarUI[index], true);
            m_needStar[index] = migawari;
        }
    }

}
