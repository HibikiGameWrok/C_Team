using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//*|***|***|***|***|***|***|***|***|***|***|***|
// プレイヤー倉庫言い換え
//*|***|***|***|***|***|***|***|***|***|***|***|
using WarehousePlayer = WarehouseData.PlayerData.WarehousePlayer;

//*|***|***|***|***|***|***|***|***|***|***|***|
// 番号データ共通
//*|***|***|***|***|***|***|***|***|***|***|***|
using WarehouseObject = WarehouseData.WarehouseObject;
using CommonImageNum = WarehouseData.WarehouseStaticData.Object2D_Numbers_Common;
using AppImageNum = WarehouseData.WarehouseStaticData.Object2D_Numbers_App;
//*|***|***|***|***|***|***|***|***|***|***|***|
// 画像データ言い換え
//*|***|***|***|***|***|***|***|***|***|***|***|
using TexImageData = GameDataPublic.TexImageData;
using RenderImageData = GameDataPublic.RenderImageData;
using PartsData = GameDataPublic.PartsData;
//*|***|***|***|***|***|***|***|***|***|***|***|
// パーツ言い換え
//*|***|***|***|***|***|***|***|***|***|***|***|
using PartsID = PlayStaticData.PartsID;

//*|***|***|***|***|***|***|***|***|***|***|***|
// パーツロケット
//*|***|***|***|***|***|***|***|***|***|***|***|
public class PlayRocketParts : MonoBehaviour
{
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // パーツ情報
    //*|***|***|***|***|***|***|***|***|***|***|***|
    [SerializeField]
    PartsID m_partsId;
    bool m_getFlag;
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // プレイヤー共通ディレクター
    //*|***|***|***|***|***|***|***|***|***|***|***|
    PlayerDirectorIndex m_playerIndex;
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 画像
    //*|***|***|***|***|***|***|***|***|***|***|***|
    GameObjectSprite m_partsSprite;
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 当たり判定
    //*|***|***|***|***|***|***|***|***|***|***|***|
    Rigidbody2D m_rigidBody2D;
    BoxCollider2D m_box2D;
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // これが出来たときに
    //*|***|***|***|***|***|***|***|***|***|***|***|
    void Awake()
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // プレイヤー共通ディレクター
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_playerIndex = PlayerDirectorIndex.GetInstance();
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // Objectの倉庫
        //*|***|***|***|***|***|***|***|***|***|***|***|
        WarehouseObject warehouseObject = WarehouseObject.GetInstance();
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 画像
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_partsSprite = gameObject.GetComponent<GameObjectSprite>();
        if (m_partsSprite == null)
        {
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 作成
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_partsSprite = gameObject.AddComponent<GameObjectSprite>();
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 貼り付け
            //*|***|***|***|***|***|***|***|***|***|***|***|
            TexImageData tex = new TexImageData();
            tex.Reset();
            tex.image = warehouseObject.GetTexture2DApp(AppImageNum.ROCKETPARTS);
            tex.rextParsent = MyCalculator.RectSizeReverse_Y(0, 3, 1);
            tex.size = new Vector2(3, 3);
            tex.pibot = new Vector2(0.5f, 0.5f);
            m_partsSprite.SetImageUpdate(tex);
        }

        //*|***|***|***|***|***|***|***|***|***|***|***|
        // パーツのレイヤーに変更
        //*|***|***|***|***|***|***|***|***|***|***|***|
        gameObject.layer = 14;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 当たり判定
        //*|***|***|***|***|***|***|***|***|***|***|***|
        CreateCollision();
        m_getFlag = false;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 当たり設定
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_rigidBody2D.isKinematic = true;
        SetStopHit();
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 更新時
    //*|***|***|***|***|***|***|***|***|***|***|***|
    void Update()
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // IDのイメージにする
        //*|***|***|***|***|***|***|***|***|***|***|***|
        Rect rect;
        rect = MyCalculator.RectSizeReverse_Y((int)m_partsId, 3, 1);
        m_partsSprite.SetRect(rect);
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 当たり設定移動
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public void SetPointArea(Vector3 point)
    {
        m_partsSprite.SetPosition(point);
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 種類設定
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public void SetPartsID(PartsID partsId)
    {
        m_partsId = partsId;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 当たり判定停止
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public void SetStopHit()
    {
        m_box2D.enabled = false;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 当たり判定再生
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public void SetPlayHit()
    {
        m_box2D.enabled = true;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 当たり判定作成
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public void CreateCollision()
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 当たり判定体
        //*|***|***|***|***|***|***|***|***|***|***|***|
        if (this.gameObject.GetComponent<Rigidbody2D>())
        {
            m_rigidBody2D = this.gameObject.GetComponent<Rigidbody2D>();
        }
        else
        {
            m_rigidBody2D = this.gameObject.AddComponent<Rigidbody2D>();
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 当たり判定
        //*|***|***|***|***|***|***|***|***|***|***|***|
        if (this.gameObject.GetComponent<BoxCollider2D>())
        {
            m_box2D = this.gameObject.GetComponent<BoxCollider2D>();
        }
        else
        {
            m_box2D = this.gameObject.AddComponent<BoxCollider2D>();
        }
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // GameObjectSprite情報獲得
    // 取扱注意
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public GameObjectSprite GetSpriteData()
    {
        return m_partsSprite;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 当たり判定を測定
    //*|***|***|***|***|***|***|***|***|***|***|***|
    void OnTriggerEnter2D(Collider2D other)
    {
        if (!m_getFlag)
        {
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // プレイヤーの一部に当たったか？
            //*|***|***|***|***|***|***|***|***|***|***|***|
            if (WarehousePlayer.BoolTagIsPlayer(other.gameObject.tag))
            {
                m_getFlag = true;
                m_playerIndex.GetParts(m_partsId);
                Destroy(this.gameObject);
            }
        }

    }

}
