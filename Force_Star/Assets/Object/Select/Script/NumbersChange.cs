using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumbersChange : MonoBehaviour
{
    SpriteRenderer MainSpriteRenderer;


    //No1の画像
    public Sprite WaterStar;
    //No2の画像
    public Sprite FireStar;
    //No3の画像
    public Sprite MachineStar;

    //画像切り替え用のカウンター
    [SerializeField]
    int starCounter = 0;



    //最大値
    const int MAX = 2;
    //最小値
    const int MIN = 0;

    [SerializeField]
    bool starNumberFlag = false;

    // Start is called before the first frame update
    void Start()
    {
        MainSpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        //MainSpriteRenderer.sprite = WaterStar;
        starCounter = 0;
    }

    // Update is called once per frame
    void Update()
    {

        if (starNumberFlag == false)
        {
            ParenthesisMove();
            SwitchingImage();

        }
        else
        {
            Destroy(gameObject);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            starNumberFlag = true;
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
        //No1の描画
        if (starCounter == 0)
        {
            MainSpriteRenderer.sprite = WaterStar;
        }

        //No2の描画
        if (starCounter == 1)
        {
            MainSpriteRenderer.sprite = FireStar;
        }

        //No3の描画
        if (starCounter == 2)
        {
            MainSpriteRenderer.sprite = MachineStar;
        }

    }


    public int GetStarCounter()
    {
        return starCounter;
    }

    public bool GetStarNumberFlag()
    {
        return starNumberFlag;
    }
}
