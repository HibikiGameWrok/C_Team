using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//*|***|***|***|***|***|***|***|***|***|***|***|
// プレイヤー倉庫言い換え
//*|***|***|***|***|***|***|***|***|***|***|***|
using WarehousePlayer = WarehouseData.PlayerData.WarehousePlayer;


public class GimmickBodyColliderBox
{
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 個別パーツデータ
    //*|***|***|***|***|***|***|***|***|***|***|***|
    protected BoxCollider2D m_box2D;
    protected GameObject m_gameObject;
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 個別の設定
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public GimmickBodyColliderBox(GameObject gameObject)
    {
        m_gameObject = gameObject;
        //m_gameObject.layer = WarehousePlayer.GetLayer_GimmickBodyParts();
        //m_gameObject.tag = WarehousePlayer.GetTag_GimmickAttackParts();
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 当たり判定つける
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_box2D = m_gameObject.AddComponent<BoxCollider2D>();
        //m_box2D;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 場所大きさ
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public void SetPointSize(Vector2 point, Vector2 size)
    {
        m_box2D.size = size;
        m_box2D.offset = point;
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
}
