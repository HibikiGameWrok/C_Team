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
// ゲームオブジェクトデータは眠らない
//*|***|***|***|***|***|***|***|***|***|***|***|
//[ExecuteInEditMode]
abstract public class AnimeSprite : MonoBehaviour
{
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // データ
    //*|***|***|***|***|***|***|***|***|***|***|***|
    PartsData m_mypartsData;
    GameObjectSprite m_mySprite;
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 継承用使用データ
    //*|***|***|***|***|***|***|***|***|***|***|***|
    protected TexImageData m_texImageData;
    protected RenderImageData m_renderImageData;
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 継承用取得データ
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public Vector2 m_size;
    public Vector3 m_localPos;
    public Vector3 m_localAngle;

    public Vector3 m_imagePos;
    public Vector3 m_imageAngle;
    public int m_dataNum;
    public int m_depth;

    //*|***|***|***|***|***|***|***|***|***|***|***|
    // これが出来たときに
    //*|***|***|***|***|***|***|***|***|***|***|***|
    void Awake()
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // データ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_mySprite = null;
        m_mypartsData = new PartsData();
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 継承用使用データ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_texImageData = new TexImageData();
        m_renderImageData = new RenderImageData();
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 継承用取得データ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_size = new Vector2();
        m_localPos = new Vector3();
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
        UpdateTex();
    }

    //*|***|***|***|***|***|***|***|***|***|***|***|
    // アップデート
    //*|***|***|***|***|***|***|***|***|***|***|***|
    void UpdateTex()
    {

        Vector3 localPos = Vector3.zero;
        Vector3 imagePos = Vector3.zero;
        Vector3 localAngle = Vector3.zero;
        Vector3 imageAngle = Vector3.zero;
        Quaternion localAngleQ = Quaternion.identity;
        Quaternion imageAngleQ = Quaternion.identity;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 反映
        //*|***|***|***|***|***|***|***|***|***|***|***|

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
        localAngleQ = Quaternion.Euler(localAngle);
        imageAngleQ = Quaternion.Euler(imageAngle);
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 描画を作成
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_renderImageData.depth = m_depth;

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
