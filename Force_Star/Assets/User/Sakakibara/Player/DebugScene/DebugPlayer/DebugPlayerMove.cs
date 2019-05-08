using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//*|***|***|***|***|***|***|***|***|***|***|***|
// プレイヤーナンバー言い換え
//*|***|***|***|***|***|***|***|***|***|***|***|
using WarehousePlayer = WarehouseData.PlayerData.WarehousePlayer;
using PlayerDataNum = WarehouseData.PlayerData.WarehousePlayer.PlayerData_Number;
using PlayerData_Number_List = WarehouseData.PlayerData.WarehousePlayer.PlayerData_Number_List;

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
    // 攻撃ボールデータ
    //*|***|***|***|***|***|***|***|***|***|***|***|
    private GameObject m_attackBoalObject;
    private Vector2 m_partsAttackPos;
    private Vector2 m_partsAttackSize;
    private DebugPlayerParts m_partsAttack2D;
    private GameObject m_rigitOnlyAttack;
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 当たりの判定脚
    //*|***|***|***|***|***|***|***|***|***|***|***|
    private GameObject m_hitFlagLegObject_L;
    private GameObject m_hitFlagLegObject_R;
    private DebugPlayerParts m_hitFlagLegParts_L;
    private DebugPlayerParts m_hitFlagLegParts_R;
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 当たりフラグ
    //*|***|***|***|***|***|***|***|***|***|***|***|
    private bool m_hitFlagLeg;



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
    //private bool m_groundFlag;
    [SerializeField]
    private bool m_groundFlagFlame;
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 動き、向きフラグ
    //*|***|***|***|***|***|***|***|***|***|***|***|
    [SerializeField]
    private bool m_addPower;
    [SerializeField]
    private bool m_moveingPower;
    [SerializeField]
    private bool m_rightArrow;
    [SerializeField]
    private bool m_reverseArrow;
    [SerializeField]
    private float m_movePowerX;
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 動き、向きフラグ(マスク)
    //*|***|***|***|***|***|***|***|***|***|***|***|
    private bool m_nextReverseFlag;
    private bool m_nowReverseFlag;
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
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 足データ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_rigidOnlyLeg = new GameObject("rigidLeg");
        m_rigidOnlyLeg.transform.parent = gameObject.transform;
        this.m_partsLeg2D = m_rigidOnlyLeg.AddComponent<DebugPlayerParts>();
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 攻撃ボールデータ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        PlayerData_Number_List partsListNum;
        partsListNum = PlayerData_Number_List.LEFTHAND;
        string partsName = partsListNum.ToString();
        GameObject parent = GameDataPublic.SearchChildAllHierarchy(gameObject, partsName);

        m_rigitOnlyAttack = new GameObject("attackBoal");
        m_rigitOnlyAttack.transform.parent = parent.transform;
        //m_rigitOnlyAttack.transform.parent = gameObject.transform;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 攻撃ボールのタグ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_rigitOnlyAttack.tag = WarehousePlayer.GetTag_AttackBoal();
        this.m_partsAttack2D = m_rigitOnlyAttack.AddComponent<DebugPlayerParts>();
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 向きフラグ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_addPower = false;
        m_rightArrow = false;
        m_reverseArrow = false;
        m_movePowerX = 0.0f;
    }

    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 当たりの判定
    //*|***|***|***|***|***|***|***|***|***|***|***|
    void AwakeHitFlag()
    {

    }




    void Start()
    {
        Vector2 sizeLeg = new Vector2(1.0f, 0.2f);
        Vector2 pointLeg = new Vector2(0.0f, -0.5f);
        m_partsLeg2D.SetPointSize(pointLeg, sizeLeg);

        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 攻撃ボールデータ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        //gameObject.transform.childCount;
        //gameObject.transform.chi;

        //for (int index = 0; ; index++)
        //m_attackBoalObject = 
        
        m_partsAttackPos = new Vector2(0.0f, 0.0f);
        m_partsAttackSize = new Vector2(0.5f, 0.5f);
        m_partsAttack2D.SetPointSize(m_partsAttackPos, m_partsAttackSize);
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
        Vector2 addForce = (force * 10);
        m_addPower = false;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 十字キーの力をつぎ込む
        //*|***|***|***|***|***|***|***|***|***|***|***|
        if (force != Vector2.zero)
        {
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 力を加える
            //*|***|***|***|***|***|***|***|***|***|***|***|
            this.m_rigid2D.AddForce(addForce);
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 力を加えられた
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_addPower = true;
        }

        if(m_controller.ChackJumpTrigger())
        {
            this.m_rigid2D.AddForce(transform.up * 300);
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 移動ベクトル取得
        //*|***|***|***|***|***|***|***|***|***|***|***|
        float addPowerX = addForce.x;
        bool rightPower = false;
        m_movePowerX = this.m_rigid2D.velocity.x;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 移動ベクトルが小さいと認知しない
        //*|***|***|***|***|***|***|***|***|***|***|***|
        if (Mathf.Abs(m_movePowerX) < 0.01f)
        {
            m_movePowerX = 0;
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 方向指定
        //*|***|***|***|***|***|***|***|***|***|***|***|
        if (m_movePowerX != 0)
        {
            if (m_movePowerX < 0)
            {
                m_rightArrow = false;
            }
            if (m_movePowerX > 0)
            {
                m_rightArrow = true;
            }
            m_moveingPower = true;
        }
        else
        {
            if (m_addPower)
            {
                m_moveingPower = true;
            }
            else
            {
                m_moveingPower = false;
            }
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 力の方向指定
        //*|***|***|***|***|***|***|***|***|***|***|***|
        if (m_addPower)
        {
            if (addPowerX < 0)
            {
                rightPower = false;
            }
            if (addPowerX > 0)
            {
                rightPower = true;
            }

            if(rightPower ^ m_rightArrow)
            {
                m_reverseArrow = true;
            }
            else
            {
                m_reverseArrow = false;
            }
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 反映
        //*|***|***|***|***|***|***|***|***|***|***|***|
        float powerY = this.m_rigid2D.velocity.y;
        Vector2 power = new Vector2(m_movePowerX, powerY);
        this.m_rigid2D.velocity = power;



        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 攻撃ボールデータ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_partsAttackPos = new Vector2(0.0f, 0.0f);
        m_partsAttackSize = new Vector2(0.5f, 0.5f);
        m_partsAttack2D.SetPointSize(m_partsAttackPos, m_partsAttackSize);
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
        //m_groundFlag = false;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // コントローラー取得
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public void LinkController(DebugPlayerController getController)
    {
        m_controller = getController;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 向き判定取得
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public bool GetAddPowerFlag()
    {
        return m_addPower;
    }
    public bool GetMoveingPowerFlag()
    {
        return m_moveingPower;
    }
    public bool GetRightArrowFlag()
    {
        return m_rightArrow;
    }
    public bool GetReverseArrowFlag()
    {
        return m_reverseArrow;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 当たり判定取得
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public bool GetGroundFlag()
    {
        return m_groundFlagFlame;
    }

    ////*|***|***|***|***|***|***|***|***|***|***|***|
    //// 当たり判定取得
    ////*|***|***|***|***|***|***|***|***|***|***|***|
    //private void OnTriggerStay2D(Collision2D col)
    //{
    //    //地面に接していたらgroundFlagをtrueにする
    //    if (col.gameObject.tag == "Floor" || col.gameObject.tag == "Enemy" || col.gameObject.tag == "Shell")
    //    {
    //        //m_groundFlag = true;
    //    }
    //}
}
