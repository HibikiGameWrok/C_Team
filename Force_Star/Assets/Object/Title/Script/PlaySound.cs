using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySound : MonoBehaviour
{
    private AudioSource sound01;

    bool seFlag = false;

    void Start()
    {
        //AudioSourceコンポーネントを取得し、変数に格納
        sound01 = GetComponent<AudioSource>();
    }

    void Update()
    {
        //SEがなっていないとき
        if (seFlag == false)
        {
            //スペースキーが押されたら音声ファイル再生
            if (Input.GetKeyDown(KeyCode.Space))
            {
                sound01.PlayOneShot(sound01.clip);
                seFlag = true;
            }
        }

    }

}
