using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class O2BarCtr : MonoBehaviour {

    Slider O2Slider;

    //酸素最大値
    [SerializeField]
    private float hp;

    public  static bool aliveFlag = true;

    private float maxhp;

    // Use this for initialization
    void Start () {
        O2Slider = GameObject.Find("O2Bar").GetComponent<Slider>();
        aliveFlag = true;
        O2Slider.maxValue = hp;
        maxhp = O2Slider.maxValue;
    }
	
	// Update is called once per frame
	void Update () {
        //時間経過
        hp += -1f;
        //最低値
        if(hp < 0)
        {
            //　プレイヤーが死んだ
            aliveFlag = false;
           // DontDestroyOnLoad(this);

            //  リザルトシーンに移行
            SceneManager.LoadScene("ResultScene");
            hp = 0;
        }
        if(hp > maxhp)
        {
            hp = maxhp;
        }


        //ここでバーを移動させる
        O2Slider.value = hp;
	}

    //これを呼び出す
    public bool GetFlag()
    {
        return aliveFlag;
    }
}

//クリアする条件
//if (col.gameObject.tag == "roket")
//{
//    if(クリアできる星の数 < 今現在の星の数)
//    {
//          aliveFlag = true;
//          SceneManager.LoadScene("ResultScene");
//    }
//}