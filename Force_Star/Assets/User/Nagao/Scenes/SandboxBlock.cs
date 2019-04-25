using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SandboxBlock : MonoBehaviour
{
    public GameObject starDirec;
    private StarDirector starCreate;
    //プレイヤーの読み込み
    public GameObject attackHand;
    private PunchController punchController;

    //プレイヤーの攻撃判定用
    private bool checkAttack = false;

    // Start is called before the first frame update
    void Start()
    {
        punchController = attackHand.GetComponent<PunchController>();
        starCreate = starDirec.GetComponent<StarDirector>();
    }

    // Update is called once per frame
    void Update()
    {
        checkAttack = punchController.attackCheck;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "AttackBoal" && checkAttack == true)
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
        }
    }
}
