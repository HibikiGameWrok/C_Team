using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMVolumeChange : MonoBehaviour
{
   public TitleFade titleFade;

    //BGM
    private AudioSource m_MyAudioSource;

    //音の大きさ
    float m_MySliderValue;
    //音の最大値
    float max = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        //外部からの読み込み
        m_MySliderValue = titleFade.GetAlfa();
        //GameObjectからAudioSourceを取得
        m_MyAudioSource = GetComponent<AudioSource>();
        //起動時にAudioSourceに接続されているAudioClipを再生する
        m_MyAudioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        //外部からの読み込み
        m_MySliderValue = titleFade.GetAlfa();
        //最大値との差を入れる
        m_MySliderValue = max - m_MySliderValue;
    }

    void OnGUI()
    {
        //オーディオの音量をスライダの値と一致させます。
        m_MyAudioSource.volume = m_MySliderValue;
    }

}
