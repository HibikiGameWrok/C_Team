using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2Move : MonoBehaviour
{
    public GameObject starDirec;

    private StarDirector starCreate;

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
    int key = 1;

    [SerializeField]
    bool highJumpMode = false;

    void Start()
    {
        //当たり判定の初期化
        this.rigid2D = GetComponent<Rigidbody2D>();

        starCreate = starDirec.GetComponent<StarDirector>();

        if(highJumpMode == true)
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

        if(col.gameObject.tag == "AttackBoal")
        {
            float posX1;
            float posX2;
            float posY;
            posX1 = this.transform.position.x + this.GetComponent<Renderer>().bounds.size.x / 2 + 3;
            posX2 = this.transform.position.x - this.GetComponent<Renderer>().bounds.size.x / 2 - 3;
            posY = this.transform.position.y - this.GetComponent<Renderer>().bounds.size.y / 2;

            // 
            starCreate.CreateStar(new Vector2(posX1, posY),new Vector2(posX2, posY),10);
            //starCreate.CreateStar(20);
        }

        //地面に接していたらgroundFlagをtrueにする
        if (col.gameObject.tag == "ground")
        {
            groundFlag = true;
        }
    }

    void OnCollisionExit2D(Collision2D col)
    {
        //地面から離れたらgroundFlagをfalseにする
        if (col.gameObject.tag == "ground")
        {
            groundFlag = false;
        }
    }

        // Update is called once per frame
        void Update()
    {
        //時間経過の管理をするif文
        if (jumpTimer < 10)
        {
            jumpTimer++;
        }
        //jumpFlagがfalseでありタイマーが最大ならjumpFlagをtrueにする
        if(jumpFlag != true && jumpTimer == 10)
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
}
