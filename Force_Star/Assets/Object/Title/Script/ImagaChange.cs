using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImagaChange : MonoBehaviour
{
    //画像切り替え用のカウンター
    [SerializeField]
    int starCounter = 0;
    [SerializeField]
    bool starFlag = false;

    //最大値
    const int MAX = 2;
    //最小値
    const int MIN = 0;


    // Start is called before the first frame update
    void Start()
    { 
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            starFlag = true;
        }
    }
}