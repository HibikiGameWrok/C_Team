using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//*|***|***|***|***|***|***|***|***|***|***|***|
// プレイヤー倉庫言い換え
//*|***|***|***|***|***|***|***|***|***|***|***|
using WarehousePlayer = WarehouseData.PlayerData.WarehousePlayer;

public class DeathMove : MonoBehaviour
{
    Rigidbody2D rigid2D;

    private Collider2D collider;

    //プレイヤーそのものの当たり判定を有効にするフラグ
    [SerializeField]
    private bool playerCollisionFlag = false;

    //吹っ飛び率(X軸)
    [SerializeField]
    private float blowoutRate = 100.0f;

    //吹っ飛び率(Y軸)
    [SerializeField]
    private float upForce = 300.0f;

    private GameObject attackHand;

    private GameObject player;

    //殴られて爆発するまでの時間
    float delTime;

    //爆発エフェクトの広がる範囲
    float delFxSize;

    //爆発エフェクトの☆の出る数
    float delFxSMass;

    //爆発エフェクトの☆の大きさ最小値
    float delFxSMin;

    //爆発エフェクトの☆の大きさ最大値
    float delFxSMax;

    // Start is called before the first frame update
    void Start()
    {
        //rigid2Dを使う
        this.rigid2D = gameObject.GetComponent<Rigidbody2D>();
        //colliderをoffにするため
        this.collider = gameObject.GetComponent<Collider2D>();
        player = GameObject.Find("Player");
        attackHand = GameObject.Find("AttackBoal");
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter2D(Collision2D col)
    {
        //タグで当たり判定を管理する
        if(playerCollisionFlag == true)
        {
            if (col.gameObject.tag == "Player")
            {
                //プレイヤーの向きを取得する
                float downVel = player.transform.position.x - this.transform.position.x;

                //プレイヤーの向きに飛ばすから反転する
                downVel *= -1;

                //当たり判定をoffにする
                collider.enabled = false;

                //移動
                this.rigid2D.AddForce(transform.up * this.upForce + transform.right * downVel * blowoutRate);
            }
        }
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "AttackBoal")
        {
            //プレイヤーの向きを取得する
            float downVel = attackHand.transform.position.x - this.transform.position.x;

            //プレイヤーの向きに飛ばすから反転する
            downVel *= -1;

            //当たり判定をoffにする
            collider.enabled = false;

            //移動
            this.rigid2D.AddForce(transform.up * this.upForce + transform.right * downVel * blowoutRate);
        }
    }
}
