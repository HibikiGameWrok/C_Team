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

public class ExprosionColor : MonoBehaviour
{
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // ゲーム共通ディレクター
    //*|***|***|***|***|***|***|***|***|***|***|***|
    private PlaySceneDirectorIndex m_directorIndex;
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // プレイヤー倉庫
    //*|***|***|***|***|***|***|***|***|***|***|***|
    private WarehousePlayer m_warehousePlayer;
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // オブジェクト倉庫
    //*|***|***|***|***|***|***|***|***|***|***|***|
    private WarehouseObject m_warehouseObject;
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 爆発経過時間
    //*|***|***|***|***|***|***|***|***|***|***|***|
    private float m_timeExplosion;
    private static float m_timeMax = 40;
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 爆発状態
    //*|***|***|***|***|***|***|***|***|***|***|***|
    private bool m_awakeFlag = false;
    private bool m_bombFlag = false;
    private bool m_fireFlag = false;
    private Vector3 m_pointBomb;
    private Vector3 m_pointBombDif;
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 爆発
    //*|***|***|***|***|***|***|***|***|***|***|***|
    GameObject m_explosionObject;
    GameObjectSprite m_explosionColor;
    AppImageNum m_imageNum;
    private TexImageData m_explosionTexImageData;
    private RenderImageData m_explosionRenderImageData;
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 爆発
    //*|***|***|***|***|***|***|***|***|***|***|***|
    void Awake()
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // ゲーム共通ディレクター
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_directorIndex = PlaySceneDirectorIndex.GetInstance();
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // プレイヤー倉庫
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_warehousePlayer = WarehousePlayer.GetInstance();
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // オブジェクト倉庫
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_warehouseObject = WarehouseObject.GetInstance();
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 初期化
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_explosionObject = new GameObject("ExplosionColor");
        m_explosionObject.transform.parent = gameObject.transform;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 爆発
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_explosionColor = m_explosionObject.AddComponent<GameObjectSprite>();
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 爆発イメージ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_imageNum = AppImageNum.EXPROSION;
        m_explosionTexImageData = new TexImageData();
        m_explosionTexImageData.image = m_warehouseObject.GetTexture2DApp(m_imageNum);
        m_explosionTexImageData.pibot = new Vector2(0.5f, 0.5f);
        m_explosionTexImageData.size = new Vector2(5.0f, 5.0f);
        m_explosionTexImageData.rextParsent = MyCalculator.RectSizeReverse_Y(0, 6, 1);

        m_explosionRenderImageData = new RenderImageData();
        m_explosionRenderImageData.depth = WarehouseData.WarehouseOrder.GetInstance().GetOrderToLayerSprite(WarehouseData.WarehouseOrder.Object_Order_Number.PLAYER_FLONT_GIMMICK);

        m_explosionColor.SetImage(m_explosionTexImageData);
        m_explosionColor.SetRenderUpdate(m_explosionRenderImageData);
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 爆発状態
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_timeExplosion = 0;
        m_awakeFlag = false;
        m_pointBomb = Vector3.zero;
    }
    void Start()
    {

    }

    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 目覚めているか
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public void SetBomb(AppImageNum exprosionNum, float timeMax)
    {
        m_timeExplosion = 0;
        m_timeMax = timeMax;
        m_imageNum = exprosionNum;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 単発爆発する
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_awakeFlag = true;
        m_bombFlag = true;
        m_fireFlag = false;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 場所変更
        //*|***|***|***|***|***|***|***|***|***|***|***|
        RandomDifPoint();
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 爆発イメージ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_explosionColor.SetTexture(m_warehouseObject.GetTexture2DApp(m_imageNum));
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 音：ドーン
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_directorIndex.PlaySoundEffect(SEManager.SoundID.PLAYERFIRE_SE);
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 場所
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public void SetPoint(Vector3 exprosionPoint)
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 場所
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_pointBomb = exprosionPoint;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // ランダム変更
    //*|***|***|***|***|***|***|***|***|***|***|***|
    void RandomDifPoint()
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 場所変更
        //*|***|***|***|***|***|***|***|***|***|***|***|
        float angle = XORShiftRand.GetSeedDivRand(360);
        float speed = MyCalculator.Division((float)XORShiftRand.GetSeedDivRand(100), 100.0f);
        Vector2 dif = ChangeData.AngleDegToVector2(angle) * speed;
        m_pointBombDif = ChangeData.GetVector3(dif);
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 更新
    //*|***|***|***|***|***|***|***|***|***|***|***|
    void Update()
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 目覚めているか
        //*|***|***|***|***|***|***|***|***|***|***|***|
        if (m_awakeFlag)
        {
            UpdateBomb();
            UpdateFire();
        }
        else
        {
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 表示切り替え
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_explosionColor.SetAlpha(0.0f);
        }
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 爆発の更新
    //*|***|***|***|***|***|***|***|***|***|***|***|
    void UpdateBomb()
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 目覚めているか
        //*|***|***|***|***|***|***|***|***|***|***|***|
        if (m_bombFlag)
        {
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 時間経過
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_timeExplosion++;
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 爆発の花
            //*|***|***|***|***|***|***|***|***|***|***|***|
            if (m_timeExplosion < m_timeMax)
            {
                //*|***|***|***|***|***|***|***|***|***|***|***|
                // アニメ番号
                //*|***|***|***|***|***|***|***|***|***|***|***|
                float parsent = MyCalculator.Division(m_timeExplosion, m_timeMax);
                float panel = parsent * 6.0f;
                int panelNum = Mathf.FloorToInt(panel);
                //*|***|***|***|***|***|***|***|***|***|***|***|
                // アニメ切り替え
                //*|***|***|***|***|***|***|***|***|***|***|***|
                m_explosionColor.SetRect(MyCalculator.RectSizeReverse_Y(panelNum, 6, 1));
                m_explosionColor.SetAlpha(1.0f);
                //*|***|***|***|***|***|***|***|***|***|***|***|
                // 場所
                //*|***|***|***|***|***|***|***|***|***|***|***|
                Vector3 pointBomb = m_pointBombDif + m_pointBomb;
                m_explosionColor.SetPosition(pointBomb);
            }
            else
            {
                m_awakeFlag = false;
            }
        }
    }

    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 炎の更新
    //*|***|***|***|***|***|***|***|***|***|***|***|
    void UpdateFire()
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 目覚めているか
        //*|***|***|***|***|***|***|***|***|***|***|***|
        if (m_fireFlag)
        {
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 時間経過
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_timeExplosion++;
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 爆発の花
            //*|***|***|***|***|***|***|***|***|***|***|***|
            if (m_timeExplosion < m_timeMax)
            {
                //*|***|***|***|***|***|***|***|***|***|***|***|
                // アニメ番号
                //*|***|***|***|***|***|***|***|***|***|***|***|
                float parsent = MyCalculator.Division(m_timeExplosion, m_timeMax);
                float panel = parsent * 6.0f;
                int panelNum = Mathf.FloorToInt(panel);
                //*|***|***|***|***|***|***|***|***|***|***|***|
                // アニメ切り替え
                //*|***|***|***|***|***|***|***|***|***|***|***|
                m_explosionColor.SetRect(MyCalculator.RectSizeReverse_Y(panelNum, 6, 1));
                m_explosionColor.SetAlpha(1.0f);
                //*|***|***|***|***|***|***|***|***|***|***|***|
                // 場所
                //*|***|***|***|***|***|***|***|***|***|***|***|
                Vector3 pointBomb = m_pointBombDif + m_pointBomb;
                m_explosionColor.SetPosition(pointBomb);
            }
            else
            {
                m_awakeFlag = false;
            }
        }
    }
}
