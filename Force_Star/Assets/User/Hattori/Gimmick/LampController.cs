﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LampController : MonoBehaviour
{
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // プレイシーン共通ディレクター
    //*|***|***|***|***|***|***|***|***|***|***|***|
    PlaySceneDirectorIndex m_playIndex;

    Rigidbody2D rigid2D;

    [SerializeField]
    private GameObject fire;

    [SerializeField]
    private bool lightFlag = false;

    //SE
    private AudioSource sound01;

    // Start is called before the first frame update
    void Start()
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // プレイヤー共通ディレクター
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_playIndex = PlaySceneDirectorIndex.GetInstance();

        this.rigid2D = GetComponent<Rigidbody2D>();


        //SE再生データ
        sound01 = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (lightFlag)
        {
            fire.gameObject.SetActive(true);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "AttackBoal")
        {
            if (!fire.gameObject.activeInHierarchy)
            {
                //SEの再生
                sound01.PlayOneShot(sound01.clip);

                float posX1;
                float posY;
                posX1 = this.transform.position.x;
                posY = this.transform.position.y;

                //
                //*|***|***|***|***|***|***|***|***|***|***|***|
                // 星が出る *10
                //*|***|***|***|***|***|***|***|***|***|***|***|
                m_playIndex.ApplyStarBounce(new Vector2(posX1, posY + 8.0f), 10);
                //StarDirector starCreate;
                // starCreate.CreateOneStar(new Vector2(posX1, posY + 8.0f), 5, false,0.5f);
                lightFlag = true;
            }
        }
    }
}
