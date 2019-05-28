using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//*|***|***|***|***|***|***|***|***|***|***|***|
// 音楽
//*|***|***|***|***|***|***|***|***|***|***|***|
using SoundID = SEManager.SoundID;
using AudioID = SEManager.AudioID;

public class StoneMove : MonoBehaviour
{
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // プレイシーン共通ディレクター
    //*|***|***|***|***|***|***|***|***|***|***|***|
    PlaySceneDirectorIndex m_playIndex;
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // プレイヤー共通ディレクター
    //*|***|***|***|***|***|***|***|***|***|***|***|
    PlayerDirectorIndex m_playerIndex;
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 時間
    //*|***|***|***|***|***|***|***|***|***|***|***|
    private float m_timeMax;
    private float m_timeLevel;

    Rigidbody2D rigid2D;


    //*|***|***|***|***|***|***|***|***|***|***|***|
    // これが出来たときに
    //*|***|***|***|***|***|***|***|***|***|***|***|
    void Awake()
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // プレイシーン共通ディレクター
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_playIndex = PlaySceneDirectorIndex.GetInstance();
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // プレイヤー共通ディレクター
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_playerIndex = PlayerDirectorIndex.GetInstance();
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 時間
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_timeMax = 150.0f;
        m_timeLevel = 100.0f;

        ////*|***|***|***|***|***|***|***|***|***|***|***|
        //// 攻撃当たり判定データ
        ////*|***|***|***|***|***|***|***|***|***|***|***|
        //GameObject parent = this.gameObject;
        //m_partsAttackObject = new GameObject("attackParts");
        //m_partsAttackObject.transform.parent = parent.transform;
        ////*|***|***|***|***|***|***|***|***|***|***|***|
        //// 攻撃当たり判定のスクリプト
        ////*|***|***|***|***|***|***|***|***|***|***|***|
        //this.m_partsAttack = m_partsAttackObject.AddComponent<EnemyAttackPartsBox>();


    }

    // Start is called before the first frame update
    void Start()
    {

        ////*|***|***|***|***|***|***|***|***|***|***|***|
        //// 攻撃当たり判定の発生
        ////*|***|***|***|***|***|***|***|***|***|***|***|
        //m_partsAttackObject.gameObject.transform.localPosition = Vector3.zero;
        //m_partsAttackObject.gameObject.transform.localRotation = Quaternion.identity;
        //m_partsAttackObject.gameObject.transform.localScale = Vector3.one;

        //m_partsAttackPos = new Vector2(0, 0);
        //m_partsAttackSize = new Vector2(1.5f, 1.5f);
        //m_partsAttack.SetPlayHit();
        //m_partsAttack.SetPointSize(m_partsAttackPos, m_partsAttackSize);
        ////*|***|***|***|***|***|***|***|***|***|***|***|
        //// 攻撃威力
        ////*|***|***|***|***|***|***|***|***|***|***|***|
        //m_partsAttack.SetAttackData(10.0f, false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D col)
    {

        if ((col.gameObject.tag == "AttackBoal"))
        {
            Vector2 pos = this.transform.position;
            Vector2 posPlayer = m_playerIndex.GetPlayerPosition();
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 星が出る
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_playIndex.CreateOneStarTime(pos, posPlayer, 20, m_timeMax, m_timeLevel);
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 画面揺れ
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_playIndex.WowEnemy();
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 音
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_playIndex.PlaySoundEffect(SoundID.SAND_BLOCK);


            //跡形もなく消えてゆけ
            Destroy(this.gameObject);
        }
    }
}
