﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalController : MonoBehaviour
{
    private AudioSource sound;

    public GameObject starDirec;

    private StarDirector starCreate;

    SpriteRenderer render;

    Rigidbody2D rigid2D;

    //星を出す回数
    private int crystalCount = 0;

    //星を出す最大数
    [SerializeField]
    private int maxCrystalCount = 5;

    //光らせるフラグ
    private bool flashFlag = false;

    //透明度
    private float flashCrystale = 1.0f;

    //点滅時間
    private float flashCount = 0.0f;

    //最大点滅時間
    [SerializeField]
    private float flashTimer = 600.0f;

    private float colorR = 1.0f;

    private float colorG = 1.0f;

    private float colorB = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        this.rigid2D = GetComponent<Rigidbody2D>();

        starCreate = starDirec.GetComponent<StarDirector>();

        render = gameObject.GetComponent<SpriteRenderer>();

        sound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(flashFlag == true)
        {
            //点滅
            flashCrystale = Mathf.Sin(Time.time * 100.0f);
        }
        else
        {
            //普通の透明度
            flashCrystale = 1.0f;
        }

        //透明度とかを更新
        render.color = new Color(colorR, colorG, colorB, flashCrystale);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "AttackBoal")
        {
            //点滅時間初期化
            flashCount = 0.0f;

            //点滅フラグをON
            flashFlag = true;

            //点滅回数 < Ｍａｘ点滅回数
            if (crystalCount < maxCrystalCount)
            {
                sound.PlayOneShot(sound.clip);

                float posX1;
                float posY;
                posX1 = this.transform.position.x;
                posY = this.transform.position.y;

                starCreate.CreateOneStar(new Vector2(posX1, posY), 5, false, 0.5f);

                //点滅回数をカウント
                crystalCount += 1;
            }

            //点滅回数 > Ｍａｘ点滅回数
            else
            {
                flashFlag = false;

                colorR = 0.5f;

                colorG = 0.5f;

                colorB = 0.5f;
            }
         }
    }
}
