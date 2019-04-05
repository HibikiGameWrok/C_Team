using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultRocketMove : MonoBehaviour
{
    //速さ
    [SerializeField]
    float vel = 1.0f;

    //位置
    float posX = 0.0f;
    float posY = 0.0f;

    //大きさ
    [SerializeField]
    float size = 1.0f;

    [SerializeField]
    float maxPosY = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        posX = transform.position.x;
        posY = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        //斜め移動
        RocketMove();
    }


    void RocketMove()
    {
        
        if (posY < maxPosY)
        {
            posX += vel;
            posY += vel;
            size += 0.003f;
        }

        //位置の移動
        transform.position = new Vector3(posX, posY, transform.position.z);

        //角度の変更
        //回転する
        //transform.Rotate(new Vector3(0.0f, 0.0f, vel));
        this.transform.rotation = Quaternion.Euler(0.0f, 0.0f, 45.0f);

        //大きさを徐々に大きく
        gameObject.transform.localScale = new Vector3(size, size, transform.localScale.z);
    }
}
