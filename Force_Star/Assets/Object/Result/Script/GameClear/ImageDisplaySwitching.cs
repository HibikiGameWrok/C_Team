using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageDisplaySwitching : MonoBehaviour
{
    private GameObject roket;
    private ResultRocketMove resultRocketMove;

    //画像表示用
    [SerializeField]
    bool displayFlag = false;

    //シーン切り替え用
    bool stageFlag = false;

    //public GameObject clsarImage;

    // コントロールを管理しているクラス
    PlayerController playercont;

    // Start is called before the first frame update
    void Start()
    {
        playercont = new PlayerController();
        roket = GameObject.Find("Rocket");
        resultRocketMove = roket.GetComponent<ResultRocketMove>();
        Debug.Log(roket.name);
    }

    // Update is called once per frame
    void Update()
    {
        playercont.Update();
        displayFlag = resultRocketMove.GetMoveEndFlag();
        Debug.Log(displayFlag);
        if (displayFlag == true)
        {
            //画像の表示
            //clsarImage.SetActive(true);

            //スペースキーが押されたとき
            if ((playercont.ChackJump()) || (Input.GetKeyDown(KeyCode.Space)))
            {
                stageFlag = true;
            }
        }
        else
        {
            //画像の非表示
            //clsarImage.SetActive(false);
        }
    }

    public bool GetStageFlag()
    {
        return stageFlag;
    }
}
