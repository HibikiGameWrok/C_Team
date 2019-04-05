using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultCameraMove : MonoBehaviour
{
    //速さ
    [SerializeField]
    float vel = 1.0f;

    //位置
    float posX = 0.0f;
    float posY = 0.0f;

    //Y軸の最大値
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
        CameraMove();
    }

    void CameraMove()
    {
        if (posY < maxPosY)
        {
            posX += vel;
            posY += vel;
        }

        //位置の移動
        transform.position = new Vector3(posX, posY, transform.position.z);
    }


}
