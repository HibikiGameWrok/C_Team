using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//*|***|***|***|***|***|***|***|***|***|***|***|
// プレイヤー倉庫言い換え
//*|***|***|***|***|***|***|***|***|***|***|***|
using WarehousePlayer = WarehouseData.PlayerData.WarehousePlayer;

public class Enemy2Move : MonoBehaviour
{
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // プレイシーン共通ディレクター
    //*|***|***|***|***|***|***|***|***|***|***|***|
    PlaySceneDirectorIndex m_playIndex;
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // プレイヤー共通ディレクター
    //*|***|***|***|***|***|***|***|***|***|***|***|
    PlayerDirectorIndex m_playerIndex;

    //当たり判定
    Rigidbody2D rigid2D;

    //Y方向にかかる力
    [SerializeField]
    float jumpForce = 300.0f;

    //X方向にかかる力
    float walkForce = 30.0f;

    //X方向に力がかかり過ぎないように抑制する値
    float maxWalkSpeed = 2.0f;

    //自動でジャンプする為のフラグ
    bool jumpFlag = false;

    //地面に当たっている時だけジャンプする為のフラグ
    bool groundFlag = false;

    //自動でジャンプする為のタイマー
    int jumpTimer = 0;

    //敵の方向
    int key = -1;

    [SerializeField]
    bool highJumpMode = false;

    //消えるまでのタイマー
    [SerializeField]
    private float deathTimer;

    //掛けたい重力の大きさ
    [SerializeField]
    private float gravityForce = 1.0f;

    //消すためのカウント
    private float deathCount = 0.0f;

    //消すためのフラグ
    private bool deathFlag = false;

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

        //当たり判定の初期化
        this.rigid2D = GetComponent<Rigidbody2D>();

        if (highJumpMode == true)
        {
            jumpForce *= 2.0f;
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        //タグで当たり判定を管理する
        if(col.gameObject.tag == "wall" || 
            col.gameObject.tag == "Enemy" ||
            col.gameObject.tag == "Player")
        {
            //方向転換
            key *= -1;
        }

        //地面に接していたらgroundFlagをtrueにする
        if (col.gameObject.tag == "Floor")
        {
            groundFlag = true;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if ((col.gameObject.tag == "AttackBoal"))
        {
            float posX1;
            float posX2;
            float posY;
            posX1 = this.transform.position.x + this.GetComponent<Renderer>().bounds.size.x / 2 + 3;
            posX2 = this.transform.position.x - this.GetComponent<Renderer>().bounds.size.x / 2 - 3;
            posY = this.transform.position.y - this.GetComponent<Renderer>().bounds.size.y / 2;

            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 星が出る *5
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_playIndex.ApplyStar(new Vector2(posX1, posY), 20);

            //好きな大きさの重力を指定する
            rigid2D.gravityScale = gravityForce;

            //「死にました」とフラグで伝える
            deathFlag = true;
        }
    }

    void OnCollisionExit2D(Collision2D col)
    {
        //地面から離れたらgroundFlagをfalseにする
        if (col.gameObject.tag == "Floor")
        {
            groundFlag = false;
        }
    }

        // Update is called once per frame
        void Update()
    {
        if (deathFlag != true)
        {
            //時間経過の管理をするif文
            if (jumpTimer < 10)
            {
                jumpTimer++;
            }
            //jumpFlagがfalseでありタイマーが最大ならjumpFlagをtrueにする
            if (jumpFlag != true && jumpTimer == 10)
            {
                jumpFlag = true;
            }

            //X方向のspeedを設定する
            float speedx = Mathf.Abs(this.rigid2D.velocity.x);

            //jumpFlagがtrueでありspeedが最大値を超えていないなら
            if (jumpFlag && speedx < this.maxWalkSpeed)
            {
                //X方向に力を加える
                this.rigid2D.AddForce(transform.right * key * this.walkForce);
            }

            //jumpFlagがtrueでありgroundFlagがtrueでありY方向の加えられる力が0.0fなら
            if (jumpFlag && groundFlag)
            {
                //ジャンプする
                this.rigid2D.AddForce(transform.up * this.jumpForce);

                //jumpFlagをfalseにしてjumpTimerを0に戻す
                jumpFlag = false;
                jumpTimer = 0;
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
}
