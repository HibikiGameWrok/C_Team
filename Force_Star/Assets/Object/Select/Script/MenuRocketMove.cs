using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuRocketMove : MonoBehaviour
{
    [SerializeField]
    float vel = 1.0f;
    [SerializeField]
    bool moveFlag = false;

    float posX = 0.0f;
    float posY = 0.0f;

    //大きさ
    [SerializeField]
    float size = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        posX = transform.position.x;
        posY = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        if(moveFlag == false)
        {
            //横移動のみ
            RocketMove();
        }
        else
        {
            //斜め移動
            RocketNextMove();
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        moveFlag = true;
    }

        void RocketMove()
    {
        posX += vel;

        if (posX < -10.0f)
        {
            posX = 9.0f;
        }

        //位置の移動
        transform.position = new Vector3(posX, posY, transform.position.z);
    }

    void RocketNextMove()
    {
        posX += vel;
        posY += vel;

        //位置の移動
        transform.position = new Vector3(posX, posY, transform.position.z);

        //角度の変更
        //回転する
        //transform.Rotate(new Vector3(0.0f, 0.0f, vel));
        this.transform.rotation = Quaternion.Euler(0.0f, 0.0f, 45.0f);

        //大きさを徐々に小さく
        size += -0.003f;
        gameObject.transform.localScale = new Vector3(size, size, transform.localScale.z);
    }


    public bool GetMoveFlag()
    {
        return moveFlag;
    }
}
