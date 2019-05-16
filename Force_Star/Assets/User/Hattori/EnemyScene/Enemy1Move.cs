using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//*|***|***|***|***|***|***|***|***|***|***|***|
// プレイヤー倉庫言い換え
//*|***|***|***|***|***|***|***|***|***|***|***|
using WarehousePlayer = WarehouseData.PlayerData.WarehousePlayer;

public class Enemy1Move : MonoBehaviour
{
    public GameObject starDirec;

    private StarDirector starCreate;


    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 攻撃当たり判定データ
    //*|***|***|***|***|***|***|***|***|***|***|***|
    private Vector2 m_partsAttackPos;
    private Vector2 m_partsAttackSize;
    private EnemyAttackPartsBox m_partsAttack;
    private GameObject m_partsAttackObject;


    //動くスピード
    [SerializeField]
    float moveSpeed;

    //往復移動の一定距離
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
        this.rigid2D = gameObject.GetComponent<Rigidbody2D>();

        starCreate = starDirec.GetComponent<StarDirector>();


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
        if ((col.gameObject.tag == "AttackBoal"))
        {
            float posX1;
            float posX2;
            float posY;
            posX1 = this.transform.position.x + this.GetComponent<Renderer>().bounds.size.x / 2 + 3;
            posX2 = this.transform.position.x - this.GetComponent<Renderer>().bounds.size.x / 2 - 3;
            posY = this.transform.position.y - this.GetComponent<Renderer>().bounds.size.y / 2;

            // 
            starCreate.CreateOneStar(this.transform.position, 10);

            rigid2D.gravityScale = gravityForce;

            //「死にました」とフラグで伝える
            deathFlag = true;
            //跡形もなく消えてゆけ
            Destroy(this.gameObject);
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        if (deathFlag != true)
        {
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

            //敵を移動させる
            transform.Translate(moveSpeed, 0, 0);
            distance += moveSpeed;
        }

        //死んでしまったら
        if (deathFlag == true)
        {
            ////死んで消えてしまうまでの猶予はここで決まっているのだ
            //deathCount++;
            ////だからそれまで余生を過ごし時が来たら
            //if (deathTimer < deathCount)
            //{
            //    //跡形もなく消えてゆけ
            //    Destroy(this.gameObject);
            //}
        }
    }
}
