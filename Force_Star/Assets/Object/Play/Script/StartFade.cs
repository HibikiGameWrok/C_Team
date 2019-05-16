using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //パネルのイメージを操作するのに必要

public class StartFade : MonoBehaviour
{
    // Imageの赤青緑の値を最大値で固定
    public const float MAX_RBG = 1;

    float fadeSpeed = 0.03f;        //透明度が変わるスピードを管理
    float alfa;                     //不透明度を管理

    public bool isFadeOut = false;  //フェードアウト処理の開始、完了を管理するフラグ
    public bool isFadeIn = true;   //フェードイン処理の開始、完了を管理するフラグ

    Image fadeImage = null;                //透明度を変更するパネルのイメージ

    // Start is called before the first frame update
    void Start()
    {
        fadeImage = GetComponent<Image>();

        alfa = fadeImage.color.a;
    }

    // Update is called once per frame
    void Update()
    {
        if (isFadeIn || isFadeOut != true)
        {
            StartFadeIn();
        }

        if (isFadeOut || isFadeIn != true)
        {
            StartFadeOut();
        }
    }

    void StartFadeIn()
    {
        alfa -= fadeSpeed;               //a)不透明度を徐々に下げる
        SetAlpha();                      //b)変更した不透明度パネルに反映する
        if (alfa <= 0)
        {
            //c)完全に透明になったら処理を抜ける
            isFadeIn = false;
            fadeImage.enabled = false;    //d)パネルの表示をオフにする
        }
    }

    void StartFadeOut()
    {

        fadeImage.enabled = true;  // a)パネルの表示をオンにする
        alfa += fadeSpeed;         // b)不透明度を徐々にあげる
        SetAlpha();               // c)変更した透明度をパネルに反映する
        if (alfa >= 1)
        {
            // d)完全に不透明になったら処理を抜ける
            isFadeOut = false;
        }
    }


    void SetAlpha()
    {
        fadeImage.color = new Color(MAX_RBG, MAX_RBG, MAX_RBG, alfa);
    }

    public bool GetFadeInFlag()
    {
        return isFadeIn;
    }

    public bool GetFadeOutFlag()
    {
        return isFadeOut;
    }

    public void SetFadeInFlag(bool isfadein)
    {
        if (isFadeOut != true)
        {
            isFadeIn = isfadein;
        }
    }

    public void SetFadeOutFlag(bool isfadeout)
    {
        if (isFadeIn != true)
        {
            isFadeOut = isfadeout;
        }
    }
}
