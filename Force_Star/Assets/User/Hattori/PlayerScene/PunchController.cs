using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchController : MonoBehaviour
{
    Rigidbody2D rigid2D;

    float punchSpeed = 1.5f;

    float maxPunchDistance = 5.0f;

    int key = 0;

    // Start is called before the first frame update
    void Awake()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        this.rigid2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        //{
        //    key = 1;
        //}
        //if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        //{
        //    key = -1;
        //}

        float speedx = Mathf.Abs(this.rigid2D.velocity.x);

        if (Input.GetKeyDown(KeyCode.X) || Input.GetKeyDown(KeyCode.K))
        {
            this.rigid2D.AddForce(transform.right * key * this.punchSpeed);
            Debug.Log("ぱんち");
        }

        if (speedx > this.maxPunchDistance)
        {
            Debug.Log("Uターン");
            this.rigid2D.AddForce(transform.right * -key * this.punchSpeed);
        }

        if (speedx < 0)
        {
            this.rigid2D.velocity = Vector2.zero;
            rigid2D.isKinematic = true;
            Debug.Log("止まるよ");
        }
    }
}
