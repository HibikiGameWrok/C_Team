using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketTracking : MonoBehaviour
{
    public Transform m_target = null;
    [SerializeField]
    float m_speed = 5;
    [SerializeField]
    float m_attenuation = 0.5f;
    private Vector3 m_velocity;

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

    float posX = 0.0f;
    float posY = 0.0f;

    //大きさ
    [SerializeField]
    float size = 1.0f;

    //
    int starCounter = 0;

    //最大値
    const int MAX = 2;
    //最小値
    const int MIN = 0;

    void Start()
    {
        posX = transform.position.x;
        posY = transform.position.y;
    }


    private void Update()
    {

        if (endFlag == true)
        {
            //上に移動
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
                //ふわふわ浮かぶ
                FloatMove();
            }
        }


        if (Input.GetKeyDown(KeyCode.Space)&& starCounter == 0)
        {
            moveFlag = true;
        }


        Test();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        endFlag = true;
    }


    void FloatMove()
    {
        VerticalSpeed += 0.01f; 

        transform.position = new Vector3(transform.position.x, posY + Mathf.Sin(VerticalSpeed) / 2.5f, transform.position.z);
    }

    void TrackingMove()
    {
        //追尾
        m_velocity += (m_target.position - transform.position) * m_speed;
        m_velocity *= m_attenuation;
        transform.position += m_velocity *= Time.deltaTime;

        //size += -0.001f;

        //大きさを徐々に小さく
        gameObject.transform.localScale = new Vector3(size, size, transform.localScale.z);
    }

    void EndMove()
    {
        //位置を当たった場所にする
        posX = transform.position.x;
        posY = transform.position.y;

        posX += vel;
        posY += vel;

        //位置の移動
        transform.position = new Vector3(posX, posY, transform.position.z);

        //画面外に出たら消去
        if(posY > 7.0f)
        {
            Destroy(this.gameObject);
        }
    }

    void Test()
    {
        //カッコの移動
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            starCounter += 1;
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            starCounter += -1;
        }

        if (starCounter < MIN)
        {
            starCounter = MAX;
        }

        if (starCounter > MAX)
        {
            starCounter = MIN;
        }
    }
}
