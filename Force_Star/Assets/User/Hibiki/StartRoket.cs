using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartRoket : MonoBehaviour
{
    // 大きさ最低値
    private const float MIN_SIZE = 1.0f;

    // 速さ
    private float speed = 0.1f;

    // 大きさ
    [SerializeField]
    private float size = 1.0f;

    // 動いているか知るフラグ
    // true = 停止, false = 進行
    [SerializeField]
    private bool moveStopFlag = false;

    // 開始地点のポイントオブジェクト（対象物）
    [SerializeField]
    private GameObject[] StartRoketPoint = null;

    // フェードインするオブジェクト
    private GameObject Panel = null;
    // フェードインアウトするscript
    private StartFade StartFade = null;

    // 乱数を保管する変数
    private int randPoint;

    // ポイントオブジェクトの座標を保管する変数
    private Vector3 targetPos = Vector3.zero;

    // 開始時間
    private float startTime;

    // 対象物間のマーカー
    private float journeyLength;

    // メインカメラを取得するオブジェクト変数
    private GameObject ParentMainCamera;
    // 子のオブジェクトを取得する変数
    private Transform MainCamera;

    int count = 0;

    // Start is called before the first frame update
    void Start()
    {
        // Inspectorで設定したオブジェクトの数の乱数を決める
        randPoint = Random.Range(0, StartRoketPoint.Length);

        // 対象物の座標
        targetPos = StartRoketPoint[randPoint].transform.position;

        // 開始時間を保管
        startTime = Time.time;

        // ロケットと対象物の距離を保管
        journeyLength = Vector3.Distance(this.transform.position, targetPos);

        // オブジェクトの取得
        ParentMainCamera = GameObject.Find("ParentMainCamera");
        // 子の取得
        MainCamera = ParentMainCamera.transform.Find("Main Camera");

        // オブジェクトの取得
        Panel = GameObject.Find("Panel");
        StartFade = Panel.GetComponent<StartFade>();
    }

    // Update is called once per frame
    void Update()
    {
        // 距離感の速度
        float distCovered = (Time.time - startTime) * 0.001f;

        // 移動を止める処理
        if (moveStopFlag != true)
        {
            // 補間移動
            transform.position = Vector3.Lerp(this.transform.position, targetPos, distCovered);
            if (count != 3)
            {
                // コルーチンを実行  
                StartCoroutine("PartsInstance");
            }
        }
    }

    void OnTriggerEnter2D(Collider2D col2D)
    {
        // 
        if (col2D.gameObject.tag == "StartRoketPoint")
        {
            if (moveStopFlag != true)
            {
                // 壊れたロケットプレハブをGameObject型で取得
                GameObject Rocket = (GameObject)Resources.Load("Rocket_1");
                // 壊れたロケットプレハブを元に、インスタンスを生成、
                Instantiate(Rocket, targetPos, Quaternion.identity);

                // サブカメラが消える前にメインカメラを起動する
                if (MainCamera != null)
                {
                    MainCamera.gameObject.SetActive(true);
                }

                // 動きを止める
                moveStopFlag = true;
            }

            // 自身のオブジェクトを消す
            Destroy(this.gameObject);
        }
    }

    // コルーチン  
    private IEnumerator PartsInstance()
    {
        // コルーチンの処理  
        // 1秒待つ  
        yield return new WaitForSeconds(1.0f);
        if (count == 0)
        {
            //// ファイアプレハブをGameObject型で取得
            GameObject RoketParts0 = (GameObject)Resources.Load("RocketParts_0");
            // ファイアプレハブを元に、インスタンスを生成、
            Instantiate(RoketParts0, this.transform.position, Quaternion.identity);
            count = 1;
        }
        // 1秒待つ  
        yield return new WaitForSeconds(1.0f);
        if (count == 1)
        {
            //// ファイアプレハブをGameObject型で取得
            GameObject RoketParts1 = (GameObject)Resources.Load("RocketParts_1");
            // ファイアプレハブを元に、インスタンスを生成、
            Instantiate(RoketParts1, this.transform.position, Quaternion.identity);
            count = 2;
        }
        // 1秒待つ  
        yield return new WaitForSeconds(1.0f);
        if (count == 2)
        {
            //// ファイアプレハブをGameObject型で取得
            GameObject RoketParts2 = (GameObject)Resources.Load("RocketParts_2");
            // ファイアプレハブを元に、インスタンスを生成、
            Instantiate(RoketParts2, this.transform.position, Quaternion.identity);
            count = 3;
        }
        yield return new WaitForSeconds(1.0f);
        StartFade.SetFadeOutFlag(true);
    }
}
