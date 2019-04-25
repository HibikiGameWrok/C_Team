using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //パネルのイメージを操作するのに必要
using UnityEngine.SceneManagement;

public class SelectFade : MonoBehaviour
{

    public NumbersChange ChangeNum;
    public MoveCameraSelect SelectCamera;

    float fadeSpeed = 0.02f;        //透明度が変わるスピードを管理
    float red, green, blue, alfa;   //パネルの色、不透明度を管理

    public bool isFadeOut = false;  //フェードアウト処理の開始、完了を管理するフラグ
    public bool isFadeIn = false;   //フェードイン処理の開始、完了を管理するフラグ

    Image fadeImage;                //透明度を変更するパネルのイメージ

    [SerializeField]
    int sceneNum = 0;

    bool sceneFlag = false;


    void Start()
    {
        sceneNum = ChangeNum.GetStarCounter();
        sceneFlag = SelectCamera.GetChangeFlag();

        fadeImage = GetComponent<Image>();
        red = fadeImage.color.r;
        green = fadeImage.color.g;
        blue = fadeImage.color.b;
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
        alfa -= fadeSpeed;                //a)不透明度を徐々に下げる
        SetAlpha();                      //b)変更した不透明度パネルに反映する
        if (alfa <= 0)
        {                    //c)完全に透明になったら処理を抜ける
            isFadeIn = false;
            fadeImage.enabled = false;    //d)パネルの表示をオフにする
        }
    }

    void StartFadeOut()
    {
        sceneNum = ChangeNum.GetStarCounter();

        fadeImage.enabled = true;  // a)パネルの表示をオンにする
        alfa += fadeSpeed;         // b)不透明度を徐々にあげる
        SetAlpha();               // c)変更した透明度をパネルに反映する
        if (alfa >= 1)
        {             // d)完全に不透明になったら処理を抜ける
            isFadeOut = false;

            //水の星
            if (sceneNum == 0)
            {
                SceneManager.LoadScene("PlayScene");
            }

            //火の星
            if (sceneNum == 1)
            {
                SceneManager.LoadScene("PlayScene");
            }

            //機械の星
            if (sceneNum == 2)
            {
                SceneManager.LoadScene("PlayScene");
            }

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

}
