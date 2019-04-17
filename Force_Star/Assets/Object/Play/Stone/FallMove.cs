using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallMove : MonoBehaviour
{
    public GameObject starDirec;
    public GameObject attackHand;

    private StarDirector starCreate;
    private PunchController punchController;
    private bool checkAttack;


    //落下速度
    [SerializeField]
    float dropSpeed = 0.0f;

    //Targetとの距離 
    [SerializeField]
    float targetDistance = 0.0f;

    //横揺れ速さ
    float speed = 0.0f;

    //モデルの初期値
    private Vector3 startPos;

    //Targetとの距離の判定フラグ
    bool targetFlag = false;

    //落下位置
    private float dropPosition = 0.0f;


    public float timeOut = 0.0f;
    private float timeElapsed = 0.0f;



    // Start is called before the first frame update
    void Start()
    {
        starCreate = starDirec.GetComponent<StarDirector>();

        punchController = attackHand.GetComponent<PunchController>();
        //落石座標を取得
        startPos = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //Targetの座標を取り出し
        Vector3 tmp = GameObject.FindGameObjectWithTag("Player").transform.position;
        //TargetのX座標を代入
        Vector3 m_targetPos = tmp;

        //距離の判定
        if (targetDistance > Mathf.Abs(m_targetPos.x - startPos.x))
        {
            targetFlag = true;
        }

        //モデルの落下の判定
        if(targetFlag)
        {
            //モデルを落下させる関数
            ModelFall();
        }
        else
        {
            //距離の判定
            if (targetDistance + 2.0f > Mathf.Abs(m_targetPos.x - startPos.x))
            {
                //モデルを振動させる関数
                ModelVibration();
            }
            else
            {
                //一定時間で揺らす処理
                timeElapsed += Time.deltaTime;

                if (timeElapsed >= timeOut)
                {
                    //モデルを振動させる関数
                    ModelVibration();
                }

                if (timeElapsed >= timeOut * 2.0f)
                {
                    //時間を0にする
                    timeElapsed = 0.0f;
                }
            }



        }

    }


    void ModelFall()
    {
        //落下用の座標
        float posY = 0.0f;

        //落下処理
        dropPosition += Time.deltaTime * dropSpeed;

        //モデルの現在地から落下
        posY = this.transform.position.y - dropPosition;

        
        this.transform.position = new Vector3(this.transform.position.x, posY, this.transform.position.z);
    }


    void ModelVibration()
    {
        //揺れ用の座標
        float posX = 0.0f;

        //横揺れ処理
        speed++;
        posX = this.transform.position.x + (Mathf.Sin(speed))/4.0f;

        this.transform.position = new Vector3(posX, this.transform.position.y, this.transform.position.z);
    }


  
    void OnCollisionEnter2D(Collision2D col)
    {
        //いずれかに当たったら星を出して消える
        if (col.gameObject.tag == "Floor" || col.gameObject.tag == "Enemy" || col.gameObject.tag == "Player")
        {
            float posX1;
            float posX2;
            float posY;
            posX1 = this.transform.position.x + this.GetComponent<Renderer>().bounds.size.x / 2 + 3;
            posX2 = this.transform.position.x - this.GetComponent<Renderer>().bounds.size.x / 2 - 3;
            posY = this.transform.position.y - this.GetComponent<Renderer>().bounds.size.y / 2;

            starCreate.CreateStar(new Vector2(posX1, posY), new Vector2(posX2, posY), 10);

            Destroy(this.gameObject);
        }
    }

}
