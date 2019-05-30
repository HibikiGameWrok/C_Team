using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    // TitleSceneのBGM管理オブジェクトを取得する変数
    private GameObject TitleBGMMane;

    // 親のBGMオブジェクト
    private GameObject ParentBGM;
    // 子のBGMオブジェクト
    private Transform BGM;

    // メインカメラを取得するオブジェクト変数
    private GameObject ParentMainCamera;
    // 子のオブジェクトを取得する変数
    private Transform MainCamera;

    // メインカメラを取得するオブジェクト変数
    private GameObject ParentPlayDirector;
    // 子のオブジェクトを取得する変数
    private Transform PlayDirector;

    // 背景の親を取得するオブジェクト
    private GameObject ParentBG;
    // 子の背景オブジェクトを取得する変数
    private Transform BackGround;

    // 敵をまとめている親オブジェクト
    private GameObject ParentEnemys;
    // 子の敵オブジェクトを取得する変数
    private Transform Enemys;

    // 手順を示す変数
    int count = 0;

    ///////////////////////////////////////
    //// プレイヤー周辺用オブジェクト
    // プレイヤーの星回収用オブジェクトの格納
    [SerializeField]
    private GameObject PlayerStarCheckColi = null;
    StarSearch starSearch;
    ///////////////////////////////////////

    private GameObject skipUI;

    // コントロールを管理しているクラス
    PlayerController playercont;

    void Awake()
    {
        TitleBGMMane = GameObject.Find("BGMManager");
    }

    // Start is called before the first frame update
    void Start()
    {
        // コントロールスクリプトを生成
        playercont = new PlayerController();

        // Inspectorで設定したオブジェクトの数の乱数を決める
        randPoint = Random.Range(0, StartRoketPoint.Length);

        // 対象物の座標
        targetPos = StartRoketPoint[randPoint].transform.position;
        // 開始時間を保管
        startTime = Time.time;
        // ロケットと対象物の距離を保管
        journeyLength = Vector3.Distance(this.transform.position, targetPos);

        // 親オブジェクトの取得
        ParentBGM = GameObject.Find("ParentBGM");
        // 子の取得
        BGM = ParentBGM.transform.Find("BGM");

        // 親オブジェクトの取得
        ParentMainCamera = GameObject.Find("ParentMainCamera");
        // 子の取得
        MainCamera = ParentMainCamera.transform.Find("Main Camera");

        // 親オブジェクトの取得
        ParentPlayDirector = GameObject.Find("ParentPlayDirector");
        // 子の取得
        PlayDirector = ParentPlayDirector.transform.Find("PlayDirector");

        // 親オブジェクトの取得
        ParentBG = GameObject.Find("BackGround");
        // 子の取得
        BackGround = ParentBG.transform.Find("Sea_BackGround_Image");

        // 親オブジェクトの取得
        ParentEnemys = GameObject.Find("Enemys");

        // オブジェクトの取得
        Panel = GameObject.Find("Panel");
        StartFade = Panel.GetComponent<StartFade>();

        // スキップUI
        skipUI = GameObject.Find("skipUI");

        //*|***|***|***|***|***|***|***|***|***|***|***|
        // データ登録
        //*|***|***|***|***|***|***|***|***|***|***|***|
        PlaySceneDirectorRocketIndex rocketIn = PlaySceneDirectorRocketIndex.GetInstance();
        rocketIn.SetPointerFadeData(StartFade);
        rocketIn.SetPointerBGMObjectData(BGM.gameObject);

        // 星の回収用スクリプトの格納
        starSearch = PlayerStarCheckColi.GetComponent<StarSearch>();
    }

    // Update is called once per frame
    void Update()
    {
        // コントロール管理クラスを更新
        playercont.Update();

        // 距離感の速度
        float distCovered = (Time.time - startTime) * 0.001f;
        // 補間移動
        transform.position = Vector3.Lerp(this.transform.position, targetPos, distCovered);

        if (moveStopFlag != true)
        {
            // コントローラーXボタンかXキーが押された時
            if (playercont.ChackAttack() || (Input.GetKeyDown(KeyCode.X)))
            {
                // フェードインが終わった時
                if (StartFade.GetFadeInFlag() != true)
                {
                    // スキップするコルーチンを実行
                    StartCoroutine("SkipMove");
                }
            }
        }

        // 移動を止める処理
        if (moveStopFlag != true)
        {
            if (count == 0)
            {
                // コルーチンを実行  
                StartCoroutine("PartsInstance");
            }
        }
    }

    private IEnumerator SkipMove()
    {
        // フェードアウト
        StartFade.SetFadeOutFlag(true);
        // 動きを止める
        moveStopFlag = true;

        // 1.0F待つ
        yield return new WaitForSeconds(1.0f);

        // フェードアウトの動作していない時
        if (StartFade.GetFadeOutFlag() != true)
        {
            count = 4;
            // オブジェクトをアクティブ化
            OnActiveObj();
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
            GameObject RoketParts0 = (GameObject)Resources.Load("StartParts_0");
            // ファイアプレハブを元に、インスタンスを生成、
            Instantiate(RoketParts0, this.transform.position, Quaternion.identity);
            count = 1;
        }
        // 1秒待つ  
        yield return new WaitForSeconds(1.0f);
        if (count == 1)
        {
            //// ファイアプレハブをGameObject型で取得
            GameObject RoketParts1 = (GameObject)Resources.Load("StartParts_1");
            // ファイアプレハブを元に、インスタンスを生成、
            Instantiate(RoketParts1, this.transform.position, Quaternion.identity);
            count = 2;
        }
        // 1秒待つ  
        yield return new WaitForSeconds(1.0f);
        if (count == 2)
        {
            //// ロケットプレハブをGameObject型で取得
            GameObject RoketParts2 = (GameObject)Resources.Load("StartParts_2");
            // ロケットプレハブを元に生成
            Instantiate(RoketParts2, this.transform.position, Quaternion.identity);
            count = 3;
        }
        yield return new WaitForSeconds(2.0f);
        if (count == 3)
        {
            StartFade.SetFadeOutFlag(true);
            count = 4;
        }
        yield return new WaitForSeconds(2.0f);
        OnActiveObj();
    }

    // オブジェクトをアクティブ化
    void OnActiveObj()
    {
        // サブカメラが消える前にメインカメラを起動する
        if (MainCamera != null)
        {
            // プレイシーン全体を管理するオブジェクトを起動
            PlayDirector.gameObject.SetActive(true);
            // プレイヤーとロケットを１回だけ生成
            if (count == 4)
            {
                //// プレイヤープレハブをGameObject型で取得
                GameObject Player = (GameObject)Resources.Load("PlayerDirector");
                // プレイヤープレハブを元に生成
                Instantiate(Player, Vector3.zero, Quaternion.identity);
                starSearch.CheckFlag();

                // 壊れたロケットプレハブをGameObject型で取得
                GameObject Rocket = (GameObject)Resources.Load("Rocket_1");
                // 壊れたロケットプレハブを元に、インスタンスを生成、
                Instantiate(Rocket, targetPos, Quaternion.identity);

                count = 5;
            }
            // メインカメラをアクティブにする
            MainCamera.gameObject.SetActive(true);
            // 背景をアクティブ
            BackGround.gameObject.SetActive(true);
            // 敵を全てアクティブにする
            foreach (Transform Enemys in ParentEnemys.transform)
            {
                Enemys.gameObject.SetActive(true);
            }

            // BGMを切り替える
            BGM.gameObject.SetActive(true);

            Debug.Log(skipUI.name);
            Destroy(skipUI.gameObject);

            // フェードインをする
            StartFade.SetFadeInFlag(true);


            // TitleBGMオブジェクトを削除
            Destroy(TitleBGMMane);

            // 自身のオブジェクトを消す
            Destroy(this.gameObject);
        }
    }
}
