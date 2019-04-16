using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellController : MonoBehaviour
{
    public GameObject starDirec;
    public GameObject attackHand;

    private StarDirector starCreate;
    private PunchController punchController;

    private bool checkAttack;


    SpriteRenderer shellSprite;
    [SerializeField]
    private Sprite wait_Image = null;
    [SerializeField]
    private Sprite action_Image = null;
    [SerializeField]
    private Sprite damage_Image = null;

    //追跡ターゲット(プレイヤー)
    [SerializeField]
    private GameObject player = null;

    //追跡速度
    [SerializeField]
    private float speed = 0.05f;

    //追跡範囲
    [SerializeField]
    private float range = 9.0f;

    //攻撃してくるか色別判定する
    Renderer shellRenderer;

    bool playerApproachFlag = false;

    // x軸の大きさ
    private float xScale;

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
        this.rigid2D = gameObject.GetComponent<Rigidbody2D>();
        xScale = this.transform.localScale.x;
        shellRenderer = GetComponent<Renderer>();
        shellSprite = gameObject.GetComponent<SpriteRenderer>();
        starCreate = starDirec.GetComponent<StarDirector>();

        punchController = attackHand.GetComponent<PunchController>();
    }

    //void OnCollisionEnter2D(Collision2D col)
    //{
    //    //当たり判定
    //    if (col.gameObject.tag == "Player")
    //    {
    //        //masuter版ではこの処理はコメント①の所に移動してください-----------------------------------
    //        //好きな大きさの重力を指定する
    //        rigid2D.gravityScale = gravityForce;

    //        //「死にました」とフラグで伝える
    //        deathFlag = true;
    //        //------------------------------------------------------------------------------------------
    //    }
    //}
    void OnTriggerEnter2D(Collider2D col)
    {
        if (playerApproachFlag == true)
        {
            if ((col.gameObject.tag == "AttackBoal")&& (checkAttack == true))
            {
                float posX1;
                float posX2;
                float posY;
                posX1 = this.transform.position.x + this.GetComponent<Renderer>().bounds.size.x / 2 + 3;
                posX2 = this.transform.position.x - this.GetComponent<Renderer>().bounds.size.x / 2 - 3;
                posY = this.transform.position.y - this.GetComponent<Renderer>().bounds.size.y / 2;

                // ☆を生成
                starCreate.CreateStar(new Vector2(posX1, posY), new Vector2(posX2, posY), 10);

                rigid2D.gravityScale = gravityForce;

                //「死にました」とフラグで伝える
                deathFlag = true;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (deathFlag != true)
        {
            //貝の向かう方向を決める
            Vector2 direction = new Vector2(player.transform.position.x - transform.position.x, transform.position.y);

            //貝とプレイヤーの距離
            float length = this.transform.position.x - player.transform.position.x;

            //プレイヤーが貝の射程範囲に入った時攻撃を開始する
            if (-range <= length && length <= range)
            {
                //Debug.Log("嚙みついてやる");
                playerApproachFlag = true;
                GetComponent<Rigidbody2D>().velocity = (direction * speed);
                shellSprite.sprite = action_Image;
            }
            //プレイヤーが貝の射程範囲外なら攻撃しない
            else
            {
                playerApproachFlag = false;
                shellSprite.sprite = wait_Image;
            }

            int dir = 1;
            // 自身がプレイヤーの位置によって向きを変える
            if (this.transform.position.x > player.transform.position.x)
            {
                dir = 1;
            }
            if (this.transform.position.x < player.transform.position.x)
            {
                dir = -1;
            }
            transform.localScale = new Vector3(xScale * dir, this.transform.localScale.y, this.transform.localScale.z);


            checkAttack = punchController.attackCheck;
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
