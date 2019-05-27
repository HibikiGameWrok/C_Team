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

    private int maxStar = 1;
    private float chengSize = 0.0f;

    // 時間・点滅情報----------
    private float grv = 0.05f;                  // 重力
    float starSize;
    //-------------------------

    //*|***|***|***|***|***|***|***|***|***|***|***|
    // プレイシーン共通ディレクター
    //*|***|***|***|***|***|***|***|***|***|***|***|
    PlaySceneDirectorIndex m_playIndex;
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
    private int m_count = 60;
    bool m_countFlag;
    bool m_ariveFlag;
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
    // 収集当たり判定
    //*|***|***|***|***|***|***|***|***|***|***|***|
    GameObject m_body;
    ChildStarCirlce m_bodyChildStar;
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

    bool objectFlag = false;

    bool starCirHitCheckFlag;

    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 当たりフラグ
    //*|***|***|***|***|***|***|***|***|***|***|***|
    bool m_hitUp;
    bool m_hitDown;
    bool m_hitLeft;
    bool m_hitRight;
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // ジャンプフラグ
    //*|***|***|***|***|***|***|***|***|***|***|***|
    bool m_jumpFlag;
    bool m_wallFlag;
    float m_fallPower;
    float m_movePowerP;
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 移動力
    //*|***|***|***|***|***|***|***|***|***|***|***|
    Vector2 m_movePower = Vector2.one;
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 時間フラグ
    //*|***|***|***|***|***|***|***|***|***|***|***|
    float m_ariveTimer = 0;
    float m_ariveTimerLevel = 0;
    float m_ariveTimerMax = 0;

    //*|***|***|***|***|***|***|***|***|***|***|***|
    // これが出来たときに
    //*|***|***|***|***|***|***|***|***|***|***|***|
    void Awake()
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // プレイシーン共通ディレクター
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_playIndex = PlaySceneDirectorIndex.GetInstance();
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
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 大きさ
            //*|***|***|***|***|***|***|***|***|***|***|***|

            //maxStar = XORShiftRand.GetSeedMaxMinRand(1, 3);
            //starSize = (float)maxStar + (float)XORShiftRand.GetSeedMaxMinRand(0, 4);
            //SizeMaxStar(starSize);

            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 大きさ設定
            //*|***|***|***|***|***|***|***|***|***|***|***|
            SizeMaxStar();

            tex.size = new Vector2(starSize, starSize);
            tex.pibot = new Vector2(0.5f, 0.5f);
            m_starSprite.SetImageUpdate(tex);
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 収集当たり判定
        //*|***|***|***|***|***|***|***|***|***|***|***|
        if (m_body == null)
        {
            m_body = new GameObject("Body");
            m_bodyChildStar = m_body.AddComponent<ChildStarCirlce>();
            m_body.transform.parent = gameObject.transform;
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

        m_hitUp = false;
        m_hitDown = false;
        m_hitLeft = false;
        m_hitRight = false;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // ジャンプフラグ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_jumpFlag = false;
        m_wallFlag = false;
        m_fallPower = 0.0f;
        m_movePowerP = 0.0f;
        m_ariveFlag = true;
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

        m_countFlag = false;

        starCirHitCheckFlag = false;
    }

    void Start()
    {

        //m_downChildStar = m_down.AddComponent<ChildStar>();
        //m_leftChildStar = m_left.AddComponent<ChildStar>();
        //m_rightChildStar = m_right.AddComponent<ChildStar>();
        //Vector2 pos, size;

        Vector2 posUp, sizeUp;
        Vector2 posDown, sizeDown;
        Vector2 posLeft, sizeLeft;
        Vector2 posRight, sizeRight;

        Vector2 posUpX, sizeUpX;
        Vector2 posDownX, sizeDownX;
        Vector2 posLeftX, sizeLeftX;
        Vector2 posRightX, sizeRightX;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 当たり判定サイズ変更前
        //*|***|***|***|***|***|***|***|***|***|***|***|
        posUp = new Vector2(0.0f, 0.35f);
        sizeUp = new Vector2(0.5f, 0.5f);

        posDown = new Vector2(0.0f, -0.35f);
        sizeDown = new Vector2(0.5f, 0.5f);

        posLeft = new Vector2(-0.35f, 0.0f);
        sizeLeft = new Vector2(0.5f, 0.5f);

        posRight = new Vector2(0.35f, 0.0f);
        sizeRight = new Vector2(0.5f, 0.5f);
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 当たり判定サイズ変更後
        //*|***|***|***|***|***|***|***|***|***|***|***|
        posUpX = MyCalculator.EachTimes(posUp, new Vector2(starSize, starSize));
        sizeUpX = MyCalculator.EachTimes(sizeUp, new Vector2(starSize, 1));

        posDownX = MyCalculator.EachTimes(posDown, new Vector2(starSize, starSize));
        sizeDownX = MyCalculator.EachTimes(sizeDown, new Vector2(starSize, 1));

        posLeftX = MyCalculator.EachTimes(posLeft, new Vector2(starSize, starSize));
        sizeLeftX = MyCalculator.EachTimes(sizeLeft, new Vector2(1, starSize));

        posRightX = MyCalculator.EachTimes(posRight, new Vector2(starSize, starSize));
        sizeRightX = MyCalculator.EachTimes(sizeRight, new Vector2(1, starSize));
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 当たり判定適応
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_upChildStar.SetPointSize(posUpX, sizeUpX);

        m_downChildStar.SetPointSize(posDownX, sizeDownX);

        m_leftChildStar.SetPointSize(posLeftX, sizeLeftX);
 
        m_rightChildStar.SetPointSize(posRightX, sizeRightX);

        m_bodyChildStar.SetPointSize(Vector2.zero, new Vector2(starSize, starSize));
    }

    void Update()
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 眠る
        //*|***|***|***|***|***|***|***|***|***|***|***|
        if (!m_ariveFlag)
        {
            DeathBoom();
            return;
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // ゲーム終了している？
        //*|***|***|***|***|***|***|***|***|***|***|***|
        bool clearAnime = m_playIndex.GetClearAnimation();
        bool deathAnime = m_playIndex.GetGameOverAnimation();
        if (clearAnime || deathAnime)
        {
            DeathFlag();
            return;
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 眠る
        //*|***|***|***|***|***|***|***|***|***|***|***|
        if (!this.isActiveAndEnabled)
        {
            SetStopHit();
            return;
        }
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 動きの一連の処理
    //*|***|***|***|***|***|***|***|***|***|***|***|
    void WorkUpdate()
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // サークルが当たったら？
        //*|***|***|***|***|***|***|***|***|***|***|***|
        if (m_bodyChildStar.GetHitFlag() && !m_countFlag)
        {
            starCirHitCheckFlag = true;
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 無敵時間
        //*|***|***|***|***|***|***|***|***|***|***|***|
        if (m_count <= 0)
        {
            SetPlayHit();
            m_box2D.size = new Vector2(starSize, starSize);
            m_countFlag = false;
        }
        else
        {
            if (m_countFlag)
                m_count--;
        }
        if (!objectFlag)
        {
            objectFlag = true;
        }

        if (!starCirHitCheckFlag)
        {
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 移動力
            //*|***|***|***|***|***|***|***|***|***|***|***|
            Vector2 movePower = Vector2.zero;
            Vector2 movePowerX = Vector2.one;

            //*|***|***|***|***|***|***|***|***|***|***|***|
            // ぽ四ぽ四移動
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_hitUp = m_upChildStar.GetHitFlag();
            m_hitDown = m_downChildStar.GetHitFlag();
            m_hitLeft = m_leftChildStar.GetHitFlag();
            m_hitRight = m_rightChildStar.GetHitFlag();
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 壁の中にいる
            //*|***|***|***|***|***|***|***|***|***|***|***|
            if (m_hitUp && m_hitDown && m_hitLeft && m_hitRight && !m_countFlag)
            {
                //*|***|***|***|***|***|***|***|***|***|***|***|
                // 自爆！
                //*|***|***|***|***|***|***|***|***|***|***|***|
                DeathFlag();
                return;
            }
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 上下
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 頭当たった
            //*|***|***|***|***|***|***|***|***|***|***|***|
            if (m_hitUp)
            {
                //*|***|***|***|***|***|***|***|***|***|***|***|
                // ジャンプ力を０より小さくする
                //*|***|***|***|***|***|***|***|***|***|***|***|
                if (m_movePower.y > 0.0f)
                {
                    m_movePower.y = 0.0f;
                }
            }
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 脚が当たった
            //*|***|***|***|***|***|***|***|***|***|***|***|
            if (!m_hitDown)
            {
                m_jumpFlag = false;
            }
            if (m_hitDown && !m_jumpFlag)
            {
                m_jumpFlag = true;
                //*|***|***|***|***|***|***|***|***|***|***|***|
                // ジャンプ力調整
                //*|***|***|***|***|***|***|***|***|***|***|***|
                if (m_movePower.y < m_fallPower) 
                {
                    m_movePower.y = m_fallPower;
                }
            }
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 挟まれた
            //*|***|***|***|***|***|***|***|***|***|***|***|
            if (m_hitUp && m_hitDown)
            {
                m_jumpFlag = false;
            }
            if (m_hitUp && m_hitDown && !m_countFlag)
            {
                m_movePower.y = 0.0f;
            }
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 左右
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 左当たった
            //*|***|***|***|***|***|***|***|***|***|***|***|
            bool hitWall = false;
            if (m_hitLeft)
            {
                hitWall = true;
                if (!m_wallFlag)
                {
                    m_movePower.x = -m_movePower.x;
                }
            }
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 右が当たった
            //*|***|***|***|***|***|***|***|***|***|***|***|
            if (m_hitRight)
            {
                hitWall = true;
                if (!m_wallFlag)
                {
                    m_movePower.x = -m_movePower.x;
                }
            }
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 壁に当たった。
            //*|***|***|***|***|***|***|***|***|***|***|***|
            if (hitWall)
            {
                m_wallFlag = true;
            }
            else
            {
                m_wallFlag = false;
            }
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 挟まれた
            //*|***|***|***|***|***|***|***|***|***|***|***|
            if (m_hitLeft && m_hitRight)
            {
                movePowerX.x = 0.0f;
                m_wallFlag = false;
            }
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 移動力設定
            //*|***|***|***|***|***|***|***|***|***|***|***|
            movePower = MyCalculator.EachTimes(m_movePower, movePowerX);
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 空中の間だけ重力
            //*|***|***|***|***|***|***|***|***|***|***|***|
            if (!m_hitDown)
            {
                //*|***|***|***|***|***|***|***|***|***|***|***|
                // 重力
                //*|***|***|***|***|***|***|***|***|***|***|***|
                if (m_movePower.y > -0.5f)
                {
                    m_movePower.y = m_movePower.y - grv;

                    if(m_movePower.y < -0.5f)
                    {
                        m_movePower.y = -0.5f;
                    }
                    m_fallPower = Mathf.Abs(m_movePower.x * 3);
                }
                else
                {
                    m_fallPower += grv;
                }
            }
            float p = 0.99f;
            m_movePower = MyCalculator.EachTimes(m_movePower, new Vector2(p, p));
            if (m_movePower.magnitude * 100 < 1.0f)
            {
                m_movePower = Vector2.zero;
            }

            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 跳ねる移動
            //*|***|***|***|***|***|***|***|***|***|***|***|
            Vector3 thisPosition = transform.position;
            Vector3 movedPosition = thisPosition + ChangeData.GetVector3(movePower);
            transform.position = movedPosition;

            
            
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 点滅   
            //*|***|***|***|***|***|***|***|***|***|***|***|
            FlashStar();
        }
        else if (starCirHitCheckFlag)
        {
            m_starSprite.SetAlpha(1);
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
        m_countFlag = true;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 速度
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_movePower.x = vecX;
        m_movePower.y = vecY;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 落下速度
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_jumpFlag = false;
        m_fallPower = Mathf.Abs(vecY);
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
    // プレイヤーに駆け付ける速さ
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public void SetTime(float timeMax, float timeLevel)
    {
        m_ariveTimer = 0;
        m_ariveTimerMax = timeMax;
        m_ariveTimerLevel = timeLevel;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // プレイヤーに駆け付ける速さ
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public void SetTimeCount(int count)
    {
        m_count = count;
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
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 無敵終了後
        //*|***|***|***|***|***|***|***|***|***|***|***|
        if (!m_countFlag)
        {
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 時間は進む
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_ariveTimer += 1.0f;
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 透明化処理
            //*|***|***|***|***|***|***|***|***|***|***|***|
            if (m_ariveTimer > m_ariveTimerLevel)
            {
                float trueTime = m_ariveTimer - m_ariveTimerLevel;
                float level = MyCalculator.WaveSin(m_ariveTimer, 0.1f, 0.9f);
                m_starSprite.SetAlpha(level);
            }
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // デストロイ処理
            //*|***|***|***|***|***|***|***|***|***|***|***|
            if (m_ariveTimer > m_ariveTimerMax)
            {
                DeathFlag();
            }
        }
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 定期更新データ
    //*|***|***|***|***|***|***|***|***|***|***|***|
    void FixedUpdate()
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 動きの一連の処理
        //*|***|***|***|***|***|***|***|***|***|***|***|
        WorkUpdate();
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
            if (!m_countFlag)
            {
                //*|***|***|***|***|***|***|***|***|***|***|***|
                // プレイヤーの一部に当たったか？
                //*|***|***|***|***|***|***|***|***|***|***|***|
                if (WarehousePlayer.BoolTagIsPlayer(other.gameObject.tag))
                {
                    flag = true;
                    PlayerDirectorIndex.GetInstance().GetStar(maxStar);
                    CatchStar();
                }
            }
        }
        if (other.gameObject.tag == "B")
        {
            DeathFlag();
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
            maxStar = 3;
            grv = 0.001f;
        }
        chengSize = size / 6;
    }
    private void SizeMaxStar()
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 追加の大きさ設定
        //*|***|***|***|***|***|***|***|***|***|***|***|
        int maxSeed = 1000;
        int randSeed = XORShiftRand.GetSeedDivRand(maxSeed);
        float addSizeMax = 1.5f;
        float addSize = MyCalculator.Division((float)randSeed, maxSeed - 1) * addSizeMax;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 大きさ設定
        //*|***|***|***|***|***|***|***|***|***|***|***|
        starSize = 1.0f + addSize;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 追加の取得料設定
        //*|***|***|***|***|***|***|***|***|***|***|***|
        int addMaxGetStar = 2;
        int addMaxStar = Mathf.RoundToInt(MyCalculator.Division((float)randSeed, maxSeed - 1) * addMaxGetStar);
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 取得料設定
        //*|***|***|***|***|***|***|***|***|***|***|***|
        maxStar = 1 + addMaxStar;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // その他設定
        //*|***|***|***|***|***|***|***|***|***|***|***|
        grv = 0.05f;
        chengSize = MyCalculator.Division(starSize, 6.0f);
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 獲得！
    //*|***|***|***|***|***|***|***|***|***|***|***|
    void CatchStar()
    {
        flag = true;
        PlayerDirectorIndex.GetInstance().GetStar(maxStar);
        DeathFlag();
        m_starSprite.SetAlpha(0);
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 自爆！
    //*|***|***|***|***|***|***|***|***|***|***|***|
    void DeathFlag()
    {
        flag = true;
        m_ariveFlag = false;
        m_starSprite.SetAlpha(0);
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 自爆！
    //*|***|***|***|***|***|***|***|***|***|***|***|
    void DeathBoom()
    {
        Destroy(transform.gameObject);
    }
}

//if (hitUp && hitUpCheck)
//{
//    jumpForce = 0.0f;
//    hitUpCheck = false;
//}
//else if (!hitUp && !hitUpCheck)
//{
//    hitUpCheck = true;
//}
//if (hitDown && hitDownCheck)
//{
//    jumpForce = startJF;
//    hitDownCheck = false;
//}
//else if (!hitDown && !hitDownCheck)
//{
//    AddCountJump();
//    hitDownCheck = true;
//}
//if (hitLeft && hitLeftCheck)
//{
//    vecX = -vecX;
//    hitLeftCheck = false;
//}
//else if (!hitLeft && !hitLeftCheck)
//{
//    AddCountJump();
//    hitLeftCheck = true;
//}
//if (hitRight && hitRightCheck)
//{
//    vecX = -vecX;
//    hitRightCheck = false;
//}
//else if (!hitRight && !hitRightCheck)
//{
//    AddCountJump();
//    hitRightCheck = true;
//}