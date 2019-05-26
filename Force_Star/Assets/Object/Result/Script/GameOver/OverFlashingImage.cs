using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverFlashingImage : MonoBehaviour
{
    //public GameObject overImage;

    [SerializeField]
    float time = 0.0f;

    [SerializeField]
    int next = 0;

    //シーン切り替え用
    bool stageFlag = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ImageDisplay();

        //スペースキーが押されたとき
        if (Input.GetKeyDown(KeyCode.Space))
        {
            stageFlag = true;
        }

    }

    void ImageDisplay()
    {
        time += 0.03f;

        next = (int)time % 2;

        //if (next == 0)
        //{

        //    //overImage.SetActive(true);
        //}
        //else
        //{

        //    overImage.SetActive(false);
        //}
    }

    public bool GetStageFlag()
    {
        return stageFlag;
    }
}
