using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1Move : MonoBehaviour
{
    public GameObject starDirec;

    private StarDirector starCreate;

    //動くスピード
    [SerializeField]
    float moveSpeed;

    //往復移動の一定距離
    float distance;

    //一定距離の最大
    [SerializeField]
    private float maxDistance = 2.0f;

    // Start is called before the first frame update
    void Start()
    {
        starCreate = starDirec.GetComponent<StarDirector>();
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "AttackBoal")
        {
            starCreate.CreateStar(20);
        }
    }
        // Update is called once per frame
        void Update()
    {
        //最大値まで行ったら反転
        if(distance > maxDistance)
        {
            moveSpeed = moveSpeed * -1;
           distance = 0.0f;
        }
        //最低値まで行ったら反転
        if(distance < -maxDistance)
        {
            moveSpeed = moveSpeed * -1;
            distance = 0.0f;
        }

        //敵を移動させる
        transform.Translate(moveSpeed, 0, 0);
        distance += moveSpeed;
    }
}
