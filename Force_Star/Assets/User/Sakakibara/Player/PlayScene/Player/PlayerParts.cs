using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParts : MonoBehaviour
{
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // パーツデータ
    //*|***|***|***|***|***|***|***|***|***|***|***|
    private Rigidbody2D m_rigid2D;
    private BoxCollider2D m_box2D;
    private bool m_hitFlag;
    private bool m_hitFlagFlame;
    private string m_hitTag;
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // これが出来たときに
    //*|***|***|***|***|***|***|***|***|***|***|***|
    void Awake()
    {
        gameObject.layer = 8;
        m_hitFlag = false;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // ボディデータ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        this.m_rigid2D = gameObject.AddComponent<Rigidbody2D>();
        this.m_box2D = gameObject.AddComponent<BoxCollider2D>();
        this.m_rigid2D.sleepMode = RigidbodySleepMode2D.NeverSleep;
        this.m_rigid2D.isKinematic = true;
        this.m_box2D.isTrigger = true;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // これが出来たときに
    //*|***|***|***|***|***|***|***|***|***|***|***|
    void Start()
    {
        this.m_rigid2D.isKinematic = true;
        this.m_box2D.isTrigger = true;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 更新データ
    //*|***|***|***|***|***|***|***|***|***|***|***|
    void Update()
    {
        
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 定期更新データ
    //*|***|***|***|***|***|***|***|***|***|***|***|
    void FixedUpdate()
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 当たり判定
        //*|***|***|***|***|***|***|***|***|***|***|***|
        if (m_hitFlag)
        {
            m_hitFlagFlame = true;
        }
        else
        {
            m_hitFlagFlame = false;
        }
        m_hitFlag = false;
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
    // 当たり判定取得
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public bool GetHitFlag()
    {
        return m_hitFlagFlame;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 当たり判定取得
    //*|***|***|***|***|***|***|***|***|***|***|***|
    private void OnTriggerEnter2D(Collider2D col)
    {
        //地面に接していたらgroundFlagをtrueにする
        if (col.gameObject.tag == "Floor" || col.gameObject.tag == "Enemy" || col.gameObject.tag == "Shell")
        {
            m_hitTag = col.gameObject.tag;
            m_hitFlag = true;
        }
    }
    private void OnTriggerStay2D(Collider2D col)
    {
        //地面に接していたらgroundFlagをtrueにする
        if (col.gameObject.tag == "Floor" || col.gameObject.tag == "Enemy" || col.gameObject.tag == "Shell")
        {
            m_hitTag = col.gameObject.tag;
            m_hitFlag = true;
        }
    }
    private void OnTriggerExit2D(Collider2D col)
    {
        //地面に接していたらgroundFlagをtrueにする
        if (col.gameObject.tag == "Floor" || col.gameObject.tag == "Enemy" || col.gameObject.tag == "Shell")
        {
            m_hitTag = col.gameObject.tag;
        }
    }
}
