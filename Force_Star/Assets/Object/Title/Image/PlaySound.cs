using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySound : MonoBehaviour
{
    private AudioSource sound01;

    private bool seFlag ;

    // コントロールを管理しているクラス
    PlayerController playercont;


    // スクリプト取得変数
    ResultFade ResultFade;

    void Start()
    {
        playercont = new PlayerController();
       
        //AudioSourceコンポーネントを取得し、変数に格納
        sound01 = GetComponent<AudioSource>();
        seFlag = false;
    }

    void Update()
    {
        playercont.Update();

        //スペースキーが押されたら音声ファイル再生
        if ( playercont.ChackStart() || playercont.ChackAttack() || Input.GetKeyDown(KeyCode.Space))
        {
            //SEがなっていないとき
            if (seFlag == false)
            {
                sound01.PlayOneShot(sound01.clip);
                seFlag = true;
            }
        }
    }

    public bool GetSeFlag()
    {
        return seFlag;
    }

}
