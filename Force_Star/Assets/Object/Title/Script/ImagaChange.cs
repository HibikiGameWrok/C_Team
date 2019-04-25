using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImagaChange : MonoBehaviour
{
    SpriteRenderer MainSpriteRenderer;
 
    //水の星の画像
    public Sprite WaterStar;
    //火の星の画像
    public Sprite FireStar;
    //機械の星の画像
    public Sprite MachineStar;

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
        MainSpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        //MainSpriteRenderer.sprite = WaterStar;
    }

    // Update is called once per frame
    void Update()
    {
        if (starFlag == false)
        {
            ParenthesisMove();
            SwitchingImage();
        }
        else
        {

        }

        if (Input.GetKeyDown(KeyCode.Space) && starCounter == 0)
        {
            starFlag = true;
        }
    }


    void ParenthesisMove()
    {
      
        //カッコの移動
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            starCounter += 1;
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            starCounter += -1;
        }

        if (starCounter < MIN)
        {
            starCounter = MAX;
        }

        if (starCounter > MAX)
        {
            starCounter = MIN;
        }

    }

    void SwitchingImage()
    {
        //水の星の描画
        if (starCounter == 0)
        {
            MainSpriteRenderer.sprite = WaterStar;
        }

        //火の星の描画
        if (starCounter == 1)
        {
            MainSpriteRenderer.sprite = FireStar;
        }

        //機械の星の描画
        if (starCounter == 2)
        {
            MainSpriteRenderer.sprite = MachineStar;
        }

    }

}
