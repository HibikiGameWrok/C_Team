﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoketTakeOff : MonoBehaviour
{
   private PlaySceneDirectorIndex directorIndex;

    // 自身からどれくらい飛ばすか
    [SerializeField]
    private float heightPos = 50.0f;

    // 計算座標
    private Vector3 storagePos;

    // 速度
    [SerializeField]
    private float speed = 0.1f;

    // 開始時間
    private float startTime;

    // 対象物間のマーカー
    private float journeyLength;

    // 動きを止める
    bool stopFlag = false;
    bool stopFlag1 = false;

    float sinVecX = 0.0f;

    // コルーチンを終了したと分かるフラグ
    private bool endCoruFlag = false;

    //SE
    private AudioSource sound01;

    void Awake()
    {
        directorIndex = PlaySceneDirectorIndex.GetInstance();

        // 開始時間を保管
        startTime = Time.time;

        // 着地地点からの高さを対象物として保管
        storagePos = new Vector3(this.transform.position.x, this.transform.position.y + heightPos, this.transform.position.z);

        // ロケットと対象物の距離を保管
        journeyLength = Vector3.Distance(this.transform.position, storagePos);

        directorIndex.SetObjectTargetCamera(this.transform.parent.gameObject);

         sinVecX = this.transform.position.x;

                //SE再生データ
        sound01 = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        // コルーチンを実行  
        StartCoroutine("TakeOff");
    }

    private IEnumerator TakeOff()
    {
        // 1秒待つ  
        yield return new WaitForSeconds(1.0f);

        if (stopFlag == false)
        {
            // 震える
            this.transform.position = new Vector3(Mathf.Sin(30.0f * Mathf.PI * 1f * Time.time) + sinVecX, this.transform.position.y, this.transform.position.z);
        }
        // 1秒待つ  
        yield return new WaitForSeconds(2.0f);

        if (stopFlag == false)
        {
            //SEの再生
            sound01.PlayOneShot(sound01.clip);
        }

        stopFlag = true;

        // 0.1秒待つ  
        yield return new WaitForSeconds(0.1f);

        // 距離感の速度
        float distCovered = (Time.time - startTime) * speed;

        Vector3 bpos = new Vector3(sinVecX, this.transform.position.y, this.transform.position.z); 

        // 飛ばす(補間移動)
        this.transform.parent.position = Vector3.Lerp(bpos, storagePos, distCovered);

        if (stopFlag1 == false)
        {
            // ロケットプレハブをGameObject型で取得
            GameObject StartJetPreFab = (GameObject)Resources.Load("StartJetPreFab");
            //ロケットプレハブを元に生成
            Instantiate(StartJetPreFab, new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z), Quaternion.identity);
            stopFlag1 = true;
        }
        
        yield return new WaitForSeconds(3.0f);

        // コルーチン終了しました
        endCoruFlag = true;
    }

    // コルーチンが終わったと知らせる関数
    public bool GetEndCoruFlag()
    {
        return endCoruFlag;
    }
}

