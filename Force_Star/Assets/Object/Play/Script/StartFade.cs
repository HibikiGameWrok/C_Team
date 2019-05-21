using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //パネルのイメージを操作するのに必要

public class StartFade : MonoBehaviour
{
    // Imageの赤青緑の値を最大値で固定
    public const float MAX_RBG = 1;

    float fadeSpeed = 0.05f;        //透明度が変わるスピードを管理
    float alpha;                     //不透明度を管理

    public bool isFadeOut = false;  //フェードアウト処理の開始、完了を管理するフラグ
    public bool isFadeIn = true;   //フェードイン処理の開始、完了を管理するフラグ

    public bool isFadeMove = true;   //フェード処理の完了を管理するフラグ
    public bool isFadeWark = true;   //フェード処理の実行中を管理するフラグ

    Image fadeImage = null;                //透明度を変更するパネルのイメージ

    // Start is called before the first frame update
    void Start()
    {
        fadeImage = GetComponent<Image>();

        alpha = fadeImage.color.a;
    }

    // Update is called once per frame
    void Update()
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 仕事中か？
        //*|***|***|***|***|***|***|***|***|***|***|***|
        isFadeWark = false;

        if (isFadeIn || isFadeOut != true)
        {
            UpdateFadeIn();
            isFadeWark = true;
        }

        if (isFadeOut || isFadeIn != true)
        {
            UpdateFadeOut();
            isFadeWark = true;
        }
    }

    void UpdateFadeIn()
    {
        alpha -= fadeSpeed;               //a)不透明度を徐々に下げる
        SetAlpha();                      //b)変更した不透明度パネルに反映する
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // アルファを０から１の間の数に修正する
        //*|***|***|***|***|***|***|***|***|***|***|***|
        alpha = ChangeData.Among(alpha, 0.0f, 1.0f);

        if (alpha <= 0.0f)
        {
            //c)完全に透明になったら処理を抜ける
            isFadeIn = false;
            fadeImage.enabled = false;    //d)パネルの表示をオフにする

            isFadeMove = true;
        }
        else
        {
            isFadeMove = false;
        }
    }

    void UpdateFadeOut()
    {

        fadeImage.enabled = true;  // a)パネルの表示をオンにする
        alpha += fadeSpeed;         // b)不透明度を徐々にあげる
        SetAlpha();               // c)変更した透明度をパネルに反映する
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // アルファを０から１の間の数に修正する
        //*|***|***|***|***|***|***|***|***|***|***|***|
        alpha = ChangeData.Among(alpha, 0.0f, 1.0f);

        if (alpha >= 1.0f)
        {
            // d)完全に不透明になったら処理を抜ける
            isFadeOut = false;

            isFadeMove = true;
        }
        else
        {
            isFadeMove = false;
        }
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // アルファの更新
    //*|***|***|***|***|***|***|***|***|***|***|***|
    void SetAlpha()
    {
        fadeImage.color = new Color(MAX_RBG, MAX_RBG, MAX_RBG, alpha);
    }

    public bool GetFadeInFlag()
    {
        return isFadeIn;
    }

    public bool GetFadeOutFlag()
    {
        return isFadeOut;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // フェードインアウトの速さ
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public void SetFadeSpeed(float speed)
    {
        fadeSpeed = speed;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // フェードインアウトの完了
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public bool GetFadeMoveFlag()
    {
        return isFadeMove;
    }
    public bool GetFadeWarkFlag()
    {
        return isFadeWark;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // アルファ強制変更
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public void SetAlphaValue(float alpha)
    {
        this.alpha = alpha;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // アルファを０から１の間の数に修正する
        //*|***|***|***|***|***|***|***|***|***|***|***|
        this.alpha = ChangeData.Among(this.alpha, 0.0f, 1.0f);
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
