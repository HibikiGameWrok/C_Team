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
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 星バラマキ
    //*|***|***|***|***|***|***|***|***|***|***|***|
    List<GameObjectSprite> m_starExplosion;
    private TexImageData m_starTexImageData;
    private RenderImageData m_starRenderImageData;
    //*|***|***|***|***|***|***|***|***|***|***|***|
    //  大爆発
    //*|***|***|***|***|***|***|***|***|***|***|***|
    GameObjectSprite m_centerExplosion;
    private TexImageData m_centeTexImageData;
    private RenderImageData m_centeRenderImageData;
    private int m_centerAnime;
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 継承用使用データ
    //*|***|***|***|***|***|***|***|***|***|***|***|
    private int m_timeExplosion;

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
        // 星バラマキ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_starExplosion = new List<GameObjectSprite>();
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 星バラマキイメージ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_starTexImageData = new TexImageData();
        m_starTexImageData.image = m_warehouseObject.GetTexture2DApp(AppImageNum.STARIMAGE);
        m_starTexImageData.pibot = new Vector2(0.5f, 0.5f);
        m_starTexImageData.size = new Vector2(3.0f, 3.0f);
        m_starTexImageData.rextParsent = MyCalculator.RectSizeReverse_Y(0, 1, 1);

        m_starRenderImageData.depth = 99;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        //  大爆発
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_centerExplosion = new GameObjectSprite();
        //*|***|***|***|***|***|***|***|***|***|***|***|
        //  大爆発イメージ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_centeTexImageData = new TexImageData();
        m_centeTexImageData.image = m_warehouseObject.GetTexture2DApp(AppImageNum.EXPROSION);
        m_centeTexImageData.pibot = new Vector2(0.5f, 0.5f);
        m_centeTexImageData.size = new Vector2(3.0f, 3.0f);
        m_centeTexImageData.rextParsent = MyCalculator.RectSizeReverse_Y(0, 6, 1);

        m_centeRenderImageData.depth = 100;

        m_timeExplosion = 0;
    }
    void Start()
    {
        m_centerExplosion.SetImage(m_centeTexImageData);
        m_centerExplosion.SetRenderUpdate(m_centeRenderImageData);
    }

    // Update is called once per frame
    void Update()
    {
        m_timeExplosion++;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_centerAnime = m_timeExplosion;
        m_centerAnime = ChangeData.AntiOverflow(m_centerAnime, 6);
        m_centerExplosion.SetRect(MyCalculator.RectSizeReverse_Y(m_centerAnime, 6, 1));
    }
}
