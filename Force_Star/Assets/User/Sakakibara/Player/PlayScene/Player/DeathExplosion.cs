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
    //*|***|***|***|***|***|***|***|***|***|***|***|
    //  爆発
    //*|***|***|***|***|***|***|***|***|***|***|***|
    List<GameObjectSprite> m_bombExplosion;
    List<TimeAndVec> m_bombExplosionTime;
    //*|***|***|***|***|***|***|***|***|***|***|***|
    //  爆発余波
    //*|***|***|***|***|***|***|***|***|***|***|***|
    List<GameObjectSprite> m_bombSecondExplosion;
    List<TimeAndVec> m_bombSecondExplosionTime;


    private TexImageData m_bombTexImageData;
    private RenderImageData m_bombRenderImageData;
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 継承用使用データ
    //*|***|***|***|***|***|***|***|***|***|***|***|
    private float m_timeExplosion;
    private static float m_timeMax = 40;
    private static float m_timeMaxSecond = 20;

    private static float m_timeMaxStar = 600;
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 爆発
    //*|***|***|***|***|***|***|***|***|***|***|***|
    private static float m_timeMaxBomb = 12;
    private static float m_timeAmongBomb = MyCalculator.Division(m_timeMaxBomb, 6);
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 爆発の余波
    //*|***|***|***|***|***|***|***|***|***|***|***|
    private static float m_timeSecondMaxBomb = 18;
    private static float m_timeSecondAmongBomb = MyCalculator.Division(m_timeSecondMaxBomb, 6);
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 爆発状態
    //*|***|***|***|***|***|***|***|***|***|***|***|
    private bool m_awakeFlag = false;
    private bool m_bombFlag = false;
    private bool m_pointReverse = false;
    private Vector3 m_pointBomb;
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 時間
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public void SetMaxTime(float time)
    {
        m_timeMax = time;
    }
    public void SetMaxSecondTime(float time)
    {
        m_timeMaxSecond = time;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 爆発起動
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public void AwakeON()
    {
        m_awakeFlag = true;
        m_bombFlag = false;
        m_timeExplosion = 0;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 爆発終了
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public void AwakeOFF()
    {
        m_awakeFlag = false;
        m_bombFlag = false;
        m_timeExplosion = 0;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 爆発地点
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public void SetPoint(Vector3 point)
    {
        m_pointBomb = point;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 爆発地点
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public void SetReverse(bool reverse)
    {
        m_pointReverse = reverse;
    }
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
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 爆発
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_bombExplosion = new List<GameObjectSprite>();
        m_bombExplosionTime = new List<TimeAndVec>();
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 爆発
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_bombSecondExplosion = new List<GameObjectSprite>();
        m_bombSecondExplosionTime = new List<TimeAndVec>();
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 爆発イメージ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_bombTexImageData = new TexImageData();
        m_bombTexImageData.image = m_warehouseObject.GetTexture2DApp(AppImageNum.EXPROSION);
        m_bombTexImageData.pibot = new Vector2(0.5f, 0.5f);
        m_bombTexImageData.size = new Vector2(1.0f, 1.0f);
        m_bombTexImageData.rextParsent = MyCalculator.RectSizeReverse_Y(0, 6, 1);

        m_bombRenderImageData = new RenderImageData();
        m_bombRenderImageData.depth = 100;

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

    // Update is called once per frame
    void Update()
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 地点移動
        //*|***|***|***|***|***|***|***|***|***|***|***|
        if (m_awakeFlag)
        {
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 時間経過
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_timeExplosion++;
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // フェーズ０ 火花
            //*|***|***|***|***|***|***|***|***|***|***|***|
            if (m_timeExplosion < m_timeMax)
            {
                //*|***|***|***|***|***|***|***|***|***|***|***|
                // 爆発が散らばる
                //*|***|***|***|***|***|***|***|***|***|***|***|
                BombMade();
                //*|***|***|***|***|***|***|***|***|***|***|***|
                // 音：ドーン
                //*|***|***|***|***|***|***|***|***|***|***|***|
                float parsent = MyCalculator.Division(m_timeExplosion, m_timeMax);
                if (m_timeExplosion % 3 == 0 && parsent < 0.75f)
                {
                    m_directorIndex.PlaySoundEffect(SEManager.SoundID.PLAYERFIRE_SE);
                }
            }
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // フェーズ１ 爆発
            //*|***|***|***|***|***|***|***|***|***|***|***|
            if (m_timeExplosion >= m_timeMax && !m_bombFlag)
            {
                //*|***|***|***|***|***|***|***|***|***|***|***|
                // 大きく星が散らばる
                //*|***|***|***|***|***|***|***|***|***|***|***|
                StarMade();
                //*|***|***|***|***|***|***|***|***|***|***|***|
                // 爆発が散らばる
                //*|***|***|***|***|***|***|***|***|***|***|***|
                m_bombFlag = true;
                //*|***|***|***|***|***|***|***|***|***|***|***|
                // 音：ドーン
                //*|***|***|***|***|***|***|***|***|***|***|***|
                m_directorIndex.PlaySoundEffect(SEManager.SoundID.PLAYERBOMB_SE_01);
                m_directorIndex.PlaySoundEffect(SEManager.SoundID.PLAYERBOMB_SE_02);
                //*|***|***|***|***|***|***|***|***|***|***|***|
                // X
                //*|***|***|***|***|***|***|***|***|***|***|***|
                gameObject.transform.position = m_pointBomb;
                m_directorIndex.SetObjectTargetCamera(this.gameObject);
                //*|***|***|***|***|***|***|***|***|***|***|***|
                // 画面揺れ
                //*|***|***|***|***|***|***|***|***|***|***|***|
                float duration = m_timeMaxSecond;
                float magnitudeMax = 0.7f;
                float magnitudeMin = 0.2f;
                m_directorIndex.LetsShake(duration, magnitudeMax, magnitudeMin);
            }
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // フェーズ２ フレアのあれ
            //*|***|***|***|***|***|***|***|***|***|***|***|
            if (m_timeExplosion < m_timeMax + m_timeMaxSecond && m_bombFlag)
            {
                float timeEx = m_timeExplosion - m_timeMax;
                //*|***|***|***|***|***|***|***|***|***|***|***|
                // 音：ドーン
                //*|***|***|***|***|***|***|***|***|***|***|***|
                if (timeEx % 3 == 0)
                {
                    m_directorIndex.PlaySoundEffect(SEManager.SoundID.PLAYERFIRE_SE);
                    //*|***|***|***|***|***|***|***|***|***|***|***|
                    // 余波
                    //*|***|***|***|***|***|***|***|***|***|***|***|
                    BombSecondMade();
                }
            }
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 破壊
        //*|***|***|***|***|***|***|***|***|***|***|***|
        StarDelete();
        BombDelete();
        BombSecondDelete();
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 星
    //*|***|***|***|***|***|***|***|***|***|***|***|
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
        // 場所
        //*|***|***|***|***|***|***|***|***|***|***|***|
        Vector3 point = m_pointBomb;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 射角
        //*|***|***|***|***|***|***|***|***|***|***|***|
        float angleMax = 360;
        float angleMin = 0;
        float angle = 0.0f;
        float speedMax = 1.5f;
        float speedMin = 0.001f;
        float speed = 0.0f;
        Vector2 vec;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 大きさ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        float sizeMax = 5.0f;
        float sizeMin = 2.5f;
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
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 爆発
            //*|***|***|***|***|***|***|***|***|***|***|***|
            newStarObj = new GameObject("StarExplosion" + count.ToString());
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 爆発絵
            //*|***|***|***|***|***|***|***|***|***|***|***|
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
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 爆発地点
            //*|***|***|***|***|***|***|***|***|***|***|***|
            //point = point;
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 反転
            //*|***|***|***|***|***|***|***|***|***|***|***|
            if (m_pointReverse)
            {
                point = MyCalculator.EachTimes(point, new Vector3(-1, 1, 1));
                vec = MyCalculator.EachTimes(vec, new Vector2(-1, 1));
            }
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 適応
            //*|***|***|***|***|***|***|***|***|***|***|***|
            newStarSprite.SetPosition(point);
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
            m_starExplosion[index].AddPosition(ChangeData.GetVector3(getTime.vec));
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
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 爆発
    //*|***|***|***|***|***|***|***|***|***|***|***|
    void BombMade()
    {
        TexImageData bombTex = m_bombTexImageData;
        RenderImageData bombRen = m_bombRenderImageData;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 生成
        //*|***|***|***|***|***|***|***|***|***|***|***|
        GameObject newBombObj = null;
        GameObjectSprite newBombSprite = null;
        int bombNum = 1;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 生成用
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 場所
        //*|***|***|***|***|***|***|***|***|***|***|***|
        Vector3 point = m_pointBomb;
        Vector2 pointDif = new Vector2(-0.5f, 1.0f);
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 射角
        //*|***|***|***|***|***|***|***|***|***|***|***|
        float angleMax = 135 + MyCalculator.Division(45.0f, 2.0f);
        float angleMin = 135 - MyCalculator.Division(45.0f, 2.0f);
        float angle = 0.0f;
        //float speedMax = 0.5f;
        //float speedMin = 0.3f;

        float speedMax = 0.35f;
        float speedMin = 0.05f;
        float speed = 0.0f;
        Vector2 vec;
        //vecDif = Vector2.zero;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 大きさ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        float sizeMax = 1.5f;
        float sizeMin = 0.05f;
        float size;
        Vector2 sizeVector2;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 時計
        //*|***|***|***|***|***|***|***|***|***|***|***|
        TimeAndVec newTime;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 作成
        //*|***|***|***|***|***|***|***|***|***|***|***|
        for (int count = 0; count < bombNum; count++)
        {
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 爆発
            //*|***|***|***|***|***|***|***|***|***|***|***|
            newBombObj = new GameObject("BombExplosion" + count.ToString());
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 爆発絵
            //*|***|***|***|***|***|***|***|***|***|***|***|
            newBombSprite = newBombObj.AddComponent<GameObjectSprite>();
            m_bombExplosion.Add(newBombSprite);
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
            bombTex.size = sizeVector2;
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 爆発地点
            //*|***|***|***|***|***|***|***|***|***|***|***|
            point = point + ChangeData.GetVector3(pointDif);
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 反転
            //*|***|***|***|***|***|***|***|***|***|***|***|
            if (m_pointReverse)
            {
                point = MyCalculator.EachTimes(point, new Vector3(-1, 1, 1));
                vec = MyCalculator.EachTimes(vec, new Vector2(-1, 1));
            }
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 適応
            //*|***|***|***|***|***|***|***|***|***|***|***|
            newBombSprite.SetPosition(point);
            newBombSprite.SetImageUpdate(bombTex);
            newBombSprite.SetRenderUpdate(bombRen);
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 時計
            //*|***|***|***|***|***|***|***|***|***|***|***|
            newTime = new TimeAndVec();
            newTime.time = 0;
            newTime.vec = vec;
            m_bombExplosionTime.Add(newTime);
        }
    }
    void BombDelete()
    {
        int animeNum = 0;
        TimeAndVec getTime;
        GameObjectSprite sprite;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 時間
        //*|***|***|***|***|***|***|***|***|***|***|***|
        for (int index = 0; index < m_bombExplosionTime.Count; index++)
        {
            getTime = m_bombExplosionTime[index];

            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 時間
            //*|***|***|***|***|***|***|***|***|***|***|***|
            animeNum = Mathf.FloorToInt(MyCalculator.Division(getTime.time, m_timeAmongBomb));
            getTime.time++;
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // うごく絵
            //*|***|***|***|***|***|***|***|***|***|***|***|
            animeNum = ChangeData.AntiOverflow(animeNum, 6);
            m_bombExplosion[index].SetRect(MyCalculator.RectSizeReverse_Y(animeNum, 6, 1));
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // うごく
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_bombExplosion[index].AddPosition(ChangeData.GetVector3(getTime.vec));



            m_bombExplosionTime[index] = getTime;
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 削除
        //*|***|***|***|***|***|***|***|***|***|***|***|
        for (int index = 0; index < m_bombExplosionTime.Count; index++)
        {
            if (m_bombExplosionTime[index].time > m_timeMaxBomb)
            {
                //*|***|***|***|***|***|***|***|***|***|***|***|
                // 削除する準備
                //*|***|***|***|***|***|***|***|***|***|***|***|
                sprite = m_bombExplosion[index];
                //*|***|***|***|***|***|***|***|***|***|***|***|
                // 削除
                //*|***|***|***|***|***|***|***|***|***|***|***|
                m_bombExplosionTime.RemoveAt(index);
                m_bombExplosion.RemoveAt(index);
                //*|***|***|***|***|***|***|***|***|***|***|***|
                // 削除する
                //*|***|***|***|***|***|***|***|***|***|***|***|
                Destroy(sprite.gameObject);
                index = 0;
            }
        }
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 爆発余波
    //*|***|***|***|***|***|***|***|***|***|***|***|
    void BombSecondMade()
    {
        TexImageData bombTex = m_bombTexImageData;
        RenderImageData bombRen = m_bombRenderImageData;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 生成
        //*|***|***|***|***|***|***|***|***|***|***|***|
        GameObject newBombObj = null;
        GameObjectSprite newBombSprite = null;
        int bombNum = 1;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 生成用
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 場所
        //*|***|***|***|***|***|***|***|***|***|***|***|
        Vector3 point = m_pointBomb;
        Vector2 pointDif = new Vector2(-0.5f, 1.0f);
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 射角
        //*|***|***|***|***|***|***|***|***|***|***|***|
        float angleMax = 360;
        float angleMin = 0;
        float angle = 0.0f;

        float speedMax = 5.5f;
        float speedMin = 0.5f;
        float speed = 0.0f;
        Vector2 vec;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 大きさ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        float sizeMax = 5.5f;
        float sizeMin = 3.5f;
        float size;
        Vector2 sizeVector2;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 時計
        //*|***|***|***|***|***|***|***|***|***|***|***|
        TimeAndVec newTime;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 作成
        //*|***|***|***|***|***|***|***|***|***|***|***|
        for (int count = 0; count < bombNum; count++)
        {
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 爆発
            //*|***|***|***|***|***|***|***|***|***|***|***|
            newBombObj = new GameObject("BombSecondExplosion" + count.ToString());
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 爆発絵
            //*|***|***|***|***|***|***|***|***|***|***|***|
            newBombSprite = newBombObj.AddComponent<GameObjectSprite>();
            m_bombSecondExplosion.Add(newBombSprite);
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
            bombTex.size = sizeVector2;
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 爆発地点
            //*|***|***|***|***|***|***|***|***|***|***|***|
            point = point + ChangeData.GetVector3(pointDif + vec);
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 反転
            //*|***|***|***|***|***|***|***|***|***|***|***|
            if (m_pointReverse)
            {
                point = MyCalculator.EachTimes(point, new Vector3(-1, 1, 1));
                vec = MyCalculator.EachTimes(vec, new Vector2(-1, 1));
            }
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 適応
            //*|***|***|***|***|***|***|***|***|***|***|***|
            newBombSprite.SetPosition(point);
            newBombSprite.SetImageUpdate(bombTex);
            newBombSprite.SetRenderUpdate(bombRen);
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 時計
            //*|***|***|***|***|***|***|***|***|***|***|***|
            newTime = new TimeAndVec();
            newTime.time = 0;
            newTime.vec = vec;
            m_bombSecondExplosionTime.Add(newTime);
        }
    }
    void BombSecondDelete()
    {
        int animeNum = 0;
        TimeAndVec getTime;
        GameObjectSprite sprite;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 時間
        //*|***|***|***|***|***|***|***|***|***|***|***|
        for (int index = 0; index < m_bombSecondExplosionTime.Count; index++)
        {
            getTime = m_bombSecondExplosionTime[index];
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 時間
            //*|***|***|***|***|***|***|***|***|***|***|***|
            animeNum = Mathf.FloorToInt(MyCalculator.Division(getTime.time, m_timeSecondAmongBomb));
            getTime.time++;
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // うごく絵
            //*|***|***|***|***|***|***|***|***|***|***|***|
            animeNum = ChangeData.AntiOverflow(animeNum, 6);
            m_bombSecondExplosion[index].SetRect(MyCalculator.RectSizeReverse_Y(animeNum, 6, 1));
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // うごく
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_bombSecondExplosionTime[index] = getTime;
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 削除
        //*|***|***|***|***|***|***|***|***|***|***|***|
        for (int index = 0; index < m_bombSecondExplosionTime.Count; index++)
        {
            if (m_bombSecondExplosionTime[index].time > m_timeSecondMaxBomb)
            {
                //*|***|***|***|***|***|***|***|***|***|***|***|
                // 削除する準備
                //*|***|***|***|***|***|***|***|***|***|***|***|
                sprite = m_bombSecondExplosion[index];
                //*|***|***|***|***|***|***|***|***|***|***|***|
                // 削除
                //*|***|***|***|***|***|***|***|***|***|***|***|
                m_bombSecondExplosionTime.RemoveAt(index);
                m_bombSecondExplosion.RemoveAt(index);
                //*|***|***|***|***|***|***|***|***|***|***|***|
                // 削除する
                //*|***|***|***|***|***|***|***|***|***|***|***|
                Destroy(sprite.gameObject);
                index = 0;
            }
        }
    }
}
