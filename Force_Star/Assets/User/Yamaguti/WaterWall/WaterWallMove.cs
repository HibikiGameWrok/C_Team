using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//*|***|***|***|***|***|***|***|***|***|***|***|
// プレイヤー倉庫言い換え
//*|***|***|***|***|***|***|***|***|***|***|***|
using WarehousePlayer = WarehouseData.PlayerData.WarehousePlayer;

public class WaterWallMove : MonoBehaviour
{

    PlaySceneDirectorIndex m_playIndex;
    PlayerDirectorIndex m_playerIndex;

    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 攻撃当たり判定データ
    //*|***|***|***|***|***|***|***|***|***|***|***|
    private Vector2 m_partsAttackPos;
    private Vector2 m_partsAttackSize;
    private EnemyAttackPartsBox m_partsAttack;
    private GameObject m_partsAttackObject;

    // Rayフラグ管理用/////////////////////////
    bool[] playerHF = { false, false, false, false, false, false };
    bool waterHF = false;
    ///////////////////////////////////////////

    // レイヤー格納用//////////
    LayerMask playerlayer;
    LayerMask playerHitlayer;
    LayerMask waterlayer;
    //////////////////////////

    public ParticleSystem HitEffect;         //エフェクトパーティクル格納用

    //*|***|***|***|***|***|***|***|***|***|***|***|
    // これが出来たときに
    //*|***|***|***|***|***|***|***|***|***|***|***|
    void Awake()
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 攻撃当たり判定データ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        GameObject parent = this.gameObject;
        m_partsAttackObject = new GameObject("attackParts");
        m_partsAttackObject.transform.parent = parent.transform;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 攻撃当たり判定のスクリプト
        //*|***|***|***|***|***|***|***|***|***|***|***|
        this.m_partsAttack = m_partsAttackObject.AddComponent<EnemyAttackPartsBox>();
    }
    // Start is called before the first frame update
    void Start()
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // プレイシーン共通ディレクター
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_playIndex = PlaySceneDirectorIndex.GetInstance();
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // プレイヤー共通ディレクター
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_playerIndex = PlayerDirectorIndex.GetInstance();

        //レイヤー格納
        playerlayer = LayerMask.GetMask(LayerMask.LayerToName(8));
        playerHitlayer = LayerMask.GetMask(LayerMask.LayerToName(10));
        waterlayer = LayerMask.GetMask(LayerMask.LayerToName(14));

        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 攻撃当たり判定の発生
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_partsAttackObject.gameObject.transform.localPosition = Vector3.zero;
        m_partsAttackObject.gameObject.transform.localRotation = Quaternion.identity;
        m_partsAttackObject.gameObject.transform.localScale = Vector3.one;

        m_partsAttackPos = new Vector2(0, 0);
        m_partsAttackSize = new Vector2(1.5f, 1.5f);
        m_partsAttack.SetPlayHit();
        m_partsAttack.SetPointSize(m_partsAttackPos, m_partsAttackSize);
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 攻撃威力
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_partsAttack.SetAttackData(25.0f, true);
    }

    // Update is called once per frame
    void Update()
    {
        // 現在の座標からのxyz を1ずつ加算して移動
        //transform.Translate(0.0f, -0.1f, 0.0f);
        // 滝の動き
        transform.position = new Vector3(transform.position.x, transform.position.y + -0.1f, transform.position.z);
        // 滝がプレイヤーに当たり続けているかどうか
        HitCheck();
    }

    void HitCheck()
    {
        // Ray プレイヤー
        Vector3 pos = new Vector3(transform.position.x - (transform.GetComponent<Renderer>().bounds.size.x / 2.0f - 1.0f), transform.position.y, transform.position.z);
        playerHF[0] = Physics2D.Linecast(pos, pos - transform.up * 14.0f, playerlayer);
        pos = new Vector3(transform.position.x + (transform.GetComponent<Renderer>().bounds.size.x / 2.0f - 1.0f), transform.position.y, transform.position.z);
        playerHF[1] = Physics2D.Linecast(pos, pos - transform.up * 14.0f, playerlayer);
        playerHF[2] = Physics2D.Linecast(this.transform.position, this.transform.position - transform.up * 14.0f, playerlayer);
        pos = new Vector3(transform.position.x - (transform.GetComponent<Renderer>().bounds.size.x / 2.0f), transform.position.y, transform.position.z);
        playerHF[3] = Physics2D.Linecast(pos, pos - transform.up * 14.0f, playerHitlayer);
        pos = new Vector3(transform.position.x + (transform.GetComponent<Renderer>().bounds.size.x / 2.0f), transform.position.y, transform.position.z);
        playerHF[4] = Physics2D.Linecast(pos, pos - transform.up * 14.0f, playerHitlayer);
        playerHF[5] = Physics2D.Linecast(this.transform.position, this.transform.position - transform.up * 14.0f, playerHitlayer);

        // Ray 滝
        pos = new Vector3(transform.position.x, transform.position.y - this.GetComponent<Renderer>().bounds.size.y / 2, transform.position.z);
        waterHF = Physics2D.Linecast(new Vector3(transform.position.x, transform.position.y - this.GetComponent<Renderer>().bounds.size.y / 2, transform.position.z), pos - transform.up * 1.0f, waterlayer);

        // Rayに当たっておらずすでに一度滝を割っていた場合新しく滝を作る
        if (!playerHF[0] && !playerHF[1] && !playerHF[2] && !playerHF[3] && !playerHF[4] && !playerHF[5] && !waterHF && this.transform.parent.GetComponent<WaterWall>().GetHitCheck())
        {
            Debug.Log(1);
            this.transform.parent.GetComponent<WaterWall>().SetSList(this.gameObject);
        }


    }
    void OnTriggerEnter2D(Collider2D other)
    {
        // 滝が一定の位置にいったら消す
        if (other.gameObject.tag == "WaterWall")
        {
            this.transform.parent.GetComponent<WaterWall>().WaterList(gameObject);
        }
        // プレイヤーとの当たり判定
        if (WarehousePlayer.BoolTagIsPlayer(other.gameObject.tag))
        {
            // Ray
            Vector3 raypos = new Vector3(transform.position.x - (transform.GetComponent<Renderer>().bounds.size.x / 2.0f), transform.position.y - (transform.GetComponent<Renderer>().bounds.size.x / 2.0f), transform.position.z);
            playerHF[0] = Physics2D.Linecast(raypos, raypos - transform.up * 14.0f, playerlayer);
            raypos = new Vector3(transform.position.x + (transform.GetComponent<Renderer>().bounds.size.x / 2.0f), transform.position.y - (transform.GetComponent<Renderer>().bounds.size.x / 2.0f), transform.position.z);
            playerHF[1] = Physics2D.Linecast(raypos, raypos - transform.up * 14.0f, playerlayer);
            playerHF[2] = Physics2D.Linecast(this.transform.position, this.transform.position - transform.up * 14.0f, playerlayer);
            raypos = new Vector3(transform.position.x - (transform.GetComponent<Renderer>().bounds.size.x / 2.0f), transform.position.y, transform.position.z);
            playerHF[3] = Physics2D.Linecast(raypos, raypos - transform.up * 14.0f, playerHitlayer);
            raypos = new Vector3(transform.position.x + (transform.GetComponent<Renderer>().bounds.size.x / 2.0f), transform.position.y, transform.position.z);
            playerHF[4] = Physics2D.Linecast(raypos, raypos - transform.up * 14.0f, playerHitlayer);
            playerHF[5] = Physics2D.Linecast(this.transform.position, this.transform.position - transform.up * 14.0f, playerHitlayer);
            // 下から触れた時だけ星とエフェクトを出す
            if (playerHF[0] || playerHF[1] || playerHF[2] || !playerHF[3] || !playerHF[4] || !playerHF[5])
            {
                // this.transform.parent.GetComponent<WaterWall>().SetHitflag(true);
                if (HitEffect != null)
                {
                    m_playIndex.ApplyStarDiffusion(this.transform.position, 20);
                    Vector3 pos = new Vector3(this.transform.position.x, this.transform.position.y - this.GetComponent<Renderer>().bounds.size.y / 2, this.transform.position.z);
                    ParticleSystem par = Instantiate(HitEffect, pos, Quaternion.identity) as ParticleSystem;
                    par.Play();
                }
            }
            // 当たった滝とそれより下の滝を全て消す
            this.transform.parent.GetComponent<WaterWall>().WaterListAt(gameObject);
        }
    }
    void OnTriggerStay2D(Collider2D other)
    {
        // 進入でもけるように
        if (other.gameObject.tag == "WaterWall")
        {
            this.transform.parent.GetComponent<WaterWall>().WaterList(gameObject);
        }
    }
}
