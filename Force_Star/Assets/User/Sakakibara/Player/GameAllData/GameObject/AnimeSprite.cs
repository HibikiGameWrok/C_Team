using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//*|***|***|***|***|***|***|***|***|***|***|***|
// プレイヤーナンバー言い換え
//*|***|***|***|***|***|***|***|***|***|***|***|
using WarehousePlayer = WarehouseData.PlayerData.WarehousePlayer;
using PlayerDataNum = WarehouseData.PlayerData.WarehousePlayer.PlayerData_Number;
using PlayerData_Number_List = WarehouseData.PlayerData.WarehousePlayer.PlayerData_Number_List;

using TexImageData = GameDataPublic.TexImageData;
using RenderImageData = GameDataPublic.RenderImageData;
using PartsData = GameDataPublic.PartsData;
//*|***|***|***|***|***|***|***|***|***|***|***|
// オーダー倉庫言い換え
//*|***|***|***|***|***|***|***|***|***|***|***|
using WarehouseOrder = WarehouseData.WarehouseOrder;
using Object_Order_Number = WarehouseData.WarehouseOrder.Object_Order_Number;


//*|***|***|***|***|***|***|***|***|***|***|***|
// ゲームオブジェクトデータは眠らない
//*|***|***|***|***|***|***|***|***|***|***|***|
abstract public class AnimeSprite : MonoBehaviour
{
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // ゲーム共通ディレクター
    //*|***|***|***|***|***|***|***|***|***|***|***|
    protected WarehouseOrder m_warehouseOrder;
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // データ
    //*|***|***|***|***|***|***|***|***|***|***|***|
    protected GameObjectSprite m_mySprite;
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 継承用使用データ
    //*|***|***|***|***|***|***|***|***|***|***|***|
    protected TexImageData m_texImageData;
    protected RenderImageData m_renderImageData;
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 継承用取得データ
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public Vector2 m_size;
    public int m_rectNum;
    public int m_rectX;
    public int m_rectY;
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 親のパーツを継承する回転、場所、拡大縮小
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public Vector3 m_localPos;
    public Vector3 m_localAngle;
    public Vector2 m_localScale;
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 親のパーツが関係ない回転、場所、拡大縮小
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public Vector3 m_imagePos;
    public Vector3 m_imageAngle;
    public Vector2 m_imageScale;
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 親のパーツに調和できないもの
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public Vector3 m_difAngle;

    public int m_dataNum;
    public int m_depth;
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 真の深度
    //*|***|***|***|***|***|***|***|***|***|***|***|
    protected int m_depthData;

    //*|***|***|***|***|***|***|***|***|***|***|***|
    // これが出来たときに
    //*|***|***|***|***|***|***|***|***|***|***|***|
    void Awake()
    {
        AwakeOrigin();
    }
    protected void AwakeOrigin()
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // データ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_mySprite = null;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 継承用使用データ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_texImageData = new TexImageData();
        m_texImageData.Reset();
        m_renderImageData = new RenderImageData();
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // オーダー倉庫
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_warehouseOrder = WarehouseOrder.GetInstance();
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 継承用取得データ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_size = new Vector2();
        m_rectNum = 1;
        m_rectX = 1;
        m_rectY = 1;
        m_localScale = Vector2.one;
        m_imageScale = Vector2.one;

        m_localPos = new Vector3();
        m_localAngle = new Vector3();

        m_imagePos = new Vector3();
        m_imageAngle = new Vector3();

        m_dataNum = 0;
        m_depth = 0;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 開始時
    //*|***|***|***|***|***|***|***|***|***|***|***|
    void Start()
    {
        StartTex();
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 開始時
    //*|***|***|***|***|***|***|***|***|***|***|***|
    void StartTex()
    {

    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // アップデート
    //*|***|***|***|***|***|***|***|***|***|***|***|
    void Update()
    {
        UpdateOrigin();
    }

    //*|***|***|***|***|***|***|***|***|***|***|***|
    // アップデート
    //*|***|***|***|***|***|***|***|***|***|***|***|
    protected void UpdateOrigin()
    {
        Vector2 localScale = Vector2.one;
        Vector2 imageScale = Vector2.one;
        Vector3 localPos = Vector3.zero;
        Vector3 imagePos = Vector3.zero;

        Vector3 localAngle = Vector3.zero;
        Vector3 imageAngle = Vector3.zero;
        Vector3 difAngle = Vector3.zero;
        Quaternion localAngleQ = Quaternion.identity;
        Quaternion imageAngleQ = Quaternion.identity;
        Quaternion difAngleQ = Quaternion.identity;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 反映
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // ノータッチ対策
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_depthData = m_depth;

        //*|***|***|***|***|***|***|***|***|***|***|***|
        // イメージを作成
        //*|***|***|***|***|***|***|***|***|***|***|***|
        UpdateImageTex();
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 場所を作成
        //*|***|***|***|***|***|***|***|***|***|***|***|
        localPos = m_localPos;
        imagePos = m_imagePos;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 回転を作成
        //*|***|***|***|***|***|***|***|***|***|***|***|
        localAngle = m_localAngle;
        imageAngle = m_imageAngle;
        difAngle = m_difAngle;
        localAngleQ = Quaternion.Euler(localAngle);
        imageAngleQ = Quaternion.Euler(imageAngle);
        difAngleQ = Quaternion.Euler(difAngle);
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 場所を再構成
        //*|***|***|***|***|***|***|***|***|***|***|***|
        localPos = difAngleQ * localPos;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 拡大拡縮を作成
        //*|***|***|***|***|***|***|***|***|***|***|***|
        localScale = m_localScale;
        imageScale = m_imageScale;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 描画を作成
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_renderImageData.depth = m_depthData;

        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 実施
        //*|***|***|***|***|***|***|***|***|***|***|***|

        if(m_mySprite != null)
        {
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // イメージを作成
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_mySprite.SetImageUpdate(m_texImageData);
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 場所を作成
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_mySprite.SetPositionLocal(localPos);
            m_mySprite.SetImagePositionLocal(imagePos);
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 回転を作成
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_mySprite.SetRotationLocal(localAngleQ);
            m_mySprite.SetImageRotationLocal(imageAngleQ);
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 拡大拡縮を作成
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_mySprite.SetImageScaleLocal(m_imageScale);
            m_mySprite.SetScaleLocal(m_localScale);
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 描画を作成
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_mySprite.SetRenderUpdate(m_renderImageData);
        }

    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // アップデート抽象クラス
    //*|***|***|***|***|***|***|***|***|***|***|***|
    protected abstract void UpdateImageTex();

    //*|***|***|***|***|***|***|***|***|***|***|***|
    // アップデート抽象クラス
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public void SetSprite(GameObjectSprite sprite)
    {
        m_mySprite = sprite;
    }
}
