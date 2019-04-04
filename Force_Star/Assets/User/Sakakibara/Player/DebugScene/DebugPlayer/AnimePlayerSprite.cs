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

    PlayerDataNum m_playerDataNum;
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // アップデート抽象クラス
    //*|***|***|***|***|***|***|***|***|***|***|***|
    protected override void UpdateImageTex()
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // イメージを作成
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_playerDataNum = (PlayerDataNum)m_dataNum;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // イメージを作成
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_texImageData.image = WarehousePlayer.GetInstance().GetTexture2D(m_playerDataNum);
        m_texImageData.rextParsent = MyCalculator.RectSize(0, 1, 1, 1, 1);
        m_texImageData.size = m_size;
    }

}
