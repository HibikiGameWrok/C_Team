using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using WarehouseData;
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

public class DeathExplosion : MonoBehaviour
{
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // プレイヤー倉庫
    //*|***|***|***|***|***|***|***|***|***|***|***|
    private WarehousePlayer m_warehousePlayer;
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // オブジェクト倉庫
    //*|***|***|***|***|***|***|***|***|***|***|***|
    private WarehouseObject m_warehouseObject;

    struct TimeAndVec
    {
        public Vector2 vec;
        public int time;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 星バラマキ
    //*|***|***|***|***|***|***|***|***|***|***|***|
    List<GameObjectSprite> m_starExplosion;
    List<TimeAndVec> m_starExplosionTime;
    private TexImageData m_starTexImageData;
    private RenderImageData m_starRenderImageData;
    ////*|***|***|***|***|***|***|***|***|***|***|***|
    ////  大爆発
    ////*|***|***|***|***|***|***|***|***|***|***|***|
    //GameObjectSprite m_centerExplosion;
    //GameObject m_centerExplosionObject;
    //private TexImageData m_centeTexImageData;
    //private RenderImageData m_centeRenderImageData;
    //private int m_centerAnime;
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 継承用使用データ
    //*|***|***|***|***|***|***|***|***|***|***|***|
    private float m_timeExplosion;
    private float m_timeMax = 90;
    private float m_timeMaxStar = 10;

    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 爆発
    //*|***|***|***|***|***|***|***|***|***|***|***|
    void Awake()
    {
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
        GameObject simple = new GameObject();
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 星バラマキ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_starExplosion = new List<GameObjectSprite>();
        m_starExplosionTime = new List<TimeAndVec>();
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 星バラマキイメージ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_starTexImageData = new TexImageData();
        m_starTexImageData.image = m_warehouseObject.GetTexture2DApp(AppImageNum.STARIMAGE);
        m_starTexImageData.pibot = new Vector2(0.5f, 0.5f);
        m_starTexImageData.size = new Vector2(3.0f, 3.0f);
        m_starTexImageData.rextParsent = MyCalculator.RectSizeReverse_Y(0, 1, 1);

        m_starRenderImageData = new RenderImageData();
        m_starRenderImageData.depth = 99;

        m_timeExplosion = 0;
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float lp = 0.0f;
        float size = 0.0f;
        float harfTime = MyCalculator.Division(m_timeMax, 2);
        float animeNum = 0;
        if(m_timeExplosion != m_timeMax)
        {
            if (m_timeExplosion < harfTime)
            {
                //*|***|***|***|***|***|***|***|***|***|***|***|
                // 大きさ
                //*|***|***|***|***|***|***|***|***|***|***|***|
                lp = MyCalculator.LeapReverseCalculation(0.0f, harfTime, m_timeExplosion);
                size = MyCalculator.Leap(1.0f, 5.0f, lp);
                //*|***|***|***|***|***|***|***|***|***|***|***|
                // アニメ番号
                //*|***|***|***|***|***|***|***|***|***|***|***|
                animeNum = 0;
            }
            else
            {
                //*|***|***|***|***|***|***|***|***|***|***|***|
                // 大きさ
                //*|***|***|***|***|***|***|***|***|***|***|***|
                size = 5.0f;
                //*|***|***|***|***|***|***|***|***|***|***|***|
                // アニメ番号
                //*|***|***|***|***|***|***|***|***|***|***|***|
                animeNum = MyCalculator.Division(m_timeExplosion - harfTime, MyCalculator.Division(harfTime, 6));
                animeNum = ChangeData.AntiOverflow(animeNum, 6);
            }
        }
        else
        {
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 時間経過
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_timeExplosion++;
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 散らばり
            //*|***|***|***|***|***|***|***|***|***|***|***|
            StarMade();
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 破壊
        //*|***|***|***|***|***|***|***|***|***|***|***|
        StarDelete();
    }

    void StarMade()
    {
        TexImageData starTex = m_starTexImageData;
        RenderImageData starRen = m_starRenderImageData;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 生成
        //*|***|***|***|***|***|***|***|***|***|***|***|
        GameObject newStarObj = null;
        GameObjectSprite newStarSprite = null;
        int starNum = 100;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 生成用
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 射角
        //*|***|***|***|***|***|***|***|***|***|***|***|
        float angleMax = 0;
        float angleMin = 360;
        float angle = 0.0f;
        float speedMax = 0.5f;
        float speedMin = 0.22f;
        float speed = 0.0f;
        Vector2 vec;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 大きさ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        float sizeMax = 2.5f;
        float sizeMin = 0.5f;
        float size;
        Vector2 sizeVector2;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 時計
        //*|***|***|***|***|***|***|***|***|***|***|***|
        TimeAndVec newTime;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 作成
        //*|***|***|***|***|***|***|***|***|***|***|***|
        for (int count = 0; count < starNum; count++)
        {
            newStarObj = new GameObject("StarExplosion" + count.ToString());
            newStarSprite = newStarObj.AddComponent<GameObjectSprite>();
            m_starExplosion.Add(newStarSprite);
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 射角
            //*|***|***|***|***|***|***|***|***|***|***|***|
            angle = Random.Range(angleMin, angleMax);
            speed = Random.Range(speedMin, speedMax);
            vec = ChangeData.AngleDegToVector2(angle);
            vec *= speed;
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 大きさ
            //*|***|***|***|***|***|***|***|***|***|***|***|
            size = Random.Range(sizeMax, sizeMin);
            sizeVector2 = new Vector2(size, size);
            starTex.size = sizeVector2;
            newStarSprite.SetImageUpdate(starTex);
            newStarSprite.SetRenderUpdate(starRen);
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 時計
            //*|***|***|***|***|***|***|***|***|***|***|***|
            newTime = new TimeAndVec();
            newTime.time = 0;
            newTime.vec = vec;
            m_starExplosionTime.Add(newTime);
        }
    }
    void StarDelete()
    {
        TimeAndVec getTime;
        GameObjectSprite sprite;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 時間
        //*|***|***|***|***|***|***|***|***|***|***|***|
        for (int index = 0; index < m_starExplosionTime.Count; index++)
        {
            getTime = m_starExplosionTime[index];

            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 時間
            //*|***|***|***|***|***|***|***|***|***|***|***|
            getTime.time++;
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // うごく
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_starExplosion[index].gameObject.transform.position += ChangeData.GetVector3(getTime.vec);

            m_starExplosionTime[index] = getTime;
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 削除
        //*|***|***|***|***|***|***|***|***|***|***|***|
        for (int index = 0; index < m_starExplosionTime.Count; index++)
        {
            if (m_starExplosionTime[index].time > m_timeMaxStar) 
            {
                //*|***|***|***|***|***|***|***|***|***|***|***|
                // 削除する準備
                //*|***|***|***|***|***|***|***|***|***|***|***|
                sprite = m_starExplosion[index];
                //*|***|***|***|***|***|***|***|***|***|***|***|
                // 削除
                //*|***|***|***|***|***|***|***|***|***|***|***|
                m_starExplosionTime.RemoveAt(index);
                m_starExplosion.RemoveAt(index);
                //*|***|***|***|***|***|***|***|***|***|***|***|
                // 削除する
                //*|***|***|***|***|***|***|***|***|***|***|***|
                Destroy(sprite.gameObject);
                index = 0;
            }
        }
    }
}
