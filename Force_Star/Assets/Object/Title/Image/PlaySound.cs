using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySound : MonoBehaviour
{
    private AudioSource sound01;

   　public bool seFlag = false;

    // コントロールを管理しているクラス
    PlayerController playercont;

    void Start()
    {
        playercont = new PlayerController();
        //AudioSourceコンポーネントを取得し、変数に格納
        sound01 = GetComponent<AudioSource>();
    }

    void Update()
    {
        playercont.Update();

        //SEがなっていないとき
        if (seFlag == false)
        {
            //スペースキーが押されたら音声ファイル再生
            if ((playercont.ChackAttack()) || (Input.GetKeyDown(KeyCode.Space)))
            {
                sound01.PlayOneShot(sound01.clip);
                seFlag = true;
            }
        }

    }

}
