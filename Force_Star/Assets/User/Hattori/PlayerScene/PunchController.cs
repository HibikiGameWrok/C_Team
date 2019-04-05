using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchController : MonoBehaviour
{
    private GameObject parent = null;

    private Rigidbody2D rigid2D;

    float punchSpeed = 0.5f;

    float maxPunchDistance = 5.0f;

    int key = 1;

    private Vector2 startPosition;

    float punchTimer = 0.0f;

    bool punchFlag = false;

    Vector3 keepPos;

    // Start is called before the first frame update
    void Awake()
    {
    }

    // Start is called before the first frame update
    void Start()
    {
        keepPos = this.transform.localPosition;
        parent = transform.root.gameObject;

        startPosition = transform.position;
        this.rigid2D = GetComponent<Rigidbody2D>();
    }

    void Move()
    {
        //GameObject.Find("PlayerBoal").GetComponent<PlayerController>();
        //this.rigid2D.AddForce(transform.right * key * punchSpeed);
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


        // ボタンを押します
        if (Input.GetKeyDown(KeyCode.X) || Input.GetKeyDown(KeyCode.K))
        {
            punchFlag = true;
        }

        // 押されたら一定距離までパンチします
        if (punchFlag == true)
        {
            punchTimer++;
            this.transform.localPosition = new Vector3(this.transform.localPosition.x - (Mathf.Sin(punchTimer * punchSpeed) * 3.0f), this.transform.localPosition.y, this.transform.localPosition.z);
        }

        //最大値まで行ったら止める
        if (((Mathf.Sin(punchTimer * punchSpeed) * 3.0f) < -2.8f))
        {
            punchFlag = false;
        }

        //止めたら戻す
        if (punchFlag == false)
        {
            punchTimer = 0;
            this.transform.localPosition = new Vector3(keepPos.x,this.transform.localPosition.y,this.transform.localPosition.z);
        }
    }
}
