using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//*|***|***|***|***|***|***|***|***|***|***|***|
// プレイヤー倉庫言い換え
//*|***|***|***|***|***|***|***|***|***|***|***|
using WarehousePlayer = WarehouseData.PlayerData.WarehousePlayer;


public class StarMove : MonoBehaviour
{



    //*|***|***|***|***|***|***|***|***|***|***|***|
    // プレイシーン共通ディレクター
    //*|***|***|***|***|***|***|***|***|***|***|***|
    PlaySceneDirectorIndex m_playIndex;

    // 星の各種情報----------
    public float jumpForce;            // ジャンプ力   
    public string hitName;             //　現在当たっている子
    public  bool hitFlag=false;        // 星がプレイヤーに当たったかどうかの変数
    public ParticleSystem particle;    // 子のパーティクル保存用
    public float grv;                  // 重力
    int JumpCount = 0;                 // 現在のジャンプ回数
    float vecX;                        // X軸への移動用
    float startJF;                     // 初期のジャンプ力保存用
    int maxStar;                       // 星の最大数
    //-------------------------


    // 時間・点滅情報----------
    private float timeElapsed;     // タイムカウント用
    public int flashInterval;      // 点滅間隔
    public int flashTime;          // 点滅時間
    public int startFlashJumpCount;// 点滅を開始する回数
    int time = 0;                  // 点滅消滅の時間計測用変数
                                   //-------------------------

    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 当たり判定
    //*|***|***|***|***|***|***|***|***|***|***|***|
    Rigidbody2D m_rigidBody2D;
    BoxCollider2D m_box2D;

    //*|***|***|***|***|***|***|***|***|***|***|***|
    // これが出来たときに
    //*|***|***|***|***|***|***|***|***|***|***|***|
    void Awake()
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // プレイヤー共通ディレクター
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_playIndex = PlaySceneDirectorIndex.GetInstance();
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 星のレイヤーに変更
        //*|***|***|***|***|***|***|***|***|***|***|***|
        gameObject.layer = 12;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 当たり判定
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_rigidBody2D = this.GetComponent<Rigidbody2D>();
        m_rigidBody2D.isKinematic = true;

        m_box2D = this.GetComponent<BoxCollider2D>();
    }



    // Use this for initialization
    void Start ()
    {
        timeElapsed = 0.0f;
        particle.Stop();
        startJF = jumpForce;
    }
	
	// Update is called once per frame
	void Update () {
        if (!hitFlag)
        {
            // 当たった子を参照して星の跳ねる方向を変える
            switch (hitName)
            {
                case "StarR":
                    vecX = -vecX;
                    JumpCount++;
                    hitName = null;
                    break;
                case "StarL":
                    vecX = -vecX;
                    JumpCount++;
                    hitName = null;
                    break;
                case "StarT":
                    jumpForce = 0.0f;
                    hitName = null;
                    break;
                case "StarB":
                    jumpForce = startJF;
                    JumpCount++;
                    hitName = null;
                    break;

            }

            transform.position = new Vector3(transform.position.x + vecX, transform.position.y + jumpForce, transform.position.z);
            jumpForce = jumpForce - grv;
            FlashStar();                // 点滅用の関数
        }
        else
        {
            if (!particle.isPlaying)
                Destroy(transform.gameObject);
        }



    }

    // 点滅関数
    public void FlashStar()
    {
        if (startFlashJumpCount <= JumpCount)
        {
            // Do anything
            float level = Mathf.Abs(Mathf.Sin(Time.time * flashInterval));
            gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, level);
            timeElapsed += Time.deltaTime;

            if (timeElapsed >= 0.6f)
            {
                time++;
                if (time >= flashTime)
                    Destroy(transform.gameObject);
                timeElapsed = 0.0f;
            }

        }
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 当たり判定
    //*|***|***|***|***|***|***|***|***|***|***|***|
    void OnTriggerEnter2D(Collider2D other)
    {
        if (!hitFlag)
        {
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // プレイヤーの一部に当たったか？
            //*|***|***|***|***|***|***|***|***|***|***|***|
            if (WarehousePlayer.BoolTagIsPlayer(other.gameObject.tag))
            {
                SetStopHit();
                hitFlag = true;
                // 衝突時星の本体を見えなくする
                this.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 0.0f);
                //*|***|***|***|***|***|***|***|***|***|***|***|
                // 星登場！
                //*|***|***|***|***|***|***|***|***|***|***|***|
                m_playIndex.ApplyStar(this.transform.position, maxStar);
                // パーティクルの再生
                particle.Play();
            }
         
        }
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 当たり判定停止
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public void SetStopHit()
    {
        m_box2D.enabled = false;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 当たり判定再生
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public void SetPlayHit()
    {
        m_box2D.enabled = true;
    }

    public void SetVecX(float x)
    {
        vecX = x;
    }
    public void SetJumpF(float jump)
    {
        startJF = jumpForce;
        jumpForce =jump;
    }
    public float GetJumpF()
    {
        return jumpForce;
    }
    public void SetMaxStar(int max)
    {
        maxStar = max;
    }
    public int GetMaxStar()
    {
        return maxStar;
    }
}
