using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//*|***|***|***|***|***|***|***|***|***|***|***|
// 音楽
//*|***|***|***|***|***|***|***|***|***|***|***|
using SoundID = SEManager.SoundID;


public class ShellController : MonoBehaviour
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
    // 攻撃当たり判定データ
    //*|***|***|***|***|***|***|***|***|***|***|***|
    private Vector2 m_partsAttackPos;
    private Vector2 m_partsAttackSize;
    private EnemyAttackPartsBox m_partsAttack;
    private GameObject m_partsAttackObject;

    private bool checkAttack;

    Collider2D collider;

    SpriteRenderer shellSprite;
    [SerializeField]
    private Sprite wait_Image = null;
    [SerializeField]
    private Sprite action_Image = null;
    [SerializeField]
    private Sprite damage_Image = null;

    //追跡速度
    [SerializeField]
    private float speed = 0.05f;

    //追跡範囲
    [SerializeField]
    private float range = 9.0f;

    ////攻撃してくるか色別判定する
    //Renderer shellRenderer;

    bool playerApproachFlag = false;

    // x軸の大きさ
    private float xScale;

    //消えるまでのタイマー
    [SerializeField]
    private float deathTimer;

    //掛けたい重力の大きさ
    [SerializeField]
    private float gravityForce = 1.0f;

    //吹っ飛び率(X軸)
    [SerializeField]
    private float blowoutRate = 100.0f;

    //吹っ飛び率(Y軸)
    [SerializeField]
    private float upForce = 300.0f;

    Rigidbody2D rigid2D;

    //消すためのカウント
    private float deathCount = 0.0f;

    //消すためのフラグ
    private bool deathFlag = false;

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
        //rigid2Dを使う
        this.rigid2D = gameObject.GetComponent<Rigidbody2D>();
        this.collider = gameObject.GetComponent<Collider2D>();
        xScale = this.transform.localScale.x;
        //shellRenderer = GetComponent<Renderer>();
        shellSprite = gameObject.GetComponent<SpriteRenderer>();

        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 攻撃当たり判定の発生
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_partsAttackObject.gameObject.transform.localPosition = Vector3.zero;
        m_partsAttackObject.gameObject.transform.localRotation = Quaternion.identity;
        m_partsAttackObject.gameObject.transform.localScale = Vector3.one;

        m_partsAttackPos = new Vector2(0, 0);
        m_partsAttackSize = new Vector2(1.0f, 1.0f);
        m_partsAttack.SetPlayHit();
        m_partsAttack.SetPointSize(m_partsAttackPos, m_partsAttackSize);
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 攻撃威力
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_partsAttack.SetAttackData(10.0f, false);
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
            if ((col.gameObject.tag == "AttackBoal"))
            {
                Vector2 pos = this.transform.position;
                Vector2 posPlayer = m_playerIndex.GetPlayerPosition();
                //*|***|***|***|***|***|***|***|***|***|***|***|
                // 星が出る
                //*|***|***|***|***|***|***|***|***|***|***|***|
                m_playIndex.CreateOneStar(pos, posPlayer, 20);
                //*|***|***|***|***|***|***|***|***|***|***|***|
                // 画面揺れ
                //*|***|***|***|***|***|***|***|***|***|***|***|
                m_playIndex.WowEnemy();
                //*|***|***|***|***|***|***|***|***|***|***|***|
                // 音
                //*|***|***|***|***|***|***|***|***|***|***|***|
                m_playIndex.PlaySoundEffect(SoundID.DAMAGE_02);

                rigid2D.gravityScale = gravityForce;

                //プレイヤーの向きを取得する
                float downVel = posPlayer.x - this.transform.position.x;

                //プレイヤーの向きに飛ばすから反転する
                downVel *= -1;

                //移動
                this.rigid2D.AddForce(transform.up * this.upForce + transform.right * downVel * blowoutRate);

                //当たり判定をoffにする
                collider.enabled = false;

                //「死にました」とフラグで伝える
                deathFlag = true;

                //当たり判定をoffにする
                m_partsAttack.SetStopHit();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (deathFlag != true)
        {
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // プレイヤーの地点入手
            //*|***|***|***|***|***|***|***|***|***|***|***|
            Vector3 playerPos = m_playerIndex.GetPlayerPosition();


            //貝の向かう方向を決める
            Vector2 direction = new Vector2(playerPos.x - transform.position.x, transform.position.y);

            //貝とプレイヤーの距離
            float length = this.transform.position.x - playerPos.x;

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
            if (this.transform.position.x > playerPos.x)
            {
                dir = 1;
            }
            if (this.transform.position.x < playerPos.x)
            {
                dir = -1;
            }
            transform.localScale = new Vector3(xScale * dir, this.transform.localScale.y, this.transform.localScale.z);

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
