using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneMove : MonoBehaviour
{
    public GameObject starDirec;

    private StarDirector starCreate;


    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 攻撃当たり判定データ
    //*|***|***|***|***|***|***|***|***|***|***|***|
    private Vector2 m_partsAttackPos;
    private Vector2 m_partsAttackSize;
    private EnemyAttackPartsBox m_partsAttack;
    private GameObject m_partsAttackObject;

    Rigidbody2D rigid2D;


    //*|***|***|***|***|***|***|***|***|***|***|***|
    // これが出来たときに
    //*|***|***|***|***|***|***|***|***|***|***|***|
    void Awake()
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 攻撃当たり判定データ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        GameObject parent = this.gameObject;
        m_partsAttackObject = new GameObject("attackParts");
        m_partsAttackObject.transform.parent = parent.transform;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 攻撃当たり判定のスクリプト
        //*|***|***|***|***|***|***|***|***|***|***|***|
        this.m_partsAttack = m_partsAttackObject.AddComponent<EnemyAttackPartsBox>();

        
    }

    // Start is called before the first frame update
    void Start()
    {
        this.rigid2D = gameObject.GetComponent<Rigidbody2D>();

        starCreate = starDirec.GetComponent<StarDirector>();


        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 攻撃当たり判定の発生
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_partsAttackObject.gameObject.transform.localPosition = Vector3.zero;
        m_partsAttackObject.gameObject.transform.localRotation = Quaternion.identity;
        m_partsAttackObject.gameObject.transform.localScale = Vector3.one;

        m_partsAttackPos = new Vector2(0, 0);
        m_partsAttackSize = new Vector2(1.5f, 1.5f);
        m_partsAttack.SetPlayHit();
        m_partsAttack.SetPointSize(m_partsAttackPos, m_partsAttackSize);
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 攻撃威力
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_partsAttack.SetAttackData(10.0f, false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D col)
    {

        if ((col.gameObject.tag == "AttackBoal"))
        {

            float posX1;
            float posX2;
            float posY;
            posX1 = this.transform.position.x + this.GetComponent<Renderer>().bounds.size.x / 2 + 3;
            posX2 = this.transform.position.x - this.GetComponent<Renderer>().bounds.size.x / 2 - 3;
            posY = this.transform.position.y - this.GetComponent<Renderer>().bounds.size.y / 2;

            // 
            starCreate.CreateStar(new Vector2(posX1, posY), new Vector2(posX2, posY), 10);

            //跡形もなく消えてゆけ
            Destroy(this.gameObject);
        }
    }
}
