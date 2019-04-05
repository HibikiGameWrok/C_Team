using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellController : MonoBehaviour
{
    //追跡ターゲット(プレイヤー)
    [SerializeField]
    private GameObject player = null;

    //追跡速度
    [SerializeField]
    private float speed = 0.05f;

    //追跡範囲
    [SerializeField]
    private float range = 9.0f;

    //攻撃してくるか色別判定する
    Renderer shellRenderer;

    // Start is called before the first frame update
    void Start()
    {
        shellRenderer = GetComponent<Renderer>();
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        //当たり判定
        if (col.gameObject.tag == "Player")
        {
            //Debug.Log("ガブガブガブ");
        }
    }

    // Update is called once per frame
    void Update()
    {
        //貝の向かう方向を決める
        Vector2 direction = new Vector2(this.player.transform.position.x - transform.position.x, transform.position.y);

        //貝とプレイヤーの距離
        float length = this.transform.position.x - this.player.transform.position.x;

        //プレイヤーが貝の射程範囲に入った時攻撃を開始する
        if (-range < length && length < range)
        {
            //Debug.Log("嚙みついてやる");
            GetComponent<Rigidbody2D>().velocity = (direction * speed);
            shellRenderer.material.color = Color.red;
        }
        //プレイヤーが貝の射程範囲外なら攻撃しない
        else
        {
            shellRenderer.material.color = Color.blue;
            //Debug.Log("届かない");
        }
    }
}
