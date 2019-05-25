using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoketClearMove : MonoBehaviour
{
    public const float STICKPOW = -0.5f;

    // フェードインするオブジェクト
    private GameObject Panel = null;
    // フェードインアウトするscript
    private StartFade StartFade = null;

    // クールタイム
    bool cooltime = false;

    // プレイヤー
    private GameObject player = null;

    // コントロール
    private PlayerController playercont;

    // プレイヤーのデータ倉庫
    private PlayerDirectorIndex playerIndex;

    // プレイシーンのデータ倉庫
    private PlaySceneDirectorIndex playDrectorIndex;

    // 飛ぶロケットオブジェクト
    private GameObject Roket_sky;
    // ロケットが飛ぶコルーチンが入ったスクリプト
    private RoketTakeOff roketTakeOff;

    private bool oneFlag = false;
    //private bool delUIFlag = false;
    private bool endFlag = false;

    void Awake()
    {
        playDrectorIndex = PlaySceneDirectorIndex.GetInstance();
        playerIndex = PlayerDirectorIndex.GetInstance();
        playercont = new PlayerController();
        player = GameObject.Find("Player");
    }

    // Start is called before the first frame update
    void Start()
    {
        // オブジェクトの取得
        Panel = GameObject.Find("Panel");
        StartFade = Panel.GetComponent<StartFade>();
    }

    // Update is called once per frame
    void Update()
    {
        // コントロール更新
        playercont.Update();

        // 飛ぶロケットが生成されている時
        if (Roket_sky != null)
        {
            // 毎フレームスクリプト内のフラグ取得関数を変数に保存
            endFlag = roketTakeOff.GetEndCoruFlag();

            // コルーチンが終わったら
            if (endFlag == true)
            {
                // クリアしたと設定する
                playDrectorIndex.SetClearFlag();
            }
        }


            // ディレクターが消す様にしてくれる //
            // ☆が目標量を達成して尚且つパーツが全て取得できているならば
            //if (playerIndex.GetHaveStarParsent() >= 1 && playerIndex.GetHaveAllPartsFlag())
            //{
            //    if (oneFlag == false)
            //    {
            //        GameObject okUI = (GameObject)Resources.Load("paneru");
            //        Instantiate(okUI, new Vector3(this.transform.position.x, this.transform.position.y + 10, this.transform.position.z), Quaternion.identity);
            //        // 一回だけ生成させるようにする。
            //        oneFlag = true;
            //    }
            //}
        }

    void OnTriggerStay2D(Collider2D col)
    {
        string layerName = LayerMask.LayerToName(col.gameObject.layer);
        // ロケットに当たっている時
        if (layerName == "PlayerDigid" || col.gameObject.tag == "Player")
        {
            // ☆が目標量を達成して尚且つパーツが全て取得できているならば
            if (playerIndex.GetHaveStarParsent() >= 1 && playerIndex.GetHaveAllPartsFlag())
            {
                // コントロールのスティックの情報
                Vector2 stick = playercont.ChackStickPower();
                if (cooltime == false)
                {
                    // ↑orWを押すとシーン移行
                    if (stick.y < STICKPOW || Input.GetKeyDown(KeyCode.UpArrow))
                    {
                        cooltime = true;
                        // フェードアウト
                        StartFade.SetFadeOutFlag(true);

                        //delUIFlag = true;

                        // コルーチンを実行  
                        StartCoroutine("ClearMove");
                    }
                }
            }
        }
    }

    //public bool GetdelUIFlag()
    //{
    //    return delUIFlag;
    //}

    // コルーチン ]
    // 1回だけ呼ばれる為毎フレーム更新されない様になっている 
    private IEnumerator ClearMove()
    {
        // ログ出力  
        Debug.Log("ロケットに☆を渡した");

        // 1秒待つ  
        yield return new WaitForSeconds(3.0f);

        if (player != null)
        {
            // カメラのターゲットをロケットに変更
            playDrectorIndex.SetObjectTargetCamera(this.gameObject);
        }

        // ディレクターが消してくれる // 
        // プレイヤーを消す
        //player.SetActive(false);
        //player = null;

        // フェードイン
        StartFade.SetFadeInFlag(true);

        // 1秒待つ  
        yield return new WaitForSeconds(3.0f);

        // 自身のスプライトコンポーネントを削除
        Destroy(GetComponent<SpriteRenderer>());

        // 自身の子オブジェクトを削除
        foreach (Transform c in this.transform)
        {
            // 子の煙エフェクトを消す
            GameObject.Destroy(c.gameObject);
        }

        //// ☆の破裂プレハブをGameObject型で取得
        GameObject ExStar = (GameObject)Resources.Load("ExplosionStar");
        // ☆の破裂プレハブを元に生成
        Instantiate(ExStar, new Vector3(this.transform.position.x, this.transform.position.y + 3, this.transform.position.z), Quaternion.identity);

        //// ロケットプレハブをGameObject型で取得
        Roket_sky = (GameObject)Resources.Load("SkyRoket");
        // ロケットプレハブを元に生成
        Instantiate(Roket_sky, new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z), Quaternion.identity);

        // 生成された後に飛ぶロケットの親を取得
        Roket_sky = GameObject.Find("SkyRoket(Clone)");
        // スクリプト取得
        roketTakeOff = Roket_sky.transform.GetChild(0).GetComponent<RoketTakeOff>();

        // 5秒待つ  
        yield return new WaitForSeconds(4.0f);
        Debug.Log("ロケットが飛んだ");
        // フェードアウト
        StartFade.SetFadeOutFlag(true);
    }
}
