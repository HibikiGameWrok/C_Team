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


    private PlayerController playercont;

    private PlayerDirectorIndex playerIndex;

    private PlaySceneDirectorIndex playDrectorIndex;

    private bool oneFlag = false;

    private bool delUIFlag = false;

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
        playercont.Update();
        // ☆が目標量を達成して尚且つパーツが全て取得できているならば
        if (playerIndex.GetHaveStarParsent() >= 1 && playerIndex.GetHaveAllPartsFlag())
        {
            if (oneFlag == false)
            {
                GameObject okUI = (GameObject)Resources.Load("paneru");
                Instantiate(okUI, new Vector3(this.transform.position.x, this.transform.position.y + 10, this.transform.position.z), Quaternion.identity);
                oneFlag = true;
            }
        }
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
                        delUIFlag = true;
                        // コルーチンを実行  
                        StartCoroutine("ClearMove");
                    }
                }
            }
        }
    }

    // コルーチン  
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
        // プレイヤーを消す
        player.SetActive(false);
        player = null;

        // フェードイン
        StartFade.SetFadeInFlag(true);
       
        // 1秒待つ  
        yield return new WaitForSeconds(3.0f);

        // 自身のスプライトコンポーネントを削除
        Destroy(GetComponent<SpriteRenderer>());

        // 自身の子オブジェクトを削除
        foreach (Transform c in this.transform)
        {
            GameObject.Destroy(c.gameObject);
        }

        //// ☆の破裂プレハブをGameObject型で取得
        GameObject ExStar = (GameObject)Resources.Load("ExplosionStar");
        // ☆の破裂プレハブを元に生成
        Instantiate(ExStar, new Vector3(this.transform.position.x, this.transform.position.y + 3, this.transform.position.z), Quaternion.identity);

        //// ロケットプレハブをGameObject型で取得
        GameObject Roket_2 = (GameObject)Resources.Load("SkyRoket");
        // ロケットプレハブを元に生成
        Instantiate(Roket_2, new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z), Quaternion.identity);


        // 5秒待つ  
        yield return new WaitForSeconds(5.0f);

        Debug.Log("ロケットが飛んだ");

        // フェードアウト
        StartFade.SetFadeOutFlag(true);

        Debug.Log("画面暗転");
    }

    public bool GetdelUIFlag()
    {
        return delUIFlag;
    }
}
