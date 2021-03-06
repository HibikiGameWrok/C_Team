﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchController : MonoBehaviour
{
    private float punchSpeed = 0.5f;

    float punchTimer = 0.0f;

    bool punchFlag = false;

    Vector3 keepPos;

    public bool attackCheck = false;

    // ダメージSE
    private AudioSource soundDamage;

    // Start is called before the first frame update
    void Awake()
    {
    }

    // Start is called before the first frame update
    void Start()
    {
        keepPos = this.transform.localPosition;

        AudioSource[] audioSources = GetComponents<AudioSource>();
        soundDamage = audioSources[0];
    }

    void Move()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // ボタンを押します
        if (Input.GetKeyDown(KeyCode.X) || Input.GetKeyDown(KeyCode.K))
        {
            punchFlag = true;
        }

        // 押されたら一定距離までパンチします
        if (punchFlag == true)
        {
            GetComponent<SpriteRenderer>().enabled = true;
            attackCheck = true;
            punchTimer++;
            this.transform.localPosition = new Vector3(this.transform.localPosition.x - (Mathf.Sin(punchTimer * punchSpeed) * 3.0f), this.transform.localPosition.y, this.transform.localPosition.z);
        }
        else
        {
            GetComponent<SpriteRenderer>().enabled = false;
            punchTimer = 0;
            this.transform.localPosition = new Vector3(keepPos.x, this.transform.localPosition.y, this.transform.localPosition.z);
            attackCheck = false;
        }

        //最大値まで行ったら止める
        if (((Mathf.Sin(punchTimer * punchSpeed) * 3.0f) < -2.8f))
        {
            punchFlag = false;
        }



    }

    void OnTriggerEnter2D(Collider2D col)
    {
        // ロケットに当たっている時
        if ((col.gameObject.tag == "Enemy")&&(attackCheck == true))
        {
            soundDamage.PlayOneShot(soundDamage.clip);
        }
    }


    public bool GetPunchFlag()
    {
        return punchFlag;
    }
}
