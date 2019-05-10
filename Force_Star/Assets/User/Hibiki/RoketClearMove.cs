using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoketClearMove : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
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
            //    // ↑orWを押すとシーン移行
            if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
            {
                // コルーチンを実行  
                StartCoroutine("ClearMove");
                //SceneManager.LoadScene("ResultScene");
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

        Debug.Log("ロケットが修復した");

        // 1秒待つ  
        yield return new WaitForSeconds(5.0f);

        Debug.Log("ロケットに乗った");

        // 1秒待つ  
        yield return new WaitForSeconds(5.0f);

        Debug.Log("ロケットが飛んだ");

        // 1秒待つ  
        yield return new WaitForSeconds(5.0f);

        Debug.Log("リザルトシーンへGO");
    }
}
