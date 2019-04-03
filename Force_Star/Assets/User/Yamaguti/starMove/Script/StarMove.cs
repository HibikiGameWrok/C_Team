using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarMove : MonoBehaviour {


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

    // Use this for initialization
    void Start () {
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
    // 当たり判定
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            // 衝突時星の本体を見えなくする
            hitFlag = true;
            Collider2D m_ObjectCollider = GetComponent<Collider2D>();
            m_ObjectCollider.isTrigger = true;     //　当たらないように
            this.GetComponent<SpriteRenderer>().color = new Color(0,0,0,0.0f);
            particle.Play();                       // パーティクルの再生
        }
    }

    public void PlayerCollision()
    {
        // 衝突時星の本体を見えなくする
        hitFlag = true;
        Collider2D m_ObjectCollider = GetComponent<Collider2D>();
        m_ObjectCollider.isTrigger = true;     //　当たらないように
        this.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 0.0f);
        particle.Play();                       // パーティクルの再生
    }

    public void SetVecX(float x)
    {
        vecX = x;
    }
    public void SetJumpF(float jump)
    {
        jumpForce=jump;
        startJF = jumpForce;
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
