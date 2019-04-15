using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathMove : MonoBehaviour
{
    Rigidbody2D rigid2D;

    private Collider2D collider;

    private bool gravityFlag = false;

    [SerializeField]
    private float gravityForce = 1.0f;

    [SerializeField]
    float jumpForce = 300.0f;

    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        this.rigid2D = gameObject.GetComponent<Rigidbody2D>();
        this.collider = gameObject.GetComponent<Collider2D>();
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter2D(Collision2D col)
    {
        //タグで当たり判定を管理する
        if (col.gameObject.tag == "Player")
        {
            float downVel = player.transform.position.x - this.transform.position.x;
            downVel *= -1;
            gravityFlag = true;
            collider.enabled = false;
            rigid2D.gravityScale = gravityForce;
            Debug.Log(downVel);
            this.rigid2D.AddForce(transform.up * this.jumpForce + transform.right * downVel * 100.0f);
        }
    }
}
