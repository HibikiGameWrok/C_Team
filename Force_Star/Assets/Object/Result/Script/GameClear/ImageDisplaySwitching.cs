using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageDisplaySwitching : MonoBehaviour
{
    public ResultRocketMove rocket;

    //画像表示用
    [SerializeField]
    bool displayFlag = false;

    //シーン切り替え用
    bool stageFlag = false;

    public GameObject clsarImage;

    // Start is called before the first frame update
    void Start()
    {
        displayFlag = rocket.GetMoveEndFlag();
    }

    // Update is called once per frame
    void Update()
    {
        displayFlag = rocket.GetMoveEndFlag();

        if (displayFlag == true)
        {
            //画像の表示
            clsarImage.SetActive(true);

            //スペースキーが押されたとき
            if (Input.GetKeyDown(KeyCode.Space))
            {
                stageFlag = true;
            }
        }
        else
        {
            //画像の非表示
            clsarImage.SetActive(false);
        }
    }

    public bool GetStageFlag()
    {
        return stageFlag;
    }
}
