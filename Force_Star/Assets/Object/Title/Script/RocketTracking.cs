using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketTracking : MonoBehaviour
{
    // 対象物
    public Transform m_target = null;

    // 速度変数
    [SerializeField]
    float m_speed = 5;
    [SerializeField]
    float m_attenuation = 0.5f;

    Vector3 m_velocity;

    [SerializeField]
    bool moveFlag = false;
    [SerializeField]
    bool endFlag = false;

    //揺れの速さ
    [SerializeField]
    float VerticalSpeed = 0.2f;

    //移動の速さ
    [SerializeField]
    float vel = 1.0f;

    // 自身の座標
    Vector2 pos = new Vector2(0.0f,0.0f);

    //大きさ
    [SerializeField]
    float size = 1.0f;

    //最大値
    const int MAX = 2;
    //最小値
    const int MIN = 0;

    // コントロールを管理しているクラス
    PlayerController playercont;

    void Start()
    {
        playercont = new PlayerController();
        pos = new Vector2(this.transform.position.x,this.transform.position.y);
    }


    private void Update()
    {
        playercont.Update();

        if((playercont.ChackStartTrigger()) || (playercont.ChackAttack()))
        {
            moveFlag = true;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            moveFlag = true;
        }

        if (endFlag == true)
        {
            //当たった後の挙動
            EndMove();
        }
        else
        {
            if (moveFlag == true)
            {
                //ロケットに追尾
                TrackingMove();
            }
            else
            {
                //(待機状態)ふわふわ浮かぶ
                StayFloatMove();
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            // 跳ね返る挙動をするフラグをtrue
            endFlag = true;

            // ファイアプレハブをGameObject型で取得
            GameObject Fire = (GameObject)Resources.Load("Fire");
            // ファイアプレハブを元に、インスタンスを生成、
            Instantiate(Fire, this.transform.position, Quaternion.identity);
        }
    }



    private void StayFloatMove()
    {
        VerticalSpeed += 0.01f; 

        this.transform.position = new Vector3(this.transform.position.x, pos.y + Mathf.Sin(VerticalSpeed) / 2.5f, this.transform.position.z);
    }

    void TrackingMove()
    {
        //追尾
        m_velocity += (m_target.position - this.transform.position) * m_speed;
        m_velocity *= m_attenuation;
        this.transform.position += m_velocity *= Time.deltaTime;

        //大きさを徐々に小さく
        gameObject.transform.localScale = new Vector3(size, size, this.transform.localScale.z);
    }

    void EndMove()
    {
        //位置を当たった場所にする
        pos = new Vector2(this.transform.position.x,this.transform.position.y);

        // 加速度を与える
        pos += new Vector2(vel,vel);

        //位置の移動
        transform.position = new Vector3(pos.x, pos.y, transform.position.z);

        //画面外に出たら消去
        if(pos.y > 7.0f)
        {
            Destroy(this.gameObject);
        }
    }
}
