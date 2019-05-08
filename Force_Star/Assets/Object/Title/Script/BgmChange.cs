using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgmChange : MonoBehaviour
{
    //
    public PlaySound PlaySound;

    //ステージを選択する前と後のBGM
    public GameObject BGMNormalManager;
    public GameObject BGMHappeningManeger;

    //BGMの切り替えるflag
    bool bgmFlag = false;

    // Start is called before the first frame update
    void Start()
    {
        PlaySound = GetComponent<PlaySound>();
    }

    // Update is called once per frame
    void Update()
    {
        bgmFlag = PlaySound.seFlag;

        if (bgmFlag)
        {
            //選択する後
            BGMHappeningManeger.SetActive(true);
            BGMNormalManager.SetActive(false);

            // Sceneを遷移してもオブジェクトが消えないようにする
            DontDestroyOnLoad(this);
        }
        else
        {
            //選択する前
            BGMHappeningManeger.SetActive(false);
            BGMNormalManager.SetActive(true);
        }

    }
}
