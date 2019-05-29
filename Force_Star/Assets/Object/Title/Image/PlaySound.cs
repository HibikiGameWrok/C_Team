using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySound : MonoBehaviour
{
    private AudioSource sound01;

    private bool seFlag ;

    // コントロールを管理しているクラス
    PlayerController playercont;

    // オブジェクト取得変数
    GameObject Panel;
    // スクリプト取得変数
    ResultFade ResultFade;

    void Start()
    {
        //// Sceneを遷移してもオブジェクトが消えないようにする
        //DontDestroyOnLoad(this);
        playercont = new PlayerController();
        //AudioSourceコンポーネントを取得し、変数に格納
        sound01 = GetComponent<AudioSource>();
        Panel = GameObject.Find("Panel");
        ResultFade = Panel.GetComponent<ResultFade>();
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
        //else 
        //{
        //    seFlag = false;
        //}
    }
    public bool GetSeFlag()
    {
        return seFlag;
    }

}
