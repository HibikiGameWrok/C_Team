using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BgmChange : MonoBehaviour
{


    //ステージを選択する前と後のBGM
    public GameObject BGMNormalManager;
    public GameObject BGMHappeningManeger;

    GameObject pushTitleKey;
    // SEを鳴らすスクリプト
    PlaySound playSound;

    //BGMの切り替えるflag
    bool bgmFlag = false;

    void Awake()
    {
        // Sceneを遷移してもオブジェクトが消えないようにする
        DontDestroyOnLoad(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        pushTitleKey = GameObject.Find("PushTitleKey");
        playSound = pushTitleKey.GetComponent<PlaySound>();
    }

    // Update is called once per frame
    void Update()
    {
        bgmFlag = playSound.GetSeFlag();

        if (bgmFlag == true)
        {
            if (BGMNormalManager.GetComponent<AudioSource>().isPlaying)
            {
                BGMNormalManager.GetComponent<AudioSource>().Stop();
                //選択する後
                BGMHappeningManeger.SetActive(true);
                BGMNormalManager.SetActive(false);
                BGMHappeningManeger.GetComponent<AudioSource>().Play();
            }
        }
        else if (bgmFlag == false)
        {
                //選択する前
                BGMHappeningManeger.SetActive(false);
                BGMNormalManager.SetActive(true);
        }
    }
}
