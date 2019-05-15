using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//*|***|***|***|***|***|***|***|***|***|***|***|
// プレイヤー倉庫言い換え
//*|***|***|***|***|***|***|***|***|***|***|***|
using WarehousePlayer = WarehouseData.PlayerData.WarehousePlayer;
//*|***|***|***|***|***|***|***|***|***|***|***|
// プレイヤー参照言い換え
//*|***|***|***|***|***|***|***|***|***|***|***|
//using WarehousePlayer = WarehouseData.PlayerData.WarehousePlayer;

abstract public class EnemyAttackParts : MonoBehaviour
{
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // プレイヤー参照
    //*|***|***|***|***|***|***|***|***|***|***|***|
    protected PlayerDirectorIndex m_playerIndex;
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // パーツデータ
    //*|***|***|***|***|***|***|***|***|***|***|***|
    protected Rigidbody2D m_rigid2D;
    protected string m_hitTag;
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 一度きりの攻撃？
    //*|***|***|***|***|***|***|***|***|***|***|***|
    protected bool m_attackOneHit;
    protected bool m_attackHit;
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // パーツデータ起動中
    //*|***|***|***|***|***|***|***|***|***|***|***|
    protected bool m_awakeFlag;
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // パーツデータ攻撃力
    //*|***|***|***|***|***|***|***|***|***|***|***|
    protected float m_partsDamage;
    protected bool m_ignoreInvincibility;
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // これが出来たときに
    //*|***|***|***|***|***|***|***|***|***|***|***|
    void Awake()
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // プレイヤー参照
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_playerIndex = PlayerDirectorIndex.GetInstance();
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 自身を改造
        //*|***|***|***|***|***|***|***|***|***|***|***|
        gameObject.layer = WarehousePlayer.GetLayer_EnemyAttackParts();
        gameObject.tag = WarehousePlayer.GetTag_EnemyAttackParts();
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // ボディデータ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        this.m_rigid2D = gameObject.AddComponent<Rigidbody2D>();
        this.m_rigid2D.sleepMode = RigidbodySleepMode2D.NeverSleep;
        this.m_rigid2D.isKinematic = true;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // パーツデータ起動中 停止中
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_awakeFlag = false;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // パーツデータ攻撃力
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_partsDamage = 0;
        m_ignoreInvincibility = false;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 一度きりの攻撃？
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_attackOneHit = false;
        m_attackHit = false;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // これが出来たときに続きは個別で
        //*|***|***|***|***|***|***|***|***|***|***|***|
        AwakeCollider();
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // これが出来たときに続きは個別で
    //*|***|***|***|***|***|***|***|***|***|***|***|
    protected abstract void AwakeCollider();
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // これが出来たときに
    //*|***|***|***|***|***|***|***|***|***|***|***|
    void Start()
    {
        this.m_rigid2D.isKinematic = true;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // これが出来たときに続きは個別で
        //*|***|***|***|***|***|***|***|***|***|***|***|
        StartCollider();
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // これが出来たときに続きは個別で
    //*|***|***|***|***|***|***|***|***|***|***|***|
    protected abstract void StartCollider();

    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 当たり判定大きさ
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public abstract void SetPointSize(Vector2 point, Vector2 size);
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 当たり判定停止
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public abstract void SetStopHit();
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 当たり判定再生
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public abstract void SetPlayHit();
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 当たり判定攻撃力
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public void SetAttackData(float damage, bool ignoreInvincibility)
    {
        m_partsDamage = damage;
        m_ignoreInvincibility = ignoreInvincibility;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 攻撃一回限り
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public void SetAttackOne(bool hitOne)
    {
        m_attackOneHit = hitOne;
    }
    public bool GetAttackOne()
    {
        return m_attackHit;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 当たり判定取得
    //*|***|***|***|***|***|***|***|***|***|***|***|
    private void OnTriggerStay2D(Collider2D col)
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 攻撃一回限り
        //*|***|***|***|***|***|***|***|***|***|***|***|
        if (m_attackHit && m_attackOneHit)
        {
            return;
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // ダメージ報告腕
        //*|***|***|***|***|***|***|***|***|***|***|***|
        if(col.tag == WarehousePlayer.GetTag_PlayerHitArmParts())
        {
            m_playerIndex.ExecutionDamageArm(m_partsDamage, m_ignoreInvincibility);
            m_attackHit = true;
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // ダメージ報告体
        //*|***|***|***|***|***|***|***|***|***|***|***|
        if (col.tag == WarehousePlayer.GetTag_PlayerHitBodyParts())
        {
            m_playerIndex.ExecutionDamageBody(m_partsDamage, m_ignoreInvincibility);
            m_attackHit = true;
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // ダメージ報告頭
        //*|***|***|***|***|***|***|***|***|***|***|***|
        if (col.tag == WarehousePlayer.GetTag_PlayerHitHeadParts())
        {
            m_playerIndex.ExecutionDamageHead(m_partsDamage, m_ignoreInvincibility);
            m_attackHit = true;
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // ダメージ報告脚
        //*|***|***|***|***|***|***|***|***|***|***|***|
        if (col.tag == WarehousePlayer.GetTag_PlayerHitLegParts())
        {
            m_playerIndex.ExecutionDamageLeg(m_partsDamage, m_ignoreInvincibility);
            m_attackHit = true;
        }
    }

}
