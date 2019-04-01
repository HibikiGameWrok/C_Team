using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class O2BarCtr : MonoBehaviour {

    Slider O2Slider;
    //酸素最大値
    public float hp;

	// Use this for initialization
	void Start () {
        O2Slider = GameObject.Find("O2Bar").GetComponent<Slider>();
	}
	
	// Update is called once per frame
	void Update () {
        //時間経過
        hp += -1f;
        //最低値
        if(hp < 0)
        {
            hp = 0;
        }
        //バグ防止の為
        if(hp > 10800)
        {
            hp = 10800.0f;
        }
        //ここでバーを移動させる
        O2Slider.value = hp;
	}
}
