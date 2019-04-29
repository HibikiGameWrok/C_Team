using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalController : MonoBehaviour
{
    public GameObject starDirec;

    private StarDirector starCreate;

    SpriteRenderer render;

    Rigidbody2D rigid2D;

    //星を出す回数
    private int crystalCount = 0;

    //星を出す最大数
    [SerializeField]
    private int maxCrystalCount = 5;

    //光らせるフラグ
    private bool flashFlag = false;

    //透明度
    private float flashCrystale = 1.0f;

    //点滅時間
    private float flashCount = 0.0f;

    //最大点滅時間
    [SerializeField]
    private float flashTimer = 600.0f;

    // Start is called before the first frame update
    void Start()
    {
        this.rigid2D = GetComponent<Rigidbody2D>();

        starCreate = starDirec.GetComponent<StarDirector>();

        render = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(flashFlag == true)
        {
            //点滅
            flashCrystale = Mathf.Sin(1.0f);

            //点滅させる
            flashCount += 1.0f;
        }
        if(flashCount > flashTimer)
        {
            flashFlag = false;

            flashCrystale = 1.0f;
        }
        render.color = new Color(1.0f, 1.0f, 1.0f, flashCrystale);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "AttackBoal")
        {
            flashCount = 0.0f;

            flashFlag = true;
            if (crystalCount < maxCrystalCount)
            {
                float posX1;
                float posY;
                posX1 = this.transform.position.x;
                posY = this.transform.position.y;

                starCreate.CreateOneStar(new Vector2(posX1, posY), 5, false, 0.5f);
                crystalCount += 1;
            }
         }
    }
}
