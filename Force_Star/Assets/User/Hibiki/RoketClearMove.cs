using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoketClearMove : MonoBehaviour
{
    // フェードインするオブジェクト
    private GameObject Panel = null;
    // フェードインアウトするscript
    private StartFade StartFade = null;

    // クールタイム
    bool cooltime = false;

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
        
    }

    void OnTriggerStay2D(Collider2D col)
    {
        // ロケットに当たっている時
        if (col.gameObject.tag == "Player")
        {
            if (cooltime == false)
            {
                //    // ↑orWを押すとシーン移行
                if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
                {
                    cooltime = true;
                    // フェードアウト
                    StartFade.SetFadeOutFlag(true);
                    // コルーチンを実行  
                    StartCoroutine("ClearMove");
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
        yield return new WaitForSeconds(5.0f);
        // フェードイン
        StartFade.SetFadeInFlag(true);
        
        //// ☆の破裂プレハブをGameObject型で取得
        GameObject GoodEff = (GameObject)Resources.Load("GoodEffect");
        // ☆の破裂プレハブを元に、インスタンスを生成、
        Instantiate(GoodEff, new Vector3(this.transform.position.x + 1, this.transform.position.y + 3, this.transform.position.z), Quaternion.identity);

        Debug.Log("ロケットの周りに☆が舞う");

        // 1秒待つ  
        yield return new WaitForSeconds(5.0f);

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
        Instantiate(ExStar, new Vector3(this.transform.position.x + 3.8f, this.transform.position.y + 3, this.transform.position.z), Quaternion.identity);

        //// ロケットプレハブをGameObject型で取得
        GameObject Roket_2 = (GameObject)Resources.Load("Rocket_2");
        // ロケットプレハブを元に生成
        Instantiate(Roket_2, new Vector3(this.transform.position.x + 3.8f, this.transform.position.y, this.transform.position.z), Quaternion.identity);

        Debug.Log("ロケットが治った");

        // 1秒待つ  
        yield return new WaitForSeconds(2.0f);

        Debug.Log("ロケットが飛んだ");

        yield return new WaitForSeconds(3.0f);

        Debug.Log("画面暗転");
        // フェードアウト
        StartFade.SetFadeOutFlag(true);

        // 1秒待つ  
        yield return new WaitForSeconds(5.0f);

        //シーン遷移
        SceneManager.LoadScene("ResultScene");

        Debug.Log("リザルトシーンへGO");
    }
}
