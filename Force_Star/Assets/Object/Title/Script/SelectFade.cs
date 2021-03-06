﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //パネルのイメージを操作するのに必要
using UnityEngine.SceneManagement;

public class SelectFade : MonoBehaviour
{
    // Imageの赤青緑の値を最大値で固定
    public const float MAX_RBG = 1;

    public MoveCameraSelect SelectCamera;

    float fadeSpeed = 0.02f;        //透明度が変わるスピードを管理
    float alfa;   //不透明度を管理

    public bool isFadeOut = false;  //フェードアウト処理の開始、完了を管理するフラグ
    public bool isFadeIn = true;   //フェードイン処理の開始、完了を管理するフラグ

    Image fadeImage;                //透明度を変更するパネルのイメージ

    bool sceneFlag = false;

    void Start()
    {
        sceneFlag = SelectCamera.GetChangeFlag();

        fadeImage = GetComponent<Image>();

        alfa = fadeImage.color.a;
    }

    void Update()
    {
        sceneFlag = SelectCamera.GetChangeFlag();

        if (isFadeIn)
        {
            StartFadeIn();
        }

        if (isFadeOut)
        {
            StartFadeOut();
        }

        if(sceneFlag == true)
        {
            isFadeOut = true;
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
            ////シーン遷移
            //SceneManager.LoadScene("PlayScene");
        }
    }

    void SetAlpha()
    {
        fadeImage.color = new Color(MAX_RBG, MAX_RBG, MAX_RBG, alfa);
    }

    public float GetAlfa()
    {
        return alfa;
    }
}
