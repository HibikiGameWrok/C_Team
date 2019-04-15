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
    }

    // Update is called once per frame
    void Update()
    {
       if(gravityFlag == true)
        {
            gravityForce--;
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        //タグで当たり判定を管理する
        if (col.gameObject.tag == "Player")
        {
            //Vector2 downVel = player.transform.position - this.transform.position;
            //downVel.x *= 1;
            //downVel.y *= -1;
            gravityFlag = true;
            collider.enabled = false;
            rigid2D.gravityScale = gravityForce;
            this.rigid2D.AddForce(transform.up * this.jumpForce);
        }
    }
}
