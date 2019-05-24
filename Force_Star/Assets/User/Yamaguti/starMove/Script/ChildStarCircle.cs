using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildStarCirlce : MonoBehaviour
{
    BoxCollider2D m_objectCollider;
    private bool m_hitFloorFlag;
    private bool m_hitFloorFlagFlame;
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // これが出来たときに
    //*|***|***|***|***|***|***|***|***|***|***|***|
    void Awake()
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 当たり判定作成
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_objectCollider = gameObject.GetComponent<BoxCollider2D>();
        if (m_objectCollider == null)
        {
            m_objectCollider = gameObject.AddComponent<BoxCollider2D>();
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 星のレイヤーに変更
        //*|***|***|***|***|***|***|***|***|***|***|***|
        gameObject.layer = 20;

        m_hitFloorFlagFlame = false;
        m_hitFloorFlag = false;
        m_objectCollider.isTrigger = true;
    }


    // Use this for initialization
    void Start()
    {
        SetPlayHit();
    }

    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 場所大きさ
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public void SetPointSize(Vector2 point, Vector2 size)
    {
        m_objectCollider.size = size;
        m_objectCollider.offset = point;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 当たり判定停止
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public void SetStopHit()
    {
        m_objectCollider.enabled = false;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 当たり判定再生
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public void SetPlayHit()
    {
        m_objectCollider.enabled = true;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 当たり判定取得
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public bool GetHitFlag()
    {
        return m_hitFloorFlagFlame;
    }

    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 定期更新データ
    //*|***|***|***|***|***|***|***|***|***|***|***|
    void FixedUpdate()
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 当たり判定
        //*|***|***|***|***|***|***|***|***|***|***|***|
        if (m_hitFloorFlag)
        {
            m_hitFloorFlagFlame = true;
        }
        else
        {
            m_hitFloorFlagFlame = false;
        }
        m_hitFloorFlag = false;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        m_hitFloorFlag = true;
    }
    void OnTriggerStay2D(Collider2D other)
    {
        m_hitFloorFlag = true;
    }
}
