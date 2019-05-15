using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoketTakeOff : MonoBehaviour
{
    private GameObject mainCamera;
    private TargetFollow targetfollow;

    // 自身からどれくらい飛ばすか
    [SerializeField]
    private Vector3 diffPos = new Vector3(0.0f,50.0f,0.0f);

    // 計算座標
    private Vector3 calcPos;

    // 速度
    [SerializeField]
    private float speed = 0.1f;

    // 開始時間
    private float startTime;

    // 対象物間のマーカー
    private float journeyLength;

    // 動きを止める
    bool stop = false;

    void Awake()
    {
        mainCamera = GameObject.Find("Main Camera");
        targetfollow = mainCamera.GetComponent<TargetFollow>();

        // 計算
        calcPos = this.transform.position + diffPos;

        // 開始時間を保管
        startTime = Time.time;

        // ロケットと対象物の距離を保管
        journeyLength = Vector3.Distance(this.transform.position, calcPos);
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
        if (stop == false)
        {
            // 震える
            this.transform.position = new Vector3(Mathf.Sin(30.0f * Mathf.PI * 1f * Time.time) + this.transform.position.x, this.transform.position.y, this.transform.position.z);
        }
        // 1秒待つ  
        yield return new WaitForSeconds(3.0f);
        stop = true;
        // 追従対象を変える
        targetfollow.SetTarget(this.gameObject);

        // 距離感の速度
        float distCovered = (Time.time - startTime) * speed;

        // 飛ばす(補間移動)
        transform.position = Vector3.Lerp(this.transform.position, calcPos, distCovered);
        //// ロケットプレハブをGameObject型で取得
        //GameObject StartJetPreFab = (GameObject)Resources.Load("StartJetPreFab");
        // ロケットプレハブを元に生成
        //Instantiate(StartJetPreFab, new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z), Quaternion.identity);

        // 作成したオブジェクトを子として登録
        //StartJetPreFab.transform.SetParent(this.transform);
        StopCoroutine("TakeOff");
    }
}
