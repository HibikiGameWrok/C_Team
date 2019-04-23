using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugPlayerMove : MonoBehaviour
{
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // ボディデータ
    //*|***|***|***|***|***|***|***|***|***|***|***|
    private Rigidbody2D m_rigid2D;
    private BoxCollider2D m_box2D;

    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 足データ
    //*|***|***|***|***|***|***|***|***|***|***|***|
    private DebugPlayerParts m_partsLeg2D;
    private GameObject m_rigidOnlyLeg;
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // ボディデータ
    //*|***|***|***|***|***|***|***|***|***|***|***|
    //private Rigidbody2D m_rigid2D;

    //*|***|***|***|***|***|***|***|***|***|***|***|
    // コントローラー情報を転換せよ
    //*|***|***|***|***|***|***|***|***|***|***|***|
    DebugPlayerController m_controller;
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 着地フラグ
    //*|***|***|***|***|***|***|***|***|***|***|***|
    private bool m_groundFlag;
    private bool m_groundFlagFlame;
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // これが出来たときに
    //*|***|***|***|***|***|***|***|***|***|***|***|
    void Awake()
    {
        gameObject.layer = 8;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // ボディデータ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        this.m_rigid2D = gameObject.AddComponent<Rigidbody2D>();
        this.m_box2D = gameObject.AddComponent<BoxCollider2D>();

        this.m_rigid2D.sleepMode = RigidbodySleepMode2D.NeverSleep;


        m_rigidOnlyLeg = new GameObject("rigidLeg");
        m_rigidOnlyLeg.transform.parent = gameObject.transform;
        this.m_partsLeg2D = m_rigidOnlyLeg.AddComponent<DebugPlayerParts>();        
    }

    void Start()
    {
        Vector2 sizeLeg = new Vector2(1.0f, 0.2f);
        Vector2 pointLeg = new Vector2(0.0f, -0.5f);
        m_partsLeg2D.SetPointSize(pointLeg, sizeLeg);
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 更新データ
    //*|***|***|***|***|***|***|***|***|***|***|***|
    void Update()
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // すでにアップデートされているものとする
        //*|***|***|***|***|***|***|***|***|***|***|***|

        Vector2 force = m_controller.ChackStickPower();

        if(force != Vector2.zero)
        {
            // 力を加える
            this.m_rigid2D.AddForce(force * 10);
        }

        if(m_controller.ChackJumpTrigger())
        {
            this.m_rigid2D.AddForce(transform.up * 300);
        }


    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 定期更新データ
    //*|***|***|***|***|***|***|***|***|***|***|***|
    void FixedUpdate()
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 当たり判定
        //*|***|***|***|***|***|***|***|***|***|***|***|
        //if (m_groundFlag)
        if (m_partsLeg2D.GetHitFlag())
        {
            m_groundFlagFlame = true;
        }
        else
        {
            m_groundFlagFlame = false;
        }
        m_groundFlag = false;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // コントローラー取得
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public void LinkController(DebugPlayerController getController)
    {
        m_controller = getController;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 当たり判定取得
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public bool GetGroundFlag()
    {
        return m_groundFlagFlame;
    }

    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 当たり判定取得
    //*|***|***|***|***|***|***|***|***|***|***|***|
    private void OnCollisionStay2D(Collision2D col)
    {
        //地面に接していたらgroundFlagをtrueにする
        if (col.gameObject.tag == "Floor" || col.gameObject.tag == "Enemy" || col.gameObject.tag == "Shell")
        {
            m_groundFlag = true;
        }
    }
}
