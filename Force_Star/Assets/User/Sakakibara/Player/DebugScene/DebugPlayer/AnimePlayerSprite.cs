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
public class AnimePlayerSprite : AnimeSprite
{

    public PlayerDataNum m_playerDataNum;

    //*|***|***|***|***|***|***|***|***|***|***|***|
    // これが出来たときに
    //*|***|***|***|***|***|***|***|***|***|***|***|
    void Awake()
    {
        AwakeOrigin();
    }

    //*|***|***|***|***|***|***|***|***|***|***|***|
    // アップデート
    //*|***|***|***|***|***|***|***|***|***|***|***|
    void Update()
    {
        UpdateOrigin();
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // アップデート抽象クラス
    //*|***|***|***|***|***|***|***|***|***|***|***|
    protected override void UpdateImageTex()
    {
        int rectX = ChangeData.AmongLess(m_rectX, 1, 16);
        int rectY = ChangeData.AmongLess(m_rectY, 1, 16);
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // イメージを作成
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_playerDataNum = (PlayerDataNum)m_dataNum;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // イメージを作成
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_texImageData.image = WarehousePlayer.GetInstance().GetPlayerTexture2D(m_playerDataNum);
        m_texImageData.rextParsent = MyCalculator.RectSize(m_rectNum, rectX, rectY, 1, 1);
        m_texImageData.size = m_size;
    }

}
