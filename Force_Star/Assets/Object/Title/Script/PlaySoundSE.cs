using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundSE : MonoBehaviour
{
    private AudioSource sound01;
    private AudioSource sound02;

    // コントロールを管理しているクラス
    PlayerController playercont;

    bool seFlag = false;

    // Start is called before the first frame update
    void Start()
    {
        playercont = new PlayerController();
        AudioSource[] audioSources = GetComponents<AudioSource>();
        sound01 = audioSources[0];
        sound02 = audioSources[1];
    }

    // Update is called once per frame
    void Update()
    {
        playercont.Update();
        if (seFlag == false)
        {
            //スペースキーが押されたら音声ファイル再生
            if (playercont.ChackStart() || playercont.ChackAttack() || Input.GetKeyDown(KeyCode.Space))
            {
                sound01.PlayOneShot(sound01.clip);
                seFlag = true;
            }



            //if (Input.GetKeyDown(KeyCode.RightArrow))
            //{
            //    sound02.PlayOneShot(sound02.clip);
            //}

            //if (Input.GetKeyDown(KeyCode.LeftArrow))
            //{
            //    sound02.PlayOneShot(sound02.clip);
            //}
        }

    }
}
