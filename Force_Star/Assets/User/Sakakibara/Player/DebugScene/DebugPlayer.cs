using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//*|***|***|***|***|***|***|***|***|***|***|***|
// プレイヤーナンバー言い換え
//*|***|***|***|***|***|***|***|***|***|***|***|
using WarehousePlayer = WarehouseData.PlayerData.WarehousePlayer;
using PlayerDataNum = WarehouseData.PlayerData.WarehousePlayer.PlayerData_Number;
using TexImageData = GameDataPublic.TexImageData;
using RenderImageData = GameDataPublic.RenderImageData;

public class DebugPlayer : MonoBehaviour
{

    [SerializeField]
    private Vector3 vector;
    [SerializeField]
    private float speed;
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // プレイヤーの実際のデータ
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // プレイヤー
    //*|***|***|***|***|***|***|***|***|***|***|***|
    GameObject m_playerCenter;
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // プレイヤーの球
    //*|***|***|***|***|***|***|***|***|***|***|***|
    [SerializeField]
    PlayerDataNum m_playerBoalNum;
    [SerializeField]
    TexImageData m_playerBoalTex;
    [SerializeField]
    RenderImageData m_playerBoalRender;
    [SerializeField]
    Vector3 m_playerBoalPosition;

    GameObjectSprite m_playerBoalImage;
    GameObject m_playerBoalObject;
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // プレイヤーの体上
    //*|***|***|***|***|***|***|***|***|***|***|***|
    [SerializeField]
    PlayerDataNum m_bodyTopNum;
    [SerializeField]
    TexImageData m_bodyTopTex;
    [SerializeField]
    RenderImageData m_bodyTopRender;
    [SerializeField]
    Vector3 m_bodyTopPosition;

    GameObjectSprite m_bodyTopImage;
    GameObject m_bodyTopObject;
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // プレイヤーの体下
    //*|***|***|***|***|***|***|***|***|***|***|***|
    [SerializeField]
    PlayerDataNum m_bodyBottomNum;
    [SerializeField]
    TexImageData m_bodyBottomTex;
    [SerializeField]
    RenderImageData m_bodyBottomRender;
    [SerializeField]
    Vector3 m_bodyBottomPosition;

    GameObjectSprite m_bodyBottomImage;
    GameObject m_bodyBottomObject;



    //*|***|***|***|***|***|***|***|***|***|***|***|
    // これが出来たときに
    //*|***|***|***|***|***|***|***|***|***|***|***|
    void Awake()
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // プレイヤー
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_playerCenter = new GameObject("Player");

        //*|***|***|***|***|***|***|***|***|***|***|***|
        // プレイヤーの球
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_playerBoalNum = PlayerDataNum.PLAYERBOAL;
        m_playerBoalTex = new TexImageData();
        m_playerBoalRender = new RenderImageData();
        m_playerBoalPosition = new Vector3();

        m_playerBoalObject = new GameObject(m_playerBoalNum.ToString());
        m_playerBoalImage = m_playerBoalObject.AddComponent<GameObjectSprite>();
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // プレイヤーの体上
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_bodyTopNum = PlayerDataNum.BODYTOP;
        m_bodyTopTex = new TexImageData();
        m_bodyTopRender = new RenderImageData();
        m_bodyTopPosition = new Vector3();

        m_bodyTopObject = new GameObject(m_bodyTopNum.ToString());
        m_bodyTopImage = m_bodyTopObject.AddComponent<GameObjectSprite>();
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // プレイヤーの体下
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_bodyBottomNum = PlayerDataNum.BODYBOTTOM;
        m_bodyBottomTex = new TexImageData();
        m_bodyBottomRender = new RenderImageData();
        m_bodyBottomPosition = new Vector3();

        m_bodyBottomObject = new GameObject(m_bodyBottomNum.ToString());
        m_bodyBottomImage = m_bodyBottomObject.AddComponent<GameObjectSprite>();

        //*|***|***|***|***|***|***|***|***|***|***|***|
        // プレイヤー親子
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_playerBoalObject.transform.parent = m_playerCenter.transform;
        m_bodyTopObject.transform.parent = m_playerCenter.transform;
        m_bodyBottomObject.transform.parent = m_playerCenter.transform;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 開始時
    //*|***|***|***|***|***|***|***|***|***|***|***|
    void Start()
    {
        ReadTexStart();
        m_playerBoalObject.AddComponent<BoxCollider2D>();
        m_bodyTopObject.AddComponent<BoxCollider2D>();
        m_bodyBottomObject.AddComponent<BoxCollider2D>();
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // アップデート
    //*|***|***|***|***|***|***|***|***|***|***|***|
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.J))
        {
            ReadTex();
        }
        ReadTex();
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 開始時
    //*|***|***|***|***|***|***|***|***|***|***|***|
    void ReadTexStart()
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // プレイヤーの球
        //*|***|***|***|***|***|***|***|***|***|***|***|
        {
            Vector3 point = Vector3.zero;
            float pointX = 0;
            float pointY = 0;
            int depth = 0;
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // イメージを作成
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_playerBoalTex.image = WarehousePlayer.GetInstance().GetTexture2D(m_playerBoalNum);
            m_playerBoalTex.rextParsent = MyCalculator.RectSize(0, 1, 1, 1, 1);
            m_playerBoalTex.size = Vector2.one;
            m_playerBoalImage.SetImageUpdate(m_playerBoalTex);
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 場所を作成
            //*|***|***|***|***|***|***|***|***|***|***|***|
            pointX = 0;
            pointY = 0;
            point.x = pointX;
            point.y = pointY;
            m_playerBoalPosition = point;
            m_playerBoalImage.SetPositionLocal(m_playerBoalPosition);
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 描画を作成
            //*|***|***|***|***|***|***|***|***|***|***|***|
            depth = 0;
            m_playerBoalRender.depth = depth;
            m_playerBoalImage.SetRenderUpdate(m_playerBoalRender);
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // プレイヤーの体上
        //*|***|***|***|***|***|***|***|***|***|***|***|
        {
            Vector3 point = Vector3.zero;
            float pointX = 0;
            float pointY = 0;
            int depth = 0;
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // イメージを作成
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_bodyTopTex.image = WarehousePlayer.GetInstance().GetTexture2D(m_bodyTopNum);
            m_bodyTopTex.rextParsent = MyCalculator.RectSize(0, 1, 1, 1, 1);
            m_bodyTopTex.size = Vector2.one;
            m_bodyTopImage.SetImageUpdate(m_bodyTopTex);
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 場所を作成
            //*|***|***|***|***|***|***|***|***|***|***|***|
            pointX = 0;
            pointY = 0;
            point.x = pointX;
            point.y = pointY;
            m_bodyTopPosition = point;
            m_bodyTopImage.SetPositionLocal(m_bodyTopPosition);
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 描画を作成
            //*|***|***|***|***|***|***|***|***|***|***|***|
            depth = 0;
            m_bodyTopRender.depth = depth;
            m_bodyTopImage.SetRenderUpdate(m_bodyTopRender);
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // プレイヤーの体下
        //*|***|***|***|***|***|***|***|***|***|***|***|
        {
            Vector3 point = Vector3.zero;
            float pointX = 0;
            float pointY = 0;
            int depth = 0;
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // イメージを作成
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_bodyBottomTex.image = WarehousePlayer.GetInstance().GetTexture2D(m_bodyBottomNum);
            m_bodyBottomTex.rextParsent = MyCalculator.RectSize(0, 1, 1, 1, 1);
            m_bodyBottomTex.size = Vector2.one;
            m_bodyBottomImage.SetImageUpdate(m_bodyBottomTex);
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 場所を作成
            //*|***|***|***|***|***|***|***|***|***|***|***|
            pointX = 0;
            pointY = 0;
            point.x = pointX;
            point.y = pointY;
            m_bodyBottomPosition = point;
            m_bodyBottomImage.SetPositionLocal(m_bodyBottomPosition);
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 描画を作成
            //*|***|***|***|***|***|***|***|***|***|***|***|
            depth = 0;
            m_bodyBottomRender.depth = depth;
            m_bodyBottomImage.SetRenderUpdate(m_bodyBottomRender);
        }
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // アップデート
    //*|***|***|***|***|***|***|***|***|***|***|***|
    void ReadTex()
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // プレイヤーの球
        //*|***|***|***|***|***|***|***|***|***|***|***|
        {
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // イメージを作成
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_playerBoalImage.SetImageUpdate(m_playerBoalTex);
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 場所を作成
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_playerBoalImage.SetPositionLocal(m_playerBoalPosition);
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 描画を作成
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_playerBoalImage.SetRenderUpdate(m_playerBoalRender);
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // プレイヤーの体上
        //*|***|***|***|***|***|***|***|***|***|***|***|
        {
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // イメージを作成
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_bodyTopImage.SetImageUpdate(m_bodyTopTex);
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 場所を作成
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_bodyTopImage.SetPositionLocal(m_bodyTopPosition);
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 描画を作成
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_bodyTopImage.SetRenderUpdate(m_bodyTopRender);
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // プレイヤーの体下
        //*|***|***|***|***|***|***|***|***|***|***|***|
        {
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // イメージを作成
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_bodyBottomImage.SetImageUpdate(m_bodyBottomTex);
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 場所を作成
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_bodyBottomImage.SetPositionLocal(m_bodyBottomPosition);
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 描画を作成
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_bodyBottomImage.SetRenderUpdate(m_bodyBottomRender);
        }
    }
}
