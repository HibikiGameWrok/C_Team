using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCameraSelect : MonoBehaviour
{
    public MenuRocketMove rocketMove;

    [SerializeField]
    float vel = 1.0f;

    [SerializeField]
    float posX = 0.0f;
    [SerializeField]
    float posY = 0.0f;

    [SerializeField]
    bool mFlag = false;

    bool changeFlag = false;

    // Start is called before the first frame update
    void Start()
    {
        mFlag = rocketMove.GetMoveFlag();
        posX = transform.position.x;
        posY = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        mFlag = rocketMove.GetMoveFlag();

        if (mFlag == true)
        {
            MoveCamera();
        }
    }


    void MoveCamera()
    {
        if(posX >= -7)
        {
            posX += vel;
            posY += vel;
        }
        else
        {
            changeFlag = true;
        }


        //位置の移動
        transform.position = new Vector3(posX, posY, transform.position.z);
    }

    public bool GetChangeFlag()
    {
        return changeFlag;
    } 

}
