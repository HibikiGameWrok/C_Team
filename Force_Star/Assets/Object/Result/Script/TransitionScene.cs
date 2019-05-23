using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionScene : MonoBehaviour
{
    // オブジェクト取得変数
    GameObject Panel;
    // スクリプト取得変数
    ResultFade ResultFade;

    // コントロールを管理しているクラス
    PlayerController playercont;

    bool isChangeFlag = false;

    // Start is called before the first frame update
    void Start()
    { 
        // クラス生成
        playercont = new PlayerController();

        Panel = GameObject.Find("Panel");
        ResultFade = Panel.GetComponent<ResultFade>();
    }

    // Update is called once per frame
    void Update()
    {
        // クラス更新
        playercont.Update();

        if ((playercont.ChackStartTrigger()) || (playercont.ChackAttack()))
        {
            if (ResultFade.GetFadeInFlag() != true)
            {
                ResultFade.SetFadeFlag(false);
                isChangeFlag = true;
            }
        }
        else
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (ResultFade.GetFadeInFlag() != true)
            {
                ResultFade.SetFadeFlag(false);
                isChangeFlag = true;
            }
        }

        if (isChangeFlag == true && ResultFade.GetFadeOutFlag() == false)
        {
            //タイトルシーンに戻る
            SceneManager.LoadScene("TitleScene");
        }
    }
}
