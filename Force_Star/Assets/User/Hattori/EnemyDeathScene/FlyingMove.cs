using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//*|***|***|***|***|***|***|***|***|***|***|***|
// プレイヤー倉庫言い換え
//*|***|***|***|***|***|***|***|***|***|***|***|
using WarehousePlayer = WarehouseData.PlayerData.WarehousePlayer;

public class FlyingMove : MonoBehaviour
{
    //動くスピード
    [SerializeField]
    float moveSpeed = 0.0f;

    //往復移動の一定距離
    [SerializeField]
    float distance;

    //一定距離の最大
    [SerializeField]
    private float maxDistance = 2.0f;
    
    //消えるまでのタイマー
    [SerializeField]
    private float deathTimer;

    //掛けたい重力の大きさ
    [SerializeField]
    private float gravityForce = 1.0f;

    Rigidbody2D rigid2D;

    //消すためのカウント
    private float deathCount = 0.0f;

    //消すためのフラグ
    private bool deathFlag = false;

    // Start is called before the first frame update
    void Start()
    {
        //rigid2Dを使う
        this.rigid2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (deathFlag != true)
        {
            //敵を移動させる
            distance += moveSpeed;
            transform.Translate(moveSpeed, 0, 0);

            //最大値まで行ったら反転
            if (distance > maxDistance)
            {
                moveSpeed = moveSpeed * -1;
                distance = 0.0f;
            }
            //最低値まで行ったら反転
            if (distance < -maxDistance)
            {
                moveSpeed = moveSpeed * -1;
                distance = 0.0f;
            }
        }

        //死んでしまったら
        if (deathFlag == true)
        {
            //死んで消えてしまうまでの猶予はここで決まっているのだ
            deathCount++;
            //だからそれまで余生を過ごし時が来たら
            if (deathTimer < deathCount)
            {
                //跡形もなく消えてゆけ
                Destroy(this.gameObject);
            }
        }
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        //タグで当たり判定を管理する
        if (col.gameObject.tag == "Player")
        {
            //カモメの重力を有効にする
            rigid2D.gravityScale = 1.0f;

            //好きな大きさの重力を指定する
            rigid2D.gravityScale = gravityForce;

            //「死にました」とフラグで伝える
            deathFlag = true;
        }
    }
}
