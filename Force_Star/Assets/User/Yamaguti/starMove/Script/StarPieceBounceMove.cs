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

public class StarPieceBounceMove : MonoBehaviour
{
    
    // 速度
    public float m_speed;
    public float m_attenuation;
    private Vector3 m_velocity;

    bool flag = false;      // 当たった時のフラグ

    //*|***|***|***|***|***|***|***|***|***|***|***|
    // プレイヤー共通ディレクター
    //*|***|***|***|***|***|***|***|***|***|***|***|
    PlayerDirectorIndex m_playerIndex;
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 画像
    //*|***|***|***|***|***|***|***|***|***|***|***|
    GameObjectSprite m_starSprite;
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 無敵時間
    //*|***|***|***|***|***|***|***|***|***|***|***|
    [SerializeField]
    private int count = 20;
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 動き用
    //*|***|***|***|***|***|***|***|***|***|***|***|
    private float vecX;
    private float vecY;
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 当たり判定
    //*|***|***|***|***|***|***|***|***|***|***|***|
    Rigidbody2D m_rigidBody2D;
    BoxCollider2D m_box2D;
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // ４辺
    //*|***|***|***|***|***|***|***|***|***|***|***|
    GameObject m_up;
    ChildStar m_upChildStar;

    GameObject m_down;
    ChildStar m_downChildStar;

    GameObject m_left;
    ChildStar m_leftChildStar;

    GameObject m_right;
    ChildStar m_rightChildStar;


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
        m_starSprite = gameObject.GetComponent<GameObjectSprite>();
        if (m_starSprite == null)
        {
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 作成
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_starSprite = gameObject.AddComponent<GameObjectSprite>();
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 貼り付け
            //*|***|***|***|***|***|***|***|***|***|***|***|
            TexImageData tex = new TexImageData();
            tex.Reset();
            tex.image = warehouseObject.GetTexture2DApp(AppImageNum.STARIMAGE);
            tex.rextParsent = MyCalculator.RectSizeReverse_Y(0, 1, 1);
            tex.size = new Vector2(1, 1);
            tex.pibot = new Vector2(0.5f, 0.5f);
            m_starSprite.SetImageUpdate(tex);
        }

        //*|***|***|***|***|***|***|***|***|***|***|***|
        // ４辺を用意する
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_up = new GameObject();
        m_upChildStar = m_up.AddComponent<ChildStar>();
        m_up.transform.parent = gameObject.transform;

        m_down = new GameObject();
        m_downChildStar = m_up.AddComponent<ChildStar>();
        m_down.transform.parent = gameObject.transform;

        m_left = new GameObject();
        m_leftChildStar = m_up.AddComponent<ChildStar>();
        m_left.transform.parent = gameObject.transform;

        m_right = new GameObject();
        m_rightChildStar = m_up.AddComponent<ChildStar>();
        m_right.transform.parent = gameObject.transform;



        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 星のレイヤーに変更
        //*|***|***|***|***|***|***|***|***|***|***|***|
        gameObject.layer = 12;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 当たり判定
        //*|***|***|***|***|***|***|***|***|***|***|***|
        CreateCollision();
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 当たり設定
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_rigidBody2D.isKinematic = true;
        SetStopHit();
    }
    
    void Start()
    {
        Vector2 pos, size;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 当たり判定上
        //*|***|***|***|***|***|***|***|***|***|***|***|
        pos = new Vector2(0.0f, 0.5f);
        size = new Vector2(0.5f, 0.5f);
        m_upChildStar.SetPointSize(pos, size);
        m_upChildStar.SetPointSize(pos, size);
    }
    
    void Update()
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 眠る
        //*|***|***|***|***|***|***|***|***|***|***|***|
        if (!this.isActiveAndEnabled)
        {
            return;
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 無敵時間
        //*|***|***|***|***|***|***|***|***|***|***|***|
        if (count <= 0)
        {
            SetPlayHit();
        }
        else
        {
            count--;
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // ぽ四ぽ四移動
        //*|***|***|***|***|***|***|***|***|***|***|***|
        bool hitUp = m_upChildStar.GetHitFlag();
        bool hitDown = m_downChildStar.GetHitFlag();
        bool hitLeft = m_leftChildStar.GetHitFlag();
        bool hitRight = m_rightChildStar.GetHitFlag();


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
    // 速度
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public void SetVec(float x, float y)
    {
        vecX = x;
        vecY = y;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 移動
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public void SetPosition(Vector2 pos)
    {
        Vector3 posV3 = ChangeData.GetVector3(pos);
        gameObject.transform.position = posV3;
    }
    public void SetPosition(Vector3 pos)
    {
        Vector3 posV3 = pos;
        gameObject.transform.position = posV3;
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



    void OnTriggerEnter2D(Collider2D other)
    {
        if (!flag)
        {
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // プレイヤーの一部に当たったか？
            //*|***|***|***|***|***|***|***|***|***|***|***|
            if (WarehousePlayer.BoolTagIsPlayer(other.gameObject.tag))
            {
                flag = true;
                PlayerDirectorIndex.GetInstance().GetStar(1);
                Destroy(this.gameObject);
            }
        }

    }
}

