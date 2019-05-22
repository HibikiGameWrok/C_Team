using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySound : MonoBehaviour
{
    private AudioSource sound01;

    public bool seFlag = false;

    // コントロールを管理しているクラス
    PlayerController playercont;

    // オブジェクト取得変数
    GameObject Panel;
    // スクリプト取得変数
    ResultFade ResultFade;

    void Start()
    {
        playercont = new PlayerController();
        //AudioSourceコンポーネントを取得し、変数に格納
        sound01 = GetComponent<AudioSource>();
        Panel = GameObject.Find("Panel");
        ResultFade = Panel.GetComponent<ResultFade>();
    }

    void Update()
    {
        playercont.Update();

        //スペースキーが押されたら音声ファイル再生
        if ((playercont.ChackAttack()) || (Input.GetKeyDown(KeyCode.Space)))
        {
            //SEがなっていないとき
            if (seFlag == false)
            {
                sound01.PlayOneShot(sound01.clip);
                seFlag = true;
            }
        }
        else
        {
            seFlag = false;
        }
    }

}
