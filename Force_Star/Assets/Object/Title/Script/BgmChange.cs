using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgmChange : MonoBehaviour
{
    public NumbersChange number;

    //ステージを選択する前と後のBGM
    public GameObject BGMNormalManager;
    public GameObject BGMHappeningManeger;


    //BGMの切り替えるflag
    bool bgmFlag = false; 

    // Start is called before the first frame update
    void Start()
    {
        bgmFlag = number.GetStarNumberFlag();
       

    }

    // Update is called once per frame
    void Update()
    {
        bgmFlag = number.GetStarNumberFlag();

        if (bgmFlag)
        {
            //選択する後
            BGMHappeningManeger.SetActive(true);
            BGMNormalManager.SetActive(false);
        }
        else
        {
            //選択する前
            BGMHappeningManeger.SetActive(false);
            BGMNormalManager.SetActive(true);
        }

    }
}
