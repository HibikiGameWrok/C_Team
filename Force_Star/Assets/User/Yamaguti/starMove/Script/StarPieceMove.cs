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



public class StarPieceMove : MonoBehaviour
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
    // 広がる時間
    //*|***|***|***|***|***|***|***|***|***|***|***|
    [SerializeField]
    private int count = 20;
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 広がり中
    //*|***|***|***|***|***|***|***|***|***|***|***|
    [SerializeField]
    private bool m_diffusion;
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 動き用
    //*|***|***|***|***|***|***|***|***|***|***|***|
    private float vecX;
    private float vecY;
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 動き用
    //*|***|***|***|***|***|***|***|***|***|***|***|
    private float m_speedData;
    private float m_speedAdd;
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
        // 広がり中
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_diffusion = false;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 画像
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_starSprite = gameObject.GetComponent<GameObjectSprite>();
        if(m_starSprite == null)
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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
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
        // 広がり中か？
        //*|***|***|***|***|***|***|***|***|***|***|***|
        if (!m_diffusion)
        {
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 拡散時間
            //*|***|***|***|***|***|***|***|***|***|***|***|
            count--;
            if(count <= 0)
            {
                m_diffusion = true;
                SetPlayHit();
            }
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 拡散中
            //*|***|***|***|***|***|***|***|***|***|***|***|
            this.transform.position = new Vector3(this.transform.position.x + vecX, this.transform.position.y + vecY, this.transform.position.z); // 拡散
        }
        else
        {
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // スピード計算
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_speedData += m_speedAdd;
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // プレイヤーの方向
            //*|***|***|***|***|***|***|***|***|***|***|***|
            Vector3 pointSet = m_playerIndex.GetPlayerPosition();
            Vector3 playerPos = transform.position;
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // へ向かう
            //*|***|***|***|***|***|***|***|***|***|***|***|
            Vector3 playerDif = (pointSet - playerPos);
            Vector3 playerDifUnit = playerDif;
            playerDifUnit.Normalize();
            Vector3 movePower = playerDifUnit * m_speedData;
            transform.position += movePower;


            //m_velocity += (pointSet - transform.position) * m_speed;
            //m_velocity *= m_attenuation;
            //transform.position += m_velocity *= Time.deltaTime;
        }
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
    public void SetVec(float x,float y)
    {
        vecX = x;
        vecY = y;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // プレイヤーに駆け付ける速さ
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public void SetSpeed(float speedData, float speedAdd)
    {
        m_speedData = speedData;
        m_speedAdd = speedAdd;
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
        if(!flag)
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
