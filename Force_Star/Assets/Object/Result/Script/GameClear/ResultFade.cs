using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //パネルのイメージを操作するのに必要
using UnityEngine.SceneManagement;

public class ResultFade : MonoBehaviour
{
    public ImageDisplaySwitching gameClear;
    public OverFlashingImage gameOver;

    float fadeSpeed = 0.02f;        //透明度が変わるスピードを管理
    float red, green, blue, alfa;   //パネルの色、不透明度を管理

    public bool isFadeOut = false;  //フェードアウト処理の開始、完了を管理するフラグ
    public bool isFadeIn = false;   //フェードイン処理の開始、完了を管理するフラグ

    Image fadeImage;                //透明度を変更するパネルのイメージ

    bool compFlag = false;

    bool clearFlag = false;
    bool overFlag = false;

    bool activeFlag = false;

    void Start()
    {
        clearFlag = gameClear.GetStageFlag();

        fadeImage = GetComponent<Image>();
        red = fadeImage.color.r;
        green = fadeImage.color.g;
        blue = fadeImage.color.b;
        alfa = fadeImage.color.a;
    }

    void Update()
    {
        clearFlag = gameClear.GetStageFlag();
        overFlag = gameOver.GetStageFlag();

        if (isFadeIn)
        {
            StartFadeIn();
        }

        if (isFadeOut)
        {
            StartFadeOut();
        }

        if (clearFlag == true || overFlag == true)
        {
            if(isFadeIn == false)
            {
                isFadeOut = true;
            }   
        }
    }



    void StartFadeIn()
    {
        alfa -= fadeSpeed;                //a)不透明度を徐々に下げる
        SetAlpha();                      //b)変更した不透明度パネルに反映する
        if (alfa <= 0)
        {                    //c)完全に透明になったら処理を抜ける
            isFadeIn = false;
            //fadeImage.enabled = false;    //d)パネルの表示をオフにする
            activeFlag = true;
        }
    }

    void StartFadeOut()
    {
        //fadeImage.enabled = true;  // a)パネルの表示をオンにする
        alfa += fadeSpeed;         // b)不透明度を徐々にあげる
        SetAlpha();               // c)変更した透明度をパネルに反映する
        if (alfa >= 1)
        {            
            // d)完全に不透明になったら処理を抜ける
            isFadeOut = false;
            compFlag = true;
        }
        else
        {
            compFlag = false;
        }
    }

    void SetAlpha()
    {
        fadeImage.color = new Color(red, green, blue, alfa);
    }

    public float GetAlfa()
    {
        return alfa;
    }

    public bool GetCompFlag()
    {
        return compFlag;
    }

    public bool GetActiveFlag()
    {
        return activeFlag;
    }

    public bool GetFadeOutFlag()
    {
        return isFadeOut;
    }

    public bool GetFadeInFlag()
    {
        return isFadeIn;
    }

    public void SetFadeFlag(bool fadeInFlag = true)
    {
        if(fadeInFlag == true)
        {
            isFadeIn = true;
        }
        else
        {
            isFadeOut = true;
        }
    }
}
