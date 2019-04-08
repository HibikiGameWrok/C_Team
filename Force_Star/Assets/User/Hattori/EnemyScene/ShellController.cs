using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellController : MonoBehaviour
{
    public GameObject starDirec;
    private StarDirector starCreate;


    SpriteRenderer shellSprite;
    [SerializeField]
    private Sprite wait_Image;
    [SerializeField]
    private Sprite action_Image;
    [SerializeField]
    private Sprite damage_Image;

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

    bool playerApproachFlag = false;

    // Start is called before the first frame update
    void Start()
    {
        shellRenderer = GetComponent<Renderer>();
        shellSprite = gameObject.GetComponent<SpriteRenderer>();
        starCreate = starDirec.GetComponent<StarDirector>();
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        //当たり判定
        if (col.gameObject.tag == "Player")
        {
            //Debug.Log("ガブガブガブ");
        }
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (playerApproachFlag == true)
        {
            if (col.gameObject.tag == "AttackBoal")
            {
                float posX1;
                float posX2;
                float posY;
                posX1 = this.transform.position.x + this.GetComponent<Renderer>().bounds.size.x / 2 + 3;
                posX2 = this.transform.position.x - this.GetComponent<Renderer>().bounds.size.x / 2 - 3;
                posY = this.transform.position.y - this.GetComponent<Renderer>().bounds.size.y / 2;

                // ☆を生成
                starCreate.CreateStar(new Vector2(posX1, posY), new Vector2(posX2, posY), 10);
                Destroy(this.gameObject);
            }
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
            playerApproachFlag = true;
            GetComponent<Rigidbody2D>().velocity = (direction * speed);
            //shellRenderer.material.color = Color.red;
            shellSprite.sprite = action_Image;
        }
        //プレイヤーが貝の射程範囲外なら攻撃しない
        else
        {
            playerApproachFlag = false;
            //shellRenderer.material.color = Color.blue;
            shellSprite.sprite = wait_Image;
            //Debug.Log("届かない");
        }
    }
}
