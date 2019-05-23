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

    public float jumpForce = 0.2f;            // ジャンプ力   
    public float startJF;                     // 初期のジャンプ力保存用
    private int maxStar = 1;
    private float chengSize = 0.0f;

    // 時間・点滅情報----------
    private float timeElapsed = 0.0f;     // タイムカウント用
    private int flashInterval = 5;      // 点滅間隔
    private int flashTime = 3;          // 点滅時間
    private int startFlashJumpCount = 3;// 点滅を開始する回数
    int time = 0;                  // 点滅消滅の時間計測用変数
    int JumpCount;                 // 現在のジャンプ回数
    private float grv = 0.005f;                  // 重力
    private float jumpAddF;
    float size;
    //-------------------------

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
    private int count = 60;
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
    // ４辺
    //*|***|***|***|***|***|***|***|***|***|***|***|
    GameObject m_up = null;
    ChildStar m_upChildStar;

    GameObject m_down = null;
    ChildStar m_downChildStar;

    GameObject m_left = null;
    ChildStar m_leftChildStar;

    GameObject m_right = null;
    ChildStar m_rightChildStar;

    bool countFlag;
    bool objectFlag = false;

    bool starCirHitCheckFlag;
    bool hitUpCheck, hitDownCheck, hitLeftCheck, hitRightCheck;


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
            size = Random.Range(1.0f, 2.0f);
            SizeMaxStar(size);
            tex.size = new Vector2(size, size);
            tex.pibot = new Vector2(0.5f, 0.5f);
            m_starSprite.SetImageUpdate(tex);
        }

        //*|***|***|***|***|***|***|***|***|***|***|***|
        // ４辺を用意する
        //*|***|***|***|***|***|***|***|***|***|***|***|

        if (m_up == null)
        {
            m_up = new GameObject("Up");
            m_upChildStar = m_up.AddComponent<ChildStar>();
            m_up.transform.parent = gameObject.transform;
        }


        //if (m_upChildStar == null)
        //{
        //    m_up = new GameObject("Up");
        //    m_upChildStar = m_up.AddComponent<ChildStar>();
        //    m_up.transform.parent = gameObject.transform;
        //}

        if (m_down == null)
        {
            m_down = new GameObject("Down");
            m_downChildStar = m_down.AddComponent<ChildStar>();
            m_down.transform.parent = gameObject.transform;
        }

        if (m_left == null)
        {
            m_left = new GameObject("Left");
            m_leftChildStar = m_left.AddComponent<ChildStar>();
            m_left.transform.parent = gameObject.transform;
        }

        if (m_right == null)
        {
            m_right = new GameObject("Right");
            m_rightChildStar = m_right.AddComponent<ChildStar>();
            m_right.transform.parent = gameObject.transform;
        }

        //m_up = new GameObject("Up");

        //m_up.transform.parent = gameObject.transform;

        //m_down = new GameObject("Down");

        //m_down.transform.parent = gameObject.transform;

        //m_left = new GameObject("Left");

        //m_left.transform.parent = gameObject.transform;

        //m_right = new GameObject("Right");

        // m_right.transform.parent = gameObject.transform;
        hitUpCheck = true;
        hitDownCheck = true;
        hitLeftCheck = true;
        hitRightCheck = true;



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

        countFlag = false;

        starCirHitCheckFlag = false;
    }

    void Start()
    {

        //m_downChildStar = m_down.AddComponent<ChildStar>();
        //m_leftChildStar = m_left.AddComponent<ChildStar>();
        //m_rightChildStar = m_right.AddComponent<ChildStar>();
        Vector2 pos, size;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 当たり判定上
        //*|***|***|***|***|***|***|***|***|***|***|***|
        pos = new Vector2(0.0f, 0.35f + chengSize);
        size = new Vector2(0.5f + chengSize, 0.2f + chengSize);
        m_upChildStar.SetPointSize(pos, size);
        m_upChildStar.SetPointSize(pos, size);
        pos = new Vector2(0.0f, -0.45f - chengSize);
        size = new Vector2(0.5f + chengSize, 0.6f + chengSize);
        m_downChildStar.SetPointSize(pos, size);
        m_downChildStar.SetPointSize(pos, size);
        pos = new Vector2(-0.35f - chengSize, 0.0f);
        size = new Vector2(0.2f + chengSize, 0.5f + chengSize);
        m_leftChildStar.SetPointSize(pos, size);
        m_leftChildStar.SetPointSize(pos, size);
        pos = new Vector2(0.35f + chengSize, 0.0f);
        size = new Vector2(0.2f + chengSize, 0.5f + chengSize);
        m_rightChildStar.SetPointSize(pos, size);
        m_rightChildStar.SetPointSize(pos, size);
    }

    void Update()
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 眠る
        //*|***|***|***|***|***|***|***|***|***|***|***|
        if (!this.isActiveAndEnabled)
        {
            SetStopHit();
            return;
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 無敵時間
        //*|***|***|***|***|***|***|***|***|***|***|***|
        if (count <= 0)
        {
            SetPlayHit();
            m_box2D.size = new Vector2(size, size);
            countFlag = false;
        }
        else
        {
            if (countFlag)
                count--;
        }
        if (!objectFlag)
        {
            objectFlag = true;
        }

        if (!starCirHitCheckFlag)
        {
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // ぽ四ぽ四移動
            //*|***|***|***|***|***|***|***|***|***|***|***|
            bool hitUp = m_upChildStar.GetHitFlag();
            bool hitDown = m_downChildStar.GetHitFlag();
            bool hitLeft = m_leftChildStar.GetHitFlag();
            bool hitRight = m_rightChildStar.GetHitFlag();

            if (hitUp && hitUpCheck)
            {
                jumpForce = 0.0f;
                hitUpCheck = false;
            }
            else if (!hitUp && !hitUpCheck)
            {
                hitUpCheck = true;
            }
            if (hitDown && hitDownCheck)
            {
                jumpForce = startJF;

                hitDownCheck = false;
            }
            else if (!hitDown && !hitDownCheck)
            {
                AddCountJump();
                hitDownCheck = true;
            }
            if (hitLeft && hitLeftCheck)
            {
                vecX = -vecX;

                hitLeftCheck = false;
            }
            else if (!hitLeft && !hitLeftCheck)
            {
                AddCountJump();
                hitLeftCheck = true;
            }
            if (hitRight && hitRightCheck)
            {
                vecX = -vecX;

                hitRightCheck = false;
            }
            else if (!hitRight && !hitRightCheck)
            {
                AddCountJump();
                hitRightCheck = true;
            }

            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 跳ねる移動
            //*|***|***|***|***|***|***|***|***|***|***|***|
            transform.position = new Vector3(transform.position.x + vecX, transform.position.y + jumpForce, transform.position.z);
            if (jumpForce > -0.2f)
            {
                jumpForce = jumpForce - grv;
            }
            FlashStar();                // 点滅用の関数
        }
        else if (starCirHitCheckFlag)
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
    public void SetVec(float x, float y)
    {
        vecX = x;
        vecY = y;
        countFlag = true;
        jumpForce = vecY;
        if (jumpForce < 0)
        {
            jumpForce -= jumpForce;
        }
        jumpForce += jumpAddF;
        //jumpForce = Random.Range(0.06f,0.3f);
        startJF = jumpForce;
        if (startJF == 0.0f)
        {
            startJF = 0.1f;
        }
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

    // 点滅関数
    public void FlashStar()
    {
        if (startFlashJumpCount <= JumpCount) // ジャンプ回数が規定ジャンプ回数を超えたら点滅スタート
        {

            // Do anything
            float level = Mathf.Abs(Mathf.Sin(Time.time * flashInterval)); // 点滅間隔


            m_starSprite.SetAlpha(level);

            //  this.GetComponent<GameObjectSprite>().GetSpriteRenderer().color= new Color(1f, 1f, 1f, level); // 点滅
            //gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, level);
            timeElapsed += Time.deltaTime;

            if (timeElapsed >= 0.6f)
            {
                time++;
                if (time >= flashTime)
                    Destroy(transform.gameObject);
                timeElapsed = 0.0f;
            }

        }
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // GameObjectSprite情報獲得
    // 取扱注意
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public GameObjectSprite GetSpriteData()
    {
        return m_starSprite;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!flag)
        {
            if (!countFlag)
            {
                //*|***|***|***|***|***|***|***|***|***|***|***|
                // プレイヤーの一部に当たったか？
                //*|***|***|***|***|***|***|***|***|***|***|***|
                if (WarehousePlayer.BoolTagIsPlayer(other.gameObject.tag))
                {
                    flag = true;
                    PlayerDirectorIndex.GetInstance().GetStar(maxStar);
                    Destroy(this.gameObject);
                }

                if (other.gameObject.tag == "R")
                {
                    starCirHitCheckFlag = true;
                }
            }
        }
        if (other.gameObject.tag == "B")
        {
            Destroy(this.gameObject);
        }

    }
    public void SetCirHitCheckFlag()
    {
        starCirHitCheckFlag = true;
    }

    private void SizeMaxStar(float size)
    {
        if (size >= 1.0f && size < 1.5f)
        {
            jumpAddF = 0.0f;
            maxStar = 1;
            grv = 0.001f;
        }
        //else if (size >= 1.5f && size < 2.0f)
        //{
        //    jumpAddF = 0.1f;
        //    maxStar = 5;
        //    grv = 0.005f;
        //}
        else if (size >= 1.5f && size <= 2.0f)
        {
            jumpAddF = 0.0f;
            maxStar = 3;
            grv = 0.001f;
        }
        chengSize = size / 6;
    }
    void AddCountJump()
    {
        if (!countFlag)
        {
            JumpCount++;
        }
    }
}

