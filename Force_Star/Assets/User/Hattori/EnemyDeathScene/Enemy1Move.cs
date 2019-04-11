using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1Move : MonoBehaviour
{
    public GameObject starDirec;
    public GameObject attackHand;

    private StarDirector starCreate;
    private PunchController punchController;
    private bool checkAttack;

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

        punchController = attackHand.GetComponent<PunchController>();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if ((col.gameObject.tag == "AttackBoal")&& (checkAttack == true))
        {
            float posX1;
            float posX2;
            float posY;
            posX1 = this.transform.position.x + this.GetComponent<Renderer>().bounds.size.x / 2 + 3;
            posX2 = this.transform.position.x - this.GetComponent<Renderer>().bounds.size.x / 2 - 3;
            posY = this.transform.position.y - this.GetComponent<Renderer>().bounds.size.y / 2;

            // 
            starCreate.CreateStar(new Vector2(posX1, posY), new Vector2(posX2, posY), 10);

            Destroy(this.gameObject);
            //starCreate.CreateStar(20);
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

        checkAttack = punchController.attackCheck;
    }
}
