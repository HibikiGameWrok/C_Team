using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rigid2D;     // 力を加える要素
    public GameObject starDirec;     //星
    private StarDirector starCreate; // 星の生成スクリプト

    [SerializeField]
    private float walkForce = 30.0f;　　 // 歩く力

    [SerializeField]
    private float jumpForce = 300.0f;     // ジャンプする力

    private bool  groundFlag = true; // true = 着地している, false = 地に着いていない
    
    // 加算させ続けない為の最大値
    [SerializeField]
    private float maxWalkSpeed = 2.0f;

    int a = 0;

    //X方向に力がかかり過ぎないように抑制する値
    //private float suppressionVelx = 2.0f;

    // Start is called before the first frame update
    void Start()
    {
        // 力を加える要素を取得
        this.rigid2D = GetComponent<Rigidbody2D>();
        // 星を生成するスクリプトを取得
        starCreate = starDirec.GetComponent<StarDirector>();
    }

    // Update is called once per frame
    void Update()
    {
        a = 1;

        AttackPlayer(); // 攻撃する処理

        WalkPlayer();   // 歩く処理

        JumpPlayer();   // ジャンプする処理

        StepOnPlayer(); // 踏みつけ処理

        //今後カメラを上に移動させる
        //上を向いて歩こう----------------------------------------------
        if (Input.GetKey(KeyCode.UpArrow) && groundFlag)
        {
            Debug.Log("上から来るぞ気を付けろ!");
        }
        if (Input.GetKey(KeyCode.W) && groundFlag)
        {
            Debug.Log("上から来るぞ気を付けろ!");
        }
        //--------------------------------------------------------------
    }

    // Hibiki Created a Function
    // プレイヤーが攻撃する関数
    void AttackPlayer()
    {
        //パンチ--------------------------------------------------------
        if (Input.GetKeyDown(KeyCode.X) || Input.GetKeyDown(KeyCode.K))
        {
            Debug.Log("ぱんちした");
        }
        //--------------------------------------------------------------
    }

    // Hibiki Created a Function
    // プレイヤーが歩く関数
    void WalkPlayer()
    {
        //移動----------------------------------------------------------------
        int key = 0;
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            key = 1;
        }
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            key = -1;
        }

        // 絶対値を取得
        float speedx = Mathf.Abs(this.rigid2D.velocity.x);



        // 着地時のX方向への移動速度の制限
        if ((speedx < this.maxWalkSpeed) && (groundFlag == true))
        {
            // 力を加える
            this.rigid2D.AddForce(transform.right * key * this.walkForce);
        }

        // 移動方向によって画像の向きを変える
        if (key != 0)
        {
            transform.localScale = new Vector3(this.transform.localScale.x * (-key), this.transform.localScale.y, this.transform.localScale.z);
        }
        //------------------------------------------------------------------
    }

    // Hibiki Created a Function
    // プレイヤーがジャンプする関数
    void JumpPlayer()
    {
        //ジャンプ------------------------------------------------
        if (Input.GetKeyDown(KeyCode.Z) && groundFlag)
        {
            this.rigid2D.AddForce(transform.up * this.jumpForce * 2);
            groundFlag = false;
        }
        if (Input.GetKeyDown(KeyCode.J) && groundFlag)
        {
            this.rigid2D.AddForce(transform.up * this.jumpForce * 2);
            groundFlag = false;
        }
        //--------------------------------------------------------
    }

    // Hibiki Created a Function
    // プレイヤーが踏みつける関数
    void StepOnPlayer()
    {
        //踏みつけ-----------------------------------------------------
        if (Input.GetKeyDown(KeyCode.DownArrow) && groundFlag != true)
        {
            this.rigid2D.AddForce(transform.up * -this.jumpForce);
        }
        if (Input.GetKeyDown(KeyCode.S) && groundFlag != true)
        {
            this.rigid2D.AddForce(transform.up * -this.jumpForce);
        }
        //------------------------------------------------------------
    }

    // 床の当たり判定タグをgroundからfloorに変更
    void OnCollisionEnter2D(Collision2D col)
    {
        //地面に接していたらgroundFlagをtrueにする
        if (col.gameObject.tag == "Floor")
        {
            groundFlag = true;
            starCreate.CreateStar();
        }
    }
    
}
