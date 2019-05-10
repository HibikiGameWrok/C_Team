using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBodyPartsBox : EnemyBodyParts
{
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 個別パーツデータ
    //*|***|***|***|***|***|***|***|***|***|***|***|
    protected BoxCollider2D m_box2D;
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 個別の設定
    //*|***|***|***|***|***|***|***|***|***|***|***|
    protected override void AwakeCollider()
    {
        m_box2D = gameObject.AddComponent<BoxCollider2D>();
        m_box2D.isTrigger = true;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 当たり判定大きさ
    //*|***|***|***|***|***|***|***|***|***|***|***|
    protected override void StartCollider()
    {
        m_box2D.isTrigger = true;
    }

    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 場所大きさ
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public override void SetPointSize(Vector2 point, Vector2 size)
    {
        m_box2D.size = size;
        m_box2D.offset = point;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 当たり判定停止
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public override void SetStopHit()
    {
        m_box2D.enabled = false;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 当たり判定再生
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public override void SetPlayHit()
    {
        m_box2D.enabled = true;
    }
}
