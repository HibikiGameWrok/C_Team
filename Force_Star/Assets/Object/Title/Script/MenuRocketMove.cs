using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuRocketMove : MonoBehaviour
{
    // 横の動き
    [SerializeField]
    float vel = -0.02f;

    // 墜落
    [SerializeField]
    float fallvel = -0.05f;

    //隕石と当たり判定のフラグ
    [SerializeField]
    bool moveFlag = false;


    //位置
    Vector2 pos = new Vector2(0.0f,0.0f);

    //大きさ
    [SerializeField]
    float size = 1.0f;

    //SE
    private AudioSource sound01;
    private AudioSource sound02;

    //音の大きさ
    float m_MySliderValue = 1.0f;

    private GameObject FadePanel;
    private StartFade startFade;

    // Start is called before the first frame update
    void Start()
    {
        //AudioSourceコンポーネントを取得し、変数に格納
        AudioSource[] audioSources = GetComponents<AudioSource>();
        sound01 = audioSources[0];
        sound02 = audioSources[1];

        FadePanel = GameObject.Find("Panel");
        startFade = FadePanel.GetComponent<StartFade>();

        pos = new Vector2(this.transform.position.x,this.transform.position.y);
    }

    // Update is called once per frame
    void Update()
    {
        if (moveFlag == false)
        {
            //横移動のみ
            RocketMove();
        }
        else
        {
            //斜め移動
            RocketNextMove();
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        moveFlag = true;
        //爆発音の再生
        sound01.PlayOneShot(sound01.clip);

        //墜落音の再生
        sound02.PlayOneShot(sound02.clip);
    }

    void RocketMove()
    {
        pos.x += vel;

        if (pos.x < -10.0f)
        {
           pos.x = 9.0f;
        }

        //位置の移動
        transform.position = new Vector3(pos.x, pos.y, transform.position.z);
    }

    void RocketNextMove()
    {
        m_MySliderValue += -0.005f;

        pos += new Vector2(fallvel, fallvel);

        //位置の移動
        transform.position = new Vector3(pos.x, pos.y, transform.position.z);

        //角度の変更
        //回転する
        this.transform.rotation = Quaternion.Euler(0.0f, 0.0f, 45.0f);

        if(size <= 0.5f)
        {
            startFade.SetFadeOutFlag(true);
        }
        //大きさを徐々に小さく
        size += -0.003f;
        gameObject.transform.localScale = new Vector3(size, size, transform.localScale.z);

    }


    public bool GetMoveFlag()
    {
        return moveFlag;
    }

    void OnGUI()
    {
        //オーディオの音量をスライダの値と一致させます。
        sound02.volume = m_MySliderValue;
    }
}
