using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rigid2D;     //力を加える要素
    //public GameObject starDirec;     //星
    //private StarDirector starCreate; //星の生成スクリプト
    //public StarCount escape;

    [SerializeField]
    private float walkForce = 30.0f;　　 //歩く力

    private Vector3 direction = new Vector3(0.0f,0.0f,0.0f);        //向き

    [SerializeField]
    private float jumpForce = 300.0f;     //ジャンプする力

    private bool  groundFlag = true; // true = 着地している, false = 地に着いていない

    // 加算させ続けない為の最大値
    [SerializeField]
    private float maxWalkSpeed = 2.0f;

    // ジャンプSE
    private AudioSource soundJump;
    [SerializeField]
    bool grounded = false;

    LayerMask groundlayer;

    // Start is called before the first frame update
    void Start()
    {
        direction = this.transform.localScale;
        // 力を加える要素を取得
        this.rigid2D = GetComponent<Rigidbody2D>();

        AudioSource[] audioSources = GetComponents<AudioSource>();
        soundJump = audioSources[0];

        groundlayer = LayerMask.GetMask(LayerMask.LayerToName(9));
    }

    // Update is called once per frame
    void Update()
    {
        AttackPlayer(); // 攻撃する処理

        WalkPlayer();   // 歩く処理

        JumpPlayer();   // ジャンプする処理

        StepOnPlayer(); // 踏みつけ処理

        //今後カメラを上に移動させる
        //上を向いて歩こう----------------------------------------------
        //if (Input.GetKey(KeyCode.UpArrow) && groundFlag)
        //{
        //    Debug.Log("上から来るぞ気を付けろ!");
        //}
        //if (Input.GetKey(KeyCode.W) && groundFlag)
        //{
        //    Debug.Log("上から来るぞ気を付けろ!");
        //}
        //--------------------------------------------------------------
    }

    // Hibiki Created a Function
    // プレイヤーが攻撃する関数
    void AttackPlayer()
    {
        //パンチ--------------------------------------------------------
        if (Input.GetKeyDown(KeyCode.X) || Input.GetKeyDown(KeyCode.K))
        {
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
        if ((speedx < maxWalkSpeed))
        {
            // 力を加える
            this.rigid2D.AddForce(transform.right * key * this.walkForce);

            if(key > 0 || key < 0)
            {
                // 力を加える
                this.rigid2D.AddForce((transform.right * key * this.walkForce) * 0.5f);
            }
        }

        // 移動方向によって画像の向きを変える
        if (key != 0)
        {
            transform.localScale = new Vector3(direction.x * key, direction.y, direction.z);
        }
        //------------------------------------------------------------------
    }

    // Hibiki Created a Function
    // プレイヤーがジャンプする関数
    void JumpPlayer()
    {
        grounded = Physics2D.Linecast(transform.position - transform.up * 1.7f,
        transform.position - transform.up * 3.0f,
        groundlayer);

        //ジャンプ------------------------------------------------
        if (Input.GetKeyDown(KeyCode.Z)/* && (grounded == true)*/)
        {
            rigid2D.velocity = new Vector2(rigid2D.velocity.x, 0);
            this.rigid2D.AddForce(transform.up * this.jumpForce);
            soundJump.PlayOneShot(soundJump.clip);
            groundFlag = false;
        }
        if (Input.GetKeyDown(KeyCode.J)/* && (grounded == true)*/)
        {
            rigid2D.velocity = new Vector2(rigid2D.velocity.x, 0);
            this.rigid2D.AddForce(transform.up * this.jumpForce * 2);
            soundJump.PlayOneShot(soundJump.clip);
            groundFlag = false;
        }
        //--------------------------------------------------------
    }

    // Hibiki Created a Function
    // プレイヤーが踏みつける関数
    void StepOnPlayer()
    {
        //踏みつけ-----------------------------------------------------
        //if (Input.GetKeyDown(KeyCode.DownArrow) && groundFlag != true)
        //{
        //    this.rigid2D.AddForce(transform.up * -this.jumpForce);
        //}
        //if (Input.GetKeyDown(KeyCode.S) && groundFlag != true)
        //{
        //    this.rigid2D.AddForce(transform.up * -this.jumpForce);
        //}
        //------------------------------------------------------------
    }

    // 床の当たり判定タグをgroundからfloorに変更
    private void OnCollisionEnter2D(Collision2D col2D)
    {
        //地面に接していたらgroundFlagをtrueにする
        if (col2D.gameObject.tag == "Floor" || col2D.gameObject.tag == "Enemy" || col2D.gameObject.tag == "Shell")
        {
            groundFlag = true;
        }
    }
    //void OnTriggerEnter2D(Collision2D col2D)
    //{
    //    //地面に接していたらgroundFlagをtrueにする
    //    if (col2D.gameObject.tag == "Floor" || col2D.gameObject.tag == "Enemy" || col2D.gameObject.tag == "Shell")
    //    {
    //        groundFlag = true;
    //    }
    //}


    void OnTriggerStay2D(Collider2D col)
    {        
        // ロケットに当たっている時
        if (col.gameObject.tag == "Roket")
        {
            // 脱出可能である時
            //if(escape.escapeFlag == true)
            //{
            //    // ↑orWを押すとシーン移行
            //    if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
            //    {
            //        SceneManager.LoadScene("ResultScene");
            //    }
            //}
        }
    }

    public bool GetJumpFlag()
    {
        return groundFlag;
    }

}
