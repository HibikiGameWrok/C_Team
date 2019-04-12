using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingMove : MonoBehaviour
{
    //動くスピード
    [SerializeField]
    float moveSpeed = 0.0f;

    //往復移動の一定距離
    [SerializeField]
    float distance;

    //一定距離の最大
    [SerializeField]
    private float maxDistance = 2.0f;

    Rigidbody2D rigid2D;

    //[SerializeField]
    //public enum Seagull_State
    //{
    //    verticalMovement,
    //    lateralMovement,
    //    notMovement
    //}
    //
    //public Seagull_State seagull_state;

    //[SerializeField]
    //private float gravy;

    [SerializeField]
    private float deathTimer;

    private float deathCount = 0.0f;

    Vector2 downVel;

    float delTime;

    float delFxSize;

    float delFxSMass;

    float delFxSMin;

    float delFxSMax;

    // Start is called before the first frame update
    void Start()
    {
        this.rigid2D = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //if(seagull_state == Seagull_State.lateralMovement)
        //{
            //敵を移動させる
            distance += moveSpeed;
            transform.Translate(moveSpeed, 0, 0);

            //最大値まで行ったら反転
            if (distance > maxDistance)
            {
               moveSpeed = moveSpeed * -1;
               distance = 0.0f;
            }
            //最低値まで行ったら反転
            if (distance < -maxDistance)
            {
                moveSpeed = moveSpeed * -1;
                distance = 0.0f;
            }
        //if(seagull_state == Seagull_State.verticalMovement)
        //{
        //    //rigid2D.bodyType = RigidbodyType2D.Dynamic;
        //    rigid2D.gravityScale = gravy;
        //    deathCount++;
        //    if(deathTimer < deathCount)
        //    {
        //        Destroy(this.gameObject);
        //    }
        //}
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        //タグで当たり判定を管理する
        if (col.gameObject.tag == "Player")
        {
            Debug.Log("落ちます");
            //seagull_state = Seagull_State.verticalMovement;
        }
    }
}
